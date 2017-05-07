using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;        // The player's score.
    private static int[] derniersScores;
    private static int curseur;


    Text text;                      // Reference to the Text component.


    void Awake()
    {
        // Set up the reference.
        text = GetComponent<Text>();

        // Reset the score.
        score = 0;

        derniersScores = new int[5] { 0, 0, 0, 0, 0 };
        curseur = 0;
    }


    void Update()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        text.text = score + " / 1500";
    }

    public static void AddToScore(int value)
    {
        score += value;
        derniersScores[curseur] = value;
        curseur = (curseur + 1) % 5;
    }

    public static void Rage()
    {
        foreach (int i in derniersScores) score -= i;
    }
}