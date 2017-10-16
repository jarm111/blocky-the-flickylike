using UnityEngine;

public class LevelStateManager : MonoBehaviour
{
    private static LevelStateManager instance;
    public static LevelStateManager Instance
    {
        get
        {
            return instance;
        }
    }

    private int numberOfCollectiblesRemaining;
    public int NumberOfCollectiblesRemaining
    {
        get
        {
            return numberOfCollectiblesRemaining;
        }
    }

    private bool isGameOverState;
    public bool IsGameOverState
    {
        get
        {
            return isGameOverState;
        }
    }

    private bool isPausedState;
    public bool IsPausedState
    {
        get
        {
            return isPausedState;
        }
    }

    public GameObject LevelExit;

    private bool isRestartLevelState;
    private bool isExitLevelState;
    private LevelExit exitScript;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        exitScript = LevelExit.GetComponent<LevelExit>();
        numberOfCollectiblesRemaining = Collectible.CountOfCollectibles;
    }

    private void Update()
    {
        if (numberOfCollectiblesRemaining <= 0 && !exitScript.IsOpen)
        {
            OpenLevelExit();
        }
            

        if (exitScript.PlayerEntersExit && !isExitLevelState)
        {
            Debug.Log("Next level!");
            isExitLevelState = true;
            GameStateManager.Instance.NextScene();
        }

        if (!Player.Instance.IsAlive && !isRestartLevelState && !isGameOverState)
        {
            GameStateManager.Instance.ReduceLife();

            if (GameStateManager.Instance.Lives > 0)
                RestartLevel();
            else
                GameOver();
        }

        if (Input.GetButtonDown("Pause"))
            TogglePause();
    }

    public void SubtractCollectiblesRemaining()
    {
        numberOfCollectiblesRemaining--;
        Debug.Log("Collectibles remaining " + numberOfCollectiblesRemaining);
    }

    public void TogglePause()
    {
        if(!isPausedState)
            AudioManager.Instance.PlaySfx(6);

        Time.timeScale = (isPausedState) ? 1f : 0f;
        isPausedState = !isPausedState;
    }

    private void OpenLevelExit()
    {
        exitScript.OpenExit();
        AudioManager.Instance.PlaySfx(7);
    }

    private void GameOver()
    {
        isGameOverState = true;
        Debug.Log("Game Over! Lives remaining " + GameStateManager.Instance.Lives);
        GameStateManager.Instance.FirstSceneWithDelay(3f);
        AudioManager.Instance.PlayMusic(1, 0.5f, false);
    }

    private void RestartLevel()
    {
        isRestartLevelState = true;
        Debug.Log("Restarting level! Lives remaining " + GameStateManager.Instance.Lives);
        GameStateManager.Instance.RestartSceneWithDelay(3f);
    }
}
