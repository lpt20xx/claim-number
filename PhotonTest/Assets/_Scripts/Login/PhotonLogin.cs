using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhotonLogin : MonoBehaviourPunCallbacks
{
    public TMP_InputField inputUsername;
    public TextMeshProUGUI textUsername;
    public Button loginButton;

    public PhotonLogout photonLogout;
    public PhotonRoom photonRoom;
    public UIPhotonRoom uiPhotonRoom;

    public GameObject warning;
    public Text warningText;

    public static string playerName;
    // Start is called before the first frame update
    private void Start()
    {
        this.SetUsernameText();
        this.DisableWarning();
        this.DisableRoomCAndJObj();
    }


    private void DisableWarning()
    {
        warning.SetActive(false);
    }

    public void Login() 
    { 
        string name = inputUsername.text;
        Debug.Log(transform.name + ": Login " + name);

        if(name == "")
        {
            string nameWarning = "Please enter your name";
            warning.SetActive(true);
            warningText.text = nameWarning;
            StartCoroutine(DisableWarningDelay());
            return;
        }

        //Auto start game with MasterClient
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LocalPlayer.NickName = name;
        PhotonNetwork.ConnectUsingSettings();

        playerName = name;
    }

    IEnumerator DisableWarningDelay()
    {
        yield return new WaitForSeconds(3f);
        warning.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = playerName;

        this.SetInteractableButton();

        uiPhotonRoom.EnableRoomCAndJ();

        inputUsername.interactable = false;

        textUsername.text = inputUsername.text;

        uiPhotonRoom.EnableRoomListUI();
    }

    private void SetUsernameText()
    {
        this.inputUsername.text = playerName;
    }

    private void SetInteractableButton()
    {
        this.loginButton.interactable = false;
        photonLogout.logoutButton.interactable = true;
    }

    public void DisableLogInOutButton()
    {
        loginButton.interactable = false;
        photonLogout.logoutButton.interactable = false;
    }

    private void DisableRoomCAndJObj()
    {
        uiPhotonRoom.DisableRoomCAndJ();
    }
}
