using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour {
    public AudioClip[] stings;
    public AudioSource stingSource;

	public AudioClip[] bruits;
	public AudioSource bruitsSource;

	public AudioClip[] voixRouge;
	public AudioClip[] voixBleu;
	public AudioClip[] voixJaune;
	public AudioClip[] voixGenerique;
	public AudioClip[] voixRage;
	public AudioClip tumfatigues;
	public AudioSource voixSource;

    private static int indiceMusiqueACharger = -1;
	private static int indiceSonAJouer = -1;
	private static int couleur = -1;
	private static bool rage = false;
	public static bool isRaging = false;
	private static bool hasRaged = false;
	private static float tempsRage;
	private static bool shutup = false;
	private static bool provoc = false;

	private static int nombreBruits;

    // Use this for initialization
    void Start () {
		nombreBruits = bruits.Length;
    }
	
	// Update is called once per frame
	void Update () {
        if (indiceMusiqueACharger > -1)
        {
            stingSource.Stop();
            stingSource.clip = stings[indiceMusiqueACharger];
            stingSource.Play();
        }
		if (indiceSonAJouer > -1) {
			bruitsSource.Stop ();
			bruitsSource.clip = bruits [indiceSonAJouer];
			bruitsSource.Play ();
		}
		if (couleur > -1 && !voixSource.isPlaying && !isRaging) {
			voixSource.Stop ();
			if (couleur == 0)
				voixSource.clip = voixRouge [Random.Range (0, voixRouge.Length)];
			if (couleur == 1)
				voixSource.clip = voixBleu [Random.Range (0, voixBleu.Length)];
			if (couleur == 2)
				voixSource.clip = voixJaune [Random.Range (0, voixJaune.Length)];
			else
				voixSource.clip = voixGenerique [Random.Range (0, voixGenerique.Length)];
			voixSource.Play ();
		}
		if (provoc) {
			voixSource.Stop ();
			voixSource.clip = voixGenerique [Random.Range (0, voixGenerique.Length)];
			voixSource.Play ();
			provoc = false;
		}
		if (rage) {
			voixSource.Stop ();
			voixSource.clip = tumfatigues;
			voixSource.Play ();
			rage = false;
			isRaging = true;
			hasRaged = false;
			tempsRage = Time.time;
		}
		if (isRaging && Time.time - tempsRage >= 22f) {
			isRaging = false;
			voixSource.Stop ();
		}
		if (Time.time - tempsRage >= 2.5f && !hasRaged && isRaging) {
			voixSource.Stop ();
			voixSource.clip = voixRage[Random.Range(0, voixRage.Length)];
			voixSource.Play ();
			hasRaged = true;
		}
		if (shutup) {
			shutup = false;
			isRaging = false;
			voixSource.Stop ();
		}
		couleur = -1;
		indiceSonAJouer = -1;
		indiceMusiqueACharger = -1;
	}

    public static void ChargerMusique(int x)
    {
        indiceMusiqueACharger = x;
    }

	public static void JouerSon() {
		indiceSonAJouer = Random.Range(0, nombreBruits);
	}

	public static void JouerVoix(int x) {
		couleur = x;
	}

	public static void JouerRage() {
		rage = true;
	}

	public static void ShutUp() {
		shutup = true;
	}

	public static void Provoquer() {
		provoc = true;
	}
}
