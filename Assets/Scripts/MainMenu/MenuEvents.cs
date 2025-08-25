using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEvents : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider musicSlider;
    public Slider effectSlider;

    public Text volumeNumber;
    public Text musicNumber;
    public Text effectNumber;

    public AudioMixer mixer;
    
    void Start()
    {
        AudioManager.instance.Play("Backsound");

        InitSlider(volumeSlider, "volume", volumeNumber, "Volume");
        InitSlider(musicSlider, "music", musicNumber, "MusicVolume");
        InitSlider(effectSlider, "effects", effectNumber, "EffectVolume");
    }

    void InitSlider(Slider slider, string mixerParam, Text number, string prefKey)
    {
        float savedValue = PlayerPrefs.GetFloat(prefKey, 0f);
        mixer.SetFloat(mixerParam, savedValue);
        slider.value = savedValue;
        number.text = ConvertToPercentage(savedValue).ToString("0.#")+"%";

        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(slider, number, mixerParam, prefKey); });
    }

    public void OnSliderValueChanged(Slider slider, Text number, string mixerParam, string prefKey)
    {
        float value = slider.value;
        mixer.SetFloat(mixerParam, value);
        number.text = ConvertToPercentage(value).ToString("0.#") + "%";
        PlayerPrefs.SetFloat(prefKey, value);
    }

    private float ConvertToPercentage(float value)
    {
        // Convert range from [-80, 0] to [0, 100]
        return Mathf.Clamp((value + 80) / 80 * 100, 0, 100);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex); 
    }

}
