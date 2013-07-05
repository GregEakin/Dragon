// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Parser.cs" company="Greg Eakin">
//   This is text
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoleX;
using Inter;
using Lexical;
using Symbols;

namespace Parser
{
    public class Parser
    {
        #region Fields

        private readonly Lexer lex;

        private Token look;

        private Env top;

        private int used;

        #endregion

        #region Constructors and Destructors

        public Parser(Lexer l)
        {
            lex = l;
            Move();
        }

        #endregion

        #region Public Methods and Operators

        public void Program()
        {
            var s = Block();
            var begin = s.NewLabel();
            var after = s.NewLabel();
            s.EmitLabel(begin);
            s.Gen(begin, after);
            s.EmitLabel(after);
        }

        #endregion

        #region Methods

        private Expr AddExpr()
        {
            var x = MultExpr();
            while (look.Tag == '+' || look.Tag == '-')
            {
                var tok = look;
                Move();
                x = new Arith(tok, x, MultExpr());
            }
            return x;
        }

        private Stmt AssignStmt()
        {
            Stmt stmt;
            var t = look;
            Match(Tag.ID);
            var id = top.Get(t);
            if (id == null)
                throw new Error("near line " + Lexer.Line + ": " + t + " undeclared");
            if (look.Tag == '=')
            {
                Move();
                stmt = new Set(id, BoolExpr());
            }
            else
            {
                var x = Offset(id);
                Match('=');
                stmt = new SetElem(x, BoolExpr());
            }
            Match(';');
            return stmt;
        }

        private Stmt Block()
        {
            Match('{');
            var savedEnv = top;
            top = new Env(top);
            Decls();
            var s = Stmts();
            Match('}');
            top = savedEnv;
            return s;
        }

        private Expr BoolExpr()
        {
            var x = JoinExpr();
            while (look.Tag == Tag.OR)
            {
                var tok = look;
                Move();
                x = new Or(tok, x, JoinExpr());
            }
            return x;
        }

        private void Decls()
        {
            while (look.Tag == Tag.BASIC)
            {
                var p = Type();
                var tok = look;
                Match(Tag.ID);
                Match(';');
                var id = new Id((Word)tok, p, used);
                top.Put(tok, id);
                used = used + p.width;
            }
        }

        private VarType Dims(VarType p)
        {
            Match('[');
            var tok = look;
            Match(Tag.NUM);
            Match(']');
            if (look.Tag == '[')
                p = Dims(p);
            return new Array(((Num)tok).Value, p);
        }

        private Expr EqualityExpr()
        {
            var x = RelExpr();
            while (look.Tag == Tag.EQ || look.Tag == Tag.NE)
            {
                var tok = look;
                Move();
                x = new Rel(tok, x, RelExpr());
            }
            return x;
        }

        private Expr ExponentExpr()
        {
            var x = UnaryExpr();
            while (look.Tag == '^')
            {
                var tok = look;
                Move();
                x = new Arith(tok, x, UnaryExpr());
            }
            return x;
        }

        private Expr FactorExpr()
        {
            Expr x;
            switch (look.Tag)
            {
                case '(':
                    Move();
                    x = BoolExpr();
                    Match(')');
                    return x;
                case Tag.NUM:
                    x = new Constant(look, VarType.INT);
                    Move();
                    return x;
                case Tag.REAL:
                    x = new Constant(look, VarType.FLOAT);
                    Move();
                    return x;
                case Tag.TRUE:
                    x = Constant.TRUE;
                    Move();
                    return x;
                case Tag.FALSE:
                    x = Constant.FALSE;
                    Move();
                    return x;
                case Tag.ID:
                    var id = top.Get(look);
                    if (id == null)
                        throw new Error("near line " + Lexer.Line + ": " + look + " undeclared");
                    Move();
                    return look.Tag != '[' ? (Expr)id : Offset(id);
                default:
                    throw new Error("near line " + Lexer.Line + ": syntax error");
            }
        }

        private Expr JoinExpr()
        {
            var x = EqualityExpr();
            while (look.Tag == Tag.AND)
            {
                var tok = look;
                Move();
                x = new And(tok, x, EqualityExpr());
            }
            return x;
        }

        private void Match(int t)
        {
            if (look.Tag == t)
                Move();
            else
                throw new Error("near line " + Lexer.Line + ": syntax error look.tag " + look.Tag + " != " + t);
        }

        private void Move()
        {
            look = lex.Scan();
        }

        private Expr MultExpr()
        {
            var x = ExponentExpr();
            while (look.Tag == '*' || look.Tag == '/')
            {
                var tok = look;
                Move();
                x = new Arith(tok, x, UnaryExpr());
            }
            return x;
        }

        private Access Offset(Id a)
        {
            var type = a.type;
            Match('[');
            var i = BoolExpr();
            Match(']');
            type = ((Array)type).Of;
            var w = new Constant(type.width);
            var t1 = new Arith(new Token('*'), i, w);
            var loc = t1;
            while (look.Tag == '[')
            {
                Match('[');
                i = BoolExpr();
                Match(']');
                type = ((Array)type).Of;
                w = new Constant(type.width);
                t1 = new Arith(new Token('*'), i, w);
                loc = new Arith(new Token('+'), loc, t1);
            }
            return new Access(a, loc, type);
        }

        private Expr RelExpr()
        {
            var x = AddExpr();
            switch (look.Tag)
            {
                case '<':
                case Tag.LE:
                case Tag.GE:
                case '>':
                    var tok = look;
                    Move();
                    return new Rel(tok, x, AddExpr());
                default:
                    return x;
            }
        }

        private Stmt Stmt()
        {
            Expr x;
            Stmt s1;
            Stmt savedStmt;
            switch (look.Tag)
            {
                case ';':
                    Move();
                    return Inter.Stmt.Null;

                case Tag.IF:
                    Match(Tag.IF);
                    Match('(');
                    x = BoolExpr();
                    Match(')');
                    s1 = Stmt();
                    if (look.Tag != Tag.ELSE)
                        return new If(x, s1);
                    Match(Tag.ELSE);
                    var s2 = Stmt();
                    return new Else(x, s1, s2);

                case Tag.WHILE:
                    var whilenode = new While();
                    savedStmt = Inter.Stmt.Enclosing;
                    Inter.Stmt.Enclosing = whilenode;
                    Match(Tag.WHILE);
                    Match('(');
                    x = BoolExpr();
                    Match(')');
                    s1 = Stmt();
                    whilenode.Init(x, s1);
                    Inter.Stmt.Enclosing = savedStmt;
                    return whilenode;

                case Tag.DO:
                    var donode = new Do();
                    savedStmt = Inter.Stmt.Enclosing;
                    Inter.Stmt.Enclosing = donode;
                    Match(Tag.DO);
                    s1 = Stmt();
                    Match(Tag.WHILE);
                    Match('(');
                    x = BoolExpr();
                    Match(')');
                    Match(';');
                    donode.Init(x, s1);
                    Inter.Stmt.Enclosing = savedStmt;
                    return donode;

                case Tag.BREAK:
                    Match(Tag.BREAK);
                    Match(';');
                    return new Break();

                case '{':
                    return Block();

                default:
                    return AssignStmt();
            }
        }

        private Stmt Stmts()
        {
            return look.Tag == '}' ? Inter.Stmt.Null : new Seq(Stmt(), Stmts());
        }

        private VarType Type()
        {
            var p = (VarType)look;
            Match(Tag.BASIC);
            return look.Tag != '[' ? p : Dims(p);
        }

        private Expr UnaryExpr()
        {
            if (look.Tag == '+')
            {
                Move();
                return UnaryExpr();
            }
            if (look.Tag == '-')
            {
                Move();
                return new Unary(Word.MINUS, UnaryExpr());
            }
            if (look.Tag == Tag.NOT)
            {
                var tok = look;
                Move();
                return new Not(tok, UnaryExpr());
            }
            return FactorExpr();
        }

        #endregion
    }
}