using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Elements")]
    public Text scoreText;
    public Image healthBarFill;
    public GameObject startScreenPanel;
    public GameObject gameOverPanel;
    public Text finalScoreText; // Added for the final score display

    [Header("Health Bar Gradient")]
    public Gradient healthGradient;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    void Start()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    public void ShowStartScreen()
    {
        if (startScreenPanel != null) startScreenPanel.SetActive(true);
    }

    public void HideStartScreen()
    {
        if (startScreenPanel != null) startScreenPanel.SetActive(false);
    }

    public void OnStartButtonClicked()
    {
        if (GameManager.Instance != null) GameManager.Instance.StartGame();
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        float percent = currentHealth / maxHealth;
        
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = percent;
            healthBarFill.color = healthGradient.Evaluate(percent);
        }
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
            
        if (finalScoreText != null && GameManager.Instance != null)
        {
            finalScoreText.text = "Final Score: " + GameManager.Instance.score;
        }
    }

    public void OnRestartButtonClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.RestartGame();
    }

    public void OnHomeButtonClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.GoToHome();
    }
}
