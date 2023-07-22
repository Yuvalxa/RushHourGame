using System;

using UnityEngine;

namespace Game.Core.Sounds
{
    public enum SoundType { Effect, Music }
    public enum SoundPrefsKeys { EffectMute, EffectVolume, MusicMute, MusicVolume }

    /// <summary>
    /// Handle all sound requests. Controls effects and music separately.
    /// </summary>
    /// <remarks>
    /// All sound requests must go through the Instance property.
    /// </remarks>
    public class SoundManager : MonoBehaviour, IPlaySounds, IToggleSounds
    {
        public static SoundManager Instance { get; private set; }
        [SerializeReference] private AudioSource effectsSource;
        [SerializeReference] private AudioSource musicSource;
        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);

            if (effectsSource == null)
                throw new Exception($"No source was set for effects!");
            if (musicSource == null)
                throw new Exception($"No source was set for music!");


            Initialize();


            //local
            void Initialize()
            {
                if (PlayerPrefs.HasKey(SoundPrefsKeys.EffectMute.ToString()))
                {
                    bool mute = PlayerPrefs.GetInt(SoundPrefsKeys.EffectMute.ToString()) == 1;
                    effectsSource.mute = mute;
                }
                if (PlayerPrefs.HasKey(SoundPrefsKeys.MusicMute.ToString()))
                {
                    bool mute = PlayerPrefs.GetInt(SoundPrefsKeys.MusicMute.ToString()) == 1;
                    musicSource.mute = mute;
                }

                if (PlayerPrefs.HasKey(SoundPrefsKeys.EffectVolume.ToString()))
                {
                    effectsSource.volume = PlayerPrefs.GetFloat(SoundPrefsKeys.EffectVolume.ToString());
                }
                if (PlayerPrefs.HasKey(SoundPrefsKeys.MusicVolume.ToString()))
                {
                    musicSource.volume = PlayerPrefs.GetFloat(SoundPrefsKeys.MusicVolume.ToString());
                }
            }
        }

        #region IPlaySounds Interface

        public void PlayEffect(AudioClip clip)
        {
            effectsSource.PlayOneShot(clip, effectsSource.volume);
        }

        public void PlayEffectAtPosition(AudioClip clip, Vector3 position, float volume = 1)
        {
            AudioSource.PlayClipAtPoint(clip, position, effectsSource.volume * volume);
        }

        public void PlayMusic(AudioClip clip)
        {
            //Stop
            if (musicSource.isPlaying)
                musicSource.Stop();
            //Set
            musicSource.clip = clip;
            //Play
            musicSource.Play();
        }

        public void StopPlayingEffect()
        {
            if(effectsSource.isPlaying)
                effectsSource.Stop();
        }

        public void StopPlayingMusic()
        {
            if(musicSource.isPlaying)
                musicSource.Stop();
        }
        #endregion

        #region IToggleSounds Interface
        public void ToggleEffects()
        {
            effectsSource.mute = !effectsSource.mute;
        }

        public void ToggleMusic()
        {
            musicSource.mute = !musicSource.mute;
        }

        public void ToggleEffects(bool value)
        {
            effectsSource.mute = value;
        }

        public void ToggleMusic(bool value)
        {
            musicSource.mute = value;
        }
        public void SetEffectsVolume(float value)
        {
            effectsSource.volume = value;
        }

        public void SetMusicVolume(float value)
        {
            musicSource.volume = value;
        }

        public void SaveChanges()
        {
            PlayerPrefs.SetFloat(SoundPrefsKeys.EffectVolume.ToString(), effectsSource.volume);
            PlayerPrefs.SetFloat(SoundPrefsKeys.MusicVolume.ToString(), musicSource.volume);
            PlayerPrefs.SetInt(SoundPrefsKeys.EffectMute.ToString(), effectsSource.mute ? 1 : 0);
            PlayerPrefs.SetInt(SoundPrefsKeys.MusicMute.ToString(), musicSource.mute ? 1 : 0);
        }


        #endregion
    }
}