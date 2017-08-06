using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progressBar : MonoBehaviour {
	public Image level_image;
    public static Image level_image_static;
	private static float remplissage;
	// Use this for initialization
	void Start () {
        level_image.fillAmount = 0;
        level_image_static = level_image;
		remplissage = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if (level_image_static.fillAmount < remplissage) {
			level_image_static.fillAmount += 0.01f;
		}
	}

    public static void AugmenterBarre(float x)
    {
		if (remplissage < 1)
        {
			if (remplissage < 0.25f && remplissage + x >= 0.25f) GameManager.Shuffle();
			if (remplissage < 0.50f && remplissage + x >= 0.50f) GameManager.Shuffle();
			if (remplissage < 0.75f && remplissage + x >= 0.75f) GameManager.Shuffle();
			if (remplissage + x >= 1)
            {
                GameManager.SpawnNewBricks();
                ScoreManager.Rage();
                MusicManager.ChargerMusique(1);
				MusicManager.JouerRage ();
			}
			remplissage += x;
        }
    }

    public static void ReinitRage()
    {
		remplissage = 0f;
        level_image_static.fillAmount = 0f;
    }
}
