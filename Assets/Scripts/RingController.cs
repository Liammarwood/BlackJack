using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private List<Color> segmentColors = new();

    private int rotationDirection;

    private void Update()
    {
        if (rotationDirection == 0)
        {
            return;
        }

        transform.Rotate(0f, 0f, rotationDirection * rotationSpeed * Time.deltaTime);
    }

    public void SetRotationDirection(int direction)
    {
        rotationDirection = Mathf.Clamp(direction, -1, 1);
    }

    public Color GetSegmentColorAtAngle(float worldAngle)
    {
        if (segmentColors.Count == 0)
        {
            return Color.white;
        }

        var relative = Mathf.Repeat(worldAngle - transform.eulerAngles.z, 360f);
        var segmentSize = 360f / segmentColors.Count;
        var index = Mathf.FloorToInt(relative / segmentSize) % segmentColors.Count;
        return segmentColors[index];
    }

    public IReadOnlyList<Color> SegmentColors => segmentColors;
}
