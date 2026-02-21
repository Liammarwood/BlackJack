# Copilot Instructions for Resonance

## Project Overview

**Resonance** is an endless arcade game for Android built with Unity. The player taps the screen to match incoming pulses to a central ring. The game features difficulty scaling, combo multipliers, high score tracking, and start/game-over state flow.

This repository also contains a legacy Python command-line Black Jack game (`Black Jack v4.py`).

## Repository Structure

```
Assets/
  Scripts/           # Unity C# MonoBehaviour scripts
    GameManager.cs   # Game state machine (Start → Playing → GameOver)
    RingController.cs# Central ring that accepts or rejects pulses
    PulseSpawner.cs  # Spawns and difficulty-scales incoming pulses
    Pulse.cs         # Individual pulse behaviour
    ScoreManager.cs  # Score, combo multiplier, and high score logic
    InputManager.cs  # Translates touch/click input into game events
    UIManager.cs     # Drives the HUD and screen transitions
.github/
  workflows/
    android-release.yml  # Builds APK + AAB and publishes a GitHub Release on every push to master
Black Jack v4.py     # Legacy Python Black Jack console game
README.md
```

## Tech Stack

| Layer | Technology |
|---|---|
| Game engine | Unity (portrait, Android target) |
| Game scripts | C# (Unity MonoBehaviour pattern) |
| CI / CD | GitHub Actions + [game-ci/unity-builder](https://game.ci/) |
| Legacy script | Python 3 |

## Coding Conventions

### C# / Unity scripts
- One `MonoBehaviour` class per file; filename must match the class name.
- Use `[SerializeField] private` for Inspector-exposed fields; avoid `public` fields.
- Game-state transitions belong in `GameManager`; do not call `StartGame` / `OnPulseResolved` from other scripts directly—raise an event or call through `GameManager`.
- Prefer `Time.deltaTime`-based calculations over coroutine delays for anything that needs to pause correctly.
- Use `var` only when the type is obvious from the right-hand side.

### Python
- Follow PEP 8 style.
- Avoid global variables; prefer passing state as function parameters or encapsulating it in a class.

## Build & Release

Builds are triggered automatically by pushing to the `master` branch via `.github/workflows/android-release.yml`.

The workflow requires the following repository secrets:
- `UNITY_LICENSE` – contents of a valid Unity license file
- `UNITY_EMAIL` – Unity account e-mail
- `UNITY_PASSWORD` – Unity account password

Artifacts (APK and AAB) are uploaded to the workflow run and also published as a GitHub Release tagged `v1.0.<run_number>`.

## Testing

There is currently no automated test suite. When adding new features:
- Manually verify game-state transitions (Start → Playing → GameOver → replay).
- Test on an Android device or emulator at the target portrait resolution.
- For Python changes, run `python "Black Jack v4.py"` and exercise all code paths.
