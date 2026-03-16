namespace UnityTemplate.Application.Interfaces
{
    public interface IAudioVolumeService
    {
        void SetMasterVolume(float volume);

        void SetBgmVolume(float volume);

        void SetSeVolume(float volume);
    }
}