using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject[] blocs;

	public static int largeur = 16;
	public static int hauteur = 17;
	
	private static int[,] blocsMatrix;
	private static GameObject[,] blocObjectsMatrix;

    public static bool spawnActive = false;
    public static bool isSpawning = false;
    public static float tempsDebut;
	
	// Use this for initialization
	void Start () {
		blocsMatrix = new int[hauteur,largeur];
		blocObjectsMatrix = new GameObject[hauteur,largeur];

		for( int i = 0; i < hauteur; i++) {
			for( int j = 0; j < largeur; j++) {
				blocsMatrix [i, j] = Random.Range (-1, blocs.Length);
				Vector2 pos = new Vector2(j/2.5f - 3f, i/2.4f - 3.45f);
				if (blocsMatrix [i, j] > -1) {
					blocObjectsMatrix [i, j] = Instantiate (blocs [blocsMatrix [i, j]], pos, new Quaternion (0, 0, 0, 0));
					blocObjectsMatrix [i, j].GetComponent<BrickBehaviour> ().SetCouleur (blocsMatrix [i, j]);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(spawnActive)
        {
            InvokeRepeating("Spawner", 1f, .5f);
            spawnActive = false;
            isSpawning = true;
        }
        if (isSpawning)
        {
            if (Time.time - tempsDebut >= 20)
            {
                CancelInvoke();
                isSpawning = false;
                progressBar.ReinitRage();
                MusicManager.ChargerMusique(0);
            }
        }
	}

    public static void Shuffle()
    {
        List<Vector2> positionsPossibles = new List<Vector2>();
        Vector2 infini = new Vector2(-1000, -1000);
        for (int i = 0; i < hauteur; i++)
        {
            for (int j = 0; j < largeur; j++)
            {
                positionsPossibles.Add(new Vector2(j / 2.5f - 3f, i / 2.4f - 3.45f));
            }
        }
        GameObject[] briques = GameObject.FindGameObjectsWithTag("Respawn");
        foreach (GameObject brique in briques)
        {
            brique.transform.position = infini;
        }
        foreach (GameObject brique in briques)
        {
            int position = Random.Range(0, positionsPossibles.Count);
            Vector2 nouvellePos = positionsPossibles[position];
            positionsPossibles.Remove(nouvellePos);
            brique.transform.position = nouvellePos;
        }
    }

    public static void SpawnNewBricks()
    {
        spawnActive = true;
        tempsDebut = Time.time;
    }

    void Spawner()
    {
        int couleur = Random.Range(0, blocs.Length);
        int posX = Random.Range(0, 16);
        GameObject instantiatedBlock = Instantiate(blocs[couleur], new Vector2 (posX / 2.5f - 3f, 3.4f), new Quaternion(0, 0, 0, 0));
        instantiatedBlock.GetComponent<BrickBehaviour>().SetCouleur(couleur);
    }
}
