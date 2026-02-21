using NUnit.Framework;
using UnityEngine;

public class ScoreManagerTests
{
    private const string HighScoreKey = "high_score";

    private GameObject go;
    private ScoreManager scoreManager;

    [SetUp]
    public void SetUp()
    {
        PlayerPrefs.DeleteKey(HighScoreKey);
        go = new GameObject();
        scoreManager = go.AddComponent<ScoreManager>();
        scoreManager.ResetScore();
    }

    [TearDown]
    public void TearDown()
    {
        PlayerPrefs.DeleteKey(HighScoreKey);
        Object.DestroyImmediate(go);
    }

    [Test]
    public void ResetScore_SetsScoreAndComboStreakToZero()
    {
        scoreManager.RegisterHit();
        scoreManager.ResetScore();

        Assert.AreEqual(0, scoreManager.Score);
        Assert.AreEqual(0, scoreManager.ComboStreak);
    }

    [Test]
    public void RegisterHit_IncreasesScore()
    {
        scoreManager.RegisterHit();

        Assert.Greater(scoreManager.Score, 0);
    }

    [Test]
    public void RegisterHit_IncreasesComboStreak()
    {
        scoreManager.RegisterHit();

        Assert.AreEqual(1, scoreManager.ComboStreak);
    }

    [Test]
    public void ComboMultiplier_IsOneWithNoStreak()
    {
        Assert.AreEqual(1, scoreManager.ComboMultiplier);
    }

    [Test]
    public void ComboMultiplier_IncreasesAfterFiveHits()
    {
        for (var i = 0; i < 5; i++)
        {
            scoreManager.RegisterHit();
        }

        Assert.AreEqual(2, scoreManager.ComboMultiplier);
    }

    [Test]
    public void ResetCombo_SetsComboStreakToZero()
    {
        scoreManager.RegisterHit();
        scoreManager.ResetCombo();

        Assert.AreEqual(0, scoreManager.ComboStreak);
    }

    [Test]
    public void CommitHighScore_UpdatesHighScoreWhenScoreIsHigher()
    {
        scoreManager.RegisterHit();
        var score = scoreManager.Score;
        scoreManager.CommitHighScore();

        Assert.AreEqual(score, scoreManager.HighScore);
    }

    [Test]
    public void CommitHighScore_DoesNotUpdateHighScoreWhenScoreIsLower()
    {
        scoreManager.RegisterHit();
        scoreManager.CommitHighScore();
        var highScore = scoreManager.HighScore;

        scoreManager.ResetScore();
        scoreManager.CommitHighScore();

        Assert.AreEqual(highScore, scoreManager.HighScore);
    }
}
