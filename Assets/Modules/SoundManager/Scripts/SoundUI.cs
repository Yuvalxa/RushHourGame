using Game.Core.Sounds;

using System.Linq;


using UnityEngine;
using UnityEngine.UI;

namespace Game.Core.UI
{
    public class SoundUI : MonoBehaviour
    {
        private IToggleSounds soundToggler;
        [Header("UI Elements")]
        [SerializeField] private Button effectsBtn;
        [SerializeField] private Button musicBtn;

        [SerializeField] private Slider effectSlider;
        [SerializeField] private Slider musicSlider;

        [SerializeField] private Button backBtn;
        [SerializeField] private Button saveBtn;

        [Header("Button Sprites")]
        [SerializeField] private Sprite effectsOn;
        [SerializeField] private Sprite effectsOff;
        [SerializeField] private Sprite musicOn;
        [SerializeField] private Sprite musicOff;

        private Image effectsImage;
        private Image musicImage;

        private bool effectsMute = false;
        private bool musicMute = false;

        private bool ui_effectMute;
        private bool ui_musicMute;
        private float ui_effectsVolume;
        private float ui_musicVolume;
        private void Awake()
        {
            effectsImage = effectsBtn.GetComponent<Image>();
            musicImage = musicBtn.GetComponent<Image>();

            EventListeners();
        }
        
        private void EventListeners()
        {
            effectsBtn.onClick.AddListener(() =>
            {
                ui_effectMute = !ui_effectMute;
                soundToggler.ToggleEffects(ui_effectMute);
                UpdateUI();
            });
            musicBtn.onClick.AddListener(() =>
            {
                ui_musicMute = !ui_musicMute;
                soundToggler.ToggleMusic(ui_musicMute);
                UpdateUI();
            });

            effectSlider.onValueChanged.AddListener((value) => soundToggler.SetEffectsVolume(value));
            musicSlider.onValueChanged.AddListener((value) => soundToggler.SetMusicVolume(value));

            backBtn.onClick.AddListener(() =>
            {
                RevertChanges();
                gameObject.SetActive(false);
            });
            saveBtn.onClick.AddListener(() =>
            {
                SaveChanges();
                gameObject.SetActive(false);
            });
        }

        public void SaveChanges()
        {
            soundToggler.SaveChanges();
        }

        public void RevertChanges()
        {
            soundToggler.ToggleMusic(musicMute);
            soundToggler.ToggleEffects(effectsMute);
            soundToggler.SetEffectsVolume(ui_effectsVolume);
            soundToggler.SetMusicVolume(ui_musicVolume);
        }

        private void LoadPlayerPrefs()
        {
            if (PlayerPrefs.HasKey(SoundPrefsKeys.EffectMute.ToString()))
            {
                effectsMute = PlayerPrefs.GetInt(SoundPrefsKeys.EffectMute.ToString()) == 1;
                ui_effectMute = effectsMute;
            }
            if (PlayerPrefs.HasKey(SoundPrefsKeys.MusicMute.ToString()))
            {
                musicMute = PlayerPrefs.GetInt(SoundPrefsKeys.MusicMute.ToString()) == 1;
                ui_musicMute = musicMute;
            }

            if (PlayerPrefs.HasKey(SoundPrefsKeys.EffectVolume.ToString()))
            {
                effectSlider.value = PlayerPrefs.GetFloat(SoundPrefsKeys.EffectVolume.ToString());
                ui_effectsVolume = effectSlider.value;
            }
            if (PlayerPrefs.HasKey(SoundPrefsKeys.MusicVolume.ToString()))
            {
                musicSlider.value = PlayerPrefs.GetFloat(SoundPrefsKeys.MusicVolume.ToString());
                ui_musicVolume = musicSlider.value;
            }
            UpdateUI();
        }

        private void OnEnable()
        {
            System.Collections.Generic.IEnumerable<IToggleSounds> soundTogglers = FindObjectsOfType<MonoBehaviour>().OfType<IToggleSounds>();
            soundToggler = soundTogglers?.SingleOrDefault();
            if (soundToggler == null)
                throw new System.Exception($"UI could not find the sound manager");
            LoadPlayerPrefs();
        }

        private void UpdateUI()
        {
            effectsImage.sprite = ui_effectMute ? effectsOff : effectsOn;
            musicImage.sprite = ui_musicMute ? musicOff : musicOn;

        }



    }
}