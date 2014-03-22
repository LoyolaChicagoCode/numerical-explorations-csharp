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

    static double pi(int n) {
        var pairs = RandomPairs(n);
        var c = pairs.Count(p => sqr(p.x) + sqr(p.y) <= 1.0);
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
            Console.WriteLine("usage listofstruct.exe number-of-iterations");
        } else {
            TimePi(int.Parse(args[0]));
        }
    }
}
