using System;

namespace Diwip.UI.Screens.ConcreteExample
{
    /// <summary>
    /// A concrete implementation of a TransitionableManagerBase that shows 1 screen at a time
    /// </summary>
    public class TransitionableScreensManager : TransitionableManagerBase
    {
        public override void Hide(Type screenType)
        {
            if (CurrentTop != null)
                Transitions.Transition(CurrentTop, null);
        }

        public override void Show(Type screenType)
        {
            BaseScreen to = ScreensManager.GetInstanceByType(screenType);
            Transitions.Transition(CurrentTop, to);
            CurrentTop = to;
        }

        protected override void PopulateTransitionManagers()
        {
        }
    }
}