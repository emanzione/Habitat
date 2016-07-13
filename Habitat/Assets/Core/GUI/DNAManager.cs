using System;
using UnityEngine;
using UnityEngine.UI;

public class DNAManager : MonoBehaviour
{
	public Text dna;

	public void SetPoint(int dna)
	{
		this.dna.text = dna + "/" + Settings.DNA_POINTS_TO_WIN + " DNA";
	}
}