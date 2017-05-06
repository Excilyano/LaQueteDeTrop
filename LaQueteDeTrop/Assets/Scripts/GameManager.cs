using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject[] blocs;

	public static int largeur = 16;
	public static int hauteur = 20;
	
	private static int[,] blocsMatrix;
	private static GameObject[,] blocObjectsMatrix;
	private static bool[,] blocsVisitesMatrix;
	
	// Use this for initialization
	void Start () {
		blocsMatrix = new int[hauteur,largeur];
		blocObjectsMatrix = new GameObject[hauteur,largeur];
		blocsVisitesMatrix = new bool[hauteur,largeur];

		for( int i = 0; i < hauteur; i++) {
			for( int j = 0; j < largeur; j++) {
				blocsMatrix [i, j] = Random.Range (-1, blocs.Length);
				blocsVisitesMatrix [i, j] = false;
				Vector2 pos = new Vector2(j/2.5f - 3f, i/2.4f - 4.6f);
				if (blocsMatrix [i, j] > -1) {
					blocObjectsMatrix [i, j] = Instantiate (blocs [blocsMatrix [i, j]], pos, new Quaternion (0, 0, 0, 0));
					blocObjectsMatrix [i, j].GetComponent<BrickBehaviour> ().SetCouleur (blocsMatrix [i, j]);
					//blocObjectsMatrix [i, j].GetComponent<BrickBehaviour>().SetPosition (i,j);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
