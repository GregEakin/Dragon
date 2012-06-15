// -----------------------------------------------------------------------
// <copyright file="Parser.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Parser
{
    using ConsoleX;
    using Inter;
    using Lexical;
    using Symbols;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Parser
    {
        private readonly Lexer lex;
        private Token look;
        private Env top;
        private int used;

        public Parser(Lexer l)
        {
            lex = l;
            move();
        }

        private void move()
        {
            look = lex.Scan();
        }

        private void error(string s)
        {
            throw new Error("near line " + Lexer.line + ": " + s);
        }

        private void match(int t)
        {
            if (look.tag == t)
                move();
            else
                error("syntax error");
        }

        public void program()
        {
            Stmt s = block();
            int begin = s.NewLabel();
            int after = s.NewLabel();
            s.EmitLabel(begin);
            s.Gen(begin, after);
            s.EmitLabel(after);
        }

        private Stmt block()
        {
            match('{');
            Env savedEnv = top;
            top = new Env(top);
            decls();
            Stmt s = stmts();
            match('}');
            top = savedEnv;
            return s;
        }

        private void decls()
        {
            while (look.tag == Tag.BASIC)
            {
                var x = this.lex;

                SType p = type();
                Token tok = look;
                match(Tag.ID);
                match(';');
                Id id = new Id((Word)tok, p, used);
                top.put(tok, id);
                used = used + p.width;
            }
        }

        private SType type()
        {
            SType p = (SType)look;
            match(Tag.BASIC);
            if (look.tag != '[')
                return p;
            else
                return dims(p);
        }

        private SType dims(SType p)
        {
            match('[');
            Token tok = look;
            match(Tag.NUM);
            match(']');
            if (look.tag == '[')
                p = dims(p);
            return new Array(((Num)tok).value, p);
        }

        private Stmt stmts()
        {
            if (look.tag == '}')
                return Stmt.Null;
            else
                return new Seq(stmt(), stmts());
        }

        private Stmt stmt()
        {
            Expr x;
            Stmt s1, s2;
            Stmt savedStmt;
            switch (look.tag)
            {
                case ';':
                    move();
                    return Stmt.Null;

                case Tag.IF:
                    match(Tag.IF);
                    match('(');
                    x = boolx();
                    match(')');
                    s1 = stmt();
                    if (look.tag != Tag.ELSE)
                        return new If(x, s1);
                    match(Tag.ELSE);
                    s2 = stmt();
                    return new Else(x, s1, s2);

                case Tag.WHILE:
                    While whilenode = new While();
                    savedStmt = Stmt.Enclosing;
                    Stmt.Enclosing = whilenode;
                    match(Tag.WHILE);
                    match('(');
                    x = boolx();
                    match(')');
                    s1 = stmt();
                    whilenode.init(x, s1);
                    return whilenode;

                case Tag.DO:
                    Do donode = new Do();
                    savedStmt = Stmt.Enclosing;
                    Stmt.Enclosing = donode;
                    match(Tag.DO);
                    s1 = stmt();
                    match(Tag.WHILE);
                    match('(');
                    x = boolx();
                    match(')');
                    match(';');
                    donode.init(x, s1);
                    Stmt.Enclosing = savedStmt;
                    return donode;

                case Tag.BREAK:
                    match(Tag.BREAK);
                    match(';');
                    return new Break();

                case '{':
                    return block();

                default:
                    return assign();
            }
        }

        private Stmt assign()
        {
            Stmt stmt;
            Token t = look;
            match(Tag.ID);
            Id id = top.get(t);
            if (id == null)
                error(t.ToString() + " undeclared");
            if (look.tag == '=')
            {
                move();
                stmt = new Set(id, boolx());
            }
            else
            {
                Access x = offset(id);
                match('=');
                stmt = new SetElem(x, boolx());
            }
            match(';');
            return stmt;
        }

        private Expr boolx()
        {
            Expr x = join();
            while (look.tag == Tag.OR)
            {
                Token tok = look;
                move();
                x = new Or(tok, x, join());
            }
            return x;
        }

        private Expr join()
        {
            Expr x = equality();
            while (look.tag == Tag.AND)
            {
                Token tok = look;
                move();
                x = new And(tok, x, equality());
            }
            return x;
        }

        private Expr equality()
        {
            Expr x = rel();
            while (look.tag == Tag.EQ || look.tag == Tag.NE)
            {
                Token tok = look;
                move();
                x = new Rel(tok, x, rel());
            }
            return x;
        }

        private Expr rel()
        {
            Expr x = expr();
            switch (look.tag)
            {
                case '<':
                case Tag.LE:
                case Tag.GE:
                case '>':
                    Token tok = look;
                    move();
                    return new Rel(tok, x, expr());
                default:
                    return x;
            }
        }

        private Expr expr()
        {
            Expr x = term();
            while (look.tag == '+' || look.tag == '-')
            {
                Token tok = look;
                move();
                x = new Arith(tok, x, term());
            }
            return x;
        }

        private Expr term()
        {
            Expr x = unary();
            while (look.tag == '*' || look.tag == '/')
            {
                Token tok = look;
                move();
                x = new Arith(tok, x, unary());
            }
            return x;
        }

        private Expr unary()
        {
            if (look.tag == '-')
            {
                move();
                return new Unary(Word.minus, unary());
            }
            else if (look.tag == '!')
            {
                Token tok = look;
                move();
                return new Not(tok, unary());
            }
            else return factor();
        }

        private Expr factor()
        {
            Expr x;
            switch (look.tag)
            {
                case '(':
                    move();
                    x = boolx();
                    match(')');
                    return x;
                case Tag.NUM:
                    x = new Constant(look, SType.Int);
                    move();
                    return x;
                case Tag.REAL:
                    x = new Constant(look, SType.Float);
                    move();
                    return x;
                case Tag.TRUE:
                    x = Constant.True;
                    move();
                    return x;
                case Tag.FALSE:
                    x = Constant.False;
                    move();
                    return x;
                case Tag.ID:
                    string a = look.ToString();
                    Id id = top.get(look);
                    if (id == null)
                        error(look.ToString() + " undeclared");
                    move();
                    if (look.tag != '[')
                        return id;
                    else
                        return offset(id);
                default:
                    error("syntax error");
                    return null;
            }
        }

        private Access offset(Id a)
        {
            Expr loc = null;
            SType type = a.type;
            match('[');
            Expr i = boolx();
            match(']');
            type = ((Array)type).of;
            Expr w = new Constant(type.width);
            Expr t1 = new Arith(new Token('*'), i, w);
            loc = t1;
            while (look.tag == '[')
            {
                match('[');
                i = boolx();
                match(']');
                type = ((Array)type).of;
                w = new Constant(type.width);
                t1 = new Arith(new Token('*'), i, w);
                loc = new Arith(new Token('+'), loc, t1);
            }
            return new Access(a, loc, type);
        }
    }
}
