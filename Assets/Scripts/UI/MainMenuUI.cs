using System.Collections;
using System.Collections.Generic;
using Game.Core.Sounds;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] AudioClip gameMusic;

    private void Awake()
    {
        playButton.onClick.AddListener(() => 
        {
            SceneManager.LoadScene(1);
        });
        optionsButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(3);

        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
    private void Start()
    {
        SoundManager.Instance.PlayMusic(gameMusic);

    }
    private void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
        optionsButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }
}
