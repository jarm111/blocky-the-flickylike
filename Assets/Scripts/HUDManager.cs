using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Text Lives;
    public Text Level;
    public Text CollectiblesRemaining;
    public GameObject GameOverPanel;
    public GameObject PauseMenuPanel;
    public Button Continue;
    public Button ReturnToMenu;

    private int currentLivesValue;
    private int currentCollectiblesRemainingValue;

    private void Start()
    {
        Level.text = "LEVEL " + GameStateManager.Instance.GetLevelNumber();

        Continue.onClick.AddListener(delegate
        {
            ContinueClick();
        });

        ReturnToMenu.onClick.AddListener(delegate
        {
            ReturnToMenuClick();
        });
    }

    private void Update()
    {
        //Lives.text = "LIVES " + GameStateManager.Instance.Lives;
        //CollectiblesRemaining.text = "REMAINING " + LevelStateManager.Instance.NumberOfCollectiblesRemaining;

        //UpdateLivesText();
        //UpdateCollectiblesRemainingText();

        UpdateText(ref currentLivesValue, GameStateManager.Instance.Lives, Lives, "LIVES ");
        UpdateText(ref currentCollectiblesRemainingValue, LevelStateManager.Instance.NumberOfCollectiblesRemaining, CollectiblesRemaining, "REMAINING ");

        if (LevelStateManager.Instance.IsGameOverState)
            GameOverPanel.SetActive(true);

        if (LevelStateManager.Instance.IsPausedState)
            PauseMenuPanel.SetActive(true);
        else
            PauseMenuPanel.SetActive(false);
    }

    //private void OnLivesChange()
    //{
    //    Lives.text = "LIVES " + GameStateManager.Instance.Lives;
    //}

    private void ContinueClick()
    {
        LevelStateManager.Instance.TogglePause();
    }

    private void ReturnToMenuClick()
    {
        LevelStateManager.Instance.TogglePause();
        GameStateManager.Instance.FirstScene();
    }

    //private void UpdateLivesText()
    //{
    //    if (currentLivesValue != GameStateManager.Instance.Lives)
    //    {
    //        currentLivesValue = GameStateManager.Instance.Lives;
    //        Lives.text = "LIVES " + currentLivesValue;
    //    }
    //}

    //private void UpdateCollectiblesRemainingText()
    //{
    //    if (currentCollectiblesRemainingValue != LevelStateManager.Instance.NumberOfCollectiblesRemaining)
    //    {
    //        currentCollectiblesRemainingValue = LevelStateManager.Instance.NumberOfCollectiblesRemaining;
    //        CollectiblesRemaining.text = "REMAINING " + currentCollectiblesRemainingValue;
    //    }
    //}

    private void UpdateText (ref int value, int newValue, Text textToUpdate, string label)
    {
        if (value != newValue)
        {
            value = newValue;
            textToUpdate.text = label + value;
            Debug.Log(label + value);
        }
    }
}