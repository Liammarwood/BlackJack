using UnityEngine;

public enum GameState
{
    Start,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private PulseSpawner pulseSpawner;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private UIManager uiManager;

    public GameState State { get; private set; } = GameState.Start;
    public float SurvivalTime { get; private set; }

    private void Start()
    {
        uiManager.ShowStart(scoreManager.HighScore);
    }

    private void Update()
    {
        if (State == GameState.Playing)
        {
            SurvivalTime += Time.deltaTime;
            return;
        }

        if (Input.GetMouseButtonDown(0) || HasTouchBegan())
        {
            StartGame();
        }
    }

    private static bool HasTouchBegan()
    {
        for (var i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                return true;
            }
        }

        return false;
    }

    public void StartGame()
    {
        SurvivalTime = 0f;
        State = GameState.Playing;
        scoreManager.ResetScore();
        pulseSpawner.BeginSpawning();
        uiManager.ShowPlaying(scoreManager.Score, scoreManager.ComboMultiplier);
    }

    public void OnPulseResolved(bool matched)
    {
        if (State != GameState.Playing)
        {
            return;
        }

        if (matched)
        {
            scoreManager.RegisterHit();
            uiManager.ShowPlaying(scoreManager.Score, scoreManager.ComboMultiplier);
            return;
        }

        scoreManager.ResetCombo();
        scoreManager.CommitHighScore();
        State = GameState.GameOver;
        pulseSpawner.StopSpawning();
        uiManager.ShowGameOver(scoreManager.Score, scoreManager.HighScore);
    }
}
