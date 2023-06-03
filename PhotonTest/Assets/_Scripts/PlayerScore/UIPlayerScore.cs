using UnityEngine;
using UnityEngine.UI;

public class UIPlayerScore : MonoBehaviour
{
    [SerializeField] protected Text playerNameText;
    [SerializeField] protected Text scoreText;
    [SerializeField] protected PlayerScore playerScore;

    public void SetPlayerScore(PlayerScore playerScore)
    {
        this.playerScore = playerScore;
        this.playerNameText.text = playerScore.playerName;
        this.scoreText.text = playerScore.score.ToString();
    }
}
