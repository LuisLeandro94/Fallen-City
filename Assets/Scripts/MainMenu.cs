using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;
using Photon.Pun;

public class MainMenu : MonoBehaviour
{
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    #region Private Constants
    const string playerNamePrefKey = "PlayerName";
    #endregion

    public void GoToNewGame()
    {
        SceneManager.LoadScene("CreateLobby");
    }

    public void GoToJoinGame()
    {
        SceneManager.LoadScene("JoinLobby");
    }

}
