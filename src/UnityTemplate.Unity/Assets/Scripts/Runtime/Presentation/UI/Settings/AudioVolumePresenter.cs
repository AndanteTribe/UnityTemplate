using UnityEngine;
using UnityEngine.UI;
using UnityTemplate.Application.Interfaces;
using VContainer;

namespace UnityTemplate.Presentation.UI.Settings
{
    public class AudioVolumePresenter : MonoBehaviour
    {
        [Inject]
        private readonly IAudioVolumeService _audioVolumeService;

        [SerializeField]
        private Slider _masterVolumeSlider;
        [SerializeField]
        private Slider _bgmVolumeSlider;
        [SerializeField]
        private Slider _seVolumeSlider;

        private void Start()
        {
            _masterVolumeSlider.onValueChanged.AddListener(volume => _audioVolumeService.SetMasterVolume(volume));
            _bgmVolumeSlider.onValueChanged.AddListener(volume => _audioVolumeService.SetBgmVolume(volume));
            _seVolumeSlider.onValueChanged.AddListener(volume => _audioVolumeService.SetSeVolume(volume));
        }
    }
}