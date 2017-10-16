using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    public Button Play;
    public Button Instructions;
    public Button CloseInstructions;
    public GameObject InstructionsPanel;
    public Slider audioVolume;

    private void Start()
    {
        Play.onClick.AddListener(delegate
        {
            PlayClick();
        });

        Instructions.onClick.AddListener(delegate
        {
            InstructionsClick();
        });

        CloseInstructions.onClick.AddListener(delegate
        {
            CloseInstructionsClick();
        });

        audioVolume.onValueChanged.AddListener(delegate
        {
            AudioVolumeChange();
        });

        audioVolume.value = AudioManager.Instance.audioVolume; 
    }

    private void CloseInstructionsClick()
    {
        InstructionsPanel.SetActive(false);
    }

    private void InstructionsClick()
    {
        InstructionsPanel.SetActive(true);
    }

    private void PlayClick()
    {
        AudioManager.Instance.PlaySfx(5);
        GameStateManager.Instance.NextScene();
    }

    private void AudioVolumeChange()
    {
        AudioManager.Instance.SetAudioVolume(audioVolume.value);
    }
}
