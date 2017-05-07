using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour {
    public AudioClip[] stings;
    public AudioSource stingSource;

    private static int indiceMusiqueACharger = -1;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (indiceMusiqueACharger > -1)
        {
            stingSource.Stop();
            stingSource.clip = stings[indiceMusiqueACharger];
            stingSource.Play();
            indiceMusiqueACharger = -1;
        }
	}

    public static void ChargerMusique(int x)
    {
        indiceMusiqueACharger = x;
    }
}
