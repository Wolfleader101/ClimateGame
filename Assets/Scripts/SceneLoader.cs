using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{   
    public void StartGame()
    {
        SceneManager.LoadScene("OutdoorScene");
    }

    public void LoadOptions()
    {
        // do nothing
    }

    public void ExitGame()
    {
       Application.Quit();
    }
}
