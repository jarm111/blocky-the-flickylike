using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager instance;
    public static GameStateManager Instance
    {
        get
        {
            return instance;
        }
    }

    private int lives;
    public int Lives
    {
        get { return lives; }
    }

    public int StartingLives = 3;

    //public event Action LivesChange;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    private void Start()
    {
        lives = StartingLives;
    }

    public void RestartSceneWithDelay(float delay)
    {
        Invoke("RestartScene", delay);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FirstScene()
    {
        SceneManager.LoadScene(0);
        ResetLives();
    }

    public void FirstSceneWithDelay(float delay)
    {
        Invoke("FirstScene", delay);
    }

    public void ReduceLife()
    {
        lives--;
        //LivesChange();
    }

    public int GetLevelNumber()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetLives()
    {
        lives = StartingLives;
    }
}
