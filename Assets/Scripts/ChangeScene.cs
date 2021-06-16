using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        FindObjectOfType<AudioManager>().Stop("Menu");
        FindObjectOfType<AudioManager>().Play("Shop");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
