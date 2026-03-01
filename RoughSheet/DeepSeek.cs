namespace RoughSheet
{
    class DeepSeek
    {
        public void ClearBelowLine(int startLine)
        {
            int currentLine = Console.CursorTop;
            Console.SetCursorPosition(0, startLine);
            for (int i = startLine; i < currentLine; i++)
            {
                Console.WriteLine(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, startLine);
            
        }
    }
}
