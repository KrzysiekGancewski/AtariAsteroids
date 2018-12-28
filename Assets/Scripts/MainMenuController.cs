using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    [SerializeField]
    private Text highscoreTxt;

    private void Start() {
        highscoreTxt.text = "HIGHEST SCORE:\n" + GameManager.instance.GetHighscore();
	}
	
	public void StartGame() {
        SceneManager.LoadScene("GameScene");
	}

    public void ExitGame() {
        Application.Quit();
    }
}
