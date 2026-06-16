using System.Threading;
using Cysharp.Threading.Tasks;

namespace UnityTemplate.Application.Interfaces
{
    public interface IAudioPlayer
    {
        /// <summary>
        /// 非同期でBGMを読み込んで再生する.
        /// </summary>
        /// <param name="address">パス.</param>
        /// <param name="volume">音量.</param>
        /// <param name="loop">ループするかどうか.</param>
        /// <param name="cancellationToken">キャンセルトークン.</param>
        UniTaskVoid PlayBgmAsync(string address, float volume, bool loop = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 全てのBGMを停止する.
        /// </summary>
        void StopAllBgm();

        /// <summary>
        /// 非同期でSEを読み込んで再生する.
        /// </summary>
        /// <param name="address">パス.</param>
        /// <param name="volume">音量.</param>
        /// <param name="cancellationToken">キャンセルトークン.</param>
        UniTask PlaySeAsync(string address, float volume, CancellationToken cancellationToken = default);

        /// <summary>
        /// 非同期でBGMをクロスフェード再生する.
        /// </summary>
        /// <param name="address">パス.</param>
        /// <param name="volume">音量.</param>
        /// <param name="loop">ループするかどうか.</param>
        /// <param name="cancellationToken">キャンセルトークン.</param>
        UniTask CrossFadeBgmAsync(string address, float volume, bool loop = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// BGMの音量を更新する.
        /// </summary>
        /// <param name="actualVolume">Bgm音量.</param>
        void UpdateBgmVolume(float actualVolume);

        /// <summary>
        /// SEの音量を更新する.
        /// </summary>
        /// <param name="actualVolume">Se音量.</param>
        void UpdateSeVolume(float actualVolume);
    }
}