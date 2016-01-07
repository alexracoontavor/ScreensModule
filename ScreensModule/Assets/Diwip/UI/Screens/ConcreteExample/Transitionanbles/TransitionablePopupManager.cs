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
        [SerializeField]
        Color backgroundColor;
        List<Type> typesToShow = new List<Type>();
        PopupBackground background;

        protected override void Start()
        {
            base.Start();

            background = gameObject.AddComponent<PopupBackground>();
            background.Initialize(backgroundColor, HandleBGClick);
        }

        private void HandleBGClick()
        {
            if (currentTop != null)
            {
                Hide(currentTop);
            }
        }

        public override void Hide(Type screenType)
        {
            if (currentTop != null)
            {
                if (typesToShow.Count == 0)
                {
                    transitions.Transition(currentTop, null);
                    currentTop = null;
                }

                ShowNext();
            }
        }

        public override void Show(Type screenType)
        {
            if (currentTop == null)
            {
                ShowInstance(screensManager.GetInstanceByType(screenType));
            }
            else
            {
                Enqueue(screenType);
            }
        }

        private void Enqueue(Type screenType)
        {
            var typeToEnque = screensManager.GetInstanceByType(screenType);

            if (typesToShow.Count > 0)
            {
                if (!typesToShow.Contains(screenType))
                {
                    for (int i = 0; i < typesToShow.Count; i++)
                    {
                        var enquedType = screensManager.GetInstanceByType(typesToShow[i]);

                        if (typeToEnque.priority >= enquedType.priority)
                        {
                            typesToShow.Insert(i, screenType);
                            return;
                        }
                    }

                    typesToShow.Add(screenType);
                }
            }
            else 
            {
                typesToShow.Insert(0, screenType);
            }
        }

        protected void ShowInstance(BaseScreen instance)
        {
            transitions.Transition(currentTop, instance);
            currentTop = instance;
            background.Attach(currentTop.transform);
        }

        protected void ShowNext()
        {
            if (typesToShow.Count > 0)
            {
                Type nextType = typesToShow[typesToShow.Count - 1];

                typesToShow.RemoveAt(typesToShow.Count - 1);
                ShowInstance(screensManager.GetInstanceByType(nextType));
            }
        }
    }
}