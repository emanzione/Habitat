using System.Collections;
using System;

public class Randomizer
{
	//private static Random random = new Random();
	public static double Range(double minimum, double maximum)
	{
		Random random = new Random(Guid.NewGuid().GetHashCode());
		return random.NextDouble() * (maximum - minimum) + minimum;
	}

	public static int Next(int max)
	{
		Random random = new Random(Guid.NewGuid().GetHashCode());
		return random.Next(max);
	}
}
