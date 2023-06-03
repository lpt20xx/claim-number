using UnityEngine;
public class UIPlayerList : MonoBehaviour
{
    public GameObject playerListUI;

    public void DisablePlayerListUI()
    {
        this.playerListUI.SetActive(false);
    }

    public void EnablePlayerListUI()
    {
        this.playerListUI.SetActive(true);
    }
}
