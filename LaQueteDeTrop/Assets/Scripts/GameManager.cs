using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject blocRouge;
	public GameObject blocBleu;
	public GameObject blocVert;

	public int largeur;
	public int hauteur;
	
	private int[,] blocsMatrix;
	
	// Use this for initialization
	void Start () {
		blocsMatrix = new int[hauteur,largeur];

		/*{
			{Random.Range(1,4),0,0,0,0,Random.Range(1,4)},
			{0,0,0,0,0,0},
			{0,Random.Range(1,4),0,Random.Range(1,4),0,Random.Range(1,4)},
			{Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4)},
			{Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4)},
			{Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4)},
			{Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4)},
			{Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4)},
			{Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4)},
			{Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4),Random.Range(1,4)}
		};
*/

		for( int i = 0; i < hauteur; i++) {
			for( int j = 0; j < largeur; j++) {
				blocsMatrix [i, j] = Random.Range (0, 4);
				Vector2 pos = new Vector2(j/2.5f - 3f, i/2.5f - 3f);
				if (blocsMatrix[i,j] == 1) Instantiate (blocRouge, pos, new Quaternion(0,0,0,0));
				if (blocsMatrix[i,j] == 2) Instantiate (blocVert, pos, new Quaternion(0,0,0,0));
				if (blocsMatrix[i,j] == 3) Instantiate (blocBleu, pos, new Quaternion(0,0,0,0));
				
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
