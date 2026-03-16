using UnityEngine;
using UnityEngine.UI;
using UnityTemplate.Application.UseCases;
using VContainer;

namespace UnityTemplate.Presentation.UI.Settings
{
    public class AudioVolumePresenter : MonoBehaviour
    {
        [Inject]
        private AudioVolumeUseCase _useCase;

        [SerializeField]
        private Slider _masterVolumeSlider;
        [SerializeField]
        private Slider _bgmVolumeSlider;
        [SerializeField]
        private Slider _seVolumeSlider;

        private void Start()
        {
            _masterVolumeSlider.onValueChanged.AddListener(volume => _useCase.Execute(AudioVolumeUseCase.VolumeType.Master, volume));
            _bgmVolumeSlider.onValueChanged.AddListener(volume => _useCase.Execute(AudioVolumeUseCase.VolumeType.Bgm, volume));
            _seVolumeSlider.onValueChanged.AddListener(volume => _useCase.Execute(AudioVolumeUseCase.VolumeType.Se, volume));
        }
    }
}