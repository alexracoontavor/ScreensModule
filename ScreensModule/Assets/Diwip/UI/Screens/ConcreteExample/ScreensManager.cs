using UnityEngine;
using System.Collections;
using Diwip.UI.Screens;
using System;

namespace Diwip.UI.Screens.ConcreteExample
{
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
    }
}

