using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMenuUI : MonoBehaviour
{
    [Header("Ссылки")]
    public PlayerDataManager dataManager;

    [Header("Панель меню")]
    public GameObject menuPanel;

    [Header("Паспорт / Статистика игрока")]
    public Text nameText;
    public Text levelText;
    public Text expText;
    public Text cashText;
    public Text bankText;

    [Header("Смена RP-имени")]
    public InputField newNameInput;

    private void Start()
    {
        if (menuPanel != null)
            menuPanel.SetActive(false);
    }

    public void ToggleMenu()
    {
        if (menuPanel == null) return;

        bool isActive = !menuPanel.activeSelf;
        menuPanel.SetActive(isActive);

        if (isActive)
        {
            UpdatePassportUI();
        }
    }

    public void UpdatePassportUI()
    {
        if (dataManager == null || dataManager.playerData == null) return;

        PlayerData data = dataManager.playerData;

        if (nameText != null) nameText.text = "Имя: " + data.playerName;
        if (levelText != null) levelText.text = "Уровень: " + data.level;
        if (expText != null) expText.text = "Опыт: " + data.experience;
        if (cashText != null) cashText.text = "Наличные: " + data.money + "$";
        if (bankText != null) bankText.text = "Банк: " + data.bankMoney + "$";
    }

    public void ChangePlayerName()
    {
        if (dataManager == null || dataManager.playerData == null || newNameInput == null) return;

        string enteredName = newNameInput.text.Trim();
        if (enteredName.Length >= 3)
        {
            dataManager.playerData.playerName = enteredName;
            dataManager.SavePlayer();
            newNameInput.text = "";
            UpdatePassportUI();
            Debug.Log("Имя изменено на: " + enteredName);
        }
        else
        {
            Debug.Log("Имя должно быть не короче 3 символов!");
        }
    }

    public void ResetProgressAndRestart()
    {
        PlayerSaveSystem.DeleteSave();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
