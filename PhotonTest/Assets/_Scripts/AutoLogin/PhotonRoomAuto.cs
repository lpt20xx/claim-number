using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonRoomAuto : MonoBehaviourPunCallbacks
{
    public string nickName = "LocalP";
    public string roomName = "LocalR";

    private void Awake()
    {
        this.AutoLogin();
    }

    protected void AutoLogin()
    {
        if (PhotonNetwork.NetworkClientState == ClientState.Joined) return;
        PhotonNetwork.LocalPlayer.NickName = this.nickName;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(transform.name + ": ConnectedToMaster");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("JoinedLobby");
        PhotonNetwork.CreateRoom(this.roomName);
    }
}
