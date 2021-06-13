using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : InputSubscriber
{
    [SerializeField]
    private Button _playBtn;
    protected override void Start()
    {
        base.Start();
        if (_playBtn)
        {
            _playBtn.onClick.AddListener(() => PauseMenuUIHandler());
        }
    }
    [SerializeField] GameObject PauseMenuUI;
    #region Pause Stuff
    protected override void OnPauseButtonPressed(bool v)
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
    #endregion

}