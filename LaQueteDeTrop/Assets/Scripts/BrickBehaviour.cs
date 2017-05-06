using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehaviour : MonoBehaviour {
	/*
	public int posX;
	public int posY;
*/
	private int couleur = 0;
	private List<GameObject> voisins = new List<GameObject>();

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
		if (voisins.Count > 1
			|| (voisins.Count == 1 && voisins[0].GetComponent<BrickBehaviour> ().GetVoisins().Count > 1)) {
			List<GameObject> historic = new List<GameObject> ();
			historic.Add (gameObject);
			Kamikaze (historic);
		}
	}

	public void Kamikaze(List<GameObject> historic) {
		foreach (GameObject current in voisins) {
			if (!historic.Contains (current)) {
				historic.Add (current);
				current.GetComponent<BrickBehaviour> ().Kamikaze (historic);
			}
		}
		Destroy (this.gameObject, 0.2f);
	}

	public void SetCouleur(int col) {
		couleur = col;
	}

	public int GetCouleur() {
		return couleur;
	}

	public List<GameObject> GetVoisins() {
		return voisins;
	}
}
