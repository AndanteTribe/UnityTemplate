using TMPro;
using Toast;
using UnityEngine;

namespace UnityTemplate.Presentation.UI.Toast
{
    /// <summary>
    /// 短いテキスト(1-2行, 頑張って3行)を表示するトーストビュー.
    /// </summary>
    public sealed class ShortToastView : ToastViewBase
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        public void Setup(string message, float firstPosY)
        {
            _text.text = message;
            RectTransform.anchoredPosition = new Vector2(-RectTransform.rect.width, firstPosY);
        }
    }
}