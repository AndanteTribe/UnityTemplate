using System.Threading;
using Cysharp.Threading.Tasks;

namespace UnityTemplate.Application.Interfaces
{
    public interface IAudioService
    {
        UniTask PlaySeAsync(string address, CancellationToken cancellationToken);
    }
}