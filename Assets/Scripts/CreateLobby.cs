using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using WebSocketSharp;
using UnityEngine.SceneManagement;
using System.Linq;

[RequireComponent(typeof(TMP_InputField))]
public class CreateLobby : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 15;
    [SerializeField]
    private string _roomInput;
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
    #endregion

    #region Public Methods
    public void SetRoomName(string value) {
        _roomInput = value;
        Connect();
    }
    public void GoToLobby()
    {
        SceneManager.LoadScene("NewGame");
    }
    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            CreateRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }
    public void CreateRoom()
    {
        if (!_roomInput.IsNullOrEmpty())
        {
            PhotonNetwork.CreateRoom(_roomInput, new RoomOptions() { MaxPlayers = maxPlayersPerRoom }, null);
        }
    }
    #endregion

    #region MonoBehaviourPunCallbacks Callbacks
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log(PhotonNetwork.NickName);
        GoToLobby();
    }
    public override void OnConnectedToMaster()
    {
        CreateRoom();
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }
    #endregion
}

