using UnityEngine;
using UnityEngine.UI;

public class UIPhotonRoom : MonoBehaviour
{

    public Button createRoom;
    public Button joinRoom;
    public Button startGame;
    public Button leaveRoom;

    public GameObject startGameButton;


    public GameObject roomCAndJ;

    public GameObject roomListUI;




    public void DisableLeaveButton()
    {
        this.leaveRoom.interactable = false;
    }

    public void EnableLeaveButton()
    {
        this.leaveRoom.interactable = true;
    }

    public void DisableRoomButton()
    {
        this.createRoom.interactable = false;
        this.joinRoom.interactable = false;
    }

    public void EnableRoomButton()
    {
        this.createRoom.interactable = true;
        this.joinRoom.interactable = true;
    }

    public void DisableRoomCAndJ()
    {
        this.roomCAndJ.SetActive(false);
    }

    public void EnableRoomCAndJ()
    {
        this.roomCAndJ.SetActive(true);
    }

    public void DisableRoomListUI()
    {
        this.roomListUI.SetActive(false);
    }

    public void EnableRoomListUI()
    {
        this.roomListUI.SetActive(true);
    }

    public void DisableStartButton()
    {
        this.startGame.interactable = false;
    }

    public void EnableStartButton()
    {
        this.startGame.interactable = true;
    }
}
