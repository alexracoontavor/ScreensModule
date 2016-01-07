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
            if (currentTop != null)
                transitions.Transition(currentTop, null);
        }

        public override void Show(Type screenType)
        {
            BaseScreen to = screensManager.GetInstanceByType(screenType);
            transitions.Transition(currentTop, to);
            currentTop = to;
        }

        protected override void PopulateTransitionManagers()
        {
        }
    }
}