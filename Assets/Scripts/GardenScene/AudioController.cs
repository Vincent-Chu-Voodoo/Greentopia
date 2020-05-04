using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [Header("Display")]
    public static bool isAudioEnabled = true;

    [Header("Config")]
    public GameObject audioEnabledGo;

    [Header("GameEvent")]
    public GameEvent OnMusicOff;
    public GameEvent OnMusicOn;

    private void Start()
    {
        UpdateAudio();
    }


    public void ToggleAudio()
    {
        isAudioEnabled = !isAudioEnabled;
        UpdateAudio();
        if (isAudioEnabled)
            OnMusicOn.Invoke(this);
        else
            OnMusicOff.Invoke(this);
    }

    private void UpdateAudio()
    {
        audioEnabledGo.SetActive(isAudioEnabled);
        AudioListener.volume = isAudioEnabled ? 1f : 0f;
    }
}
