using UnityEngine;
using UnityEngine.UI;

public class EndingUIManager : MonoBehaviour
{

    public Button CloseEndingScreen;

    private void Start()
    {
        CloseEndingScreen.onClick.AddListener(delegate
        {
            CloseEndingScreenClick();
        });

        AudioManager.Instance.PlayMusic(0, 0, true);
    }

    private void CloseEndingScreenClick()
    {
        AudioManager.Instance.StopMusic();
        GameStateManager.Instance.FirstScene();
    }
}