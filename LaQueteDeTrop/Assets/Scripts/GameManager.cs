using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject[] blocs;
	public static GameObject[] blocsUtilises;

	public GameObject canvasGame;
	public GameObject canvasFin;
	public GameObject canvasAbandon;
	public static GameObject canvasGameStatic;
	public static GameObject canvasFinStatic;
	public static GameObject canvasAbandonStatic;

	public GameObject boutonProvoc;
	private Button leBouton;

	public static int largeur = 16;
	public static int hauteur = 17;

    public static bool spawnActive = false;
    public static bool isSpawning = false;
    public static float tempsDebut;

	public static int niveauCourant;
	public static int[] scoreNecessaire;
	public static string[] objectifs;
	public static string[] descriptions;

	private static int nbBlocs;
	public static bool isPaused;

	public static int provocation;
	
	// Use this for initialization
	void Start () {
		isPaused = false;
		nbBlocs = 3;
		canvasGame.SetActive (true);
		canvasFin.SetActive (false);
		canvasAbandon.SetActive (false);
		canvasFinStatic = canvasFin;
		canvasGameStatic = canvasGame;
		canvasAbandonStatic = canvasAbandon;

		blocsUtilises = blocs;
		GenererNiveau ();

		niveauCourant = 1;
		scoreNecessaire = new int[10]{ 2000 , 2000 , 2500 , 5000 , 5000 , 5000 , 5000 , 5000 , 5000 , 20000 };
		descriptions = new string[10] {
			"Entrainement",
			"Mort a la verdure",
			"Moi je connais une chanson...",
			"A l'aventure compagnon",
			"Au violet !",
			"Infiltration et discretion",
			"Dans l'antre des demons",
			"Les tenebres...",
			"...et le feu !",
			"La quete de trop"
		};
		objectifs = new string[10] {
			"Atteint 2.000 points !",
			"Laisse moins de 8 gemmes vertes a l'elfe !",
			"Enerve l'elfe en moins de 11 coups !",
			"Atteint 5.000 points !",
			"Laisse moins de 8 gemmes violettes au marchand !",
			"Elimine 50 gemmes sans énerver l'elfe !",
			"Atteint 8.000 points !",
			"Laisse moins de 8 gemmes noires au Rogbal !",
			"Laisse moins de 8 gemmes rouges au Rogbal en moins de 30 coups !",
			"Atteint 20.000 points en moins de X coups !"
		};

		provocation = 0;
		leBouton = boutonProvoc.GetComponent<Button> ();
	}

	private static void GenererNiveau() {
		int[,] blocsMatrix = new int[hauteur,largeur];
		GameObject[,] blocObjectsMatrix = new GameObject[hauteur,largeur];

		for( int i = 0; i < hauteur; i++) {
			for( int j = 0; j < largeur; j++) {
				blocsMatrix [i, j] = Random.Range (-1, nbBlocs);
				Vector2 pos = new Vector2(j/2.5f - 3.015f, i/2.4f - 3.45f);
				if (blocsMatrix [i, j] > -1) {
					blocObjectsMatrix [i, j] = Instantiate (blocsUtilises [blocsMatrix [i, j]], pos, new Quaternion (0, 0, 0, 0));
					blocObjectsMatrix [i, j].GetComponent<BrickBehaviour> ().SetCouleur (blocsMatrix [i, j]);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnActive) {
			InvokeRepeating ("Spawner", 1f, .5f);
			spawnActive = false;
			isSpawning = true;
		}
		if (isSpawning) {
			if (Time.time - tempsDebut >= 20) {
				CancelInvoke ();
				isSpawning = false;
				progressBar.ReinitRage ();
				MusicManager.ChargerMusique (0);
			}
		}
		if (ConditionVictoire ()) {
			isPaused = true;
			GameObject[] briques = GameObject.FindGameObjectsWithTag ("Respawn");
			foreach (GameObject brique in briques) {
				Destroy (brique);
			}
			if (isSpawning) {
				CancelInvoke ();
				isSpawning = false;
				MusicManager.ChargerMusique (0);
			}
			progressBar.ReinitRage ();
			MusicManager.ShutUp ();
			canvasGame.SetActive (false);
			canvasAbandon.SetActive (false);
			canvasFin.SetActive (true);
		}
		leBouton.interactable = (provocation >= 10 && !MusicManager.isRaging);
	}

	public bool ConditionVictoire() {
		bool victoire = false;
		switch (niveauCourant) {
		case 1:
			victoire = (ScoreManager.score >= scoreNecessaire [niveauCourant - 1]);
			break;
		case 2:
			victoire = (BrickBehaviour.nbCouleursSpawnees[3] < 8);
			break;
		case 3:
			victoire = MusicManager.isRaging;
			break;
		case 4:
			victoire = (ScoreManager.score >= scoreNecessaire [niveauCourant - 1]);
			break;
		case 5:
			victoire = (BrickBehaviour.nbCouleursSpawnees[4] < 8);
			break;
		case 6:
			victoire = (BrickBehaviour.nbBriquesDetruites >= 50);
			break;
		case 7:
			victoire = (ScoreManager.score >= scoreNecessaire [niveauCourant - 1]);
			break;
		case 8:
			victoire = (BrickBehaviour.nbCouleursSpawnees[5] < 8);
			break;
		case 9:
			victoire = (BrickBehaviour.nbCouleursSpawnees[0] < 8);
			break;
		case 10:
			victoire = (ScoreManager.score >= scoreNecessaire [niveauCourant - 1]);
			break;
		}
		return victoire;
	}

    public static void Shuffle()
    {
        List<Vector2> positionsPossibles = new List<Vector2>();
        Vector2 infini = new Vector2(-1000, -1000);
        for (int i = 0; i < hauteur; i++)
        {
            for (int j = 0; j < largeur; j++)
            {
				positionsPossibles.Add(new Vector2(j / 2.5f - 3.015f, i / 2.4f - 3.45f));
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
		int couleur = Random.Range(0, nbBlocs);
        int posX = Random.Range(0, 16);
        GameObject instantiatedBlock = Instantiate(blocs[couleur], new Vector2 (posX / 2.5f - 3.015f, 3.4f), new Quaternion(0, 0, 0, 0));
        instantiatedBlock.GetComponent<BrickBehaviour>().SetCouleur(couleur);
    }

	public static void QueteSuivante() {
		niveauCourant++;
		provocation = 0;
		if (niveauCourant > 1) {
			nbBlocs = 4;
		}
		if (niveauCourant > 4) {
			nbBlocs = 5;
		}
		if (niveauCourant > 7) {
			nbBlocs = 6;
		}

		ScoreManager.score = 0;
		BrickBehaviour.nbClics = 0;
		BrickBehaviour.nbBriquesDetruites = 0;
		BrickBehaviour.nbCouleursSpawnees = new int[6] { 0, 0, 0, 0, 0, 0 };
		canvasGameStatic.SetActive (true);
		canvasFinStatic.SetActive (false);
		canvasAbandonStatic.SetActive (false);
		GenererNiveau ();
		isPaused = false;
	}

	public void AfficherAbandon() {
		isPaused = true;
		canvasGameStatic.SetActive (false);
		canvasFinStatic.SetActive (false);
		canvasAbandonStatic.SetActive (true);
	}

	public void ReprendrePartie() {
		isPaused = false;
		canvasGameStatic.SetActive (true);
		canvasFinStatic.SetActive (false);
		canvasAbandonStatic.SetActive (false);
	}

	public void Recommencer() {
		provocation = 0;
		tempsDebut = -21f;
		MusicManager.ShutUp ();
		GameObject[] briques = GameObject.FindGameObjectsWithTag ("Respawn");
		foreach (GameObject brique in briques) {
			Destroy (brique);
		}
		progressBar.ReinitRage ();
		ScoreManager.score = 0;
		BrickBehaviour.nbClics = 0;
		BrickBehaviour.nbBriquesDetruites = 0;
		BrickBehaviour.nbCouleursSpawnees = new int[6] { 0, 0, 0, 0, 0, 0 };
		canvasGameStatic.SetActive (true);
		canvasFinStatic.SetActive (false);
		canvasAbandonStatic.SetActive (false);
		GenererNiveau ();
		isPaused = false;
	}

	public void Provoquer() {
		MusicManager.Provoquer ();
		progressBar.AugmenterBarre (0.25f);
		provocation = 0;
	}
}
