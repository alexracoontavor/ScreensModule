using UnityEngine;
using System.Collections;

namespace Diwip.UI.Screens.ConcreteExample
{
    public class TestScreen1 : SimpleScreen
    {
        public void ShowScreen0()
        {
            screensManager.Show(typeof(TestScreen0));
        }
    }
}