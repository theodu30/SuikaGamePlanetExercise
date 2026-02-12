using UnityEngine;
using UnityEngine.InputSystem;

public class GameEvents : MonoBehaviour
{
    public InputAction PlayerTurnLeftAction;
    public static event System.EventHandler OnPlayerTurnLeft;
    public static void RaisePlayerTurnLeft()
    {
        OnPlayerTurnLeft?.Invoke(null, System.EventArgs.Empty);
    }

    public InputAction PlayerTurnRightAction;
    public static event System.EventHandler OnPlayerTurnRight;
    public static void RaisePlayerTurnRight()
    {
        OnPlayerTurnRight?.Invoke(null, System.EventArgs.Empty);
    }

    public InputAction PlayerSpawnFruitAction;
    public static event System.EventHandler OnPlayerSpawnFruit;
    public static void RaisePlayerSpawnFruit()
    {
        OnPlayerSpawnFruit?.Invoke(null, System.EventArgs.Empty);
    }

    public static int Score = 0;
    public static event System.EventHandler<int> OnScoreChange;
    public static void RaiseScoreChange(int scoreToAdd)
    {
        Score += scoreToAdd;
        OnScoreChange?.Invoke(null, Score);
    }

    public static void ResetScore()
    {
        Score = 0;
        OnScoreChange?.Invoke(null, Score);
    }

    public static Fruit.FruitType CurrentType;
    public static Fruit.FruitType NextType;
    public static event System.EventHandler<Fruit.FruitType> OnNextTypeChange;
    public static void RaiseNextTypeChange(Fruit.FruitType newType)
    {
        NextType = newType;
        OnNextTypeChange?.Invoke(null, NextType);
    }

    public static bool IsGameLost = false;
    public static event System.EventHandler OnGameLost;
    public static void RaiseGameLost()
    {
        IsGameLost = true;
        OnGameLost?.Invoke(null, System.EventArgs.Empty);
    }

    private void Awake()
    {
        CurrentType = Utils.GetRandomFruitType();
        NextType = Utils.GetRandomFruitType();
    }

    private void Start()
    {
        IsGameLost = false;
        RaiseNextTypeChange(NextType);
        ResetScore();
    }

    private void OnEnable()
    {
        PlayerTurnLeftAction.Enable();
        PlayerTurnRightAction.Enable();
        PlayerSpawnFruitAction.Enable();
    }

    private void OnDisable()
    {
        PlayerTurnLeftAction.Disable();
        PlayerTurnRightAction.Disable();
        PlayerSpawnFruitAction.Disable();
    }

    private void Update()
    {
        if (!IsGameLost)
        {
            if (PlayerTurnLeftAction.IsPressed())
            {
                RaisePlayerTurnLeft();
            }

            if (PlayerTurnRightAction.IsPressed())
            {
                RaisePlayerTurnRight();
            }

            if (PlayerSpawnFruitAction.WasPerformedThisFrame())
            {
                RaisePlayerSpawnFruit();
            }
        }
        else
        {
            if (Keyboard.current != null)
            {
                if (Keyboard.current.escapeKey.wasPressedThisFrame)
                {
                    Application.Quit();
                }
                else if (Keyboard.current.rKey.wasPressedThisFrame)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                }
            }
        }
    }
}
