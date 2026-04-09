using System;
using MessagePack;

namespace UnityTemplate.Domain.Entities
{
    [MessagePackObject]
    public class AudioVolumeEntity
    {
        private float _masterVolume = 0.5f;
        private float _bgmVolume = 0.5f;
        private float _seVolume =  0.5f;

        [Key(0)]
        public float MasterVolume
        {
            get => _masterVolume;
            set => _masterVolume = Math.Clamp(value, 0f, 1f);
        }

        [Key(1)]
        public float BgmVolume
        {
            get => _bgmVolume;
            set => _bgmVolume = Math.Clamp(value, 0f, 1f);
        }

        [Key(2)]
        public float SeVolume
        {
            get => _seVolume;
            set => _seVolume = Math.Clamp(value, 0f, 1f);
        }

        [IgnoreMember]
        public float FinalBgmVolume => BgmVolume * MasterVolume;

        [IgnoreMember]
        public float FinalSeVolume => SeVolume * MasterVolume;
    }
}
