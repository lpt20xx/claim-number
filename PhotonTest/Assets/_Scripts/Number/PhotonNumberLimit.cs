using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhotonNumberLimit : MonoBehaviour, IPunObservable
{
    public static PhotonNumberLimit instance;

    public TextMeshPro textNumber;
    public int number = 0;

    protected void Awake()
    {
        this.CheckInstance();
    }

    private void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected void OnEnable()
    {
        this.Set(0);
    }

    public void Set(int number)
    {
        this.number = number;
        this.textNumber.text = number.ToString();
    }

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
        this.number = (int)stream.ReceiveNext();
        this.textNumber.text = this.number.ToString();
        //stream.SendNext(this.number);
    }

    public bool CanClaim(int number)
    {
        return number == this.number;
    }
}
