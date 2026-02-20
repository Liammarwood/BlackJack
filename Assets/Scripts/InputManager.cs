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

        if (Input.touchCount > 0)
        {
            HandlePosition(Input.GetTouch(0).position);
            return;
        }

        if (Input.GetMouseButton(0))
        {
            HandlePosition(Input.mousePosition);
            return;
        }

        ringController.SetRotationDirection(0);
    }

    private void HandlePosition(Vector2 position)
    {
        var direction = position.x < Screen.width * 0.5f ? 1 : -1;
        ringController.SetRotationDirection(direction);
    }
}
