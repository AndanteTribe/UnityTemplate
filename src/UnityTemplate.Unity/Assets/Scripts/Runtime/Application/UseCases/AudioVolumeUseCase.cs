using UnityTemplate.Application.Interfaces;
using UnityTemplate.Domain.Entities;

namespace UnityTemplate.Application.UseCases
{
    public class AudioVolumeUseCase
    {
        public enum VolumeType : byte
        {
            Invalid,
            Master,
            Bgm,
            Se
        }

        private readonly AudioSettings _settings;
        private readonly IAudioVolumeService _audioVolumeService;

        public AudioVolumeUseCase(AudioSettings settings, IAudioVolumeService audioVolumeService)
        {
            _settings = settings;
            _audioVolumeService = audioVolumeService;
        }

        public void Execute(VolumeType volumeType, float volume)
        {
            switch (volumeType)
            {
                case VolumeType.Master:
                    _settings.MasterVolume = volume;
                    _audioVolumeService.SetMasterVolume(volume);
                    break;
                case VolumeType.Bgm:
                    _settings.BgmVolume = volume;
                    _audioVolumeService.SetBgmVolume(volume);
                    break;
                case VolumeType.Se:
                    _settings.SeVolume = volume;
                    _audioVolumeService.SetSeVolume(volume);
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(volumeType), volumeType, null);
            }
        }
    }
}