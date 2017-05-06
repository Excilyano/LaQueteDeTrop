using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progressBar : MonoBehaviour {
	[SerializeField]
	private Image level_image;
	private float test = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		test+=0.001f;
		level_image.fillAmount = test;
	}
}
