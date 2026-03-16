using Cysharp.Threading.Tasks;
using UnityTemplate.Domain.Entities;

namespace UnityTemplate.Application.Interfaces
{
    // ロードは初期化でやればいいので、セーブだけ.
    public interface IAudioSettingsRepository
    {
        UniTask SaveAsync(AudioSettings entity);
    }
}