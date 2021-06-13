using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;
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
    private Button _Optionsreturn;

    [SerializeField]
    private Button _Creditsreturn;

    [SerializeField]
    private Button _credits;

    [SerializeField]
    private GameObject optionsMenu;

    [SerializeField]
    private GameObject creditsMenu;

    [SerializeField]
    private GameObject mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        if (_playBtn)
        {
            _playBtn.onClick.AddListener(() => OnPlayGame());
        }

        if (_quitBtn)
        {
            _quitBtn.onClick.AddListener(() => OnQuitGame());
        }

        if (_options)
        {
            _options.onClick.AddListener(() => OptionsMenuActivationHandler());
        }
        if (_credits)
        {
            _credits.onClick.AddListener(() => CreditsMenuActivationHandler());
        }

        if (_Optionsreturn)
        {
            _Optionsreturn.onClick.AddListener(() => OptionsMenuActivationHandler());
        }

        if (_Creditsreturn)
        {
            _Creditsreturn.onClick.AddListener(() => CreditsMenuActivationHandler());
        }
    }

    private void OptionsMenuActivationHandler()
    {
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
        mainMenu.SetActive(!mainMenu.activeInHierarchy);
    }
    private void CreditsMenuActivationHandler()
    {
        creditsMenu.SetActive(!creditsMenu.activeInHierarchy);
        mainMenu.SetActive(!mainMenu.activeInHierarchy);
    }

    private void OnPlayGame()
    {
        LoadScene(GetActiveScene().buildIndex + 1);
    }

    private void OnQuitGame()
    {
        Application.Quit();
    }
}