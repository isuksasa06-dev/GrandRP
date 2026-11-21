using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Коллайдеры колес (WheelColliders)")]
    public WheelCollider frontLeftCollider;
    public WheelCollider frontRightCollider;
    public WheelCollider rearLeftCollider;
    public WheelCollider rearRightCollider;

    [Header("3D-модели колес")]
    public Transform frontLeftMesh;
    public Transform frontRightMesh;
    public Transform rearLeftMesh;
    public Transform rearRightMesh;

    [Header("Характеристики авто")]
    public float motorForce = 1500f;
    public float brakeForce = 3000f;
    public float maxSteerAngle = 30f;

    [HideInInspector] public bool isDriving = false;

    private float moveInput;
    private float steerInput;
    private bool isBraking;

    private void FixedUpdate()
    {
        if (!isDriving)
        {
            ApplyBrakes(brakeForce);
            return;
        }

        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    // Методы для мобильных UI-кнопок (EventTrigger: PointerDown / PointerUp)
    public void SetGasInput(float value) => moveInput = value;
    public void SetSteerInput(float value) => steerInput = value;
    public void SetBrakeInput(bool braking) => isBraking = braking;

    private void HandleMotor()
    {
        rearLeftCollider.motorTorque = moveInput * motorForce;
        rearRightCollider.motorTorque = moveInput * motorForce;

        float currentBrake = isBraking ? brakeForce : 0f;
        ApplyBrakes(currentBrake);
    }

    private void ApplyBrakes(float force)
    {
        frontLeftCollider.brakeTorque = force;
        frontRightCollider.brakeTorque = force;
        rearLeftCollider.brakeTorque = force;
        rearRightCollider.brakeTorque = force;
    }

    private void HandleSteering()
    {
        float currentSteerAngle = maxSteerAngle * steerInput;
        frontLeftCollider.steerAngle = currentSteerAngle;
        frontRightCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftCollider, frontLeftMesh);
        UpdateSingleWheel(frontRightCollider, frontRightMesh);
        UpdateSingleWheel(rearLeftCollider, rearLeftMesh);
        UpdateSingleWheel(rearRightCollider, rearRightMesh);
    }

    private void UpdateSingleWheel(WheelCollider collider, Transform mesh)
    {
        if (mesh == null) return;
        collider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        mesh.position = pos;
        mesh.rotation = rot;
    }
}
