using Diwip.UI.Screens;

namespace Diwip.UI.Screens.ConcreteExample
{
    public class TestScreen0 : SimpleScreen
    {
        public override void Close()
        {
            ScreensManager.Hide(GetType());
        }

        public void HideSelf()
        {
            ScreensManager.Hide(GetType());
        }

        public void ShowScreen1()
        {
            ScreensManager.Show(typeof(TestScreen1));
        }
    }
}