using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public bool isGameOver = false;
    public bool isGameStarted = false;
    public int score = 0;
    
    [Header("Speed Settings")]
    public float globalSpeed = 5f;
    public float maxGlobalSpeed = 15f;
    public float speedIncreaseRate = 0.2f; 
    
    // Static variable persists across scene reloads!
    private static bool shouldAutoStart = false;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    void Start()
    {
        if (shouldAutoStart)
        {
            // Reset the flag and start the game immediately!
            shouldAutoStart = false;
            StartGame();
            if (UIManager.Instance != null) UIManager.Instance.UpdateScore(score);
        }
        else if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateScore(score);
            UIManager.Instance.ShowStartScreen();
        }
    }
    
    void Update()
    {
        if (isGameStarted && !isGameOver)
        {
            if (globalSpeed < maxGlobalSpeed)
            {
                globalSpeed += speedIncreaseRate * Time.deltaTime;
            }
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        
        if (UIManager.Instance != null)
        {
            UIManager.Instance.HideStartScreen();
        }
    }
    
    public void AddScore(int amount)
    {
        if (isGameOver) return;
        score += amount;
        if (score < -100) score = -100; // Allow a small negative floor just to show it's working!
        
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateScore(score);
        }
    }
    
    public void GameOver()
    {
        isGameOver = true;
        
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowGameOver();
        }
    }
    
    public void RestartGame()
    {
        // Set the static flag so the next instance knows to start instantly!
        shouldAutoStart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToHome()
    {
        // Ensure the static flag is false so the start screen shows up!
        shouldAutoStart = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // --- DIRECT UI BUTTON HANDLERS ---
    // User can drag the GameManager object into the Button's OnClick and select these:

    public void OnStartButtonClicked()
    {
        StartGame();
    }

    public void OnRestartButtonClicked()
    {
        RestartGame();
    }

    public void OnHomeButtonClicked()
    {
        GoToHome();
    }
}
