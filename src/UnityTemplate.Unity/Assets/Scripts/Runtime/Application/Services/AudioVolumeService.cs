using Cysharp.Threading.Tasks;
using UnityTemplate.Application.Interfaces;
using UnityTemplate.Domain.Entities;

namespace UnityTemplate.Application.Services
{
    public class AudioVolumeService : IAudioVolumeService
    {
        private readonly AudioVolumeEntity _volumeEntity;
        private readonly IAudioVolumeRepository _repository;
        private readonly IAudioService _audioService;

        public AudioVolumeService(AudioVolumeEntity volumeEntity, IAudioVolumeRepository repository, IAudioService audioService)
        {
            _volumeEntity = volumeEntity;
            _repository = repository;
            _audioService = audioService;
        }

        public float GetMasterVolume() => _volumeEntity.MasterVolume;
        public float GetBgmVolume() => _volumeEntity.BgmVolume;
        public float GetSeVolume() => _volumeEntity.SeVolume;

        public void SetMasterVolume(float volume)
        {
            _volumeEntity.MasterVolume = volume;
            _audioService.UpdateBgmVolume(_volumeEntity.FinalBgmVolume);
            _audioService.UpdateSeVolume(_volumeEntity.FinalSeVolume);
        }

        public void SetBgmVolume(float volume)
        {
            _volumeEntity.BgmVolume = volume;
            _audioService.UpdateBgmVolume(_volumeEntity.FinalBgmVolume);
        }

        public void SetSeVolume(float volume)
        {
            _volumeEntity.SeVolume = volume;
            _audioService.UpdateSeVolume(_volumeEntity.FinalSeVolume);
        }

        public UniTask SaveAsync()
        {
            return _repository.SaveAsync(_volumeEntity);
        }
    }
}
