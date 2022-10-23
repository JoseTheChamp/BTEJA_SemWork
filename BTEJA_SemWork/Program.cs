



using BTEJA_SemWork;

Lexer lexer = new Lexer();
string text = System.IO.File.ReadAllText(@"C:\Projects\C#\BTEJA_SemWork\BTEJA_SemWork\SourceCodeTest.txt");
List<Token> tokens = lexer.Lexicate(text);

foreach (Token token in tokens)
{
    if (token.Value != null)
    {
        Console.WriteLine("Token:  " + token.Type.ToString() + "  " + token.Value.ToString());
    }
    else
    {
        Console.WriteLine("Token:  " + token.Type.ToString());
    }
}