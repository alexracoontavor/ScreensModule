using UnityEngine;
using System.Collections;
using System;

namespace Diwip.UI.Screens.ConcreteExample
{
    /// <summary>
    /// A concrete implementation of TransitionableManager that simple shows and hides a single screen, ignoring any other considerations
    /// </summary>
    public class TransitionableHUDManager : TransitionableManagerBase
    {
        public override void Hide(Type screenType)
        {
            BaseScreen to = ScreensManager.GetInstanceByType(screenType);
            Transitions.Transition(to, null);
        }

        public override void Show(Type screenType)
        {
            BaseScreen to = ScreensManager.GetInstanceByType(screenType);
            Transitions.Transition(null, to);
        }
    }
}