using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button menuButton;
    [SerializeField] TMP_Text playerScore;
    [SerializeField] TMP_Text bestScore;


    private void Awake()
    {
        playAgainButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });

        menuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }
    private void Start()
    {
        playerScore.text = "Player Score :" + PlayerPrefs.GetInt("PlayersScore");
        bestScore.text = "Best Score :" + PlayerPrefs.GetInt("BestScore");

    }

    private void OnDestroy()
    {
        playAgainButton.onClick.RemoveAllListeners();
        menuButton.onClick.RemoveAllListeners();
    }
}
