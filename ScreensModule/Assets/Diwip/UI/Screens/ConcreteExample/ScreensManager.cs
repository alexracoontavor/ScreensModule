using UnityEngine;
using System.Collections;
using Diwip.UI.Screens;
using System;
using Diwip.Tools.Events;

namespace Diwip.UI.Screens.ConcreteExample
{
    public class ScreenManagerEvent : BaseEvent
    {
        public Type HandledType;
        public bool IsShowing = false;
        public Action<BaseScreen> OnComplete;
    }

    /// <summary>
    /// Concrete implementation of a TransitionablesManager
    /// This is the "root" Manager that Screens should register to
    /// Defines the Transition layers of Screen, Popup and HUD
    /// </summary>
    public class ScreensManager : TransitionablesManager
    {
        [SerializeField] private TransitionableManagerBase _screensTransitioner;
        [SerializeField] private TransitionableManagerBase _popupsTransitioner;
        [SerializeField] private TransitionableManagerBase _hudTransitioner;

        protected override void PopulateTransitionManagers()
        {
            TransitionableManagers.Add(ScreenType.Screen, _screensTransitioner);
            TransitionableManagers.Add(ScreenType.Popup, _popupsTransitioner);
            TransitionableManagers.Add(ScreenType.Hud, _hudTransitioner);
        }

        protected override void Start()
        {
            base.Start();

            EventsManager.AddListener(typeof(ScreenManagerEvent), HandleScreenManagerEvent);
        }

        private void HandleScreenManagerEvent(BaseEvent eventObject)
        {
            ScreenManagerEvent e = (ScreenManagerEvent)eventObject;

            if (e.IsShowing)
            {
                Show(e.HandledType);
            }
            else
            {
                Hide(e.HandledType);
            }

            if (e.OnComplete != null)
            {
                e.OnComplete(GetInstanceByType(e.HandledType));
            }
        }
    }
}

