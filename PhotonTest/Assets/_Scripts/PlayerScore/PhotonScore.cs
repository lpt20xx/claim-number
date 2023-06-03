
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PhotonScore : MonoBehaviour
{
    public PlayerScore playerScore;
    public PhotonPlayer photonPlayer;
    public UIPlayerScore playerScorePrefab;
    
    public List<PlayerScore> playerScorelist = new List<PlayerScore>();
    public List<UIPlayerScore> uiPlayerScorelist = new List<UIPlayerScore>();

    public Transform playerScoreContent;
    public void GetPlayerScore()
    {
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Count(); i++)
        {
            PlayerScore score = new PlayerScore();
            playerScore.playerName = photonPlayer.nickNameLable.text;
            playerScore.score = photonPlayer.numberCount;

            playerScorelist.Add(score);
        }

        this.LoadUIPlayerScore();
    }

    private void LoadUIPlayerScore()
    {
        this.ClearPlayerScoreUI();
        foreach (PlayerScore playerScore in this.playerScorelist)
        {
            UIPlayerScore uiPlayerScore = Instantiate(this.playerScorePrefab);
            uiPlayerScore.SetPlayerScore(playerScore);
            uiPlayerScore.transform.SetParent(this.playerScoreContent, false);
        }

            
    }

    private void ClearPlayerScoreUI()
    {
        foreach (Transform child in playerScoreContent)
        {
            Destroy(child.gameObject);
            Debug.Log("Destroy child");
        }
    }
}
