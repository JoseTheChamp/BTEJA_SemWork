



using BTEJA_SemWork;

Lexer lexer = new Lexer();
string text = System.IO.File.ReadAllText(@"C:\Projects\C#\BTEJA_SemWork\BTEJA_SemWork\SourceCodeTest.txt");
List<Token> tokens = lexer.Lexicate(text);

for (int i = 0; i < tokens.Count;i++)
{
    if (tokens[i].Value != null)
    {
        Console.WriteLine("Token[" + i + "]:  " + tokens[i].Type.ToString() + "  " + tokens[i].Value.ToString());
    }
    else
    {
        Console.WriteLine("Token[" + i + "]:  " + tokens[i].Type.ToString());
    }
}

Parser parser = new Parser(tokens);
parser.Parse();
Console.WriteLine("PARSED");