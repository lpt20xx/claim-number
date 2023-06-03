using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using System.Collections.Generic;
using Photon.Realtime;
using System.Collections;
using System.Linq;

public class PhotonRoom : MonoBehaviourPunCallbacks
{
    public static PhotonRoom instance { get; private set; }

    public PhotonLogin photonLogin;
    public PhotonLogout photonLogout;

    public TMP_InputField inputRoomName;

    //room list
    [Header("Room List")]
    public List<RoomInfo> updatedRooms;
    public List<RoomProfile> rooms = new List<RoomProfile>();

    public Transform roomContent;
    public UIRoomProfile roomPrefab;

    public UIPhotonRoom uiPhotonRoom;
    public UIPlayerList uiPlayerList;

    //start game
    [Header("Start Game")]
    public bool gameStarted = false;

    //warning
    [Header("Warning")]
    public GameObject warning;
    public Text warningText;



    private readonly string roomWarning = "Please enter room name";
    private readonly string startWarning = "You are not Master Client";
    private void Awake()
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

    private void Start()
    {
        //this.SetRoomName();
        this.DisableUIPhotonRoom();
        this.DisablePlayerList();
        this.DisableStartGameButton();
    }

    private void SetRoomName()
    {
        inputRoomName.text = "Room1";
    }


   
    public void Create()
    {
        string name = inputRoomName.text;
        Debug.Log(transform.name + ": Create Room " + name);

        if (name == "")
        {
            warning.SetActive(true);
            warningText.text = roomWarning;
            StartCoroutine(DisableWarningDelay());
            return;
        }

        PhotonNetwork.CreateRoom(name);

        
    }

    public void Join()
    {
        string name = inputRoomName.text;
        Debug.Log(transform.name + ": Joined Room " + name);

        if (name == "")
        {
            warning.SetActive(true);
            warningText.text = roomWarning;
            StartCoroutine(DisableWarningDelay());
            return;
        }

        PhotonNetwork.JoinRoom(name);

        
    }

    public void StartGame()
    {
        //PhotonNetwork.LoadLevel("2_StartGame");
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log(transform.name + " : Start Game");
            PhotonNetwork.LoadLevel("2_StartGame");
            gameStarted = true;
        }
        else
        {
            warning.SetActive(true);
            warningText.text = startWarning;
            Debug.LogWarning(startWarning);
            StartCoroutine(DisableWarningDelay());
        }


    }

    IEnumerator DisableWarningDelay()
    {
        yield return new WaitForSeconds(3f);
        warning.SetActive(false);
    }


    public void JoinAfterGameStarted()
    {
        PhotonNetwork.LoadLevel("2_StartGame");
    }

    public void Leave()
    {
        Debug.Log("Leave Room");
        PhotonNetwork.LeaveRoom();

    }

    

    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");

        photonLogin.DisableLogInOutButton();

        this.DisableRoomUI();

        this.EnableStartLeaveButton();


        this.EnablePlayerList();

        this.DisableTextRoomName();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");

        photonLogin.DisableLogInOutButton();

        this.ClearRoomProfileUI();

        this.DisableRoomUI();

        this.EnableStartLeaveButton();

        this.EnablePlayerList();

        this.DisableTextRoomName();

        if (gameStarted)    
        {
            JoinAfterGameStarted();
        }
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left Room");

        this.EnableRoomUI();
        this.DisableStartLeaveButton();

        this.DisablePlayerList();

        photonLogout.EnableLogoutButton();

        inputRoomName.interactable = true;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning("Create Room Failed: " + message);

        warning.SetActive(true);
        warningText.text = message;
        StartCoroutine(DisableWarningDelay());
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning("Join Room Failed: " + message);

        warning.SetActive(true);
        warningText.text = message;
        StartCoroutine(DisableWarningDelay());
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Room List Updated");
        this.updatedRooms = roomList;

        foreach (RoomInfo roomInfo in roomList)
        {
            if (roomInfo.RemovedFromList) this.RoomRemove(roomInfo);
            else this.RoomAdd(roomInfo);
        }

        this.UpdateRoomProfileUI();
    }

    protected void UpdateRoomProfileUI()
    {
        this.ClearRoomProfileUI();

        foreach (RoomProfile roomProfile in this.rooms)
        {
            UIRoomProfile uiRoomProfile = Instantiate(this.roomPrefab);
            uiRoomProfile.SetRoomProfile(roomProfile);
            uiRoomProfile.transform.SetParent(this.roomContent, false);
        }
    }
    protected void ClearRoomProfileUI()
    {
        foreach (Transform child in this.roomContent)
        {
            Destroy(child.gameObject);
        }

    }
    protected void RoomAdd(RoomInfo roomInfo)
    {
        RoomProfile roomProfile;
        roomProfile = this.RoomByName(roomInfo.Name);
        if (roomProfile != null)
        {
            Debug.LogWarning("This room name has existed");
            return;
        }

        roomProfile = new RoomProfile
        {
            name = roomInfo.Name
        };
        this.rooms.Add(roomProfile);
    }

    protected void RoomRemove(RoomInfo roomInfo)
    {
        RoomProfile roomProfile = this.RoomByName(roomInfo.Name);
        if (roomProfile == null) return;
        this.rooms.Remove(roomProfile);
    }

    protected RoomProfile RoomByName(string name)
    {
        foreach (RoomProfile roomProfile in this.rooms)
        {
            if(roomProfile.name == name) return roomProfile;
        }
        return null;
    }

    /*public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(transform.name + ": Player Entered Room" + newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(transform.name + ": Player Left Room" + otherPlayer);
    }*/


    public void DisableTextRoomName()
    {
        inputRoomName.interactable = false;
    }

    private void DisableUIPhotonRoom()
    {
        uiPhotonRoom.DisableLeaveButton();
        uiPhotonRoom.DisableRoomListUI();
    }

    private void DisablePlayerList()
    {
        uiPlayerList.DisablePlayerListUI();
    }

    private void EnablePlayerList()
    {
        uiPlayerList.EnablePlayerListUI();
    }

    private void DisableStartGameButton()
    {
        uiPhotonRoom.DisableStartButton();
    }

    private void DisableRoomUI()
    {
        uiPhotonRoom.DisableRoomButton();
        uiPhotonRoom.DisableRoomListUI();
    }

    private void EnableRoomUI()
    {
        uiPhotonRoom.EnableRoomButton();
        uiPhotonRoom.EnableRoomListUI();
    }

    private void EnableStartLeaveButton()
    {
        uiPhotonRoom.EnableStartButton();
        uiPhotonRoom.EnableLeaveButton();
    }

    private void DisableStartLeaveButton()
    {
        uiPhotonRoom.DisableLeaveButton();
        uiPhotonRoom.DisableStartButton();
    }


}
