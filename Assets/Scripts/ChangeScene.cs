using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            FindObjectOfType<AudioManager>().Stop("Menu");
            FindObjectOfType<AudioManager>().Play("Shop");
        }
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            FindObjectOfType<AudioManager>().Stop("Shop");
            FindObjectOfType<AudioManager>().Play("Credits");
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            
            FindObjectOfType<AudioManager>().Stop("Credits");
            FindObjectOfType<AudioManager>().Play("Menu");
            SceneManager.LoadScene(0);
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //Doesnt Work For whatever Reason

        /*
        if ((SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1)).IsValid())
        {
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (!((SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1)).IsValid()))
        {
           Debug.Log("The Next Scene doesn't Exist!");
        }*/
        
    }

    public void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            FindObjectOfType<CustomerManager>().EnterNewCustomer();
        }
    }
}
