using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Ссылки")]
    public Animator animator;
    public MobileJoystick joystick;
    public CharacterController characterController; // Необязательно: для проверки реального движения

    [Header("Настройки плавности")]
    public float smoothTime = 10f;
    public float maxMoveSpeed = 5f; // Максимальная скорость бега (если указан CharacterController)

    // Имена параметров в Animator Controller
    private readonly int speedHash = Animator.StringToHash("Speed");
    private readonly int isCarryingHash = Animator.StringToHash("IsCarrying");

    private float currentSpeed;

    private void Update()
    {
        if (animator == null || joystick == null) return;

        // Вычисляем силу отклонения джойстика (от 0 до 1)
        Vector2 inputDir = new Vector2(joystick.Horizontal, joystick.Vertical);
        float targetSpeed = Mathf.Clamp01(inputDir.magnitude);

        // Если привязан CharacterController и игрок уперся в стену, сбрасываем анимацию бега
        if (characterController != null)
        {
            Vector3 horizontalVelocity = new Vector3(characterController.velocity.x, 0f, characterController.velocity.z);
            if (horizontalVelocity.magnitude < 0.1f)
            {
                targetSpeed = 0f;
            }
        }

        // Плавно меняем параметр Speed для Blend Tree (0 = стоит, 0.5 = идет, 1 = бежит)
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, smoothTime * Time.deltaTime);
        animator.SetFloat(speedHash, currentSpeed);
    }

    // Можно вызывать из DeliveryJob, когда игрок берет/сдает ящик
    public void SetCarryingState(bool isCarrying)
    {
        if (animator != null)
            animator.SetBool(isCarryingHash, isCarrying);
    }

    // Метод для триггера прыжка или взаимодействия
    public void TriggerInteractAnimation()
    {
        if (animator != null)
            animator.SetTrigger("Interact");
    }
}

