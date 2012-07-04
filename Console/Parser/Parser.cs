// -----------------------------------------------------------------------
// <copyright file="Parser.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using ConsoleX;
using Inter;
using Lexical;
using Symbols;

namespace Parser
{
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

        public void program()
        {
            Stmt s = block();
            int begin = s.NewLabel();
            int after = s.NewLabel();
            s.EmitLabel(begin);
            s.Gen(begin, after);
            s.EmitLabel(after);
        }

        private void move()
        {
            look = lex.Scan();
        }

        private void match(int t)
        {
            if (look.tag == t)
                move();
            else
                throw new Error("near line " + Lexer.Line + ": syntax error");
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

                VarType p = type();
                Token tok = look;
                match(Tag.ID);
                match(';');
                Id id = new Id((Word)tok, p, used);
                top.put(tok, id);
                used = used + p.width;
            }
        }

        private VarType type()
        {
            VarType p = (VarType)look;
            match(Tag.BASIC);
            if (look.tag != '[')
                return p;
            else
                return dims(p);
        }

        private VarType dims(VarType p)
        {
            match('[');
            Token tok = look;
            match(Tag.NUM);
            match(']');
            if (look.tag == '[')
                p = dims(p);
            return new Array(((Num)tok).Value, p);
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
                    x = boolExpr();
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
                    x = boolExpr();
                    match(')');
                    s1 = stmt();
                    whilenode.Init(x, s1);
                    Stmt.Enclosing = savedStmt;
                    return whilenode;

                case Tag.DO:
                    Do donode = new Do();
                    savedStmt = Stmt.Enclosing;
                    Stmt.Enclosing = donode;
                    match(Tag.DO);
                    s1 = stmt();
                    match(Tag.WHILE);
                    match('(');
                    x = boolExpr();
                    match(')');
                    match(';');
                    donode.Init(x, s1);
                    Stmt.Enclosing = savedStmt;
                    return donode;

                case Tag.BREAK:
                    match(Tag.BREAK);
                    match(';');
                    return new Break();

                case '{':
                    return block();

                default:
                    return assignStmt();
            }
        }

        private Stmt assignStmt()
        {
            Stmt stmt;
            Token t = look;
            match(Tag.ID);
            Id id = top.get(t);
            if (id == null)
                throw new Error("near line " + Lexer.Line + ": " + t + " undeclared");
            if (look.tag == '=')
            {
                move();
                stmt = new Set(id, boolExpr());
            }
            else
            {
                Access x = offset(id);
                match('=');
                stmt = new SetElem(x, boolExpr());
            }
            match(';');
            return stmt;
        }

        private Expr boolExpr()
        {
            Expr x = joinExpr();
            while (look.tag == Tag.OR)
            {
                Token tok = look;
                move();
                x = new Or(tok, x, joinExpr());
            }
            return x;
        }

        private Expr joinExpr()
        {
            Expr x = equalityExpr();
            while (look.tag == Tag.AND)
            {
                Token tok = look;
                move();
                x = new And(tok, x, equalityExpr());
            }
            return x;
        }

        private Expr equalityExpr()
        {
            Expr x = relExpr();
            while (look.tag == Tag.EQ || look.tag == Tag.NE)
            {
                Token tok = look;
                move();
                x = new Rel(tok, x, relExpr());
            }
            return x;
        }

        private Expr relExpr()
        {
            Expr x = addExpr();
            switch (look.tag)
            {
                case '<':
                case Tag.LE:
                case Tag.GE:
                case '>':
                    Token tok = look;
                    move();
                    return new Rel(tok, x, addExpr());
                default:
                    return x;
            }
        }

        private Expr addExpr()
        {
            Expr x = multExpr();
            while (look.tag == '+' || look.tag == '-')
            {
                Token tok = look;
                move();
                x = new Arith(tok, x, multExpr());
            }
            return x;
        }

        private Expr multExpr()
        {
            Expr x = exponentExpr();
            while (look.tag == '*' || look.tag == '/')
            {
                Token tok = look;
                move();
                x = new Arith(tok, x, unaryExpr());
            }
            return x;
        }

        private Expr exponentExpr()
        {
            Expr x = unaryExpr();
            while (look.tag == '^')
            {
                Token tok = look;
                move();
                x = new Arith(tok, x, unaryExpr());
            }
            return x;
        }

        private Expr unaryExpr()
        {
            if (look.tag == '+')
            {
                move();
                return unaryExpr();
            }
            if (look.tag == '-')
            {
                move();
                return new Unary(Word.MINUS, unaryExpr());
            }
            else if (look.tag == Tag.NOT)
            {
                Token tok = look;
                move();
                return new Not(tok, unaryExpr());
            }
            else return factorExpr();
        }

        private Expr factorExpr()
        {
            Expr x;
            switch (look.tag)
            {
                case '(':
                    move();
                    x = boolExpr();
                    match(')');
                    return x;
                case Tag.NUM:
                    x = new Constant(look, VarType.INT);
                    move();
                    return x;
                case Tag.REAL:
                    x = new Constant(look, VarType.FLOAT);
                    move();
                    return x;
                case Tag.TRUE:
                    x = Constant.TRUE;
                    move();
                    return x;
                case Tag.FALSE:
                    x = Constant.FALSE;
                    move();
                    return x;
                case Tag.ID:
                    string a = look.ToString();
                    Id id = top.get(look);
                    if (id == null)
                        throw new Error("near line " + Lexer.Line + ": " + look + " undeclared");
                    move();
                    if (look.tag != '[')
                        return id;
                    else
                        return offset(id);
                default:
                    throw new Error("near line " + Lexer.Line + ": syntax error");
            }
        }

        private Access offset(Id a)
        {
            VarType type = a.type;
            match('[');
            Expr i = boolExpr();
            match(']');
            type = ((Array)type).of;
            Expr w = new Constant(type.width);
            Expr t1 = new Arith(new Token('*'), i, w);
            Expr loc = t1;
            while (look.tag == '[')
            {
                match('[');
                i = boolExpr();
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
