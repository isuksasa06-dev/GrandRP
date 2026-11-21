
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;
    public float gravity = -9.81f;

    public MobileJoystick joystick;

    private CharacterController controller;
    private float verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector2 input = joystick != null
            ? joystick.InputVector
            : Vector2.zero;

        float horizontal = input.x;
        float vertical = input.y;

        transform.Rotate(
            0f,
            horizontal * rotationSpeed * Time.deltaTime,
            0f
        );

        Vector3 move = transform.forward * vertical;

        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;

        verticalVelocity += gravity * Time.deltaTime;

        move.y = verticalVelocity;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}
