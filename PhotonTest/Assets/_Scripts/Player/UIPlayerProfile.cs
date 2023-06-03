using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class UIPlayerProfile : MonoBehaviourPunCallbacks
{
    /*[SerializeField] protected TextMeshProUGUI playerName;
    [SerializeField] protected PlayerProfile playerProfile;
    public void SetPlayerProfile(PlayerProfile playerProfile)
    {
        this.playerProfile = playerProfile;
        this.playerName.text = playerProfile.nickName;
    }*/

    [SerializeField] TMP_Text playerName;

    Player player;

    public void AddPlayerToList(Player _player)
    {
        player = _player;
        playerName.text = _player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(player == otherPlayer) 
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
