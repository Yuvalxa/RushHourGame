
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Button quitButton;
    [SerializeField] TMP_Text playHealth;
    [SerializeField] TMP_Text playerScoreTMP;
    [SerializeField] TMP_Text totalPoints;
    private int playerScore = 0;
    private int bestScore = 0;
    private void Awake()
    {
        SetScoreSettings();
        quitButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(2);
        });
        PlayerController.OnCarCrush += HandlePlayerCrush;
        EnemyController.OnOutOfBounds += HandlePlayerScore;
    }
    private void SetScoreSettings() {
     PlayerPrefs.SetInt("PlayersScore", 0);
        if (PlayerPrefs.HasKey("BestScore"))
            bestScore = PlayerPrefs.GetInt("BestScore");
        else
            PlayerPrefs.SetInt("BestScore", 0);

    }
    private void HandlePlayerScore(AreaObjectController controller)
    {
        if (!(controller is EnemyController)) return;
            playerScore++;
        int playerPoints = playerScore * 100;
        PlayerPrefs.SetInt("PlayersScore", playerPoints);
        if (playerPoints > bestScore)
        {
            if (playerPoints > bestScore)
                bestScore = playerPoints;
                PlayerPrefs.SetInt("BestScore", bestScore);
        }

        playerScoreTMP.text = "Cars : " + playerScore;
        totalPoints.text = "Points : " + playerScore * 100;
    }

    private void HandlePlayerCrush(int playerHealth)
    {
        if(playerHealth == 0)
            SceneManager.LoadScene(2);

        playHealth.text = "Lives :" + playerHealth.ToString();
    }

    private void OnDestroy()
    {
        PlayerController.OnCarCrush -= HandlePlayerCrush;
        quitButton.onClick.RemoveAllListeners();
    }
}
