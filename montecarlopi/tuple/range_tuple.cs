using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

// Please note that this version was just to test the effectiveness of working with tuples. It is a tad slower
// than working with structs.
// We will not be putting to much more work into this version.

class Program
{
	private static Random random = new Random(int.MaxValue-1);

	public static IEnumerable<Tuple<double, double>> RandomPairs (long numPairs)
	{
		for (var i=0L; i < numPairs; i++) {
			yield return Tuple.Create(random.NextDouble(), random.NextDouble());
		}
	}

	static double sqr(double x) {
		return x * x;
	}

	static double pi(long n) {
		var pairs = RandomPairs(n);
		var c = pairs.LongCount(p => sqr(p.Item1) + sqr(p.Item2) <= 1.0);
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
			Console.WriteLine("usage range_tuple.exe number-of-darts");
		} else {
			TimePi(long.Parse(args[0]));
		}
	}
}
