using AndanteTribe.IO;
using Cysharp.Threading.Tasks;
using UnityTemplate.Application.Interfaces;
using UnityTemplate.Domain.Entities;

namespace UnityTemplate.Infrastructure.Local
{
    public class AudioVolumeRepository : IAudioVolumeRepository
    {
        private readonly ILocalPrefs _prefs;

        public AudioVolumeRepository(ILocalPrefs prefs)
        {
            _prefs = prefs;
        }

        public AudioVolumeEntity Load()
        {
            var entity = _prefs.Load<AudioVolumeEntity>(nameof(AudioVolumeEntity));

            return entity ?? new AudioVolumeEntity();
        }

        public async UniTask SaveAsync(AudioVolumeEntity entity)
        {
            await _prefs.SaveAsync(nameof(AudioVolumeEntity), entity);
        }
    }
}