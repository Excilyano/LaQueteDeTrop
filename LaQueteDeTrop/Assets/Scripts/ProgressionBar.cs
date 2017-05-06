using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionBar : MonoBehaviour {

    public float progress = 0;
    public Vector2 position;
    public Vector2 size;
    public Texture2D progress_empty_Image;
    public Texture2D progress_full_Image;
    public float run = 0.01f;
    public float speed = 0;
    bool check = false;

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(position.x, position.y, size.x, size.y), progress_empty_Image);
        GUI.DrawTexture(new Rect(position.x, position.y, size.x*Mathf.Clamp01(progress), size.y), progress_full_Image);
    }

    void update()
    {
        if(check==false)
        {
            StartCoroutine(delay());
        }
    }

    IEnumerator delay()
    {
        check = true;
        progress = progress + run;
        yield return new WaitForSeconds(speed);
        check = false;
    }
}
