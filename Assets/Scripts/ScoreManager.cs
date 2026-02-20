using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const string HighScoreKey = "high_score";

    public int Score { get; private set; }
    public int ComboStreak { get; private set; }
    public int ComboMultiplier => Mathf.Max(1, ComboStreak / 5 + 1);
    public int HighScore { get; private set; }

    private void Awake()
    {
        HighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    public void ResetScore()
    {
        Score = 0;
        ComboStreak = 0;
    }

    public void RegisterHit()
    {
        ComboStreak++;
        Score += ComboMultiplier;
    }

    public void ResetCombo()
    {
        ComboStreak = 0;
    }

    public void CommitHighScore()
    {
        if (Score <= HighScore)
        {
            return;
        }

        HighScore = Score;
        PlayerPrefs.SetInt(HighScoreKey, HighScore);
        PlayerPrefs.Save();
    }
}
