using Com.EliottTan.SceneTransitions;
using FMODUnity;
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

    [SerializeField] EventReference playSfx, settingSfx, creditSfx, quitSfx;


    // Start is called before the first frame update
    private void Awake()
    {
        play.onClick.AddListener(PlayButton);
        setting.onClick.AddListener(SettingButton);
        credit.onClick.AddListener(CreditButton);
        quit.onClick.AddListener(QuitGame);
    }

    void PlayButton()
    {
        SoundManager.Instance.PlaySFX(playSfx,transform.position);
        TransitionManager.ChangeScene(playScene);
    }

    void CreditButton()
    {
        SoundManager.Instance.PlaySFX(creditSfx, transform.position);
        TransitionManager.ChangeScene(creditTransition,creditScene);
    }

    void SettingButton()
    {
        SoundManager.Instance.PlaySFX(settingSfx, transform.position);
        Instantiate(soundSettingPrefab);
    }

    void QuitGame()
    {
        SoundManager.Instance.PlaySFX(quitSfx, transform.position);
        Application.Quit();
    }

    private void OnDestroy()
    {
        play.onClick.RemoveAllListeners();
        credit.onClick.RemoveAllListeners();
        setting.onClick.RemoveAllListeners();
        quit.onClick.RemoveAllListeners();
    }
}
