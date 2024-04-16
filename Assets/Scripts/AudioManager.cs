using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer soundMixer;
    public Slider musicSlider;
    public Slider soundSlider;

    private void Start()
    {
        if(musicSlider != null)
        {
            musicSlider.value = 1f;
            SetVolumeMusic(musicSlider.value); 
        }
        if (soundSlider != null)
        {
            soundSlider.value = 1f;
            SetVolumeSound(soundSlider.value);
        }
    }
    public void SetVolumeMusic(float volume)
    {
        musicMixer.SetFloat("MusicVol", volume);
    }

    public void SetVolumeSound(float volume)
    {
        soundMixer.SetFloat("SoundVol", volume);
    }
}
