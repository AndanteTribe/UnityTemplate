using Cysharp.Threading.Tasks;

namespace UnityTemplate.Application.Interfaces
{
    public interface IAudioVolumeService
    {
        float GetMasterVolume();

        float GetBgmVolume();

        float GetSeVolume();

        float GetFinalBgmVolume();

        float GetFinalSeVolume();

        void SetMasterVolume(float volume);

        void SetBgmVolume(float volume);

        void SetSeVolume(float volume);

        UniTask SaveAsync();
    }
}