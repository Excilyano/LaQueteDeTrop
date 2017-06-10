using UnityEngine;

/// <summary>
/// Title screen script
/// </summary>
public class menuScript : MonoBehaviour
{
	public GameObject canvasMenu;
	public GameObject canvasCredit;

	public static GameObject canvasMenuStatic;
	public static GameObject canvasCreditStatic;

	void Start () {
		canvasMenuStatic = canvasMenu;
		canvasCreditStatic = canvasCredit;
		if (canvasMenu && canvasCredit) {
			canvasMenuStatic.SetActive (true);
			canvasCreditStatic.SetActive (false);
		}
	}

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Etienne");
    }

    public void Credits()
    {
		canvasMenuStatic.SetActive(false);
		canvasCreditStatic.SetActive(true);
    }

    public void Menu()
    {
		canvasMenuStatic.SetActive(true);
		canvasCreditStatic.SetActive(false);
    }

	public void GoToMenu() {
		UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
	}

	public void GoToNextLevel() {
		//UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
		GameManager.QueteSuivante();
	}
}

