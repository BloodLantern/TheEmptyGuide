using Com.EliottTan.SceneTransitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Button quitBtn, resumeBtn;

    public bool gamePaused { get; private set; } = false;

    PlayerActions inputs;
    string pauseKey = "Pause";

    private void Awake()
    {
        quitBtn.onClick.AddListener(QuitGame);
        resumeBtn.onClick.AddListener(TogglePause);
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
        gamePaused = !gamePaused;
        if (gamePaused) Time.timeScale = 0f;
        else Time.timeScale = 1f;

        pauseMenu.SetActive(gamePaused);
    }

    void QuitGame()
    {
        if (gamePaused)
        {
            TogglePause();
            TransitionManager.ChangeScene(0);
        }
        
    }

}
