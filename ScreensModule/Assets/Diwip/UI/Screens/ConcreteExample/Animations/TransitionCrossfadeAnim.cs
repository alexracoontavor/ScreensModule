using UnityEngine;

namespace Diwip.UI.Screens.ConcreteExample
{
    public class TransitionCrossfadeAnim : Transition
    {
        [SerializeField]
        float duration = 1f;
        [SerializeField]
        iTween.EaseType easetype = iTween.EaseType.linear;

        SimpleScreen from, to;
        TransitionComplete onTransitionComplete;

        void Play()
        {
            iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1f, "time", duration, "easetype", easetype, "onupdate", "OnUpdate", "oncomplete", "OnComplete"));
        }

        void OnUpdate(float val)
        {
            if (from != null) from.canvasGroup.alpha = 1f - val;
            if (to != null) to.canvasGroup.alpha = val;
        }

        void OnComplete()
        {
            if (onTransitionComplete != null) onTransitionComplete();
            Reset();
        }

        private void Reset()
        {
            from = to = null;
            onTransitionComplete = null;
        }

        public override void Play<T>(T from, T to, TransitionComplete onTransitionComplete)
        {
            if (from != null)
            {
                this.from = from as SimpleScreen;
                this.from.canvasGroup.alpha = 1f;
            }
            if (to != null)
            {
                this.to = to as SimpleScreen;
                this.to.canvasGroup.alpha = 0f;
            }
            this.onTransitionComplete = onTransitionComplete;

            Play();
        }
    }
}

