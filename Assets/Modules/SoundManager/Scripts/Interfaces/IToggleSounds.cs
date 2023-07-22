
namespace Game.Core.Sounds
{
    public interface IToggleSounds
    {
        /// <summary>
        /// Mute or Unmute effects
        /// </summary>
        public void ToggleEffects();

        /// <summary>
        /// Mute or Unmute music
        /// </summary>
        public void ToggleMusic();


        /// <summary>
        /// Set effects to specific value
        /// </summary>
        public void ToggleEffects(bool value);

        /// <summary>
        /// Set music to specific value
        /// </summary>
        public void ToggleMusic(bool value);


        /// <summary>
        /// Set global effects volume
        /// </summary>
        /// <param name="value"></param>
        public void SetEffectsVolume(float value);

        /// <summary>
        /// Set global music volume
        /// </summary>
        /// <param name="value"></param>
        public void SetMusicVolume(float value);

        /// <summary>
        /// Save volume changes to PlayerPrefs
        /// </summary>
        public void SaveChanges();

    }
}