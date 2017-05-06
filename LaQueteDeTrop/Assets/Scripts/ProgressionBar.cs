using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionBar : MonoBehaviour {

    public float progress = 0;
    public Vector2 position;
    public Vector2 size;
    public Image progress_full_Image;

    void update()
    {
        progress_full_Image.fillAmount += 1;
    }
}
