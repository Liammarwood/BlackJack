using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text comboText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text finalScoreText;

    public void ShowStart(int highScore)
    {
        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        highScoreText.text = $"High Score: {highScore}";
        scoreText.text = "0";
        comboText.text = "x1";
    }

    public void ShowPlaying(int score, int combo)
    {
        startPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        scoreText.text = score.ToString();
        comboText.text = $"x{combo}";
    }

    public void ShowGameOver(int score, int highScore)
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = $"Score: {score}";
        highScoreText.text = $"High Score: {highScore}";
    }
}
