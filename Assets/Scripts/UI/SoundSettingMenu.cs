using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingMenu : MonoBehaviour
{
    [SerializeField] Button backBtn;
    [SerializeField] Slider masterSlider, sfxSlider, musicSlider;

    [SerializeField] EventReference backSound;
    void Awake()
    {
        backBtn.onClick.AddListener(CloseScreen);
    }

    private void Start()
    {
        masterSlider.value = SoundManager.Instance.GetVolume(SoundManager.volType.master);
        musicSlider.value = SoundManager.Instance.GetVolume(SoundManager.volType.music);
        sfxSlider.value = SoundManager.Instance.GetVolume(SoundManager.volType.sfx);
        print(SoundManager.Instance.GetVolume(SoundManager.volType.master));
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void CloseScreen() {
        SoundManager.Instance.PlaySFX(backSound, transform.position);
        Destroy(gameObject);
    }

    void SetMasterVolume(float pValue) {
        SoundManager.Instance.SetVolume(SoundManager.volType.master, pValue);
    }

    void SetMusicVolume(float pValue) {
        SoundManager.Instance.SetVolume(SoundManager.volType.music, pValue);
    }

    void SetSFXVolume(float pValue) {
        SoundManager.Instance.SetVolume(SoundManager.volType.sfx, pValue);
    }

    private void OnDestroy()
    {
        backBtn.onClick.RemoveAllListeners();
        masterSlider.onValueChanged.RemoveAllListeners();
        musicSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();
    }

}
