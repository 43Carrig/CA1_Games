using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Starting the game. Changing the scene from current scene to next scene. 
    public void PlayGame()
    {
        // Changing the main game scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        // This will be to change the application
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
