namespace UnityTemplate.Presentation
{
    public interface IAudioVolumeController
    {
        /// <summary>
        /// BGMの音量を更新する.
        /// </summary>
        /// <param name="finalVolume">Bgm音量.</param>
        void UpdateBgmVolume(float finalVolume);

        /// <summary>
        /// SEの音量を更新する.
        /// </summary>
        /// <param name="finalVolume">Se音量.</param>
        void UpdateSeVolume(float finalVolume);
    }
}
