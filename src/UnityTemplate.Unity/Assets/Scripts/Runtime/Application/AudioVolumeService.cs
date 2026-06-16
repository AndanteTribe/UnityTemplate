using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityTemplate.Application.Interfaces;
using UnityTemplate.Domain.Entities;

namespace UnityTemplate.Application
{
    public class AudioVolumeService : IAudioVolumeService
    {
        private readonly AudioVolumeEntity _volumeEntity;
        private readonly IAudioVolumeRepository _repository;
        private readonly IAudioPlayer _audioPlayer;

        private CancellationTokenSource _saveCts;

        private readonly TimeSpan _saveDelay = TimeSpan.FromSeconds(0.3);

        public AudioVolumeService(AudioVolumeEntity volumeEntity, IAudioVolumeRepository repository, IAudioPlayer audioPlayer)
        {
            _volumeEntity = volumeEntity;
            _repository = repository;
            _audioPlayer = audioPlayer;
        }

        public float GetMasterVolume() => _volumeEntity.MasterVolume;
        public float GetBgmVolume() => _volumeEntity.BgmVolume;
        public float GetSeVolume() => _volumeEntity.SeVolume;

        public void SetMasterVolume(float volume)
        {
            _volumeEntity.MasterVolume = volume;
            _audioPlayer.UpdateBgmVolume(_volumeEntity.ActualBgmVolume);
            _audioPlayer.UpdateSeVolume(_volumeEntity.ActualSeVolume);
            DebounceSaveAsync().Forget();
        }

        public void SetBgmVolume(float volume)
        {
            _volumeEntity.BgmVolume = volume;
            _audioPlayer.UpdateBgmVolume(_volumeEntity.ActualBgmVolume);
            DebounceSaveAsync().Forget();
        }

        public void SetSeVolume(float volume)
        {
            _volumeEntity.SeVolume = volume;
            _audioPlayer.UpdateSeVolume(_volumeEntity.ActualSeVolume);
            DebounceSaveAsync().Forget();
        }

        private async UniTaskVoid DebounceSaveAsync()
        {
            _saveCts?.Cancel();
            _saveCts?.Dispose();

            _saveCts = new CancellationTokenSource();
            var token = _saveCts.Token;

            // スライダーを動かしている間は、セーブ処理を遅延させる。
            try
            {
                await UniTask.Delay(_saveDelay, cancellationToken: token);

                await _repository.SaveAsync(_volumeEntity);
            }
            catch (OperationCanceledException)
            {
                // 待っている間にスライダーが動かされてキャンセルされた場合は、何もせずにスルーする
            }
        }
    }
}