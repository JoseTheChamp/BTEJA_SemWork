using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BTEJA_SemWork
{
    public class Lexer
    {
        private String vstup;
        private int index = 0;
        private int tokenIndex = 0;
        private bool konec = false;
        private List<Token> tokens = new List<Token>();
        private bool isReadingString = false;

        public Token PeekToken()
        {
            if (tokenIndex < tokens.Count)
            {
                return tokens[tokenIndex];
            }
            return null;
        }

        public Token ReadToken()
        {
            tokenIndex++;
            return tokens[tokenIndex - 1];
        }

        private char Next()
        {
            return vstup[index];
        }
        private char Pop()
        {
            index++;
            return vstup[index - 1];
        }
        private bool hasNext()
        {
            if (index <= vstup.Length - 1) return true;
            return false;
        }
        private List<char> breakPoints = new List<char>(new char[] {'!', ',', ';', '=', ':', '<', '>', '+', '-', '*', '/', '(', ')', '%','{','}','&','|','"', '\n', '\r' });
        private List<string> keyWords = new List<string>(new string[] { "double", "int", "var", "val", "string", "if", "else", "while", "return" , "fun"});
        public List<Token> Lexicate(String vstup)
        {
            this.vstup = vstup;

            while (!konec)
            {
                char v = Next();
                if (breakPoints.Contains(v))
                {
                    ReadPoint();
                }
                else if (v == ' ')
                {
                    Pop();
                }
                else
                {
                    ReadText();
                }
                if (!hasNext())
                {
                    konec = true;
                }
            }
            return tokens;
        }
        private void ReadPoint()
        {
            char v = Pop();
            char v2 = ' ';
            if (v == '<' || v == '>' || v == '&' || v == '|' || v == '=' || v == '!')
            {
                if (hasNext())
                {
                    v2 = Next();
                        //case ':': tokens.Add(new Token(Token.TokenType.Colon)); break;
                        switch (v2)
                        {
                            case '&': Pop(); tokens.Add(new Token(Token.TokenType.And)); break;
                            case '|': Pop(); tokens.Add(new Token(Token.TokenType.Or)); break;
                            case '=':
                            if (v == '>')
                            {
                                Pop();
                                tokens.Add(new Token(Token.TokenType.GreaterEqual));
                            } else if (v == '<') {
                                Pop();
                                tokens.Add(new Token(Token.TokenType.SmallerEqual));
                            } else if (v == '=') {
                                Pop();
                                tokens.Add(new Token(Token.TokenType.DoubleEqual));
                            } else if (v == '!') {
                                Pop();
                                tokens.Add(new Token(Token.TokenType.NotEqual));
                            }
                            else {
                                throw new Exception("Inproper Symbol [ & , | ....]");
                            }
                                break;
                            default:
                            switch (v)
                            {
                                case '<': Pop(); tokens.Add(new Token(Token.TokenType.Smaller)); break;
                                case '>': Pop(); tokens.Add(new Token(Token.TokenType.Greater)); break;
                                case '=': Pop(); tokens.Add(new Token(Token.TokenType.Equal)); break;
                                case '!': Pop(); tokens.Add(new Token(Token.TokenType.Exclamation)); break;
                                default: throw new Exception("Inproper Symbol [ & , | ....]"); break;
                            }
                            break;
                        }
                }
                else
                {
                    konec = true;
                }
            }
            else
            {
                switch (v)
                {
                    case ',': tokens.Add(new Token(Token.TokenType.Comma)); break;
                    case ';': tokens.Add(new Token(Token.TokenType.SemiColon)); break;
                    case '+': tokens.Add(new Token(Token.TokenType.Plus)); break;
                    case '-': tokens.Add(new Token(Token.TokenType.Minus)); break;
                    case '*': tokens.Add(new Token(Token.TokenType.Multiplication)); break;
                    case '/': tokens.Add(new Token(Token.TokenType.Division)); break;
                    case '(': tokens.Add(new Token(Token.TokenType.LeftParenthesis)); break;
                    case ')': tokens.Add(new Token(Token.TokenType.RightParenthesis)); break;
                    case '{': tokens.Add(new Token(Token.TokenType.LeftBracket)); break;
                    case '}': tokens.Add(new Token(Token.TokenType.RightBracket)); break;
                    case ':': tokens.Add(new Token(Token.TokenType.Colon)); break;
                    case '"':
                        isReadingString = !isReadingString;
                        tokens.Add(new Token(Token.TokenType.Quotation));
                        break;
                    case '\n': break;
                    case '\r': break;
                }
            }
        }

        private void ReadText()
        {
            if (isReadingString)
            {
                string s = "";
                while (Next() != '"')
                {
                    s = s + Pop().ToString();
                    if (!hasNext())
                    {
                        break;
                    }
                }
                tokens.Add(new Token(Token.TokenType.StringLit, s));
            }
            else
            {
                char v = ' ';
                string s = "";
                string sLower = "";
                while (!breakPoints.Contains(Next()) && Next() != ' ')
                {
                    s = s + Pop().ToString();
                    if (!hasNext())
                    {
                        break;
                    }
                }
                s = s.Trim();
                sLower = s.ToLower();
                if (keyWords.Contains(sLower))
                {
                    switch (sLower)
                    {
                        case "fun": tokens.Add(new Token(Token.TokenType.Fun)); break;
                        case "var": tokens.Add(new Token(Token.TokenType.Var)); break;
                        case "val": tokens.Add(new Token(Token.TokenType.Val)); break;
                        case "if": tokens.Add(new Token(Token.TokenType.If)); break;
                        case "else": tokens.Add(new Token(Token.TokenType.Else)); break;
                        case "while": tokens.Add(new Token(Token.TokenType.While)); break;
                        case "return": tokens.Add(new Token(Token.TokenType.Return)); break;
                        case "int": tokens.Add(new Token(Token.TokenType.Int)); break;
                        case "string": tokens.Add(new Token(Token.TokenType.String)); break;
                        case "double": tokens.Add(new Token(Token.TokenType.Double)); break;
                    }
                }
                else
                {
                    double numd = 0;
                    int num = 0;
                    try
                    {
                        double d = Double.Parse(s, CultureInfo.InvariantCulture);
                        if (s.Contains('.'))
                        {
                            tokens.Add(new Token(Token.TokenType.Double, s));
                        }
                        else
                        {
                            tokens.Add(new Token(Token.TokenType.Int, s));
                        }
                    }
                    catch (Exception)
                    {
                        if (Int32.TryParse(s, out num))
                        {
                            tokens.Add(new Token(Token.TokenType.Int, s));
                        }
                        else if (s != "\n")
                        {
                            //Figureout if ident or string
                            string reg = @"^[a-zA-Z_][a-zA-Z0-9_]*$";
                            if (Regex.IsMatch(s, reg))
                            {
                                tokens.Add(new Token(Token.TokenType.Ident, s));
                            }
                            else
                            {
                                throw new Exception("Given text does not correspond to identificator: ^[a-zA-Z_][a-zA-Z0-9_]*$ ");
                            }
                        }
                    }
                    /*
                    if (Double.TryParse(s, out numd))
                    {
                        if (numd % 1 == 0)
                        {
                            tokens.Add(new Token(Token.TokenType.Int, s));
                        }
                        else {
                            tokens.Add(new Token(Token.TokenType.Double, s));
                        }
                    } else if (Int32.TryParse(s, out num)) {
                        tokens.Add(new Token(Token.TokenType.Int, s));
                    }
                    else if (s != "\n")
                    {
                        //Figureout if ident or string
                        string reg = @"^[a-zA-Z_][a-zA-Z0-9_]+$";
                        if (Regex.IsMatch(s, reg))
                        {
                            tokens.Add(new Token(Token.TokenType.Ident, s));
                        }
                        else {
                            tokens.Add(new Token(Token.TokenType.StringLit, s));
                        }
                    }
                    */
                }  
            }
            if (!hasNext())
            {
                konec = true;
            }
        }
    }
}
