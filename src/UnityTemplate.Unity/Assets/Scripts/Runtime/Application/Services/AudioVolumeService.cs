using Cysharp.Threading.Tasks;
using UnityTemplate.Application.Interfaces;
using UnityTemplate.Domain.Entities;

namespace UnityTemplate.Application.Services
{
    public class AudioVolumeService : IAudioVolumeService
    {
        private readonly AudioVolumeEntity _volumeEntity;
        private readonly IAudioVolumeRepository _repository;

        public AudioVolumeService(AudioVolumeEntity volumeEntity, IAudioVolumeRepository repository)
        {
            _volumeEntity = volumeEntity;
            _repository = repository;
        }

        public float GetMasterVolume() => _volumeEntity.MasterVolume;
        public float GetBgmVolume() => _volumeEntity.BgmVolume;
        public float GetSeVolume() => _volumeEntity.SeVolume;
        public float GetFinalBgmVolume() => _volumeEntity.FinalBgmVolume;
        public float GetFinalSeVolume() => _volumeEntity.FinalSeVolume;

        public void SetMasterVolume(float volume)
        {
            _volumeEntity.MasterVolume = volume;
        }

        public void SetBgmVolume(float volume)
        {
            _volumeEntity.BgmVolume = volume;
        }

        public void SetSeVolume(float volume)
        {
            _volumeEntity.SeVolume = volume;
        }

        public UniTask SaveAsync()
        {
            return _repository.SaveAsync(_volumeEntity);
        }
    }
}
