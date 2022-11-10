using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void GoToNewGame()
    {
        SceneManager.LoadScene("NewGame");
    }

    public void GoToJoinGame()
    {
        SceneManager.LoadScene("JoinGame");
    }
}
