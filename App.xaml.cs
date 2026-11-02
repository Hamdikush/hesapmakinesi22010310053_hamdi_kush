namespace CalculatorApp;

public partial class App : Application
{
    // Tercih edilen temanın kaydedileceği anahtar
    public const string ThemePreferenceKey = "UserSelectedTheme";

    public App()
    {
        InitializeComponent();

        // 1. Ödev Gereksinimi: Uygulama açılırken kaydedilen tema Preferences'tan geri yüklenir
        ApplySavedTheme();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell())
        {
            Title = "Hesap Makinesi",
            Width = 420,
            Height = 650
        };
    }

    /// <summary>
    /// Preferences'ta saklanan son temayı okur ve anında uygular.
    /// </summary>
    public static void ApplySavedTheme()
    {
        string themeChoice = Preferences.Default.Get(ThemePreferenceKey, "System");
        SetTheme(themeChoice);
    }

    /// <summary>
    /// Uygulama temasını Açık, Koyu veya Sistem olarak belirler ve Preferences'a kaydeder.
    /// </summary>
    public static void SetTheme(string themeChoice)
    {
        Preferences.Default.Set(ThemePreferenceKey, themeChoice);

        if (Current == null) return;

        switch (themeChoice)
        {
            case "Light":
                Current.UserAppTheme = AppTheme.Light;
                break;
            case "Dark":
                Current.UserAppTheme = AppTheme.Dark;
                break;
            case "System":
            default:
                Current.UserAppTheme = AppTheme.Unspecified; // Sistem temasını takip eder
                break;
        }
    }
}
