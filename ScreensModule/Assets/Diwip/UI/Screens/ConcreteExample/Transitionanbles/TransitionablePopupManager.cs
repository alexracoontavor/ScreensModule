using UnityEngine;
using System;
using System.Collections.Generic;

namespace Diwip.UI.Screens.ConcreteExample
{
    /// <summary>
    /// A Popups implementation of TransitionableManagerBase
    /// Keeps a queue of screens to show, ordered by priority
    /// Has a Background object that interacts with Screens
    /// Screens transition in relation to each other. Rules:
    ///     Only 1 screen can be open at a given time
    ///     When a screen is closed, the next enqued screen (if any) is shown 
    ///     The 1 shown screen has a Background object attached to it
    /// </summary>
    public class TransitionablePopupManager : TransitionableManagerBase
    {
        [SerializeField] private Color _backgroundColor;
        private List<Type> _typesToShow = new List<Type>();
        private PopupBackground _background;

        protected override void Start()
        {
            base.Start();

            _background = gameObject.AddComponent<PopupBackground>();
            _background.Initialize(_backgroundColor, HandleBgClick);
        }

        private void HandleBgClick()
        {
            if (CurrentTop != null)
            {
                Hide(CurrentTop);
            }
        }

        public override void Hide(Type screenType)
        {
            if (CurrentTop != null)
            {
                if (_typesToShow.Count == 0)
                {
                    Transitions.Transition(CurrentTop, null);
                    CurrentTop = null;
                }

                ShowNext();
            }
        }

        public override void Show(Type screenType)
        {
            if (CurrentTop == null)
            {
                ShowInstance(ScreensManager.GetInstanceByType(screenType));
            }
            else
            {
                Enqueue(screenType);
            }
        }

        private void Enqueue(Type screenType)
        {
            var typeToEnque = ScreensManager.GetInstanceByType(screenType);

            if (_typesToShow.Count > 0)
            {
                if (!_typesToShow.Contains(screenType))
                {
                    for (int i = 0; i < _typesToShow.Count; i++)
                    {
                        var enquedType = ScreensManager.GetInstanceByType(_typesToShow[i]);

                        if (typeToEnque.Priority >= enquedType.Priority)
                        {
                            _typesToShow.Insert(i, screenType);
                            return;
                        }
                    }

                    _typesToShow.Add(screenType);
                }
            }
            else 
            {
                _typesToShow.Insert(0, screenType);
            }
        }

        protected void ShowInstance(BaseScreen instance)
        {
            Transitions.Transition(CurrentTop, instance);
            CurrentTop = instance;
            _background.Attach(CurrentTop.transform);
        }

        protected void ShowNext()
        {
            if (_typesToShow.Count > 0)
            {
                Type nextType = _typesToShow[_typesToShow.Count - 1];

                _typesToShow.RemoveAt(_typesToShow.Count - 1);
                ShowInstance(ScreensManager.GetInstanceByType(nextType));
            }
        }
    }
}