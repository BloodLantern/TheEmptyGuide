using Com.EliottTan.SceneTransitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button play, setting, quit, credit;
    [SerializeField] GameObject soundSettingPrefab;

    // Start is called before the first frame update
    private void Awake()
    {
        play.onClick.AddListener(PlayButton);
        setting.onClick.AddListener(SettingButton);
    }

    void PlayButton()
    {
        TransitionManager.ChangeScene(1);
    }

    void SettingButton()
    {
        Instantiate(soundSettingPrefab);
    }
}
