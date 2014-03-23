using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

// This example shows how to take advantage of C# range with lambda expressions
// to do our ongoin Monte Carlo exploration. While you see the word "var" here in various
// places, it is "val" and functional style.

// This version uses the C# native Tuple, which appears to be slower than using
// a C# struct (value class).

class Program
{
    private static Random random = new Random();

    public static IEnumerable<Tuple<double, double>> RandomPairs (int numPairs)
    {
        for (var i=0; i < numPairs; i++) {
            yield return Tuple.Create(random.NextDouble(), random.NextDouble());
        }
    }

    static double sqr(double x) {
        return x * x;
    }

    static double pi(int n) {
        var pairs = RandomPairs(n);
        var c = pairs.Count(p => sqr(p.Item1) + sqr(p.Item2) <= 1.0);
        return 4.0 * c / n;
    }

    static void TimePi(int n) {
        var timer = new System.Diagnostics.Stopwatch();
        timer.Start();
        var myPi = pi(n);
        timer.Stop();
        Console.WriteLine("Points = {0}, Time = {1}, Pi = {2}", n, timer.ElapsedMilliseconds, myPi);
    }

    static void Main(string[] args)
    {
        if (args.Length < 1) {
            Console.WriteLine("usage range_tuple.exe number-of-darts");
        } else {
            TimePi(int.Parse(args[0]));
        }
    }
}
