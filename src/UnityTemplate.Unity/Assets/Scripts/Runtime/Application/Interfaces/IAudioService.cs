using System.Threading;
using Cysharp.Threading.Tasks;

namespace UnityTemplate.Application.Interfaces
{
    public interface IAudioService
    {
        void PlayBgm(string address, bool loop = true);

        void StopAllBgm();

        UniTask PlaySeAsync(string address, CancellationToken cancellationToken = default);

        UniTask CrossFadeBgmAsync(string address, bool loop = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// BGMの音量を更新する.
        /// </summary>
        /// <param name="finalVolume">Bgm音量.</param>
        void UpdateBgmVolume(float finalVolume);

        /// <summary>
        /// SEの音量を更新する.
        /// </summary>
        /// <param name="finalVolume">Se音量.</param>
        void UpdateSeVolume(float finalVolume);
    }
}