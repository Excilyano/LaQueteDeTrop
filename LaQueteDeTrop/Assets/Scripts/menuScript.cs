using UnityEngine;

/// <summary>
/// Title screen script
/// </summary>
public class menuScript : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Etienne");
    }

    public void Credits()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    }

    public void Menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}

