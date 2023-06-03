using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;

public class PhotonRoomMembers : MonoBehaviourPunCallbacks
{
    
    public List<PlayerProfile> players = new List<PlayerProfile>();
    public List<UIPlayerProfile> members = new List<UIPlayerProfile>();
    //public UIPlayerProfile playerPrefab;
    public Transform playerContent;

    public GameObject playerPrefab;
    private void Start()
    {
        this.ClearPlayerProfileUI();
    }

    /*private void LoadUIPlayers()
    {
        this.ClearPlayerProfileUI();
        foreach (PlayerProfile player in this.players)
        {
            UIPlayerProfile uiPlayerProfile = Instantiate(this.playerPrefab);
            uiPlayerProfile.SetPlayerProfile(player);
            uiPlayerProfile.transform.SetParent(this.playerContent, false);
        }
    }*/

    private void ClearPlayerProfileUI()
    {
        foreach (Transform child in playerContent)
        {
            Destroy(child.gameObject);
            Debug.Log("Destroy child");
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(transform.name + ": OnJoinedRoom");
        /*string playerName = PhotonLogin.playerName;
        this.AddPlayerToList(playerName);
        Invoke("LoadUIPlayers", 1f);*/

        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(playerPrefab, playerContent).GetComponent<UIPlayerProfile>().AddPlayerToList(players[i]);
        }
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(transform.name + ": PlayerEnteredRoom: " + newPlayer);
        /*string playerName = newPlayer.NickName;
        this.AddPlayerToList(playerName);
        Invoke("LoadUIPlayers", 1f);*/

        Instantiate(playerPrefab, playerContent).GetComponent<UIPlayerProfile>().AddPlayerToList(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(transform.name + ": PlayerLeftRoom: " + otherPlayer);
        /*this.RemovePlayerFromList(otherPlayer.NickName);
        Invoke("LoadUIPlayers", 1f);*/
    }

    /*private void AddPlayerToList(string name)
    {
        PlayerProfile playerProfile = new PlayerProfile
        {
            nickName = name
        };
        this.players.Add(playerProfile);
    }

    private void RemovePlayerFromList(string name)
    {
        PlayerProfile playerProfile = new PlayerProfile
        {
            nickName = name
        };
        this.players.Remove(playerProfile);
    }*/

    
}
