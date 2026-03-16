using MessagePack;

namespace UnityTemplate.Domain.Entities
{
    [MessagePackObject]
    public class AudioSettings
    {
        [Key(0)]
        public float MasterVolume { get; set; }
        [Key(1)]
        public float BgmVolume { get; set; }
        [Key(2)]
        public float SeVolume { get; set; }
    }
}
