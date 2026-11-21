using UnityEngine;

public class CrimePoint : MonoBehaviour
{
    [Header("Ссылки")]
    public PlayerDataManager dataManager;
    public PoliceSystem policeSystem;

    [Header("Настройки ограбления")]
    public int stolenMoney = 800;
    public int wantedStarsAdded = 2;
    public float cooldown = 60f;

    private float nextRobTime = 0f;

    public void CommitCrime()
    {
        if (Time.time < nextRobTime) return;
        if (dataManager == null || policeSystem == null) return;

        nextRobTime = Time.time + cooldown;

        dataManager.AddMoney(stolenMoney);
        policeSystem.AddWantedStars(wantedStarsAdded);
    }
}
