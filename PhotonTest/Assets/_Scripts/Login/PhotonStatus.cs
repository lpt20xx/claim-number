using UnityEngine;
using TMPro;
using Photon.Pun;

public class PhotonStatus : MonoBehaviour
{
    public string photonStatus;
    public TextMeshProUGUI textStatus;


    // Update is called once per frame
    private void Update()
    {
        this.photonStatus = PhotonNetwork.NetworkClientState.ToString();
        this.textStatus.text = this.photonStatus;
    }
}
