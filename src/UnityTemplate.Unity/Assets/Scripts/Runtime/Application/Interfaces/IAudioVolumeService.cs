using UnityTemplate.Domain.Entities;

namespace UnityTemplate.Application.Interfaces
{
    public interface IAudioVolumeService
    {
        float GetMasterVolume();

        float GetBgmVolume();

        float GetSeVolume();

        void SetMasterVolume(float volume);

        void SetBgmVolume(float volume);

        void SetSeVolume(float volume);
    }
}