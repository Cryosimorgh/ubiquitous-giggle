using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Button _playBtn;

    [SerializeField]
    private Button _quitBtn;

    [SerializeField]
    private Button _options;

    [SerializeField]
    private Button _return;

    [SerializeField]
    private GameObject optionsMenu;

    [SerializeField]
    private GameObject mainMenu;
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

        if (_options)
        {
            _options.onClick.AddListener(() => this.OptionsMenuActivationHandler());
        }

        if (_return)
        {
            _return.onClick.AddListener(() => this.OptionsMenuActivationHandler());
        }
    }

    private void OptionsMenuActivationHandler()
    {
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
        mainMenu.SetActive(!optionsMenu.activeInHierarchy);
    }

    private void OnPlayGame()
    {
        LoadScene(1);
    }

    private void OnQuitGame()
    {
        Application.Quit();
    }

    private void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
