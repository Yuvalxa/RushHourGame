using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Core.Sounds;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button applyButton;
    [SerializeField] List<Toggle> difficulyToggles;
    [SerializeField] GameObject soundUIPrefab;
    private void Awake()
    {
        loadSettings();
        backButton.onClick.AddListener(() => 
        {
            SceneManager.LoadScene(0);
        });
        applyButton.onClick.AddListener(() =>
        {
            SetSettings();
            SceneManager.LoadScene(0);
        });
        soundUIPrefab.SetActive(true);
    }
    private void loadSettings() {

        if(PlayerPrefs.HasKey("DiffcultLevel"))
            difficulyToggles[PlayerPrefs.GetInt("DiffcultLevel")].isOn = true;
        else // diffult Medium on
            difficulyToggles[1].isOn = true;

    }
    private void SetSettings() {
        for (int i = 0; i < difficulyToggles.Count; i++) {
            Toggle diff = difficulyToggles[i];
            if(diff.isOn)
            {
                PlayerPrefs.SetInt("DiffcultLevel", i);// 0- Easy, 1- medium, 2- hard
                Debug.Log(i);
            }
        }
    }
    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
        applyButton.onClick.RemoveAllListeners();
    }
}
