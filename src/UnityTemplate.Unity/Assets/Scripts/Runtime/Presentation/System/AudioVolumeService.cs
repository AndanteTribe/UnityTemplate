using System.Threading;
using Cysharp.Threading.Tasks;
using Radio;
using UnityEngine;
using UnityTemplate.Application.Interfaces;
using UnityTemplate.Domain.Entities;
using AudioSettings = UnityTemplate.Domain.Entities.AudioSettings;

namespace UnityTemplate.Presentation.System
{
    public class AudioVolumeService : IAudioVolumeService, IAudioService
    {
        private readonly AudioSettings _settingses;

        private readonly AudioPlayerCore _core;

        public AudioVolumeService(AudioSettings settingses, GameObject root)
        {
            _core = new AudioPlayerCore(root);
        }

        void IAudioVolumeService.SetMasterVolume(float volume)
        {
            _core.SetMasterVolume(volume);
        }

        void IAudioVolumeService.SetBgmVolume(float volume)
        {
            _core.SetBgmVolume(volume);
        }

        void IAudioVolumeService.SetSeVolume(float volume)
        {
            _core.SetSeVolume(volume);
        }

        public UniTask PlaySeAsync(string address, CancellationToken cancellationToken)
        {
            return _core.PlaySeAsync(address, cancellationToken);
        }

        public void Dispose()
        {
            _core.Dispose();
        }
    }
}