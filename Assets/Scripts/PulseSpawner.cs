using System.Collections.Generic;
using UnityEngine;

public class PulseSpawner : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private RingController ringController;
    [SerializeField] private Pulse pulsePrefab;
    [SerializeField] private int poolSize = 20;
    [SerializeField] private float ringRadius = 4f;
    [SerializeField] private float initialSpawnInterval = 1.1f;
    [SerializeField] private float minSpawnInterval = 0.35f;
    [SerializeField] private float initialPulseSpeed = 2.2f;
    [SerializeField] private float maxPulseSpeed = 7f;
    [SerializeField] private float difficultyRamp = 0.02f;
    [SerializeField, Range(0f, 1f)] private float overlapChance = 0.15f;

    private readonly Queue<Pulse> pool = new();
    private readonly List<Pulse> activePulses = new();
    private bool spawning;
    private float spawnTimer;

    private void Awake()
    {
        for (var i = 0; i < poolSize; i++)
        {
            var pulse = Instantiate(pulsePrefab, transform);
            pulse.gameObject.SetActive(false);
            pool.Enqueue(pulse);
        }
    }

    private void Update()
    {
        if (!spawning || gameManager.State != GameState.Playing)
        {
            return;
        }

        spawnTimer -= Time.deltaTime;
        if (spawnTimer > 0f)
        {
            return;
        }

        SpawnPulse();
        if (Random.value < overlapChance)
        {
            SpawnPulse();
        }

        spawnTimer = Mathf.Max(minSpawnInterval, initialSpawnInterval - CurrentDifficulty);
    }

    public void BeginSpawning()
    {
        spawning = true;
        spawnTimer = 0f;
        ClearActive();
    }

    public void StopSpawning()
    {
        spawning = false;
    }

    private void SpawnPulse()
    {
        if (pool.Count == 0 || ringController.SegmentColors.Count == 0)
        {
            return;
        }

        var angle = Random.Range(0f, 360f);
        var color = ringController.GetSegmentColorAtAngle(angle);
        var speed = Mathf.Min(maxPulseSpeed, initialPulseSpeed + CurrentDifficulty);

        var pulse = pool.Dequeue();
        activePulses.Add(pulse);
        pulse.Launch(color, angle, speed, ringRadius, ResolvePulse);
    }

    private void ResolvePulse(Pulse pulse, bool reachedRing)
    {
        if (reachedRing)
        {
            var expected = ringController.GetSegmentColorAtAngle(pulse.Angle);
            var matched = expected == pulse.PulseColor;
            gameManager.OnPulseResolved(matched);
        }

        pulse.gameObject.SetActive(false);
        activePulses.Remove(pulse);
        pool.Enqueue(pulse);
    }

    private void ClearActive()
    {
        for (var i = activePulses.Count - 1; i >= 0; i--)
        {
            var pulse = activePulses[i];
            pulse.gameObject.SetActive(false);
            pool.Enqueue(pulse);
            activePulses.RemoveAt(i);
        }
    }

    private float CurrentDifficulty => gameManager.SurvivalTime * difficultyRamp;
}
