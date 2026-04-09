using System;
using System.Collections.Generic;
using System.Threading;
using AndanteTribe.Unity.Extensions;
using Cysharp.Threading.Tasks;
using LitMotion;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityTemplate.Application.Interfaces;

namespace UnityTemplate.Presentation.System
{
    public class AudioPlayer : IAudioPlayer
    {
        private readonly AudioSource[] _allChannels;
        private readonly AssetsRegistry _bgmRegistry;
        private readonly List<AudioSource> _excludeVolumeManagementChannels = new();

        private ReadOnlySpan<AudioSource> BgmChannels => _allChannels.AsSpan(1);
        private AudioSource SeChannel => _allChannels[0];

        private int _currentBgmChannelIndex = -1;

        public readonly TimeSpan FadeDuration = TimeSpan.FromSeconds(3f);

        /// <summary>
        /// Initialize a new instance of <see cref="AudioPlayer"/>.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="bgmChannelCount"></param>
        /// <param name="bgmRegistry"></param>
        public AudioPlayer(GameObject root, uint bgmChannelCount = 3, AssetsRegistry? bgmRegistry = null)
        {
            var allChannels = root.GetComponents<AudioSource>();
            var existingChannels = allChannels.AsSpan();

            var allChannelCount = bgmChannelCount + 1; // BGM + SE
            if (existingChannels.Length < allChannelCount)
            {
                var channels = new AudioSource[allChannelCount];
                existingChannels.CopyTo(channels);
                for (var i = 0; i < channels.Length; i++)
                {
                    var channel = channels[i];
                    if (channel == null)
                    {
                        channel = channels[i] = root.AddComponent<AudioSource>();
                    }
                    channel.loop = false;
                    channel.playOnAwake = false;
                }
                allChannels = channels;
            }
            _allChannels = allChannels;

            _bgmRegistry = bgmRegistry ?? new AssetsRegistry();
        }

        /// <inheritdoc/>
        public async UniTaskVoid PlayBgmAsync(string address, float volume, bool loop = true, CancellationToken cancellationToken = default)
        {
            var clip = await _bgmRegistry.LoadAsync<AudioClip>(address, cancellationToken);

            var channel = GetAvailableBgmChannel();
            channel.Stop();
            channel.clip = clip;
            channel.loop = loop;
            channel.volume = volume;
            channel.Play();
        }

        /// <inheritdoc/>
        public void StopAllBgm()
        {
            foreach (var channel in BgmChannels)
            {
                if (channel.isPlaying)
                {
                    channel.Stop();
                    channel.clip = null;
                    channel.loop = false;
                }
            }
            _bgmRegistry.Clear();
            _currentBgmChannelIndex = -1;
        }

        /// <inheritdoc/>
        public async UniTask PlaySeAsync(string address, float volume, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var channel = SeChannel;
            var handle = Addressables.LoadAssetAsync<AudioClip>(address);

            try
            {
                var result = await handle.ToUniTask(cancellationToken: cancellationToken);
                if (result == null)
                {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                    Debug.LogError("Failed to load SE: " + handle.DebugName);
#endif
                    return;
                }

                channel.PlayOneShot(result);
                await UniTask.Delay(TimeSpan.FromSeconds(result.length), cancellationToken: cancellationToken);
            }
            finally
            {
                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }
            }
        }

        /// <inheritdoc/>
        public async UniTask CrossFadeBgmAsync(string address, float volume, bool loop = true, CancellationToken cancellationToken = default)
        {
            var clip = await _bgmRegistry.LoadAsync<AudioClip>(address, cancellationToken);

            // If no track is currently playing, start with a fade-in
            if (_currentBgmChannelIndex == -1)
            {
                var channel = GetAvailableBgmChannel();
                channel.Stop();
                channel.clip = clip;
                channel.loop = loop;
                channel.volume = 0.0f;
                channel.Play();
                _excludeVolumeManagementChannels.Add(channel);

                try
                {
                    // Fade in from 0.0 to PI/2
                    await LMotion.Create(0.0f, 1.0f, (float)FadeDuration.TotalSeconds)
                        .Bind((self: this, channel, volume), static (rate, args) =>
                            args.self.ApplyBgmVolume(args.channel, args.volume, Mathf.Sin(Mathf.PI * 0.5f * rate)))
                        .ToUniTask(cancellationToken);
                }
                finally
                {
                    _excludeVolumeManagementChannels.Remove(channel);
                }

                return;
            }

            var currentChannel = BgmChannels[_currentBgmChannelIndex];
            var nextChannel = GetAvailableBgmChannel();
            nextChannel.Stop();
            nextChannel.clip = clip;
            nextChannel.loop = loop;
            nextChannel.volume = 0.0f;
            nextChannel.time = currentChannel.time;
            nextChannel.Play();
            _excludeVolumeManagementChannels.Add(currentChannel);
            _excludeVolumeManagementChannels.Add(nextChannel);

            try
            {
                await LMotion.Create(0.0f, 1.0f, (float)FadeDuration.TotalSeconds)
                    .Bind((self: this, cur: currentChannel, next: nextChannel, volume), static (rate, args) =>
                    {
                        // NOTE:
                        // Using Sin/Cos curves for fading keeps the perceived volume constant throughout.
                        // A linear fade would cause a momentary volume dip at the midpoint of the fade duration.
                        var (self, cur, next, volume) = args;
                        var f = Mathf.PI * 0.5f * rate;
                        self.ApplyBgmVolume(cur, volume, Mathf.Cos(f));
                        self.ApplyBgmVolume(next, volume, Mathf.Sin(f));
                    })
                    .ToUniTask(cancellationToken);
            }
            finally
            {
                _excludeVolumeManagementChannels.Remove(currentChannel);
                _excludeVolumeManagementChannels.Remove(nextChannel);
            }

            currentChannel.Stop();
            currentChannel.clip = null;
        }

        /// <inheritdoc/>
        public void UpdateBgmVolume(float actualVolume)
        {
            foreach (var channel in BgmChannels)
            {
                if (!_excludeVolumeManagementChannels.Contains(channel))
                {
                    channel.volume = actualVolume;
                }
            }
        }

        /// <inheritdoc/>
        public void UpdateSeVolume(float actualVolume)
        {
            SeChannel.volume = actualVolume;
        }

        private AudioSource GetAvailableBgmChannel() =>
            BgmChannels[_currentBgmChannelIndex = (_currentBgmChannelIndex + 1) % BgmChannels.Length];

        private void ApplyBgmVolume(AudioSource channel, float volume, float rate)
        {
            channel.volume = volume * rate;
        }
    }
}