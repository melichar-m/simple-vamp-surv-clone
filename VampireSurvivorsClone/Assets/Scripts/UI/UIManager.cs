using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VampireSurvivors.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [Header("HUD Elements")]
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI killCountText;
        [SerializeField] private TextMeshProUGUI fpsText;
        [SerializeField] private Slider healthBar;
        [SerializeField] private Slider experienceBar;

        [Header("Screens")]
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject levelUpScreen;

        [Header("FPS Settings")]
        [SerializeField] private float fpsUpdateInterval = 0.5f;
        private float fpsTimer = 0f;
        private int frameCount = 0;
        private float currentFPS = 0f;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            // Create basic UI if elements don't exist
            if (timerText == null || killCountText == null || fpsText == null)
            {
                CreateBasicHUD();
            }
        }

        private void Start()
        {
            // Hide all screens initially
            if (pauseMenu != null) pauseMenu.SetActive(false);
            if (gameOverScreen != null) gameOverScreen.SetActive(false);
            if (levelUpScreen != null) levelUpScreen.SetActive(false);
        }

        private void Update()
        {
            UpdateFPS();
            UpdateHUD();
        }

        private void UpdateFPS()
        {
            frameCount++;
            fpsTimer += Time.unscaledDeltaTime;

            if (fpsTimer >= fpsUpdateInterval)
            {
                currentFPS = frameCount / fpsTimer;
                frameCount = 0;
                fpsTimer = 0f;

                if (fpsText != null)
                {
                    fpsText.text = $"FPS: {currentFPS:F0}";
                    
                    // Color code FPS
                    if (currentFPS >= 55)
                        fpsText.color = Color.green;
                    else if (currentFPS >= 30)
                        fpsText.color = Color.yellow;
                    else
                        fpsText.color = Color.red;
                }
            }
        }

        private void UpdateHUD()
        {
            if (Managers.GameManager.Instance == null) return;

            // Update timer
            if (timerText != null)
            {
                timerText.text = Managers.GameManager.Instance.GetFormattedTime();
            }

            // Update kill count
            if (killCountText != null)
            {
                killCountText.text = $"Kills: {Managers.GameManager.Instance.GetEnemiesKilled()}";
            }
        }

        private void CreateBasicHUD()
        {
            // Create Canvas if it doesn't exist
            GameObject canvasObj = GameObject.Find("UICanvas");
            if (canvasObj == null)
            {
                canvasObj = new GameObject("UICanvas");
                Canvas canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasObj.AddComponent<CanvasScaler>();
                canvasObj.AddComponent<GraphicRaycaster>();
            }

            // Create HUD container
            GameObject hudContainer = new GameObject("HUD");
            hudContainer.transform.SetParent(canvasObj.transform, false);
            RectTransform hudRect = hudContainer.AddComponent<RectTransform>();
            hudRect.anchorMin = Vector2.zero;
            hudRect.anchorMax = Vector2.one;
            hudRect.sizeDelta = Vector2.zero;
            hudRect.anchoredPosition = Vector2.zero;

            // Create Timer Text (Top Center)
            if (timerText == null)
            {
                GameObject timerObj = new GameObject("Timer");
                timerObj.transform.SetParent(hudContainer.transform, false);
                timerText = timerObj.AddComponent<TextMeshProUGUI>();
                timerText.text = "00:00";
                timerText.fontSize = 36;
                timerText.alignment = TextAlignmentOptions.Center;
                timerText.color = Color.white;
                
                RectTransform timerRect = timerObj.GetComponent<RectTransform>();
                timerRect.anchorMin = new Vector2(0.5f, 1f);
                timerRect.anchorMax = new Vector2(0.5f, 1f);
                timerRect.anchoredPosition = new Vector2(0, -30);
                timerRect.sizeDelta = new Vector2(200, 50);
            }

            // Create Kill Count (Top Left)
            if (killCountText == null)
            {
                GameObject killObj = new GameObject("KillCount");
                killObj.transform.SetParent(hudContainer.transform, false);
                killCountText = killObj.AddComponent<TextMeshProUGUI>();
                killCountText.text = "Kills: 0";
                killCountText.fontSize = 24;
                killCountText.alignment = TextAlignmentOptions.Left;
                killCountText.color = Color.white;
                
                RectTransform killRect = killObj.GetComponent<RectTransform>();
                killRect.anchorMin = new Vector2(0f, 1f);
                killRect.anchorMax = new Vector2(0f, 1f);
                killRect.anchoredPosition = new Vector2(20, -30);
                killRect.sizeDelta = new Vector2(200, 50);
            }

            // Create FPS Counter (Top Right)
            if (fpsText == null)
            {
                GameObject fpsObj = new GameObject("FPS");
                fpsObj.transform.SetParent(hudContainer.transform, false);
                fpsText = fpsObj.AddComponent<TextMeshProUGUI>();
                fpsText.text = "FPS: 60";
                fpsText.fontSize = 20;
                fpsText.alignment = TextAlignmentOptions.Right;
                fpsText.color = Color.green;
                
                RectTransform fpsRect = fpsObj.GetComponent<RectTransform>();
                fpsRect.anchorMin = new Vector2(1f, 1f);
                fpsRect.anchorMax = new Vector2(1f, 1f);
                fpsRect.anchoredPosition = new Vector2(-20, -20);
                fpsRect.sizeDelta = new Vector2(100, 30);
            }
        }

        public void ShowPauseMenu(bool show)
        {
            if (pauseMenu != null)
                pauseMenu.SetActive(show);
        }

        public void ShowGameOverScreen(bool show)
        {
            if (gameOverScreen != null)
                gameOverScreen.SetActive(show);
        }

        public void ShowLevelUpScreen(bool show)
        {
            if (levelUpScreen != null)
                levelUpScreen.SetActive(show);
        }

        public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            if (healthBar != null)
            {
                healthBar.value = currentHealth / maxHealth;
            }
        }

        public void UpdateExperienceBar(float currentExp, float maxExp)
        {
            if (experienceBar != null)
            {
                experienceBar.value = currentExp / maxExp;
            }
        }
    }
}
