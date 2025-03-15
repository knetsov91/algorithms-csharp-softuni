namespace exam;

public class Task2
{
    private static Dictionary<string, List<string>> graph;
    private static HashSet<string> visited;
    public static void Main1(string[] args)
    {
        var nodes = int.Parse(Console.ReadLine());

        graph = new Dictionary<string, List<string>>();
        visited = new HashSet<string>();
        for (int i = 0; i < nodes; i++)
        {
            var nodeAndChild = Console.ReadLine().Split(" - ");
        
            var first = nodeAndChild[0].Trim();
            var second = nodeAndChild[1].Trim();
            if (!graph.ContainsKey(first))
            {
                graph.Add(first, new List<string>());
            }
            if (!graph.ContainsKey(second))
            {
                graph.Add(second, new List<string>());
            }
             
            graph[first].Add(second);
            graph[second].Add(first);
            
        }
        
        var query = Console.ReadLine().Split(" -> ");
        var start = query[0];
        var end = query[1];

        Console.WriteLine(FindTrades(start, end));
    }

    private static int FindTrades(string start, string end)
    {
       
        var queue = new Queue<string>();
        var trades = new Dictionary<string, string> { { start, null} };
        visited.Add(start);
        queue.Enqueue(start);
  
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current == end)
            {
                return TradeStepsCount(trades, end) ;
            }
            foreach (var child in graph[current])
            {
                if (visited.Contains(child))
                {
                    continue;
                }
                
                queue.Enqueue(child);
                visited.Add(child);
                trades[child] = current;
            }
        }
        
        return -1;
     }

    private static int TradeStepsCount(Dictionary<string, string> trades, string target)
    {

        int steps = 0;
        var node = target;

        while (trades[node] != null)
        {
            steps++;
            node = trades[node];
        }

        return steps;
    }
}