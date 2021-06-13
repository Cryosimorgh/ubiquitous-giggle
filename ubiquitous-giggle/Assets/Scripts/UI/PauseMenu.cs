using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : InputSubscriber
{
    [SerializeField] GameObject PauseMenuUI;
    protected override void PauseTheGame(bool v)
    {
        if (PauseMenuUI)
        {
            PauseMenuUIHandler();
        }
    }
    private void PauseMenuUIHandler()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            PauseMenuUI.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            PauseMenuUI.SetActive(true);
        }
    }
}