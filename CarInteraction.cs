using UnityEngine;

public class CarInteraction : MonoBehaviour
{
    [Header("Ссылки на машину")]
    public CarController carController;
    public Transform exitPoint;

    [Header("Ссылки на игрока и камеру")]
    public GameObject playerObject;
    public CameraFollow cameraFollow;

    [Header("Интерфейс (UI)")]
    public GameObject footControlsUI;
    public GameObject carControlsUI;

    private bool isPlayerInside = false;

    public void EnterCar()
    {
        if (isPlayerInside || playerObject == null || carController == null) return;

        isPlayerInside = true;
        carController.isDriving = true;

        // Прячем игрока
        playerObject.SetActive(false);

        // Переключаем камеру на машину
        if (cameraFollow != null)
            cameraFollow.target = transform;

        // Меняем кнопки управления на экране
        if (footControlsUI != null) footControlsUI.SetActive(false);
        if (carControlsUI != null) carControlsUI.SetActive(true);
    }

    public void ExitCar()
    {
        if (!isPlayerInside) return;

        isPlayerInside = false;
        carController.isDriving = false;

        // Сбрасываем педали при выходе
        carController.SetGasInput(0f);
        carController.SetSteerInput(0f);
        carController.SetBrakeInput(true);

        // Высаживаем игрока рядом с дверью
        Vector3 spawnPos = exitPoint != null ? exitPoint.position : transform.position + transform.right * -2f;
        playerObject.transform.position = spawnPos;
        playerObject.SetActive(true);

        // Возвращаем камеру на игрока
        if (cameraFollow != null)
            cameraFollow.target = playerObject.transform;

        // Возвращаем пешее управление
        if (carControlsUI != null) carControlsUI.SetActive(false);
        if (footControlsUI != null) footControlsUI.SetActive(true);
    }
}
