
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraTouch : MonoBehaviour,
    IDragHandler
{
    public CameraFollow cameraFollow;

    public float sensitivity = 0.15f;

    public void OnDrag(PointerEventData eventData)
    {
        if (cameraFollow == null)
            return;

        float horizontal =
            eventData.delta.x * sensitivity;

        float vertical =
            eventData.delta.y * sensitivity;

        cameraFollow.RotateCamera(horizontal, vertical);
    }
}
