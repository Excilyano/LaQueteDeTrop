using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;        // The player's score.
    private static int[] derniersScores;
    private static int curseur;
	private static int toIncrement;

    public Text scoreText;
	public Text levelText;
	public Text levelReussiText;
	public Text descriptionQueteText;
	public Text objectifText;

    void Awake()
    {
        // Reset the score.
        score = 0;

        derniersScores = new int[5] { 0, 0, 0, 0, 0 };
        curseur = 0;
		toIncrement = 0;
    }


    void Update()
    {
		if (toIncrement > 0) {
			toIncrement-=2;
			score+=2;
		}
		// Set the displayed text to be the word "Score" followed by the score value.
		scoreText.text = score + " / " + GameManager.scoreNecessaire[GameManager.niveauCourant -1];
		descriptionQueteText.text = GameManager.descriptions[GameManager.niveauCourant -1];
		objectifText.text = GameManager.objectifs[GameManager.niveauCourant -1];
		levelText.text = "Quete " + GameManager.niveauCourant;
		levelReussiText.text = "" + GameManager.niveauCourant;
    }

    public static void AddToScore(int value)
    {
		toIncrement += value;
        score += value;
        derniersScores[curseur] = value;
        curseur = (curseur + 1) % 5;
    }

    public static void Rage()
    {
        foreach (int i in derniersScores) score -= i;
    }
}