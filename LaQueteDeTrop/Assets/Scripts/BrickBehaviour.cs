using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehaviour : MonoBehaviour {
	private int couleur = 0;
	private List<GameObject> voisins = new List<GameObject>();
    private static List<GameObject> historic;

	public static int[] nbCouleursSpawnees = new int[6] {0, 0, 0, 0, 0, 0};
	public static int nbClics = 0;
	public static int nbBriquesDetruites = 0;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		if ((Mathf.Abs(col.gameObject.transform.position.y - gameObject.transform.position.y) < 0.3
			|| Mathf.Abs(col.gameObject.transform.position.x - gameObject.transform.position.x) < 0.3)
			&& col.gameObject.GetComponent<BrickBehaviour> () != null
			&& couleur == col.gameObject.GetComponent<BrickBehaviour> ().GetCouleur()
			&& !voisins.Contains(col.gameObject))
			voisins.Add (col.gameObject);
	}

	void OnTriggerExit2D(Collider2D col) {
		voisins.Remove (col.gameObject);
	}

	void OnMouseDown() {
		if (!GameManager.isPaused) {
			if (voisins.Count > 1
			   || (voisins.Count == 1 && voisins [0].GetComponent<BrickBehaviour> ().GetVoisins ().Count > 1)) {
				historic = new List<GameObject> ();
				historic.Add (gameObject);
				Kamikaze ();
				GameManager.provocation++;
				nbClics++;
				MusicManager.JouerSon ();
				// Determiner la voix de facon aleatoire
				MusicManager.JouerVoix (couleur);
				if (historic.Count < 5) {
					ScoreManager.AddToScore ((historic.Count - 2) * 30);
					progressBar.AugmenterBarre (0.05f + 0.025f * (historic.Count - 3));
				} else {
					ScoreManager.AddToScore (90);
					progressBar.AugmenterBarre (0.1f);
				}
			}
		}
	}

	public void Kamikaze() {
		foreach (GameObject current in voisins) {
            if (current && !historic.Contains (current)) {
				historic.Add (current);
				current.GetComponent<BrickBehaviour> ().Kamikaze ();
			}
		}
		nbCouleursSpawnees [this.couleur]--;
		nbBriquesDetruites++;
		Destroy (this.gameObject, 0.1f);
	}

	public void SetCouleur(int col) {
		couleur = col;
		nbCouleursSpawnees [col]++;
	}

	public int GetCouleur() {
		return couleur;
	}

	public List<GameObject> GetVoisins() {
		return voisins;
	}
}
