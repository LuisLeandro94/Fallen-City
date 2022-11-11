using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 15;
    [SerializeField]
    private TMP_InputField _roomInput;
    #endregion

    #region Private Fields

    string gameVersion = "1";
    const string playerNamePrefKey = "PlayerName";

    #endregion

    #region MonoBehaviour Callbacks

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        Connect();
    }
    #endregion

    #region Public Methods

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }
    #endregion

    #region MonoBehaviourPunCallbacks Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room:" + PhotonNetwork.CurrentRoom.Name);
        string defaultName = PlayerPrefs.GetString(playerNamePrefKey);
        Debug.Log(defaultName);
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(_roomInput.text, new RoomOptions() { MaxPlayers= maxPlayersPerRoom });
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }
    #endregion
}
