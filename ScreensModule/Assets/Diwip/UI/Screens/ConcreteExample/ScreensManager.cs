using UnityEngine;
using System.Collections;
using Diwip.UI.Screens;
using System;
using Diwip.Tools.Events;

namespace Diwip.UI.Screens.ConcreteExample
{
    public class ScreenManagerEvent : BaseEvent
    {
        public Type handledType;
        public bool isShowing = false;
        public Action<BaseScreen> onComplete;
    }

    /// <summary>
    /// Concrete implementation of a TransitionablesManager
    /// This is the "root" Manager that Screens should register to
    /// Defines the Transition layers of Screen, Popup and HUD
    /// </summary>
    public class ScreensManager : TransitionablesManager
    {
        [SerializeField]
        TransitionableManagerBase screensTransitioner;
        [SerializeField]
        TransitionableManagerBase popupsTransitioner;
        [SerializeField]
        TransitionableManagerBase hudTransitioner;

        protected override void PopulateTransitionManagers()
        {
            transitionableManagers.Add(ScreenType.Screen, screensTransitioner);
            transitionableManagers.Add(ScreenType.Popup, popupsTransitioner);
            transitionableManagers.Add(ScreenType.HUD, hudTransitioner);
        }

        protected override void Start()
        {
            base.Start();

            EventsManager.AddListener(typeof(ScreenManagerEvent), HandleScreenManagerEvent);
        }

        private void HandleScreenManagerEvent(BaseEvent eventObject)
        {
            ScreenManagerEvent e = (ScreenManagerEvent)eventObject;

            if (e.isShowing)
            {
                Show(e.handledType);
            }
            else
            {
                Hide(e.handledType);
            }

            if (e.onComplete != null)
            {
                e.onComplete(GetInstanceByType(e.handledType));
            }
        }
    }
}

