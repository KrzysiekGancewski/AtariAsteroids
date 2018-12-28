using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenuController : MonoBehaviour {

    [SerializeField]
    private GameObject[] gameObjectsToDisable;
    [SerializeField]
    private ScoreManager scoreManager;
    [SerializeField]
    private Text GameOverCurrentPointsTxt;
    [SerializeField]
    private Text GameOverHighscoreTxt;

    public void ShowGameOverPanel() {
        foreach (var go in gameObjectsToDisable) {
            go.SetActive(false);
        }

        GameOverCurrentPointsTxt.text = "YOUR SCORE: " + scoreManager.CurrentScore.ToString();
        GameManager.instance.SetHighscore(scoreManager.CurrentScore);
        GameOverHighscoreTxt.text = "HIGHEST SCORE: " + GameManager.instance.GetHighscore().ToString();
        gameObject.SetActive(true);
    }

	public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
