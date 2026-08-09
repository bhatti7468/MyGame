using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject gameOverPanel;

    [Header("Score & HighScore UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI mainMenuHighScoreText;

    [Header("Coin UI")]
    public TextMeshProUGUI inGameCoinText;     // ચાલુ રમતે રનના કોઇન બતાવવા માટે
    public TextMeshProUGUI mainMenuTotalCoinText; // Main Page પર ટ્રાન્સફર થયેલા Total Coins બતાવવા માટે

    private float score = 0f;
    private bool isGameOver = false;
    private int runCoins = 0; // આ રન દરમિયાન જમા થયેલા કલેક્ટેડ કાઇન્સ

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;

        // રનના કોઇન 0 થી શરૂ થશે
        runCoins = 0;
        UpdateInGameCoinUI();

        // Main Menu પર Total Coins અને High Score બતાવવા માટે
        UpdateMainMenuUI();

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StartBGM(); //[cite: 2]
        }

        // GameOverPanel શરૂઆતમાં બંધ જ હોવું જોઈએ
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // બાકીનો તમારો જૂનો Start() કોડ...
        Time.timeScale = 0f; // Main Menu ખુલ્લું હોય ત્યારે ગેમ પાઉસ રહે
    }

    void Update()
    {
        if (!isGameOver)
        {
            score += Time.deltaTime * 10f; //[cite: 2]
            if (scoreText != null)
            {
                scoreText.text = Mathf.FloorToInt(score).ToString(); //[cite: 2]
            }
        }
    }

    // ચાલુ રમતે કોઇન ભેગો થાય ત્યારે આ કૉલ થશે
    public void AddCoin(int amount)
    {
        runCoins += amount; // માત્ર રનના કોઇન વધશે
        UpdateInGameCoinUI();
    }

    void UpdateInGameCoinUI()
    {
        if (inGameCoinText != null)
        {
            inGameCoinText.text = runCoins.ToString();
        }
    }

    void UpdateMainMenuUI()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (mainMenuTotalCoinText != null)
        {
            mainMenuTotalCoinText.text = totalCoins.ToString();
        }

        if (mainMenuHighScoreText != null)
        {
            mainMenuHighScoreText.text = "Best: " + highScore.ToString();
        }
    }

    public void GameOver()
    {
        if (isGameOver) return; //[cite: 2]
        isGameOver = true; //[cite: 2]

        Time.timeScale = 0f; //[cite: 2]

        // 1. Spawner અને ઓબ્સ્ટેકલ્સ બંધ કરો[cite: 2]
        ObstacleSpawner spawner = FindFirstObjectByType<ObstacleSpawner>(); //[cite: 2]
        if (spawner != null)
        {
            spawner.CancelInvoke(); //[cite: 2]
            spawner.enabled = false; //[cite: 2]
        }

        CoinSpawner coinSpawner = FindFirstObjectByType<CoinSpawner>();
        if (coinSpawner != null)
        {
            coinSpawner.enabled = false;
        }

        ObstacleMovement[] allObstacles = FindObjectsByType<ObstacleMovement>(FindObjectsSortMode.None); //[cite: 2]
        foreach (ObstacleMovement obs in allObstacles)
        {
            obs.enabled = false; //[cite: 2]
            Rigidbody2D rb = obs.GetComponent<Rigidbody2D>(); //[cite: 2]
            if (rb != null) rb.simulated = false; //[cite: 2]
        }

        // 2. Game Over થતા જ આ રનના Coins ને Total Coins માં સેવ કરો
        int currentTotalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        PlayerPrefs.SetInt("TotalCoins", currentTotalCoins + runCoins);

        // 3. High Score સેવ[cite: 2]
        int finalScore = Mathf.FloorToInt(score); //[cite: 2]
        int highScore = PlayerPrefs.GetInt("HighScore", 0); //[cite: 2]

        if (finalScore > highScore)
        {
            highScore = finalScore; //[cite: 2]
            PlayerPrefs.SetInt("HighScore", highScore); //[cite: 2]
        }
        PlayerPrefs.Save(); //[cite: 2]

        if (highScoreText != null)
        {
            highScoreText.text = "Best: " + highScore.ToString(); //[cite: 2]
        }

        // 4. Audio Control[cite: 2]
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopBGM(); //[cite: 2]
            AudioManager.Instance.PlayGameOver(); //[cite: 2]
        }

        // 5. Game Over Panel બતાવો[cite: 2]
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); //[cite: 2]
        }
    }

    public void RestartGame()
    {
        StartCoroutine(RestartRoutine()); //[cite: 2]
    }

    private IEnumerator RestartRoutine()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayButtonClick(); //[cite: 2]
            AudioManager.Instance.RestartAudio(); //[cite: 2]
        }

        yield return new WaitForSecondsRealtime(0.1f); //[cite: 2]

        Time.timeScale = 1f; //[cite: 2]
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //[cite: 2]
    }
}