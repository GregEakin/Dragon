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

        private readonly Lexer _lex;

        private Token _look;

        private Env _top;

        private int _used;

        #endregion

        #region Constructors and Destructors

        public Parser(Lexer l)
        {
            _lex = l;
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
            while (_look.Tag == '+' || _look.Tag == '-')
            {
                var tok = _look;
                Move();
                x = new Arith(tok, x, MultExpr());
            }
            return x;
        }

        private Stmt AssignStmt()
        {
            Stmt stmt;
            var t = _look;
            Match(Tag.ID);
            var id = _top.Get(t);
            if (id == null)
                throw new Error("near line " + Lexer.Line + ": " + t + " undeclared");
            if (_look.Tag == '=')
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
            var savedEnv = _top;
            _top = new Env(_top);
            Decls();
            var s = Stmts();
            Match('}');
            _top = savedEnv;
            return s;
        }

        private Expr BoolExpr()
        {
            var x = JoinExpr();
            while (_look.Tag == Tag.OR)
            {
                var tok = _look;
                Move();
                x = new Or(tok, x, JoinExpr());
            }
            return x;
        }

        private void Decls()
        {
            while (_look.Tag == Tag.BASIC)
            {
                var p = Type();
                var tok = _look;
                Match(Tag.ID);
                Match(';');
                var id = new Id((Word)tok, p, _used);
                _top.Put(tok, id);
                _used = _used + p.Width;
            }
        }

        private VarType Dims(VarType p)
        {
            Match('[');
            var tok = _look;
            Match(Tag.NUM);
            Match(']');
            if (_look.Tag == '[')
                p = Dims(p);
            return new Array(((Num)tok).Value, p);
        }

        private Expr EqualityExpr()
        {
            var x = RelExpr();
            while (_look.Tag == Tag.EQ || _look.Tag == Tag.NE)
            {
                var tok = _look;
                Move();
                x = new Rel(tok, x, RelExpr());
            }
            return x;
        }

        private Expr ExponentExpr()
        {
            var x = UnaryExpr();
            while (_look.Tag == '^')
            {
                var tok = _look;
                Move();
                x = new Arith(tok, x, UnaryExpr());
            }
            return x;
        }

        private Expr FactorExpr()
        {
            Expr x;
            switch (_look.Tag)
            {
                case '(':
                    Move();
                    x = BoolExpr();
                    Match(')');
                    return x;
                case Tag.NUM:
                    x = new Constant(_look, VarType.INT);
                    Move();
                    return x;
                case Tag.REAL:
                    x = new Constant(_look, VarType.FLOAT);
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
                    var id = _top.Get(_look);
                    if (id == null)
                        throw new Error("near line " + Lexer.Line + ": " + _look + " undeclared");
                    Move();
                    return _look.Tag != '[' ? (Expr)id : Offset(id);
                default:
                    throw new Error("near line " + Lexer.Line + ": syntax error");
            }
        }

        private Expr JoinExpr()
        {
            var x = EqualityExpr();
            while (_look.Tag == Tag.AND)
            {
                var tok = _look;
                Move();
                x = new And(tok, x, EqualityExpr());
            }
            return x;
        }

        private void Match(int t)
        {
            if (_look.Tag == t)
                Move();
            else
                throw new Error("near line " + Lexer.Line + ": syntax error look.tag " + _look.Tag + " != " + t);
        }

        private void Move()
        {
            _look = _lex.Scan();
        }

        private Expr MultExpr()
        {
            var x = ExponentExpr();
            while (_look.Tag == '*' || _look.Tag == '/')
            {
                var tok = _look;
                Move();
                x = new Arith(tok, x, UnaryExpr());
            }
            return x;
        }

        private Access Offset(Id a)
        {
            var type = a.Type;
            Match('[');
            var i = BoolExpr();
            Match(']');
            type = ((Array)type).Of;
            var w = new Constant(type.Width);
            var t1 = new Arith(new Token('*'), i, w);
            var loc = t1;
            while (_look.Tag == '[')
            {
                Match('[');
                i = BoolExpr();
                Match(']');
                type = ((Array)type).Of;
                w = new Constant(type.Width);
                t1 = new Arith(new Token('*'), i, w);
                loc = new Arith(new Token('+'), loc, t1);
            }

            return new Access(a, loc, type);
        }

        private Expr RelExpr()
        {
            var x = AddExpr();
            switch (_look.Tag)
            {
                case '<':
                case Tag.LE:
                case Tag.GE:
                case '>':
                    var tok = _look;
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
            switch (_look.Tag)
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
                    if (_look.Tag != Tag.ELSE)
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
            return _look.Tag == '}' ? Inter.Stmt.Null : new Seq(Stmt(), Stmts());
        }

        private VarType Type()
        {
            var p = (VarType)_look;
            Match(Tag.BASIC);
            return _look.Tag != '[' ? p : Dims(p);
        }

        private Expr UnaryExpr()
        {
            if (_look.Tag == '+')
            {
                Move();
                return UnaryExpr();
            }
            if (_look.Tag == '-')
            {
                Move();
                return new Unary(Word.MINUS, UnaryExpr());
            }
            if (_look.Tag == Tag.NOT)
            {
                var tok = _look;
                Move();
                return new Not(tok, UnaryExpr());
            }

            return FactorExpr();
        }

        #endregion
    }
}