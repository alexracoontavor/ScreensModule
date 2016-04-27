using UnityEngine;
using System;
using UnityEngine.UI;

namespace Diwip.UI.Screens.ConcreteExample
{
    public class PopupBackground : MonoBehaviour
    {
        private GameObject _background;
        private CanvasGroup _backgroundCanvasGroup;
        private RectTransform _rectTransformReference;
        private Action _onClick;
        
        public void Initialize(Color color, Action onClick)
        {
            _background = CreateBackground(color);
            this._onClick = onClick;
        }

        public void Attach(Transform transform)
        {
            _rectTransformReference.SetParent(transform, false);
            _rectTransformReference.sizeDelta = new Vector2(Screen.width*2, Screen.height*2);
            _rectTransformReference.SetAsFirstSibling();
            _rectTransformReference.anchoredPosition = Vector3.zero;
            _rectTransformReference.position = Vector3.zero;
            _rectTransformReference.localPosition = Vector3.zero;
        }

        private GameObject CreateBackground(Color color)
        {
            _background = new GameObject("BackgroundDarkener");
            this._rectTransformReference = _background.AddComponent<RectTransform>();
            _background.AddComponent<CanvasRenderer>();
            var image = _background.AddComponent<Image>();
            var button = _background.AddComponent<Button>();
            image.sprite = null;
            image.color = color;
            button.transition = Selectable.Transition.None;
            button.onClick.AddListener(() => OnScreenClick());

            _backgroundCanvasGroup = _background.AddComponent<CanvasGroup>();
            _backgroundCanvasGroup.interactable = true;
            _backgroundCanvasGroup.blocksRaycasts = true;

            return _background;
        }

        private void OnScreenClick()
        {
            if (_onClick != null)
                _onClick();
        }
    }
}