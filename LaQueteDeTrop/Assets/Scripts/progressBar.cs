using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progressBar : MonoBehaviour {
	public Image level_image;
    public static Image level_image_static;
	// Use this for initialization
	void Start () {
        level_image.fillAmount = 0;
        level_image_static = level_image;
    }
	
	// Update is called once per frame
	void Update () {

	}

    public static void AugmenterBarre(float x)
    {
        float tmp = level_image_static.fillAmount;
        if (tmp < 1)
        {
            level_image_static.fillAmount += x;
            if (tmp < 0.25f && tmp + x >= 0.25f) GameManager.Shuffle();
            if (tmp < 0.50f && tmp + x >= 0.50f) GameManager.Shuffle();
            if (tmp < 0.75f && tmp + x >= 0.75f) GameManager.Shuffle();
            if (tmp + x >= 1)
            {
                GameManager.SpawnNewBricks();
                ScoreManager.Rage();
                MusicManager.ChargerMusique(1);
            }
        }
    }

    public static void ReinitRage()
    {
        level_image_static.fillAmount = 0f;
    }
}
