using Cysharp.Threading.Tasks;
using UnityTemplate.Domain.Entities;

namespace UnityTemplate.Application.Interfaces
{
    // ロードは初期化でやればいいので、セーブだけ.
    public interface IAudioVolumeRepository
    {
        UniTask SaveAsync(AudioVolumeEntity entity);
    }
}