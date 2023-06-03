using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PhotonNumber : MonoBehaviourPun, IPunObservable
{
    public TextMeshPro textNumber;
    public int number = 0;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("OnPhotonSerializeView");
        if (stream.IsWriting) this.StreamWriting(stream);
        else this.StreamReading(stream, info);
    }

    private void StreamWriting(PhotonStream stream)
    {
        Debug.Log("StreamWriting");
        stream.SendNext(this.number);
    }

    private void StreamReading(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("Reading");
        this.number = (int) stream.ReceiveNext();
        this.textNumber.text = this.number.ToString();
        //stream.SendNext(this.number);
    }

    public void Set(int number)
    {
        this.number = number;
        this.textNumber.text = number.ToString();
    }

    public void OnClaim()
    {
        Debug.Log(transform.name + " OnClaim: " + this.number);

        if (!PhotonNumberLimit.instance.CanClaim(this.number)) return;

        object[] datas = new object[] { this.number };

        //All players receive event
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };

        PhotonPlayer.me.numberCount++;

        PhotonNetwork.RaiseEvent(
            ((byte)EventCode.onNumberClaimed),
            datas,
            raiseEventOptions,
            SendOptions.SendUnreliable
            );
    }

    internal int Claimed()
    {
        Debug.Log("Claimed: " + this.number);
        gameObject.SetActive(false);

        return this.number;
    }
}
