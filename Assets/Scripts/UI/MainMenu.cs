using Com.EliottTan.SceneTransitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button play, setting, quit, credit;
    [SerializeField] GameObject soundSettingPrefab;

    [SerializeField] int playScene, creditScene;
    [SerializeField] Transition creditTransition;

    // Start is called before the first frame update
    private void Awake()
    {
        play.onClick.AddListener(PlayButton);
        setting.onClick.AddListener(SettingButton);
        credit.onClick.AddListener(CreditButton);
    }

    void PlayButton()
    {
        TransitionManager.ChangeScene(playScene);
        
    }

    void CreditButton()
    {
        TransitionManager.ChangeScene(creditTransition,creditScene);
    }

    void SettingButton()
    {
        Instantiate(soundSettingPrefab);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
