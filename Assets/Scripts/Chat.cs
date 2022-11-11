using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class Chat : MonoBehaviour
{
    bool isChatting = false;
    string chatInput = "";

    [System.Serializable]
    public class ChatMessage
    {
        public string sender = "";
        public string message = "";
        public float timer = 0;
    }

    List<ChatMessage> chatMessages = new List<ChatMessage>();
    PhotonView photonView;

    void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        //Initialize Photon View
        if (gameObject.GetComponent<PhotonView>() == null)
        {
            photonView.ViewID = 1;
        }
    }

    void Update()
    {
        if (!isChatting)
        {
            GUI.Label(new Rect(5, Screen.height - 25, 200, 25), "Press 'T' to chat");
        }    
        else
        {
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
            {
                isChatting = false;
                if(chatInput.Replace(" ","") != "")
                {
                    //Send message
                    photonView.RPC("SendChat", RpcTarget.All, PhotonNetwork.LocalPlayer, chatInput);
                }
                chatInput = "";
            }

            GUI.SetNextControlName("ChatField");
            GUI.Label(new Rect(5, Screen.height - 25, 200, 25), "Say:");
            GUIStyle inputStyle = GUI.skin.GetStyle("box");
            inputStyle.alignment = TextAnchor.MiddleLeft;
            chatInput = GUI.TextField(new Rect(10 + 25, Screen.height - 27, 400, 22), chatInput, 60, inputStyle);

            GUI.FocusControl("ChatField");
        }

        //Show messages
        for(int i = 0; i < chatMessages.Count; i++)
        {
            if (chatMessages[i].timer > 0 || isChatting)
            {
                GUI.Label(new Rect(5, Screen.height - 50 - 25 * i, 500, 25), chatMessages[i].sender + ": " + chatMessages[i].message);
            }
        }
    }

    [PunRPC]
    void SendChat(Player sender, string message)
    {
        ChatMessage m = new ChatMessage();
        m.sender = sender.NickName;
        m.message = message;
        m.timer = 15.0f;

        chatMessages.Insert(0, m);
        if (chatMessages.Count > 8)
        {
            chatMessages.RemoveAt(chatMessages.Count - 1);
        }
    }
}
