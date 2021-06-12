using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Button _playBtn;

    [SerializeField]
    private Button _quitBtn;

    // Start is called before the first frame update
    void Start()
    {
        if (_playBtn)
        {
            _playBtn.onClick.AddListener(() => this.OnPlayGame());
        }

        if (_quitBtn)
        {
            _quitBtn.onClick.AddListener(() => this.OnQuitGame());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPlayGame()
    {
        LoadScene("level-0");
    }

    private void OnQuitGame()
    {
        Application.Quit();
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
