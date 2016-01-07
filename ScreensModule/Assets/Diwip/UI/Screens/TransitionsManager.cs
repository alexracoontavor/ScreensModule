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
        TransitionComplete onTransitionComplete;

        public abstract void Play<T>(T from, T to, TransitionComplete onTransitionComplete) where T : BaseScreen;
    }

    public delegate void TransitionComplete();

    /// <summary>
    /// Responsible for finding and calling the relevant Transitions on BaseScreens
    /// </summary>
    public class TransitionsManager : MonoBehaviour
    {
        public TransitionComplete onTransitionComplete;

        BaseScreen from, to;

        public void Transition(BaseScreen from, BaseScreen to)
        {
            this.to = to;
            this.from = from;

            Transition transition = null;

            if (to != null)
            {
                to.Show();
                to.Disable();
                transition = to.transition;
            }

            if (from != null)
            {
                from.Disable();
                transition = from.transition;
            }

            if (transition != null)
                transition.Play(from, to, OnComplete);
            else
                OnComplete();
        }

        void OnComplete()
        {
            if (to != null) to.Enable();
            if (from != null) from.Hide();

            to = from = null;

            if (onTransitionComplete != null)
                onTransitionComplete();
        }
    }
}