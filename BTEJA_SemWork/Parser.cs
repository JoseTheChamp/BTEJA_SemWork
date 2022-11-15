﻿using BTEJA_SemWork.ParserClasses;

namespace BTEJA_SemWork
{
    public class Parser
    {
        private List<Token> tokens;
        private int index = 0;

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
                    if (Peek().Type != Token.TokenType.SemiColon) throw new Exception("Expected ; after return statement [ReadStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                    Pop();
                    return statement;
                case Token.TokenType.If: return ReadIfStatement();
                case Token.TokenType.Fun: return ReadFunctionStatement();
                case Token.TokenType.Var: 
                    statement = ReadDefinitionStatement();
                    if (Peek().Type != Token.TokenType.SemiColon) throw new Exception("Expected ; after call statement [ReadStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                    Pop();
                    return statement;
                case Token.TokenType.Val:
                    statement = ReadDefinitionStatement();
                    if (Peek().Type != Token.TokenType.SemiColon) throw new Exception("Expected ; after call statement [ReadStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                    Pop();
                    return statement;
                default:
                    if (Peek().Type == Token.TokenType.Ident)
                    {
                        if (Peek(1).Type == Token.TokenType.LeftParenthesis)
                        {
                            statement = ReadCallStatement();
                            if (Peek().Type != Token.TokenType.SemiColon) throw new Exception("Expected ; after call statement [ReadStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                            Pop();
                            return statement;
                        } else if (Peek(1).Type == Token.TokenType.Equal) {
       
                            statement = ReadAssignStatement();
                            if (Peek().Type != Token.TokenType.SemiColon) throw new Exception("Expected ; after call statement [ReadStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                            Pop();
                            return statement;
                        }
                        else {
                            throw new Exception("Incorrect start of a statement. [ReadStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                        }
                    }
                    else {
                        throw new Exception("Expected start of a statement. [ReadStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                    }
            }
        }

        private Statement ReadAssignStatement()
        {
            if (Peek().Type != Token.TokenType.Ident) throw new Exception("Expected IDENT [ReadAssignStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            //Console.WriteLine("AT ident: " + lexer.PeekToken().Value);
            var ident = Pop().Value;
            if (Peek().Type != Token.TokenType.Equal) throw new Exception("Expected = [ReadAssignStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            Expression expr = ReadExpression();
            //Console.WriteLine(lexer.PeekToken().Type);
            return new AssignStatement(ident, expr);
        }

        private Expression ReadExpression()
        {
            {
                Expression expression;
                Token.TokenType? op = null;
                if (Peek().Type == Token.TokenType.Plus || Peek().Type == Token.TokenType.Minus)
                {
                    op = Pop().Type;
                }
                if (op == Token.TokenType.Minus)
                {
                    MinusUnary minusUnary = new MinusUnary();
                    minusUnary.Expression= ReadTerm();
                    expression = minusUnary;
                }
                else if (op == Token.TokenType.Plus)
                {
                    PlusUnary plusUnary = new PlusUnary();
                    plusUnary.Expression = ReadTerm();
                    expression = plusUnary;
                }
                else
                {
                    expression = ReadTerm();
                }
                while (Peek() != null && (Peek().Type == Token.TokenType.Plus || Peek().Type == Token.TokenType.Minus))
                {
                    op = Pop().Type;
                    if (op == Token.TokenType.Minus)
                    {
                        Minus minus = new Minus();
                        minus.Left = expression;
                        minus.Right = ReadTerm();
                        expression = minus;
                    }
                    else
                    {
                        Plus plus = new Plus();
                        plus.Left = expression;
                        plus.Right = ReadTerm();
                        expression = plus;
                    }
                }
                return expression;
            }
        }

        private Expression ReadTerm()
        {
            Expression expression;
            Token.TokenType op;
            expression = ReadFactor();
            while (Peek() != null && (Peek().Type == Token.TokenType.Multiplication || Peek().Type == Token.TokenType.Division))
            {
                op = Pop().Type;
                if (op == Token.TokenType.Division)
                {
                    Divide divide = new Divide();
                    divide.Left = expression;
                    divide.Right = ReadFactor();
                    expression = divide;

                }
                else if (op == Token.TokenType.Multiplication)
                {
                    Multiply multiply = new Multiply();
                    multiply.Left = expression;
                    multiply.Right = ReadFactor();
                    expression = multiply;
                }
                else
                {
                    throw new Exception("Expected * or / [ReadTermStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                }
            }
            return expression;
        }

        private Expression ReadFactor()
        {
            Expression expression;
            if (Peek().Type == Token.TokenType.LeftParenthesis)
            {
                Pop();
                expression = ReadExpression();
                if (Peek().Type != Token.TokenType.RightParenthesis) throw new Exception("Expected ) [ReadFactorStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                Pop();
            }
            else
            {
                switch (Peek().Type)
                {
                    case Token.TokenType.DoubleLit: return ReadDoubleExpression();
                    case Token.TokenType.IntLit: return ReadIntExpression();
                    case Token.TokenType.Quotation: return ReadStringExpression();
                    case Token.TokenType.Ident:
                        if (Peek(1).Type == Token.TokenType.LeftParenthesis)
                        {
                            return ReadCallExpression();
                        }
                        return ReadIdentExpression();
                    default:
                        throw new Exception("Expected ident/StringLit/doubleLit/intLit [ReadFactorStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                }
            }
            return expression;
        }

        private Expression ReadDoubleExpression()
        {
            DoubleExpression doubleExpression = new DoubleExpression();
            doubleExpression.Value = Pop().Value;
            return doubleExpression;
        }

        private Expression ReadIntExpression()
        {
            IntExpression intExpression = new IntExpression();
            intExpression.Value = Pop().Value;
            return intExpression;
        }

        private Expression ReadStringExpression()
        {
            if (Peek().Type != Token.TokenType.Quotation) throw new Exception("Expected \" before string [ReadStringExpression] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            StringExpression stringExpression = new StringExpression();
            stringExpression.Value = Pop().Value;
            if (Peek().Type != Token.TokenType.Quotation) throw new Exception("Expected \" after string [ReadStringExpression] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            return stringExpression;
        }

        private Expression ReadIdentExpression()
        {
            IdentExpression identExpression = new IdentExpression();
            identExpression.Ident = Pop().Value;
            return identExpression;
        }

        private Statement ReadCallStatement()
        {
            if (Peek().Type != Token.TokenType.Ident) throw new Exception("Expected IDENT [ReadCallStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            //Console.WriteLine("AT ident: " + lexer.PeekToken().Value);
            var ident = Pop().Value;
            if (Peek().Type != Token.TokenType.LeftParenthesis) throw new Exception("Expected ( [ReadCallStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            CallStatement callStatement = new CallStatement(ident);
            if (Peek().Type != Token.TokenType.RightParenthesis)
            {
                callStatement.Params.Add(ReadExpression());
                while (Peek().Type == Token.TokenType.Comma)
                {
                    Pop();
                    callStatement.Params.Add(ReadExpression());
                }
            }
            if (Peek().Type != Token.TokenType.RightParenthesis) throw new Exception("Expected ) [ReadCallStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            return callStatement;
        }

        private Expression ReadCallExpression() {
            if (Peek().Type != Token.TokenType.Ident) throw new Exception("Expected IDENT  [ReadCallExpression] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            CallExpression callExpression = new CallExpression(Pop().Value);
            if (Peek().Type != Token.TokenType.LeftParenthesis) throw new Exception("Expected ( after ident [ReadCallExpression] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            if (Peek().Type != Token.TokenType.RightParenthesis)
            {
                callExpression.Parameters.Add(ReadExpression());
                while (Peek().Type == Token.TokenType.Comma)
                {
                    Pop();
                    callExpression.Parameters.Add(ReadExpression());
                }
            }
            if (Peek().Type != Token.TokenType.RightParenthesis) throw new Exception("Expected ) [ReadCallExpression] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            return callExpression;
        }

        private Statement ReadFunctionStatement()
        {
            if (Peek().Type != Token.TokenType.Fun) throw new Exception("Expected fun [ReadFunctionStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            if (Peek().Type != Token.TokenType.Ident) throw new Exception("Expected IDENT [ReadFunctionStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            var ident = Pop().Value;
            FunctionStatement functionStatement = new FunctionStatement(ident);
            if (Peek().Type != Token.TokenType.LeftParenthesis) throw new Exception("Expected ( [ReadFunctionStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            if (Peek().Type == Token.TokenType.Ident)
            {
                var paramIdent = Pop().Value;
                if (Peek().Type != Token.TokenType.Colon) throw new Exception("Expected : after ident [ReadFunctionStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                Pop();
                DataType dataType = ReadDataType();
                functionStatement.Parameters.Add(new Parameter(paramIdent, dataType));
                while (Peek().Type == Token.TokenType.Comma)
                {
                    Pop();
                    if (Peek().Type != Token.TokenType.Ident) throw new Exception("Expected IDENT after , [ReadFunctionStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                    paramIdent = Pop().Value;
                    if (Peek().Type != Token.TokenType.Colon) throw new Exception("Expected : after ident [ReadFunctionStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                    Pop();
                    dataType = ReadDataType();
                    functionStatement.Parameters.Add(new Parameter(paramIdent, dataType));
                }
            }
            if (Peek().Type != Token.TokenType.RightParenthesis) throw new Exception("Expected ) [ReadFunctionStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            if (Peek().Type == Token.TokenType.Colon)
            {
                Pop();
                functionStatement.ReturnType = ReadDataType();
            }
            if (Peek().Type != Token.TokenType.LeftBracket) throw new Exception("Expected { [ReadFunctionStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            while (Peek().Type != Token.TokenType.RightBracket)
            {
                functionStatement.Statements.Add(ReadStatement());
            }
            if (Peek().Type != Token.TokenType.RightBracket) throw new Exception("Expected } [ReadFunctionStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
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
                    throw new Exception("Expected a DataType [ReadDataType] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            }
        }
        private Statement ReadIfStatement()
        {
            if (Peek().Type != Token.TokenType.If) throw new Exception("Expected if [ReadIfStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            if (Peek().Type != Token.TokenType.LeftParenthesis) throw new Exception("Expected ( after if [ReadIfStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            IfStatement ifStatement = new IfStatement(ReadCondition());
            if (Peek().Type != Token.TokenType.RightParenthesis) throw new Exception("Expected ) after condition [ReadIfStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            if (Peek().Type == Token.TokenType.LeftBracket)
            {
                Pop();
                while (Peek().Type != Token.TokenType.RightBracket)
                {
                    ifStatement.Statements.Add(ReadStatement());
                }
                if (Peek().Type != Token.TokenType.RightBracket) throw new Exception("Expected } [ReadIfStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                Pop();
                if (Peek().Type == Token.TokenType.Else) {
                    Pop();
                    if (Peek().Type != Token.TokenType.LeftBracket) throw new Exception("Expected { [ReadIfStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                    Pop();
                    while (Peek().Type != Token.TokenType.RightBracket)
                    {
                        ifStatement.ElseStatements.Add(ReadStatement());
                    }
                    if (Peek().Type != Token.TokenType.RightBracket) throw new Exception("Expected } [ReadIfStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                    Pop();
                }
            } else if (Peek().Type == Token.TokenType.Return) {
                ifStatement.Statements.Add(ReadStatement());
            }else if (Peek().Type == Token.TokenType.Ident) {
                if (Peek(1).Type == Token.TokenType.LeftParenthesis || Peek(1).Type == Token.TokenType.Equal)
                {
                    ifStatement.Statements.Add(ReadStatement());
                }
                else {
                    throw new Exception("Expected Assign,return or call statement [ReadIfStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                }
            }
            else {
                throw new Exception("Expected Assign,return or call statement [ReadIfStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            }
            return ifStatement;
        }

        private Statement ReadWhileStatement()
        {
            if (Peek().Type != Token.TokenType.While) throw new Exception("Expected while [ReadWhileStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            if (Peek().Type != Token.TokenType.LeftParenthesis) throw new Exception("Expected ( after while [ReadWhileStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            WhileStatement whileStatement = new WhileStatement(ReadCondition());
            if (Peek().Type != Token.TokenType.RightParenthesis) throw new Exception("Expected ) after condition [ReadWhileStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            if (Peek().Type != Token.TokenType.LeftBracket) throw new Exception("Expected { [ReadWhileStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            while (Peek().Type != Token.TokenType.RightBracket)
            {
                whileStatement.Statements.Add(ReadStatement());
            }
            if (Peek().Type != Token.TokenType.RightBracket) throw new Exception("Expected } [ReadWhileStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            return whileStatement;
        }

        private Condition ReadCondition()
        {
            LogCondition logCondition = new LogCondition();
            logCondition.Conditions.Add(ReadBinaryCondition());
            while (Peek().Type == Token.TokenType.Or || Peek().Type == Token.TokenType.And)
            {
                if (Peek().Type == Token.TokenType.Or)
                {
                    Pop();
                    logCondition.Conditions.Add(new OrCondition(ReadBinaryCondition()));
                }
                else {
                    Pop();
                    logCondition.Conditions.Add(new AndCondition(ReadBinaryCondition()));
                }
            }
            return logCondition;
        }

        private Condition ReadBinaryCondition()
        {
            Expression expression = ReadExpression();
            switch (Peek().Type)
            {

                case Token.TokenType.DoubleEqual: Pop(); return new EqualCond(expression,ReadExpression());
                case Token.TokenType.SmallerEqual: Pop(); return new LesserEqualCond(expression,ReadExpression());
                case Token.TokenType.GreaterEqual: Pop(); return new GreaterEqualCond(expression, ReadExpression());
                case Token.TokenType.Smaller: Pop(); return new LesserCond(expression, ReadExpression());
                case Token.TokenType.Greater: Pop(); return new GreaterCond(expression, ReadExpression());
                case Token.TokenType.NotEqual: Pop(); return new NotEqualCond(expression, ReadExpression());
                default:
                    throw new Exception("Expected > < == != <= >= [ReadReturnStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            }
        }

        private Statement ReadReturnStatement()
        {
            if (Peek().Type != Token.TokenType.Return) throw new Exception("Expected Return [ReadReturnStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            Pop();
            Expression expression = ReadExpression();
            return new ReturnStatement(expression);
        }
        private Statement ReadDefinitionStatement()
        {
            if (Peek().Type != Token.TokenType.Val && Peek().Type != Token.TokenType.Var) throw new Exception("Expected Var or Val [ReadDefinitionStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            DefinitionStatement definitionStatement = new DefinitionStatement();
            if (Pop().Type == Token.TokenType.Var)
            {
                definitionStatement.IsVal = false;
            }
            else {
                definitionStatement.IsVal = true;
            }
            if (Peek().Type != Token.TokenType.Ident) throw new Exception("Expected ident after var/val [ReadDefinitionStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            DataType? holder = null;
            definitionStatement.Ident = Pop().Value;
            if (Peek().Type == Token.TokenType.Colon)
            {
                Pop();
                switch (Pop().Type)
                {
                    case Token.TokenType.Double: definitionStatement.DataType = DataType.Double; holder = DataType.Double; break;
                    case Token.TokenType.String: definitionStatement.DataType = DataType.String; holder = DataType.String; break;
                    case Token.TokenType.Int: definitionStatement.DataType = DataType.Int; holder = DataType.Int; break;
                    default:
                        throw new Exception("Expected String/Double/Int after : [ReadDefinitionStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                }
            } 
            if (Peek().Type == Token.TokenType.Equal) {
                Pop();
                switch (Peek().Type)
                {
                    case Token.TokenType.DoubleLit:
                        //if (Peek().Type != Token.TokenType.Return) throw new Exception("Expected Return [ReadDefinitionStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                        if (holder == DataType.Double || holder == null)
                        {
                            definitionStatement.DataType = DataType.Double;
                            definitionStatement.Value = Convert.ToDouble(Pop().Value);
                        }
                        else {
                            throw new Exception("Specified datatype and value do not match [ReadDefinitionStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                        }
                        break;
                    case Token.TokenType.IntLit:
                        if (holder == DataType.Int || holder == null)
                        {
                            definitionStatement.DataType = DataType.Int;
                            definitionStatement.Value = Convert.ToInt32(Pop().Value);
                        }
                        else {
                            throw new Exception("Specified datatype and value do not match [ReadDefinitionStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                        }                        
                        break;
                    case Token.TokenType.Quotation:
                        if (holder == DataType.String || holder == null)
                        {
                            definitionStatement.DataType = DataType.String;
                            definitionStatement.Value = Convert.ToString(((StringExpression)ReadStringExpression()).Value);
                        }
                        throw new Exception("Specified datatype and value do not match [ReadDefinitionStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                        break;
                    default:
                        throw new Exception("Expected stringlit/doublelit/intlit [ReadDefinitionStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
                }
            }
            if (definitionStatement.DataType == null) {
                throw new Exception("Either specify datatype of inicialize variable [ReadDefinitionStatement] Line: " + tokens[index].Line + " at token " + tokens[index].LineToken);
            }
            return definitionStatement;
        }
    }
}
