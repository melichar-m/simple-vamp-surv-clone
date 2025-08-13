using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace VampireSurvivors.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Game State")]
        [SerializeField] private GameState currentState = GameState.Menu;
        [SerializeField] private bool isPaused = false;

        [Header("Game Stats")]
        [SerializeField] private float gameTime = 0f;
        [SerializeField] private int enemiesKilled = 0;
        [SerializeField] private int experienceCollected = 0;

        [Header("References")]
        [SerializeField] private GameObject playerPrefab;
        private GameObject currentPlayer;

        public enum GameState
        {
            Menu,
            Playing,
            Paused,
            LevelUp,
            GameOver
        }

        private void Awake()
        {
            // Singleton pattern
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            // Start in playing state for now (no menu yet)
            StartGame();
        }

        private void Update()
        {
            if (currentState == GameState.Playing)
            {
                gameTime += Time.deltaTime;
                
                // Handle pause input
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    TogglePause();
                }
            }
        }

        public void StartGame()
        {
            currentState = GameState.Playing;
            gameTime = 0f;
            enemiesKilled = 0;
            experienceCollected = 0;
            isPaused = false;
            Time.timeScale = 1f;

            // Spawn player if not exists
            if (currentPlayer == null)
            {
                SpawnPlayer();
            }
        }

        private void SpawnPlayer()
        {
            // Create player from scratch if no prefab
            if (playerPrefab == null)
            {
                currentPlayer = new GameObject("Player");
                currentPlayer.tag = "Player";
                currentPlayer.layer = LayerMask.NameToLayer("Default");
                
                // Add player controller
                currentPlayer.AddComponent<Player.PlayerController>();
                
                // Add a circle collider
                CircleCollider2D collider = currentPlayer.AddComponent<CircleCollider2D>();
                collider.radius = 0.3f;
                collider.isTrigger = false;
                
                // Position at center
                currentPlayer.transform.position = Vector3.zero;
            }
            else
            {
                currentPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            }
        }

        public void TogglePause()
        {
            if (currentState != GameState.Playing && currentState != GameState.Paused)
                return;

            isPaused = !isPaused;
            currentState = isPaused ? GameState.Paused : GameState.Playing;
            Time.timeScale = isPaused ? 0f : 1f;
        }

        public void GameOver()
        {
            currentState = GameState.GameOver;
            Time.timeScale = 0f;
            
            // Show game over UI (when implemented)
            Debug.Log($"Game Over! Survived: {GetFormattedTime()} | Enemies Killed: {enemiesKilled}");
        }

        public void RestartGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void AddKill()
        {
            enemiesKilled++;
        }

        public void AddExperience(int amount)
        {
            experienceCollected += amount;
        }

        public string GetFormattedTime()
        {
            int minutes = Mathf.FloorToInt(gameTime / 60f);
            int seconds = Mathf.FloorToInt(gameTime % 60f);
            return $"{minutes:00}:{seconds:00}";
        }

        // Getters
        public GameState GetCurrentState() => currentState;
        public float GetGameTime() => gameTime;
        public int GetEnemiesKilled() => enemiesKilled;
        public int GetExperienceCollected() => experienceCollected;
        public bool IsPaused() => isPaused;
        public GameObject GetPlayer() => currentPlayer;
    }
}
