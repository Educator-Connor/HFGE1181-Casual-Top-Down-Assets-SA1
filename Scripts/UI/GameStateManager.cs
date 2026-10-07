using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState
{
    Playing,
    Paused,
    GameOver
}

public class GameStateManager : MonoBehaviour
{
    public PlayerInputActions InputActions;
    public static GameStateManager Instance;
    
    [SerializeField] private GameState gameState = GameState.Playing;

    private void Awake()
    {
        InputActions = new PlayerInputActions();
        
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        InputActions.UI.Enable();
        
        // **CREATE A PAUSE BUTTON IN YOUR INPUT ACTIONS**
        InputActions.UI.Pause.performed += OnPause;
        

    }
    
    private void OnDisable()
    {
        InputActions.UI.Pause.performed -= OnPause;
        InputActions.UI.Disable();
    }

    public void SetGameState(GameState newState)
    {
        gameState = newState;
    }
    
    public GameState GetGameState()
    {
        return gameState;
    }
    
    private void OnPause(InputAction.CallbackContext obj)
    {
        if (gameState == GameState.Paused)
        {
            gameState = GameState.Playing;
        }
        else
        {
            gameState = GameState.Paused;
        }
    }

}
