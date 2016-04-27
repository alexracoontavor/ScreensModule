using UnityEngine;
using System.Collections;

/// <summary>
/// Base classes responsible for Transitions between BaseScreens
/// </summary>
namespace Diwip.UI.Screens
{
    /// <summary>
    /// Transition animation definition class base
    /// </summary>
    public abstract class Transition : MonoBehaviour
    {
        private TransitionComplete _onTransitionComplete;

        public abstract void Play<T>(T from, T to, TransitionComplete onTransitionComplete) where T : BaseScreen;
    }

    public delegate void TransitionComplete();

    /// <summary>
    /// Responsible for finding and calling the relevant Transitions on BaseScreens
    /// </summary>
    public class TransitionsManager : MonoBehaviour
    {
        public TransitionComplete OnTransitionComplete;

        private BaseScreen _from;
        private BaseScreen _to;

        public void Transition(BaseScreen from, BaseScreen to)
        {
            if (from == to)
                from = null;

            this._to = to;
            this._from = from;

            Transition transition = null;

            if (to != null)
            {
                to.Show();
                to.Disable();
                transition = to.Transition;
            }

            if (from != null)
            {
                from.Disable();
                transition = from.Transition;
            }

            if (transition != null)
                transition.Play(from, to, OnComplete);
            else
                OnComplete();
        }

        private void OnComplete()
        {
            if (_to != null) _to.Enable();
            if (_from != null) _from.Hide();

            _to = _from = null;

            if (OnTransitionComplete != null)
                OnTransitionComplete();
        }
    }
}