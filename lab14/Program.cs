using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static Dictionary<int,int> PPrimeGraphAlgor(Dictionary<int, Dictionary<int, int>> graph, int start_pos)
    {

        var mstSet = new Dictionary<int,int> { { start_pos, 0 } };

        while (mstSet.Count < graph.Count)
        {
            //отримати ребра від набору MST до набору без MST 
            var edges = mstSet.AsParallel().SelectMany(node => graph[node.Key]).Where(edge => !mstSet.ContainsKey(edge.Key));
            // отримати мінімальне ребро 
            var minEdge = edges.AsParallel().OrderBy(edge => edge.Value).First();
            // додати мінімальне ребро до MST 
            mstSet.Add(minEdge.Key, minEdge.Value);
        }
        return mstSet;
    }

    public static void Main()
    {
        var graph = new Dictionary<int, Dictionary<int, int>>()
        {
            {1, new Dictionary<int, int>(){{2, 12}, {3, 11}, {4, 14}}},
            {2, new Dictionary<int, int>(){{1, 12}, {3, 11}, {6, 8}, {7, 16}}},
            {3, new Dictionary<int, int>(){{1, 11}, {2, 12}, {5, 6}, {6, 3}}},
            {4, new Dictionary<int, int>(){{1, 14}, {5, 4}}},
            {5, new Dictionary<int, int>(){{3, 6}, {4, 4}}},
            {6, new Dictionary<int, int>(){{2, 8}, {3, 3}, {8, 6}}},
            {7, new Dictionary<int, int>(){{2, 16}, {8, 8}}},
            {8, new Dictionary<int, int>(){{6, 6}, {7, 8}}}
        };

        Stopwatch watch = new();
        watch.Start();
        var rezult = PPrimeGraphAlgor(graph, 6);
        watch.Stop();
        Console.WriteLine("Execution time of the Parallel `Prime` alghorithm: {0} \n", watch.Elapsed.TotalMilliseconds);

        Console.Write("start=>");
        foreach (var item in rezult)
        {
            Console.Write(item.ToString() + "=>");
        }
        Console.Write("finish");
    }
}