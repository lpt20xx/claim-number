using TMPro;
using UnityEngine;

public class UIRoomProfile : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI roomName;
    [SerializeField] protected RoomProfile roomProfile;
    public void SetRoomProfile(RoomProfile roomProfile)
    {
        this.roomProfile = roomProfile;
        this.roomName.text = roomProfile.name;
    }

    public void OnClick()
    {
        Debug.Log("OnClick: " + this.roomProfile.name);
        PhotonRoom.instance.inputRoomName.text = this.roomProfile.name;
    }
}
