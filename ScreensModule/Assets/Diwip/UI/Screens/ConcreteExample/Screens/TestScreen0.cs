using Diwip.UI.Screens;

namespace Diwip.UI.Screens.ConcreteExample
{
    public class TestScreen0 : SimpleScreen
    {
        public override void Close()
        {
            screensManager.Hide(GetType());
        }

        public void HideSelf()
        {
            screensManager.Hide(GetType());
        }

        public void ShowScreen1()
        {
            screensManager.Show(typeof(TestScreen1));
        }
    }
}