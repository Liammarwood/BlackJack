using System;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float speed;
    private float ringRadiusSquared;
    private float angle;
    private Action<Pulse, bool> resolveCallback;

    public Color PulseColor { get; private set; }
    public float Angle => angle;

    public void Launch(Color color, float targetAngle, float travelSpeed, float targetRadius, Action<Pulse, bool> onResolve)
    {
        PulseColor = color;
        angle = targetAngle;
        speed = travelSpeed;
        ringRadiusSquared = targetRadius * targetRadius;
        resolveCallback = onResolve;
        spriteRenderer.color = color;
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        var direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f);
        transform.localPosition += direction * (speed * Time.deltaTime);

        if (transform.localPosition.sqrMagnitude >= ringRadiusSquared)
        {
            resolveCallback?.Invoke(this, true);
            return;
        }
    }
}
