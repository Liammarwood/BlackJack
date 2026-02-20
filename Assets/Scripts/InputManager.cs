using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private RingController ringController;

    private void Update()
    {
        if (gameManager.State != GameState.Playing)
        {
            ringController.SetRotationDirection(0);
            return;
        }

        if (TryGetActiveTouch(out var touchPosition))
        {
            HandlePosition(touchPosition);
            return;
        }

        if (Input.GetMouseButton(0))
        {
            HandlePosition(Input.mousePosition);
            return;
        }

        ringController.SetRotationDirection(0);
    }

    private static bool TryGetActiveTouch(out Vector2 position)
    {
        for (var i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            if (touch.phase is TouchPhase.Began or TouchPhase.Moved or TouchPhase.Stationary)
            {
                position = touch.position;
                return true;
            }
        }

        position = default;
        return false;
    }

    private void HandlePosition(Vector2 position)
    {
        var rotationDirection = position.x < Screen.width * 0.5f ? 1 : -1;
        ringController.SetRotationDirection(rotationDirection);
    }
}
