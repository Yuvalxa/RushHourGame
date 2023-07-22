using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Core.Sounds;
using Game.Core.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button applyButton;
    [SerializeField] List<Toggle> difficulyToggles;
    [SerializeField] SoundUI soundUI;
    private void Awake()
    {
        loadSettings();
        backButton.onClick.AddListener(() => 
        {
            soundUI.RevertChanges();
            SceneManager.LoadScene(0);
        });
        applyButton.onClick.AddListener(() =>
        {
            ApplySettings();
            SceneManager.LoadScene(0);
        });
        soundUI.gameObject.SetActive(true);
    }

    private void loadSettings() {

        if(PlayerPrefs.HasKey("DiffcultLevel"))
            difficulyToggles[PlayerPrefs.GetInt("DiffcultLevel")].isOn = true;
        else // diffult easy on
            difficulyToggles[1].isOn = true;

    }

    private void ApplySettings() { // Set the chosen toggle on and the rest to off
        soundUI.SaveChanges();
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
