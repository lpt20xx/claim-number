using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class PhotonLogout : MonoBehaviour
{
    public Button logoutButton;

    public PhotonLogin photonLogin;
    public PhotonRoom photonRoom;
    public UIPhotonRoom uiPhotonRoom;
    private void Start()
    {
        this.DisableLogoutButton();
    }

    private void DisableLogoutButton()
    {
        this.logoutButton.interactable = false;
    }

    public void EnableLogoutButton()
    {
        this.logoutButton.interactable = true;
    }

    private void EnableLoginButton()
    {
        photonLogin.loginButton.interactable = true;
    }


    public void Logout()
    {
        Debug.Log(transform.name + ": Logout ");
        PhotonNetwork.Disconnect();

        this.DisableLogoutButton();
        this.EnableLoginButton();

        uiPhotonRoom.DisableRoomCAndJ();
        uiPhotonRoom.DisableRoomListUI();

        photonLogin.inputUsername.interactable = true;
    }

    

   
}
