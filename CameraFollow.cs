

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float distance = 5f;
    public float height = 2.5f;
    public float smoothSpeed = 8f;

    public float minPitch = -20f;
    public float maxPitch = 60f;

    private float yaw;
    private float pitch = 15f;

    public void RotateCamera(float x, float y)
    {
        yaw += x;
        pitch -= y;

        pitch = Mathf.Clamp(
            pitch,
            minPitch,
            maxPitch
        );
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        Quaternion rotation = Quaternion.Euler(
            pitch,
            yaw,
            0f
        );

        Vector3 offset = rotation *
            new Vector3(0f, 0f, -distance);

        Vector3 desiredPosition =
            target.position +
            offset +
            Vector3.up * height;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.LookAt(
            target.position + Vector3.up * 1.5f
        );
    }
}
