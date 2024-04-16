using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;

    public static GameManager instance;

    // start game
    public Button startButton;
    public GameObject enemySpawner;
    private EnemySpawner enemySpawnerScript;

    // pause
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    //end game
    public GameObject gameOverUI;
    public GameObject playerObject;
    private bool isGameOver = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        enemySpawnerScript = enemySpawner.GetComponent<EnemySpawner>();

        if (PlayerController.instance != null)
        {
            PlayerController.instance.OnPlayerDeath += HandlePlayerDeath;
        }
    }

    private void Update()
    {
        // Pause the game when the "Esc" key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        // Check if the player is dead
        if (playerObject == null)
        {
            GameOver();
        }
    }

    private void StartGame()
    {
        enemySpawner.SetActive(true);
        startButton.gameObject.SetActive(false);
        enemySpawnerScript.StartCoroutine(enemySpawnerScript.SpawnWaveWithDelay());
    }

    private void PauseGame()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f; // Pause or unpause the game
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }

    public void GameOver()
    {
        isGameOver = true;
        // Show the game over UI
        gameOverUI.SetActive(isGameOver);
        // Pause the game
        Time.timeScale = 0f; 


        // Optional: Disable or destroy the player GameObject
        //if (playerObject != null)
        //{
        //    playerObject.SetActive(false);
        //    // or
        //    // Destroy(playerObject);
        //}
    }

    public void RestartGame()
    {

        isGameOver = false;

        // Unpause the game
        Time.timeScale = 1f;

        // reload the current scene 
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        if (PlayerController.instance != null)
        {
            PlayerController.instance.OnPlayerDeath -= HandlePlayerDeath;
        }
    }

    private void HandlePlayerDeath()
    {
        GameOver();
    }
}