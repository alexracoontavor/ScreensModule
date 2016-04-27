using UnityEngine;
using System.Collections;

namespace Diwip.UI.Screens.ConcreteExample
{
    public class TestScreen1 : SimpleScreen
    {
        public void ShowScreen0()
        {
            ScreensManager.Show(typeof(TestScreen0));
        }
    }
}