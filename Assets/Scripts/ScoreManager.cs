using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    
    [SerializeField]
    private Text currentScoreTxt;
    private int currentScore;
    public int CurrentScore { 
        get { return currentScore; }
        set {
            currentScore = value;
            UpdateCurrentScoreUIText();
        }
    }

    private void UpdateCurrentScoreUIText() {
        currentScoreTxt.text = CurrentScore.ToString();
    }
}
