

namespace MauiApp2
{
    public partial class App : Application
    {
        public App()
        {
            Logic logic = new Logic();
            logic.SetStartingSettings();
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}