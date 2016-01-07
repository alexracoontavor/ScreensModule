using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;

namespace Diwip.UI.Screens.ConcreteExample
{
    public class PopupBackground : MonoBehaviour
    {
        GameObject background;
        CanvasGroup _backgroundCanvasGroup;
        RectTransform transform;
        Action onClick;

        public void Initialize(Color color, Action onClick)
        {
            background = CreateBackground(color);
            this.onClick = onClick;
        }

        public void Attach(Transform transform)
        {
            background.transform.SetParent(transform, false);
            this.transform.position = new Vector2(Screen.width * .5f, Screen.height * .5f);
            this.transform.sizeDelta = new Vector2(Screen.width, Screen.height);
            background.transform.SetAsFirstSibling();
        }

        private GameObject CreateBackground(Color color)
        {
            var obj = new GameObject("BackgroundDarkener");
            this.transform = obj.AddComponent<RectTransform>();
            obj.AddComponent<CanvasRenderer>();
            var image = obj.AddComponent<Image>();
            var button = obj.AddComponent<Button>();
            image.sprite = null;
            image.color = color;
            button.transition = Selectable.Transition.None;
            button.onClick.AddListener(() => OnScreenClick());

            _backgroundCanvasGroup = obj.AddComponent<CanvasGroup>();
            _backgroundCanvasGroup.interactable = true;
            _backgroundCanvasGroup.blocksRaycasts = true;

            return obj;
        }

        private void OnScreenClick()
        {
            if (onClick != null)
                onClick();
        }
    }
}