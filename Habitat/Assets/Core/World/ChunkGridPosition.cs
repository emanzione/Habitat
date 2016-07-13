using System;

public class ChunkGridPosition
{
	public int X;
	public int Y;

	public ChunkGridPosition (int x, int y)
	{
		X = x;
		Y = y;
	}

	public override string ToString ()
	{
		return GenerateKey(X, Y);
	}

	public static string GenerateKey(int x, int y)
	{
		return x.ToString() + "_" + y.ToString();
	}
}

