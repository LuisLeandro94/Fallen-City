using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Network : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private void Start()
    {
        Connect();
    }

    private void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom(
            "GameRoom",
            new Photon.Realtime.RoomOptions() { MaxPlayers = 15 }, null);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("Player", new Vector3(Random.RandomRange(-10, 10), Random.RandomRange(-10, 10), 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
