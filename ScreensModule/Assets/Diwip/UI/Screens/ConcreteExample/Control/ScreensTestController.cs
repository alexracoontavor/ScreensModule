using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace Diwip.UI.Screens.ConcreteExample
{
    /// <summary>
    /// A test controller for this concrete implementation
    /// Finds all BaseScreens
    /// User can select a Screen by type and call Open or Close on it
    /// ScreensTestController itself is part of the HUD transitions layer
    /// </summary>
    public class ScreensTestController : SimpleScreen
    {
        public Dropdown Dropdown;
        private BaseScreen[] _screens;

        private bool _isOpened;
        private int _selectedIndex = 0;

        protected override void Start()
        {
            base.Start();

            BaseScreen[] screens = GameObject.FindObjectsOfType<BaseScreen>();
            Array.Resize<BaseScreen>(ref screens, screens.Length +1);
            screens[screens.Length - 1] = this;

            PopulateDropdown(screens);

            Show();
        }

        private void PopulateDropdown(BaseScreen[] screens)
        {
            this._screens = screens;

            List<Dropdown.OptionData> data = new List<Dropdown.OptionData>();

            for (int i = 0; i < screens.Length; i++)
            {
                string s = screens[i].GetType().ToString();
                s = s.Substring(s.LastIndexOf(".") + 1);

                data.Add(new Dropdown.OptionData(s));
            }

            Dropdown.AddOptions(data);
        }

        public void ItemSelected()
        {
            _selectedIndex = Dropdown.value;
        }

        public void ToggleOpened()
        {            
            if (!_isOpened)
                ScreensManager.Show(typeof(ScreensTestController));
            else
                ScreensManager.Hide(typeof(ScreensTestController));
        }

        public override void Show()
        {
            base.Show();
            _isOpened = true;
        }

        public override void Hide()
        {
            base.Hide();
            _isOpened = false;
        }

        public void OpenSelected()
        {
            ScreensManager.Show(_screens[_selectedIndex].GetType());
        }

        public void CloseSelected()
        {
            ScreensManager.Hide(_screens[_selectedIndex].GetType());
        }
    }
}