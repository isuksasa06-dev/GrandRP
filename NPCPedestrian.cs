using UnityEngine;
using UnityEngine.AI;

public class NPCPedestrian : MonoBehaviour
{
    [Header("Навигация")]
    public NavMeshAgent agent;
    public Transform[] waypoints; // Точки маршрута по тротуару

    [Header("Настройки движения")]
    public float walkSpeed = 1.8f;
    public float waitTimeAtPoint = 2f;
    public float obstacleCheckDistance = 2.2f; // Дистанция остановки перед игроком/машиной
    public float obstacleCheckRadius = 0.4f;   // Ширина обзора бота

    [Header("Анимация (необязательно)")]
    public Animator animator;

    private int currentWaypointIndex = 0;
    private float waitTimer = 0f;
    private readonly int speedHash = Animator.StringToHash("Speed");

    private void Start()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (agent != null)
            agent.speed = walkSpeed;

        GoToNextWaypoint();
    }

    private void Update()
    {
        if (agent == null || waypoints == null || waypoints.Length == 0) return;

        // Проверяем, нет ли перед ботом игрока или машины
        if (IsObstacleAhead())
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero; // Гасим инерцию, чтобы бот не скользил вперед
            UpdateAnimation(0f);
            return;
        }
        else
        {
            agent.isStopped = false;
        }

        // Проверяем, дошел ли бот до текущей точки
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            waitTimer += Time.deltaTime;
            UpdateAnimation(0f);

            if (waitTimer >= waitTimeAtPoint)
            {
                waitTimer = 0f;
                GoToNextWaypoint();
            }
        }
        else
        {
            UpdateAnimation(agent.velocity.magnitude / walkSpeed);
        }
    }

    private void GoToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    private bool IsObstacleAhead()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 1f;
        
        // Используем SphereCast вместо Raycast для более надежного обнаружения препятствий
        if (Physics.SphereCast(rayOrigin, obstacleCheckRadius, transform.forward, out RaycastHit hit, obstacleCheckDistance))
        {
            if (hit.collider.CompareTag("Player") || hit.collider.GetComponentInParent<CarController>() != null)
            {
                return true;
            }
        }
        return false;
    }

    private void UpdateAnimation(float normalizedSpeed)
    {
        if (animator != null)
        {
            animator.SetFloat(speedHash, Mathf.Clamp01(normalizedSpeed));
        }
    }
}

