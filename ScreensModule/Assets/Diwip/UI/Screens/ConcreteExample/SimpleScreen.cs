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
        public CanvasGroup canvasGroup;

        [SerializeField]
        protected Animator animator;

        protected Action animFinishedCallback;
        protected TransitionComplete onTransitionComplete;

        protected Canvas canvas;
        protected GraphicRaycaster graphicRaycaster;

        protected string currentAnimName = "";
        protected string inAnimName = "In";
        protected string outAnimName = "Out";
        protected int currNormalizedTime = 0;

        /// <summary>
        /// Gets the instances / pipeline connections needed for the concrete implementation
        /// </summary>
        protected override void Start()
        {
            canvas = transform.GetOrAddComponent<Canvas>();
            canvas.overrideSorting = false;
            graphicRaycaster = transform.GetOrAddComponent<GraphicRaycaster>();
            canvasGroup = transform.GetOrAddComponent<CanvasGroup>();

            base.Start();
        }

        protected void Update()
        {
            if (currentAnimName != "")
            {
                CheckAnimFinished();
            }
        }

        private void CheckAnimFinished()
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

            if (info.IsName(currentAnimName))
            {
                if (currNormalizedTime == -1)
                    currNormalizedTime = (int)animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

                if ((int)info.normalizedTime > currNormalizedTime)
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
            screensManager.Hide(GetType());
        }

        /// <summary>
        /// Disables UI raycasting on this gameObject
        /// </summary>
        public override void Disable()
        {
            graphicRaycaster.enabled = canvasGroup.blocksRaycasts = canvasGroup.interactable = false;
        }

        /// <summary>
        /// Enables UI raycasting on this gameObject
        /// </summary>
        public override void Enable()
        {
            graphicRaycaster.enabled = canvasGroup.blocksRaycasts = canvasGroup.interactable = true;
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

            currentAnimName = "In";

            animFinishedCallback = Enable;

            this.onTransitionComplete = onTransitionComplete;

            if (animator != null)
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
            if (animator != null)
            {
                animator.Play(currentAnimName, 0, 0.0f);
                currNormalizedTime = -1;
            }
        }

        /// <summary>
        /// Starts transition to invisible
        /// </summary>
        /// <param name="onTransitionComplete">Callback for when transition is complete</param>
        public override void TransitionOut(TransitionComplete onTransitionComplete)
        {
            Disable();

            currentAnimName = "Out";

            animFinishedCallback = Hide;

            this.onTransitionComplete = onTransitionComplete;

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
            currentAnimName = "";

            if (animFinishedCallback != null)
            {
                animFinishedCallback();
                animFinishedCallback = null;
            }

            if (onTransitionComplete != null)
            {
                onTransitionComplete();
                onTransitionComplete = null;
            }
        }
    }
}
