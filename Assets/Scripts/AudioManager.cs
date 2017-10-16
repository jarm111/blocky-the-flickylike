using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
    }

    public float audioVolume = 0.5f;

    public List<AudioClip> audioClips = new List<AudioClip>();
    public List<AudioClip> musicClips = new List<AudioClip>();

    public AudioSource audiosourceSfx;
    public AudioSource audiosourceMusic;
    public AudioSource audiosourceSfxVaried;

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

    private void Start () {
        SetAudioVolume(audioVolume);
    }

    public void SetAudioVolume(float volume)
    {
        audioVolume = volume;
        audiosourceSfx.volume = volume;
        audiosourceMusic.volume = volume;
        audiosourceSfxVaried.volume = volume;
    }

    public void PlaySfx(int index)
    {
        audiosourceSfx.PlayOneShot(audioClips[index]);
    }

    public void PlayMusic(int index, float delay, bool loop)
    {
        audiosourceMusic.loop = loop;
        audiosourceMusic.clip = musicClips[index];
        audiosourceMusic.PlayDelayed(delay);
    }

    public void StopMusic()
    {
        audiosourceMusic.Stop();
    }

    public void PlaySfxVaried(int index)
    {
        StartCoroutine(PlaySfxVariedRoutine(index));
    }

    IEnumerator PlaySfxVariedRoutine(int index)
    {
        float normalVolume = audiosourceSfxVaried.volume;
        float normalPitch = audiosourceSfxVaried.pitch;
        float minVolumeModifier = 0.8f;
        float maxPitchModifier = 1.2f; // WebGL: only positive values for pitch are supported.
        audiosourceSfxVaried.volume = Random.Range(audiosourceSfxVaried.volume * minVolumeModifier, audiosourceSfxVaried.volume);
        audiosourceSfxVaried.pitch = Random.Range(audiosourceSfxVaried.pitch, audiosourceSfxVaried.pitch * maxPitchModifier);
        audiosourceSfxVaried.clip = audioClips[index];
        audiosourceSfxVaried.Play();

        while (audiosourceSfxVaried.isPlaying)
            yield return null;

        audiosourceSfxVaried.volume = normalVolume;
        audiosourceSfxVaried.pitch = normalPitch;
    }
}
