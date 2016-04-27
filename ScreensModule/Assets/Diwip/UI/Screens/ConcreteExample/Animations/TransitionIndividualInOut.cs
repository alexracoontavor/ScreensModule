namespace Diwip.UI.Screens.ConcreteExample
{
    public class TransitionIndividualInOut : Transition
    {
        private SimpleScreen _from;
        private SimpleScreen _to;
        private TransitionComplete _onTransitionComplete;

        public override void Play<T>(T from, T to, TransitionComplete onTransitionComplete)
        {
            if (from != null)
            {
                from.TransitionOut(onTransitionComplete);
            }
            if (to != null)
            {
                to.TransitionIn(onTransitionComplete);
            }
        }
    }
}

