using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public GameObject optionsMenu;
    public GameObject collectionMenu;

    public void Start()
    {
        optionsMenu.SetActive(false);
        collectionMenu.SetActive(false);
    }

    public void Play()
    {
        //SceneManager.LoadScene("Game");
        Debug.Log("Play!");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
