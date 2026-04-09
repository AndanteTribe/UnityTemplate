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
    }
}