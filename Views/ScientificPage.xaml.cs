using CalculatorApp.Services;

namespace CalculatorApp.Views;

public partial class ScientificPage : ContentPage
{
    private readonly CalculatorEngine _engine;

    public ScientificPage()
    {
        InitializeComponent();

        _engine = new CalculatorEngine();
        _engine.StateChanged += UpdateDisplay;

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            ResultLabel.Text = _engine.DisplayResult;
            ExpressionLabel.Text = _engine.DisplayExpression;
            DegRadButton.Text = _engine.IsDegree ? "DEG" : "RAD";

            if (_engine.HasError)
            {
                ResultLabel.TextColor = (Color)Application.Current!.Resources["ErrorRed"];
            }
            else
            {
                ResultLabel.SetDynamicResource(Label.TextColorProperty, "TextLightPrimary");
            }
        });
    }

    private void OnToggleDegRadClicked(object sender, EventArgs e)
    {
        _engine.IsDegree = !_engine.IsDegree;
        UpdateDisplay();
    }

    private void OnDigitClicked(object sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            _engine.AppendDigit(btn.Text);
        }
    }

    private void OnDecimalClicked(object sender, EventArgs e)
    {
        _engine.AppendDecimal();
    }

    private void OnConstantClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter != null)
        {
            _engine.InsertConstant(btn.CommandParameter.ToString()!);
        }
    }

    private void OnOperatorClicked(object sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            string op = btn.CommandParameter?.ToString() ?? btn.Text;
            _engine.SetOperator(op);
        }
    }

    private void OnUnaryOperatorClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter != null)
        {
            _engine.PerformUnary(btn.CommandParameter.ToString()!);
        }
    }

    private void OnEqualsClicked(object sender, EventArgs e)
    {
        _engine.CalculateEquals();
    }

    private void OnClearAllClicked(object sender, EventArgs e)
    {
        _engine.ResetAll();
    }

    private void OnClearEntryClicked(object sender, EventArgs e)
    {
        _engine.ClearEntry();
    }

    private void OnBackspaceClicked(object sender, EventArgs e)
    {
        _engine.Backspace();
    }

    // Bellek İşlemleri
    private void OnMemoryClearClicked(object sender, EventArgs e) => _engine.MemoryClear();
    private void OnMemoryRecallClicked(object sender, EventArgs e) => _engine.MemoryRecall();
    private void OnMemoryAddClicked(object sender, EventArgs e) => _engine.MemoryAdd();
    private void OnMemorySubtractClicked(object sender, EventArgs e) => _engine.MemorySubtract();
    private void OnMemoryStoreClicked(object sender, EventArgs e) => _engine.MemoryStore();
}
