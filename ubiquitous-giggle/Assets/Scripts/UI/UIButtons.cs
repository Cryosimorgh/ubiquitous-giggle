using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtons : MonoBehaviour
{
    [SerializeField] private GameObject missionSelect, mSBack, options, opBack;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {

        SceneManager.LoadScene(0);
    }

    public void Next_Level()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void Select_Mission_Select()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mSBack);
    }
    public void Select_Back_missionselect()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(missionSelect);
    }
    public void Select_Options()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(opBack);
    }
    public void Select_Back_options()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(options);
    }
    public void Tutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void Mission1()
    {
        SceneManager.LoadScene(2);
    }
    public void Mission2()
    {
        SceneManager.LoadScene(3);
    }
    public void Mission3()
    {
        SceneManager.LoadScene(4);
    }
    public void Credits()
    {
        SceneManager.LoadScene(5);
    }
}
