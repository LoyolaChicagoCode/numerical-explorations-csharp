using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

// This example shows how to take advantage of C# range with lambda expressions
// to do our ongoin Monte Carlo exploration. While you see the word "var" here in various
// places, it is "val" and functional style.

// This version uses a C# struct (value class) for getting a range of random pairs.

struct RandomPoint {
    private static Random r = new Random();
    public double x, y;

    public static RandomPoint Create () {
        var p = new RandomPoint();
        p.x = r.NextDouble();
        p.y = r.NextDouble();
        return p;
    }
}

class Program
{
    public static IEnumerable<RandomPoint> RandomPairs (int numPairs) 
    {
        for (var i=0; i < numPairs; i++) {
            yield return RandomPoint.Create();
        }
    }

    static double sqr(double x) {
        return x * x;
    }

    static double pi(double incircle, double n) {
        return 4.0 * incircle / n;
    }

    static void Main(string[] args)
    {
        var timer = new System.Diagnostics.Stopwatch();
        timer.Start();
        var n = int.Parse(args[0]);
        var pairs = RandomPairs(n);
        var c = pairs.Count(p => sqr(p.x) + sqr(p.y) <= 1.0);
        timer.Stop();
        Console.WriteLine("Points = {0}, In Circle = {1}, Time = {2}, Pi = {3}", n, c, timer.ElapsedMilliseconds, pi(c, n));

    }
}
