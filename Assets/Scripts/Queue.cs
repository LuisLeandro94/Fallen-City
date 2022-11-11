using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon;
using Photon.Pun;
using System.Linq;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
public class Queue : MonoBehaviourPunCallbacks
{
    #region Private Fields
    const string playerNamePrefKey = "PlayerName";
    #endregion

    #region MonoBehaviourCallbacks
    void Start()
    {
        PhotonNetwork.NickName = "Player" + (int)Random.Range(0, 100);
        for (int i = 0; i < PhotonNetwork.CurrentRoom.Players.Count; i++)
        {
            Debug.LogFormat("Player {0}: {1}", i, PhotonNetwork.CurrentRoom.Players.ElementAt(i));
        }
    }
    #endregion

    #region Public Methods
    public void ReturnToMainMenu()
    {
        LeaveRoom();
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = value;
    }
    #endregion

    #region MonoBehaviourPunCallbacks
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
}
