using BTEJA_SemWork.ParserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork
{
    public class Parser
    {
        private List<Token> tokens;
        private int index;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        private Token Peek() {
            if (index >= tokens.Count)
            {
                return null;
            }
            return tokens[index];
        }
        private Token Peek(int i) {
            if (index + i >= tokens.Count)
            {
                return null;
            }
            return tokens[index + i];
        }
        private Token Pop() {
            if (index >= tokens.Count)
            {
                return null;
            }
            index++;
            return tokens[index - 1];
        }
        public ProgramAST Parse() {
            ProgramAST programAST = new ProgramAST();
            while (Peek() != null) {
                programAST.Statements.Add(ReadStatement());
            }
            return programAST;
        }

        private Statement ReadStatement() {
            Statement statement;
            switch (Peek().Type)
            {
                case Token.TokenType.While: return ReadWhileStatement();
                case Token.TokenType.Return:
                    statement = ReadReturnStatement();
                    if (Peek().Type != Token.TokenType.SemiColon) throw new Exception("Expected ; after return statement [ReadStatement] Token: " + index);
                    Pop();
                    return statement;
                case Token.TokenType.If: return ReadIfStatement();
                case Token.TokenType.Fun: return ReadFunctionStatement();
                case Token.TokenType.Var: 
                    statement = ReadDefinitionStatement();
                    if (Peek().Type != Token.TokenType.SemiColon) throw new Exception("Expected ; after call statement [ReadStatement] Token: " + index);
                    Pop();
                    return statement;
                case Token.TokenType.Val:
                    statement = ReadDefinitionStatement();
                    if (Peek().Type != Token.TokenType.SemiColon) throw new Exception("Expected ; after call statement [ReadStatement] Token: " + index);
                    Pop();
                    return statement;
                default:
                    if (Peek().Type == Token.TokenType.Ident)
                    {
                        if (Peek(1).Type == Token.TokenType.LeftParenthesis)
                        {
                            statement = ReadCallStatement();
                            if (Peek().Type != Token.TokenType.SemiColon) throw new Exception("Expected ; after call statement [ReadStatement] Token: " + index);
                            Pop();
                            return statement;
                        } else if (Peek(1).Type == Token.TokenType.Equal) {
                            statement = ReadCallStatement();
                            if (Peek().Type != Token.TokenType.SemiColon) throw new Exception("Expected ; after call statement [ReadStatement] Token: " + index);
                            Pop();
                            return statement;
                        }
                        else {
                            throw new Exception("Incorrect start of a statement. [ReadStatement] Token: " + index);
                        }
                    }
                    else {
                        throw new Exception("Expected start of a statement. [ReadStatement] Token: " + index);
                    }
            }
        }

        private Statement ReadAssignStatement()
        {
            if (Peek().Type != Token.TokenType.Ident) throw new Exception("Expected IDENT [ReadAssignStatement] Token: " + index);
            //Console.WriteLine("AT ident: " + lexer.PeekToken().Value);
            var ident = Pop().Value;
            if (Peek().Type != Token.TokenType.Equal) throw new Exception("Expected = [ReadAssignStatement] Token: " + index);
            Pop();
            Expression expr = ReadExpression();
            //Console.WriteLine(lexer.PeekToken().Type);
            return new AssignStatement(ident, expr);
        }

        private Expression ReadExpression()
        {
            throw new NotImplementedException();
        }

        private Statement ReadCallStatement()
        {
            if (Peek().Type != Token.TokenType.Ident) throw new Exception("Expected IDENT [ReadCallStatement] Token: " + index);
            //Console.WriteLine("AT ident: " + lexer.PeekToken().Value);
            var ident = Pop().Value;
            if (Peek().Type != Token.TokenType.LeftParenthesis) throw new Exception("Expected ( [ReadCallStatement] Token: " + index);
            Pop();
            CallStatement callStatement = new CallStatement(ident);
            if (Peek().Type == Token.TokenType.Ident)
            {
                callStatement.Params.Add(Pop().Value);
                while (Peek().Type == Token.TokenType.Comma)
                {
                    Pop();
                    if (Peek().Type != Token.TokenType.Ident) throw new Exception("Expected IDENT after , [ReadCallStatement] Token: " + index);
                    callStatement.Params.Add(Pop().Value);
                }
            }
            if (Peek().Type != Token.TokenType.RightParenthesis) throw new Exception("Expected ) [ReadCallStatement] Token: " + index);
            Pop();
            return callStatement;
        }

        private Statement ReadFunctionStatement()
        {
            if (Peek().Type != Token.TokenType.Fun) throw new Exception("Expected fun [ReadFunctionStatement] Token: " + index);
            Pop();
            if (Peek().Type != Token.TokenType.Ident) throw new Exception("Expected IDENT [ReadFunctionStatement] Token: " + index);
            var ident = Pop().Value;
            FunctionStatement functionStatement = new FunctionStatement(ident);
            if (Peek().Type != Token.TokenType.LeftParenthesis) throw new Exception("Expected ( [ReadFunctionStatement] Token: " + index);
            Pop();
            if (Peek().Type == Token.TokenType.Ident)
            {
                var paramIdent = Pop().Value;
                if (Peek().Type != Token.TokenType.Colon) throw new Exception("Expected : after ident [ReadFunctionStatement] Token: " + index);
                Pop();
                DataType dataType = ReadDataType();
                functionStatement.Parameters.Add(new Parameter(paramIdent, dataType));
                while (Peek().Type != Token.TokenType.Comma)
                {
                    Pop();
                    if (Peek().Type != Token.TokenType.Ident) throw new Exception("Expected IDENT after , [ReadFunctionStatement] Token: " + index);
                    paramIdent = Pop().Value;
                    if (Peek().Type != Token.TokenType.Colon) throw new Exception("Expected : after ident [ReadFunctionStatement] Token: " + index);
                    Pop();
                    dataType = ReadDataType();
                    functionStatement.Parameters.Add(new Parameter(paramIdent, dataType));
                }
            }
            if (Peek().Type != Token.TokenType.RightParenthesis) throw new Exception("Expected ) [ReadFunctionStatement] Token: " + index);
            Pop();
            if (Peek().Type == Token.TokenType.Colon)
            {
                Pop();
                functionStatement.ReturnType = ReadDataType();
            }
            if (Peek().Type != Token.TokenType.LeftBracket) throw new Exception("Expected { [ReadFunctionStatement] Token: " + index);
            Pop();
            while (Peek().Type != Token.TokenType.RightBracket)
            {
                functionStatement.Statements.Add(ReadStatement());
            }
            if (Peek().Type != Token.TokenType.RightBracket) throw new Exception("Expected } [ReadFunctionStatement] Token: " + index);
            Pop();
            return functionStatement;
        }

        private DataType ReadDataType()
        {
            switch (Pop().Type)
            {
                case Token.TokenType.Double: return DataType.Double;
                case Token.TokenType.String: return DataType.String;
                case Token.TokenType.Int: return DataType.Int;
                default:
                    throw new Exception("Expected a DataType [ReadDataType] Token: " + index);
            }
        }
        private Statement ReadIfStatement()
        {
            if (Peek().Type != Token.TokenType.While) throw new Exception("Expected if [ReadIfStatement] Token: " + index);
            Pop();
            if (Peek().Type != Token.TokenType.LeftParenthesis) throw new Exception("Expected ( after if [ReadIfStatement] Token: " + index);
            Pop();
            IfStatement ifStatement = new IfStatement(ReadCondition());
            if (Peek().Type != Token.TokenType.RightParenthesis) throw new Exception("Expected ) after condition [ReadIfStatement] Token: " + index);
            Pop();
            if (Peek().Type != Token.TokenType.LeftBracket)
            {
                Pop();
                while (Peek().Type != Token.TokenType.RightBracket)
                {
                    ifStatement.Statements.Add(ReadStatement());
                }
                if (Peek().Type != Token.TokenType.RightBracket) throw new Exception("Expected } [ReadIfStatement] Token: " + index);
                Pop();
            } else if (Peek().Type != Token.TokenType.Return) {
                ifStatement.Statements.Add(ReadStatement());
            }else if (Peek().Type != Token.TokenType.Ident) {
                if (Peek(1).Type != Token.TokenType.LeftParenthesis || Peek(1).Type != Token.TokenType.Equal)
                {
                    ifStatement.Statements.Add(ReadStatement());
                }
                else {
                    throw new Exception("Expected Assign,return or call statement [ReadIfStatement] Token: " + index);
                }
            }
            else {
                throw new Exception("Expected Assign,return or call statement [ReadIfStatement] Token: " + index);
            }
            return ifStatement;
        }

        private Statement ReadWhileStatement()
        {
            if (Peek().Type != Token.TokenType.While) throw new Exception("Expected while [ReadWhileStatement] Token: " + index);
            Pop();
            if (Peek().Type != Token.TokenType.LeftParenthesis) throw new Exception("Expected ( after while [ReadWhileStatement] Token: " + index);
            Pop();
            WhileStatement whileStatement = new WhileStatement(ReadCondition());
            if (Peek().Type != Token.TokenType.RightParenthesis) throw new Exception("Expected ) after condition [ReadWhileStatement] Token: " + index);
            Pop();
            if (Peek().Type != Token.TokenType.LeftBracket) throw new Exception("Expected { [ReadWhileStatement] Token: " + index);
            Pop();
            while (Peek().Type != Token.TokenType.RightBracket)
            {
                whileStatement.Statements.Add(ReadStatement());
            }
            if (Peek().Type != Token.TokenType.RightBracket) throw new Exception("Expected } [ReadWhileStatement] Token: " + index);
            Pop();
            return whileStatement;
        }

        private Condition ReadCondition()
        {
            throw new NotImplementedException();
        }

        private Statement ReadReturnStatement()
        {
            if (Peek().Type != Token.TokenType.Return) throw new Exception("Expected Return [ReadReturnStatement] Token: " + index);
            Pop();
            Expression expression = ReadExpression();
            return new ReturnStatement(expression);
        }
        private Statement ReadDefinitionStatement()
        {
            if (Peek().Type != Token.TokenType.Val || Peek().Type != Token.TokenType.Var) throw new Exception("Expected Var or Val [ReadDefinitionStatement] Token: " + index);
            DefinitionStatement definitionStatement = new DefinitionStatement();
            if (Pop().Type == Token.TokenType.Var)
            {
                definitionStatement.IsVal = false;
            }
            else {
                definitionStatement.IsVal = true;
            }
            if (Peek().Type != Token.TokenType.Val || Peek().Type != Token.TokenType.Var) throw new Exception("Expected ident after var/val [ReadDefinitionStatement] Token: " + index);
            definitionStatement.Ident = Pop().Value;
            if (Peek().Type == Token.TokenType.Colon)
            {
                Pop();
                switch (Pop().Type)
                {
                    case Token.TokenType.Double: definitionStatement.DataType = DataType.Double; break;
                    case Token.TokenType.String: definitionStatement.DataType = DataType.String; break;
                    case Token.TokenType.Int: definitionStatement.DataType = DataType.Int; break;
                    default:
                        throw new Exception("Expected String/Double/Int after : [ReadDefinitionStatement] Token: " + index);
                }
            } 
            if (Peek().Type == Token.TokenType.Equal) {
                Pop();
                switch (Peek().Type)
                {
                    case Token.TokenType.DoubleLit:
                        if (Peek().Type != Token.TokenType.Return) throw new Exception("Expected Return [ReadReturnStatement] Token: " + index);
                        definitionStatement.DataType = DataType.Double;
                        definitionStatement.Value = Pop().Value;
                        break;
                    case Token.TokenType.IntLit:
                        definitionStatement.DataType = DataType.Int;
                        definitionStatement.Value = Pop().Value;
                        break;
                    case Token.TokenType.StringLit:
                        definitionStatement.DataType = DataType.String;
                        definitionStatement.Value = Pop().Value;
                        break;
                    default:
                        throw new Exception("Expected stringlit/doublelit/intlit [ReadDefinitionStatement] Token: " + index);
                }
            }
            if (definitionStatement.DataType == null) {
                throw new Exception("Either specify datatype of inicialize variable [ReadDefinitionStatement] Token: " + index);
            }
            return definitionStatement;
        }
    }
}
