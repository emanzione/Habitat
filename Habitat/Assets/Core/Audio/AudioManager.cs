using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioClip pickup;
	public AudioClip poo;
	public AudioClip enemyEaten;
	public AudioClip playerDeath;

	private AudioSource source;

	void Start()
	{
		source = GetComponent<AudioSource> ();
	}

	public void Play(AudioClip clip)
	{
		source.clip = clip;
		source.Play ();
	}
}