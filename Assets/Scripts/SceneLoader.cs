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
        int i = 0;

        while (true)
        {
            ++i;
        }
    }
}
