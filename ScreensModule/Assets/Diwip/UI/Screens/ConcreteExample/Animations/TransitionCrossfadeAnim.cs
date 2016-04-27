using UnityEngine;

namespace Diwip.UI.Screens.ConcreteExample
{
    public class TransitionCrossfadeAnim : Transition
    {
        [SerializeField] private float _duration = 1f;
        [SerializeField] private iTween.EaseType _easetype = iTween.EaseType.linear;

        private SimpleScreen _from;
        private SimpleScreen _to;
        private TransitionComplete _onTransitionComplete;

        private void Play()
        {
            iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1f, "time", _duration, "easetype", _easetype, "onupdate", "OnUpdate", "oncomplete", "OnComplete"));
        }

        private void OnUpdate(float val)
        {
            if (_from != null) _from.CanvasGroup.alpha = 1f - val;
            if (_to != null) _to.CanvasGroup.alpha = val;
        }

        private void OnComplete()
        {
            if (_onTransitionComplete != null) _onTransitionComplete();
            Reset();
        }

        private void Reset()
        {
            _from = _to = null;
            _onTransitionComplete = null;
        }

        public override void Play<T>(T from, T to, TransitionComplete onTransitionComplete)
        {
            if (from != null)
            {
                this._from = from as SimpleScreen;
                this._from.CanvasGroup.alpha = 1f;
            }
            if (to != null)
            {
                this._to = to as SimpleScreen;
                this._to.CanvasGroup.alpha = 0f;
            }
            this._onTransitionComplete = onTransitionComplete;

            Play();
        }
    }
}

