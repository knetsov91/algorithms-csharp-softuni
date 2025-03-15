namespace exam;

public class Task3
{
    
    public static void Main(string[] args)
    {
       var n = int.Parse(Console.ReadLine());
        var blockedCells = new HashSet<string>();
        var fertileCount = 0;
        var matrix = new int[n, n];
        int maxRow = 0;
        int maxCol = 0;
        
        for (int i = 0; i < n; i++)
        {
            var rowElements = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            for (int c = 0; c < rowElements.Length; c++)
            {
                matrix[i, c] = rowElements[c];
            }
        }
        var blockedCellsLst = Console.ReadLine().Split(" ");
        foreach (var cell in blockedCellsLst)
        {
            blockedCells.Add(cell);
        }
        var dp = new int[n, n];
        
         dp[0, 0] = matrix[0, 0];

         for (int c = 1; c < n; c++)
         {
             if (blockedCells.Contains($"0,{c}"))
             {
                 dp[0, c] = 0;
             }
             else
             {
                 dp[0, c] = dp[0, c- 1] + matrix[0, c]; 
             }
             
         }

         for (int r = 1; r < n; r++)
         {
             if (blockedCells.Contains($"0,{r}"))
             {
                 dp[r, 0] = 0;
             }
             else
             {
                 dp[r, 0] = dp[r - 1, 0] + matrix[r, 0];
             }
              
         }

         for (int r = 1; r < n; r++)
         {
             for (int c = 1; c < n; c++)
             {
                if (!blockedCells.Contains($"{r},{c}"))
                 {
                     
                     
                    if (blockedCells.Contains($"{r - 1},{c}"))
                    {
                        dp[r,c] = dp[r, c- 1] + matrix[r, c];

                    } else if (blockedCells.Contains($"{r},{c - 1}"))
                    {
                        dp[r,c] = dp[r - 1, c] + matrix[r, c];
                    }
                    else
                    {
                        dp[r,c] = Math.Max(dp[r - 1, c], dp[r, c- 1]) + matrix[r, c];
                        }
                    
                     maxRow = r;
                     maxCol = c;

                 } 
             }
         }

        
         bool pathFromStart = true;
         var stack = new Stack<string>();
         var row =maxRow;
         var col = maxCol;
         while (row > 0 && col > 0)
         {
             if (!blockedCells.Contains($"{row},{col}"))
             {
                 stack.Push($"({row}, {col})");
                 fertileCount += matrix[row, col];
             }
             var top = dp[row - 1, col];
             var left = dp[row, col - 1];
             if (top > left  && !blockedCells.Contains($"{row -1},{col}"))
             {
                 row = row - 1;
             }
             else
             {
                 col = col - 1;
             }
           
           
         }

         while (row > 0)
         {
             if (blockedCells.Contains($"{row},{col}"))
             {
                 pathFromStart = false;
                 break;
             }
             fertileCount += matrix[row, col];

             stack.Push($"({row}, {col})");

             row -= 1;
         }

         while (col > 0)
         {
             if (blockedCells.Contains($"{row},{col}"))
             {
                 pathFromStart = false;
                 break;
             }
             fertileCount += matrix[row, col];

             stack.Push($"({row}, {col})");

             col -= 1;
         }

         if (pathFromStart)
         {
             stack.Push($"({row}, {col})");
             fertileCount += matrix[row, col];
         }

         Console.WriteLine($"Max total fertility: {dp[maxRow, maxCol]}");
         Console.WriteLine($"[{string.Join(" ", stack)}]");
    }
}