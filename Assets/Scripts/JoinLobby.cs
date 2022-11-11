using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using WebSocketSharp;
using UnityEngine.SceneManagement;
using System.Linq;

[RequireComponent(typeof(TMP_InputField))]
public class JoinLobby : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields
    [SerializeField]
    private string _roomInput;
    #endregion

    #region Private Fields
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();
    string gameVersion = "1";
    string roomName = "";
    #endregion

    #region MonoBehaviour Callbacks
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    #endregion

    #region Public Methods
    public void GoToLobby()
    {
        SceneManager.LoadScene("NewGame");
    }
    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                cachedRoomList.Remove(info.Name);
            }
            else
            {
                cachedRoomList[info.Name] = info;
            }
        }
    }
    public void SetRoomName(string value)
    {
        roomName = value;
        Connect();
        bool isExist = CheckForRoom(roomName);
        if (isExist)
        {
            PhotonNetwork.JoinRoom(roomName);
        }
        else Debug.Log("Room " + roomName + " does not exist!");
    }
    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            JoinRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }
    public void JoinRoom()
    {
        if (!roomName.IsNullOrEmpty())
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }
    public bool CheckForRoom(string roomName)
    {
        bool isExist = false;
        var rooms = cachedRoomList.Values.ToArray();
        for (int i = 0; i < rooms.Length; i++)
        {
            if (rooms[i].Name == roomName)
                isExist = true;
        }
        return isExist;
    }
    #endregion

    #region MonoBehaviourPunCallbacks Callbacks
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateCachedRoomList(roomList);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log(PhotonNetwork.NickName);
        GoToLobby();
    }
    public override void OnConnectedToMaster()
    {
        JoinRoom();
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        cachedRoomList.Clear();
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }
    public override void OnLeftLobby()
    {
        cachedRoomList.Clear();
    }
    public override void OnJoinedLobby()
    {
        cachedRoomList.Clear();
    }
    #endregion
}
