using UnityEngine;
using System.Collections;

public class Utility
{
	public static float GetHighestPointOnMesh(Chunk chunk)
	{
		Mesh mesh = chunk.GetComponent<MeshFilter>().mesh;

		float height = 0f;
		for (int i = 0; i < mesh.vertices.Length; i++) {
			if (i == 0)
				height = mesh.vertices [i].y;
			else {
				float y = mesh.vertices [i].y;
				if (y > height)
					height = y;
			}
		}

		return height;
	}
}
