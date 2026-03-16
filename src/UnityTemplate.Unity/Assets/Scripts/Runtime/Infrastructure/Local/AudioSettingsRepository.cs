using AndanteTribe.IO;
using Cysharp.Threading.Tasks;
using UnityTemplate.Application.Interfaces;
using UnityTemplate.Domain.Entities;

namespace UnityTemplate.Infrastructure.Local
{
    public class AudioSettingsRepository : IAudioSettingsRepository
    {
        private readonly ILocalPrefs _prefs;

        public AudioSettingsRepository(ILocalPrefs prefs)
        {
            _prefs = prefs;
        }

        public async UniTask SaveAsync(AudioSettings entity)
        {
            await _prefs.SaveAsync(nameof(AudioSettings), entity);
        }
    }
}