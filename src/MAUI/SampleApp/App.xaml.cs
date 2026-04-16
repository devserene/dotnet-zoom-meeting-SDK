namespace SampleApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }


    protected override void OnStart()
    {
        string jwtToken = ZoomHelper.GenerateJwtToken(AppSettings.SDK_KEY, AppSettings.SDK_SECRET);
        MauiProgram.ZoomSDKService.InitZoomLib(jwtToken);
    }
            
}