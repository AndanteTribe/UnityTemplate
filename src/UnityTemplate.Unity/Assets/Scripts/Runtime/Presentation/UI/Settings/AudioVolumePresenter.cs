using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplate.Application.Interfaces;
using VContainer;

namespace UnityTemplate.Presentation.UI.Settings
{
    public class AudioVolumePresenter : MonoBehaviour
    {
        [Inject]
        private IAudioVolumeService _audioVolumeService;

        [Inject]
        private IAudioVolumeController _audioVolumeController;

        [SerializeField]
        private Slider _masterVolumeSlider;
        [SerializeField]
        private Slider _bgmVolumeSlider;
        [SerializeField]
        private Slider _seVolumeSlider;

        private void Start()
        {
            _masterVolumeSlider.value = _audioVolumeService.GetMasterVolume();
            _bgmVolumeSlider.value = _audioVolumeService.GetBgmVolume();
            _seVolumeSlider.value = _audioVolumeService.GetSeVolume();

            _masterVolumeSlider.onValueChanged.AddListener(volume =>
            {
                _audioVolumeService.SetMasterVolume(volume);
                ApplyVolumesToAudioPlayer();
            });
            _bgmVolumeSlider.onValueChanged.AddListener(volume =>
            {
                _audioVolumeService.SetBgmVolume(volume);
                ApplyVolumesToAudioPlayer();
            });
            _seVolumeSlider.onValueChanged.AddListener(volume =>
            {
                _audioVolumeService.SetSeVolume(volume);
                ApplyVolumesToAudioPlayer();
            });
        }

        private void OnDestroy()
        {
            _audioVolumeService.SaveAsync().Forget();
        }

        private void ApplyVolumesToAudioPlayer()
        {
            _audioVolumeController.UpdateBgmVolume(_audioVolumeService.GetFinalBgmVolume());
            _audioVolumeController.UpdateSeVolume(_audioVolumeService.GetFinalSeVolume());
        }
    }
}