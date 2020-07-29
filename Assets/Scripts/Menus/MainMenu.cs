using UnityEngine;
using UnityEngine.SceneManagement;

class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Main");
    }

    public void TrainingGround()
    {
        SceneManager.LoadScene("TrainingGround");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GitHub()
    {
        Application.OpenURL("https://github.com/Xwilarg/Isathos");
    }

    public void Credits()
    {
        Application.OpenURL("https://github.com/Xwilarg/Isathos/blob/master/CREDITS.md");
    }
}