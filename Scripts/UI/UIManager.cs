using System;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private GameObject hud;
    [SerializeField] private PlayerSizeHandler playerSizeHandler;
    [SerializeField] private TextMeshProUGUI tmpSize;
    [SerializeField] private TextMeshProUGUI tmpTimer;
    private float timer = 0;
    
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenu;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.Instance.GetGameState() == GameState.Playing)
        {
            Cursor.visible = false;
            hud.SetActive(true);
            pauseMenu.SetActive(false);
            UpdateTimer();
            UpdateSize();  
        }
        else
        {
            Cursor.visible = true;
            hud.SetActive(false);
            pauseMenu.SetActive(true);
        }
        
    }

    public void UpdateTimer()
    {
        timer += Time.deltaTime;
        tmpTimer.text = "Timer: " + TimeSpan.FromSeconds(timer).ToString(@"mm\:ss");
    }

    public void UpdateSize()
    {
        tmpSize.text = "Size: " + playerSizeHandler.GetSize();
    }

    public void Resume()
    {
        GameStateManager.Instance.SetGameState(GameState.Playing);
    }
    
    
}
