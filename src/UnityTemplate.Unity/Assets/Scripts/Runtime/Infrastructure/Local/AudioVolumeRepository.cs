using AndanteTribe.IO;
using Cysharp.Threading.Tasks;
using UnityTemplate.Application.Interfaces;
using UnityTemplate.Domain.Entities;

namespace UnityTemplate.Infrastructure.Local
{
    public class AudioVolumeRepository : IAudioVolumeRepository
    {
        private const string AudioVolumeKey = "audio_volume";

        private readonly ILocalPrefs _prefs;

        public AudioVolumeRepository(ILocalPrefs prefs)
        {
            _prefs = prefs;
        }

        public AudioVolumeEntity Load()
        {
            var entity = _prefs.Load<AudioVolumeEntity>(AudioVolumeKey);

            return entity ?? new AudioVolumeEntity();
        }

        public async UniTask SaveAsync(AudioVolumeEntity entity)
        {
            await _prefs.SaveAsync(AudioVolumeKey, entity);
        }
    }
}