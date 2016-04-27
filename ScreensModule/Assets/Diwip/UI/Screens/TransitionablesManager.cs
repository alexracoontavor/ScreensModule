using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// This namespace contains the abstract base classes of Screens mechanisms
/// </summary>
namespace Diwip.UI.Screens
{
    public enum ScreenType { Screen, Popup, Hud };

    /// <summary>
    /// The Client abstract base
    /// Concrete implementations should inherit from this
    /// </summary>
    public abstract class BaseScreen : MonoBehaviour
    {
        public bool IsHideOnLoad = true;
        public int Priority = 0;
        public ScreenType ScreenType = ScreenType.Screen;

        public Transition Transition;

        public TransitionablesManager ScreensManager;

        protected virtual void Start()
        {
            Initialize();
        }

        protected virtual void OnDestroy()
        {
            Deinitialize();
        }

        /// <summary>
        /// Pipeline initialization
        /// </summary>
        protected void Initialize()
        {
            if (ScreensManager == null)
                ScreensManager = FindObjectOfType<TransitionablesManager>();

            if (ScreensManager == null)
                throw new System.Exception(GetType().ToString() + " could not find ScreensManager!");

            ScreensManager.Register(GetType(), this);
        }

        protected void Deinitialize()
        {
            if (ScreensManager != null)
            {
                ScreensManager.Deregister(GetType());
            }
        }

        /// <summary>
        /// Called when transition in is complete
        /// </summary>
        public abstract void Show();

        /// <summary>
        /// Called when transition out is complete
        /// </summary>
        public abstract void Hide();

        /// <summary>
        /// Should be used to make the screen unresponsive to input
        /// </summary>
        public abstract void Enable();

        /// <summary>
        /// Should be used to make the screen responsive to input
        /// </summary>
        public abstract void Disable();

        /// <summary>
        /// For playing individual transition ending in screen being visible and active
        /// </summary>
        /// <param name="onTransitionComplete">Should be called when the transition is complete</param>
        public abstract void TransitionIn(TransitionComplete onTransitionComplete);

        /// <summary>
        /// For playing individual transition ending in screen being invsible and inactive
        /// </summary>
        /// <param name="onTransitionComplete">Should be called when the transition is complete</param>
        public abstract void TransitionOut(TransitionComplete onTransitionComplete);
    }


    /// <summary>
    /// Base class responsible for generating Screens-TransitionManager interactions
    /// </summary>
    public abstract class TransitionableManagerBase : MonoBehaviour
    {
        /// <summary>
        /// Instance of class responsible for transitions
        /// </summary>
        [SerializeField]
        protected TransitionsManager Transitions;

        /// <summary>
        /// The manager responsible for distributing screens among sub-managers
        /// </summary>
        [SerializeField]
        protected TransitionablesManager ScreensManager;

        protected BaseScreen CurrentTop;

        protected void Awake()
        {
            PopulateTransitionManagers();
        }

        protected virtual void PopulateTransitionManagers()
        {
        }

        protected virtual void Start()
        {
            if (Transitions == null)
                Transitions = GameObject.FindObjectOfType<TransitionsManager>();

            if (ScreensManager == null)
                ScreensManager = GameObject.FindObjectOfType<TransitionablesManager>();
        }

        public abstract void Show(System.Type screenType);
        public void Show<T>(T screen) where T : BaseScreen
        {
            Show(typeof(T));
        }

        public abstract void Hide(System.Type screenType);
        public void Hide<T>(T screen) where T : BaseScreen
        {
            Hide(typeof(T));
        }
    }

    /// <summary>
    /// Class responsible for distributing calls among TransitionableManagers
    /// </summary>
    public abstract class TransitionablesManager : TransitionableManagerBase
    {
        protected Dictionary<System.Type, BaseScreen> TypeToInstanceTable = new Dictionary<Type, BaseScreen>();
        protected Dictionary<ScreenType, TransitionableManagerBase> TransitionableManagers = new Dictionary<ScreenType, TransitionableManagerBase>();

        /// <summary>
        /// Finds the appropriate manager and starts transition process to hide the screen
        /// </summary>
        /// <param name="screenType">The screen to hide in this transition</param>
        public override void Hide(Type screenType)
        {
            if (TypeToInstanceTable.ContainsKey(screenType))
            {
                TransitionableManagers[TypeToInstanceTable[screenType].ScreenType].Hide(screenType);
            }
        }

        /// <summary>
        /// Finds the appropriate manager and starts transition process to show the screen
        /// </summary>
        /// <param name="screenType">The screen type to show in this transition</param>
        public override void Show(Type screenType)
        {
            if (TypeToInstanceTable.ContainsKey(screenType))
            {
                TransitionableManagers[TypeToInstanceTable[screenType].ScreenType].Show(screenType);
            }
        }

        /// <summary>
        /// Helper function for getting an instance of screen by its type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public BaseScreen GetInstanceByType(Type type)
        {
            if (TypeToInstanceTable.ContainsKey(type))
            {
                return TypeToInstanceTable[type];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Registers a screen instance by type. This is called by BaseScreen
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">Explicit System.Type of the screen</param>
        /// <param name="screen">screen instance</param>
        internal void Register<T>(Type type, T screen) where T : BaseScreen
        {
            if (!TypeToInstanceTable.ContainsKey(type))
            {
                TypeToInstanceTable[type] = screen;

                if (screen.IsHideOnLoad)
                    screen.Hide();
                else
                    screen.Show();
            }
        }

        /// <summary>
        /// Deregisters a screen. Called by BaseScreen.
        /// </summary>
        /// <param name="type">Explicit System.Type of the screen</param>
        internal void Deregister(Type type)
        {
            if (TypeToInstanceTable.ContainsKey(type))
            {
                TypeToInstanceTable.Remove(type);
            }
        }
    }
}