using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

// This example shows how to take advantage of C# range with lambda expressions
// to do our ongoin Monte Carlo exploration. While you see the word "var" here in various
// places, it is "val" and functional style.

// This version uses a C# struct (value class) for getting a range of random pairs.

struct RandomPoint {
	private static readonly Random r = new Random(int.MaxValue-1);

	public readonly double x, y;

	public RandomPoint(double x, double y) {
		this.x = x;
		this.y = y;
	}

	public static RandomPoint Create () {
		return new RandomPoint(r.NextDouble(), r.NextDouble());
	}
}

class MonteCarloPi
{
	public static IEnumerable<RandomPoint> RandomPairs (long numPairs)
	{
		for (var i=0L; i < numPairs; i++) {
			yield return RandomPoint.Create();
		}
	}

	delegate double d2d(double d);

	static double pi(long n) {
		var pairs = RandomPairs(n);
		d2d sqr = x => x * x;
		var c = pairs.LongCount(p => sqr(p.x) + sqr(p.y) <= 1.0);
		return 4.0 * c / n;
	}

	static void TimePi(long n) {
		var timer = new System.Diagnostics.Stopwatch();
		timer.Start();
		var myPi = pi(n);
		timer.Stop();
		Console.WriteLine("Points = {0}, Time = {1}, Pi = {2}", n, timer.ElapsedMilliseconds, myPi);
	}

	static void Main(string[] args)
	{
		if (args.Length < 1) {
			Console.WriteLine("usage range_struct.exe number-of-darts");
		} else {
			TimePi(long.Parse(args[0]));
		}
	}
}
