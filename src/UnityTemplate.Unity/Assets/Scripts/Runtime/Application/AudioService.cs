using System.Threading;
using Cysharp.Threading.Tasks;
using UnityTemplate.Application.Interfaces;
using UnityTemplate.Domain.Entities;

namespace UnityTemplate.Application
{
    public class AudioService : IAudioService
    {
        private readonly AudioVolumeEntity _audioVolume;
        private readonly IAudioPlayer _audioPlayer;

        public AudioService(AudioVolumeEntity audioVolume, IAudioPlayer audioPlayer)
        {
            _audioVolume = audioVolume;
            _audioPlayer = audioPlayer;
        }

        public void PlayBgm(string address, bool loop = true)
        {
            var volume = _audioVolume.ActualBgmVolume;

            _audioPlayer.PlayBgmAsync(address, volume, loop).Forget();
        }

        public void StopAllBgm()
        {
            _audioPlayer.StopAllBgm();
        }

        public UniTask PlaySeAsync(string address, CancellationToken cancellationToken = default)
        {
            var volume = _audioVolume.ActualSeVolume;

            return _audioPlayer.PlaySeAsync(address, volume, cancellationToken);
        }

        public UniTask CrossFadeBgmAsync(string address, bool loop = true, CancellationToken cancellationToken = default)
        {
            var volume = _audioVolume.ActualBgmVolume;

            return _audioPlayer.CrossFadeBgmAsync(address, volume, loop, cancellationToken);
        }
    }
}