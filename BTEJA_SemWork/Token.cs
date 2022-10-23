using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork
{
    public class Token
    {
        public TokenType Type { get; set; }
        public String? Value { get; set; }
        public enum TokenType
        {
            Double,
            String,
            Int,
            SemiColon,
            Colon,
            Exclamation,
            Comma,
            Equal,
            DoubleEqual,
            SmallerEqual,
            GreaterEqual,
            Smaller,
            Greater,
            Or,
            And,
            NotEqual,
            Plus,
            Minus,
            Multiplication,
            Division,
            LeftParenthesis,
            RightParenthesis,
            LeftBracket,
            RightBracket,
            If,
            Else,
            While,
            Var,
            Val,
            Return,
            Fun,
            DoubleLit,
            IntLit,
            StringLit,
            Ident,
            Quotation
        }

        public Token(TokenType type)
        {
            Type = type;
        }

        public Token(TokenType Type, string value) : this(Type)
        {
            Value = value;
        }
    }
}
