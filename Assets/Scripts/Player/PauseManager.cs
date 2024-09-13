using Com.EliottTan.SceneTransitions;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Button quitBtn, resumeBtn,settingBtn;

    [SerializeField] GameObject soundSettings;

    public bool gamePaused { get; private set; } = false;

    PlayerActions inputs;
    string pauseKey = "Pause";

    [SerializeField] EventReference toggleSfx, settingSfx, quitSfx;

    private void Awake()
    {
        quitBtn.onClick.AddListener(QuitGame);
        resumeBtn.onClick.AddListener(TogglePause);
        settingBtn.onClick.AddListener(OpenSettings);
    }

    void Start()
    {
        inputs = new PlayerActions();
        inputs.Enable();
        pauseMenu.SetActive(gamePaused);
    }

    private void Update()
    {
        if (inputs.asset[pauseKey].WasPressedThisFrame())
        {
            TogglePause();
        }
        
    }

    void TogglePause()
    {
        SoundManager.Instance.PlaySFX(toggleSfx, transform.position);
        gamePaused = !gamePaused;
        if (gamePaused) Time.timeScale = 0f;
        else Time.timeScale = 1f;

        pauseMenu.SetActive(gamePaused);
    }

    void OpenSettings()
    {
        SoundManager.Instance.PlaySFX(settingSfx,transform.position);
        Instantiate(soundSettings);
    }

    void QuitGame()
    {
        if (gamePaused)
        {
            SoundManager.Instance.PlaySFX(quitSfx, transform.position);
            TogglePause();
            TransitionManager.ChangeScene(0);
        }
        
    }

}
