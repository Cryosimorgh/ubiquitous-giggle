using System;
using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;
using UnityEngine.UI;

public class PauseMenu : InputSubscriber
{
    [SerializeField] private Button resumeGUI;
    [SerializeField] private Button optionsGUI;
    [SerializeField] private Button returnoptionsGUI;
    [SerializeField] private Button mainMenuGUI;
    [SerializeField] GameObject PauseMenuParent;
    [SerializeField] GameObject OptionsMenuParent;
    protected override void Start()
    {
        base.Start();
        if (resumeGUI)
        {
            resumeGUI.onClick.AddListener(() => PauseMenuUIHandler());
            optionsGUI.onClick.AddListener(() => OptionsMenuUIHandler());
            returnoptionsGUI.onClick.AddListener(() => OptionsMenuUIHandler());
            mainMenuGUI.onClick.AddListener(() => MainMenuMenuUIHandler());
        }
    }

    private void MainMenuMenuUIHandler()
    {
        LoadScene("MainMenu");
    }

    private void OptionsMenuUIHandler()
    {
        OptionsMenuParent.SetActive(!OptionsMenuParent.activeInHierarchy);
        PauseMenuParent.SetActive(!PauseMenuParent.activeInHierarchy);
    }
    #region Pause Stuff
    protected override void OnPauseButtonPressed(bool v)
    {
        if (PauseMenuParent)
        {
            PauseMenuUIHandler();
        }
    }
    private void PauseMenuUIHandler()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            PauseMenuParent.SetActive(false);
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        else
        {
            Time.timeScale = 0;
            PauseMenuParent.SetActive(true);
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
    #endregion

}