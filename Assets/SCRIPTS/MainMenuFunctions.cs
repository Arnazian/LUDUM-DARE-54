using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour
{
    public void OpenGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenCocoLink()
    {
        Application.OpenURL("https://arnazian.itch.io/");
    }
    public void OpenUmbraink()
    {
        Application.OpenURL("https://github.com/Umbrason");
    }
    public void OpenDinoLink()
    {
        Application.OpenURL("https://itch.io/profile/kyoryuzain");
    }
    public void OpenHastyLink()
    {
        Application.OpenURL("https://hasty46.itch.io/");
    }

}
