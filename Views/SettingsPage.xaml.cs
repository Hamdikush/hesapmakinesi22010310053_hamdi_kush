namespace CalculatorApp.Views;

public partial class SettingsPage : ContentPage
{
    private bool _isInitializing = true;

    public SettingsPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // 6. Ödev Kriteri: Kaydedilmiş temayı Preferences üzerinden oku ve UI'da işaretle
        _isInitializing = true;

        string currentTheme = Preferences.Default.Get(App.ThemePreferenceKey, "System");
        switch (currentTheme)
        {
            case "Light":
                RadioLight.IsChecked = true;
                break;
            case "Dark":
                RadioDark.IsChecked = true;
                break;
            case "System":
            default:
                RadioSystem.IsChecked = true;
                break;
        }

        _isInitializing = false;
    }

    private void OnThemeRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (_isInitializing || !e.Value) return;

        string selectedTheme = "System";

        if (RadioLight.IsChecked)
            selectedTheme = "Light";
        else if (RadioDark.IsChecked)
            selectedTheme = "Dark";
        else if (RadioSystem.IsChecked)
            selectedTheme = "System";

        // Temayı anında uygula ve Preferences ile sakla
        App.SetTheme(selectedTheme);
    }
}
