using UnityEngine;

public class JobTriggerPoint : MonoBehaviour
{
    public enum PointType { Pickup, Dropoff }

    [Header("Настройки точки")]
    public PointType pointType;
    public DeliveryJob deliveryJob;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || deliveryJob == null) return;

        if (pointType == PointType.Pickup)
        {
            deliveryJob.OnPickupReached();
        }
        else if (pointType == PointType.Dropoff)
        {
            deliveryJob.OnDropoffReached();
        }
    }
}
