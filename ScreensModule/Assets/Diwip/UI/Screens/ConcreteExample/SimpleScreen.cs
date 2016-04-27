using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace Diwip.UI.Screens.ConcreteExample
{

    /// <summary>
    /// Concrete implementation of a BaseScreen
    ///     Handles In and Out animations;
    ///     Manages Hide, Show, Disable and Enable commands
    /// </summary>
    public abstract class SimpleScreen : BaseScreen
    {
        public CanvasGroup CanvasGroup;

        [SerializeField]
        protected Animator Animator;

        protected Action AnimFinishedCallback;
        protected TransitionComplete OnTransitionComplete;

        protected Canvas Canvas;
        protected GraphicRaycaster GraphicRaycaster;

        protected string CurrentAnimName = "";
        protected string InAnimName = "In";
        protected string OutAnimName = "Out";
        protected int CurrNormalizedTime = 0;

        /// <summary>
        /// Gets the instances / pipeline connections needed for the concrete implementation
        /// </summary>
        protected override void Start()
        {
            Canvas = transform.GetOrAddComponent<Canvas>();
            Canvas.overrideSorting = true;
            GraphicRaycaster = transform.GetOrAddComponent<GraphicRaycaster>();
            CanvasGroup = transform.GetOrAddComponent<CanvasGroup>();

            base.Start();
        }

        protected void Update()
        {
            if (CurrentAnimName != "")
            {
                CheckAnimFinished();
            }
        }

        private void CheckAnimFinished()
        {
            AnimatorStateInfo info = Animator.GetCurrentAnimatorStateInfo(0);

            if (info.IsName(CurrentAnimName))
            {
                if (CurrNormalizedTime == -1)
                    CurrNormalizedTime = (int)Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

                if ((int)info.normalizedTime > CurrNormalizedTime)
                {
                    DismissAnim();
                }
            }
        }

        /// <summary>
        /// Triggers Hide transition
        /// </summary>
        public virtual void Close()
        {
            ScreensManager.Hide(GetType());
        }

        /// <summary>
        /// Disables UI raycasting on this gameObject
        /// </summary>
        public override void Disable()
        {
            GraphicRaycaster.enabled = CanvasGroup.blocksRaycasts = CanvasGroup.interactable = false;
        }

        /// <summary>
        /// Enables UI raycasting on this gameObject
        /// </summary>
        public override void Enable()
        {
            GraphicRaycaster.enabled = CanvasGroup.blocksRaycasts = CanvasGroup.interactable = true;
        }

        /// <summary>
        /// Changes position in sorting order to back
        /// Disables and deactivates the containing game object
        /// </summary>
        public override void Hide()
        {
            transform.SetAsLastSibling();
            Disable();
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Changes position in sorting order to front
        /// Enables and deactivates the containing game object
        /// </summary>
        public override void Show()
        {
            transform.SetAsFirstSibling();
            Enable();
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Starts transition to vsibile
        /// </summary>
        /// <param name="onTransitionComplete">Callback for when transition is complete</param>
        public override void TransitionIn(TransitionComplete onTransitionComplete)
        {
            Show();
            Disable();

            CurrentAnimName = "In";

            AnimFinishedCallback = Enable;

            this.OnTransitionComplete = onTransitionComplete;

            if (Animator != null)
            {
                PlayAnim();
            }
            else
            {
                DismissAnim();
            }
        }

        private void PlayAnim()
        {
            if (Animator != null)
            {
                Animator.Play(CurrentAnimName, 0, 0.0f);
                CurrNormalizedTime = -1;
            }
        }

        /// <summary>
        /// Starts transition to invisible
        /// </summary>
        /// <param name="onTransitionComplete">Callback for when transition is complete</param>
        public override void TransitionOut(TransitionComplete onTransitionComplete)
        {
            Disable();

            CurrentAnimName = "Out";

            AnimFinishedCallback = Hide;

            this.OnTransitionComplete = onTransitionComplete;

            if (enabled)
            {
                PlayAnim();
            }
            else
                DismissAnim();
        }


        /// <summary>
        /// Called when animation is complete
        /// Calls and resets callbacks, if any
        /// </summary>
        private void DismissAnim()
        {
            CurrentAnimName = "";

            if (AnimFinishedCallback != null)
            {
                AnimFinishedCallback();
                AnimFinishedCallback = null;
            }

            if (OnTransitionComplete != null)
            {
                OnTransitionComplete();
                OnTransitionComplete = null;
            }
        }
    }
}
