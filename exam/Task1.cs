using System.Text;

namespace exam;

public class Task1
{
    private static HashSet<string> words;
    private static HashSet<string> foundedWords;

    private static char[,] grid;
    private static bool[,] used;
    private static Stack<char> tmpWord;
    
    public static void Main1(string[] args)
    {
        var rows = int.Parse(Console.ReadLine());
        var cols = int.Parse(Console.ReadLine());
        grid = new char[rows, cols];
        used = new bool[rows, cols];
        words = new HashSet<string>();
        foundedWords = new HashSet<string>();
      
        for (int row = 0; row < rows; row++)
        {
            var line = Console.ReadLine();
            for (int col = 0; col < cols; col++)
            {
               

                grid[row, col] = line[col];
            }
        }
        
        var wordList = Console.ReadLine().Split(" ");
        foreach (var word in wordList)
        {
            words.Add(word);
        }

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                tmpWord = new Stack<char>();

                Gen(row, col);
            }
        }

        Console.WriteLine(string.Join(Environment.NewLine, foundedWords));
    }

    public static bool CanGenerate(string word, HashSet<string> words)
    {
        return words.Any(w => w.StartsWith(word));
    }
    private static void Gen(int row, int col)
    {
        if (row < 0 || row >= grid.GetLength(0) || col < 0 || col >= grid.GetLength(1) || used[row, col])
        {
            return;
        }
        
        tmpWord.Push(grid[row, col]);
        string currentWord = string.Join("", tmpWord.Reverse());
        
        if (!CanGenerate(currentWord, words))
        {
            tmpWord.Pop( );
            return;
        }
        
        if (words.Contains(currentWord)  && !foundedWords.Contains(currentWord))
        {
            foundedWords.Add(currentWord);
           
        }
        
        used[row, col] = true;
        Gen(row - 1, col - 1);
        Gen(row + 1, col + 1);

        Gen(row + 1, col);
        Gen(row - 1, col);
        Gen(row - 1, col + 1);
        Gen(row + 1, col - 1);

        Gen(row, col - 1);
        Gen(row, col + 1);
        used[row, col] = false;
        tmpWord.Pop();
    }
}