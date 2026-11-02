using System.Globalization;

namespace CalculatorApp.Services;

/// <summary>
/// Windows Hesap Makinesi'nin Standart ve Bilimsel modlarının
/// tüm hesaplama, bellek ve hata yönetimi mantığını yürüten motor sınıfı.
/// </summary>
public class CalculatorEngine
{
    // Giriş ve Gösterge Durumları
    public string DisplayResult { get; private set; } = "0";
    public string DisplayExpression { get; private set; } = "";
    public bool HasError { get; private set; } = false;

    // Açı Modu (Standart: Derece / Degree)
    public bool IsDegree { get; set; } = true;

    // Bellek (Memory) Değeri
    public double Memory { get; private set; } = 0;
    public bool HasMemory => Memory != 0;

    // İç Hesaplama Değişkenleri
    private double _firstOperand = 0;
    private string _pendingOperator = "";
    private bool _isNewEntry = true;
    private bool _isCalculated = false;

    public event Action? StateChanged;

    public CalculatorEngine()
    {
        ResetAll();
    }

    /// <summary>
    /// Rakam tuşuna basıldığında çağrılır (0 - 9).
    /// </summary>
    public void AppendDigit(string digit)
    {
        if (HasError)
        {
            ResetAll();
        }

        // Eğer negatif işareti başlatıldıysa ("-" veya "-0")
        if (DisplayResult == "-" || DisplayResult == "-0")
        {
            DisplayResult = "-" + digit;
            _isNewEntry = false;
            _isCalculated = false;
        }
        else if (_isNewEntry || _isCalculated)
        {
            DisplayResult = digit;
            _isNewEntry = false;
            _isCalculated = false;
        }
        else
        {
            if (DisplayResult == "0")
                DisplayResult = digit;
            else if (DisplayResult.Length < 16) // Ekran taşmasını engelleme
                DisplayResult += digit;
        }

        NotifyState();
    }

    /// <summary>
    /// Virgül / Nokta tuşuna basıldığında çağrılır.
    /// </summary>
    public void AppendDecimal()
    {
        if (HasError)
        {
            ResetAll();
        }

        if (DisplayResult == "-" || DisplayResult == "-0")
        {
            DisplayResult = "-0,";
            _isNewEntry = false;
            _isCalculated = false;
        }
        else if (_isNewEntry || _isCalculated)
        {
            DisplayResult = "0,";
            _isNewEntry = false;
            _isCalculated = false;
        }
        else if (!DisplayResult.Contains(','))
        {
            DisplayResult += ",";
        }

        NotifyState();
    }

    /// <summary>
    /// İşaret değiştirme tuşu (+/-).
    /// </summary>
    public void ToggleSign()
    {
        if (HasError) return;

        if (DisplayResult.StartsWith("-"))
        {
            DisplayResult = DisplayResult.Substring(1);
            if (string.IsNullOrEmpty(DisplayResult) || DisplayResult == "0")
                DisplayResult = "0";
        }
        else
        {
            if (DisplayResult == "0" || _isNewEntry)
                DisplayResult = "-0";
            else
                DisplayResult = "-" + DisplayResult;
        }

        NotifyState();
    }

    /// <summary>
    /// Geri silme (Backspace / ⌫) tuşu.
    /// </summary>
    public void Backspace()
    {
        if (HasError || _isCalculated || _isNewEntry) return;

        if (DisplayResult.Length > 1)
        {
            DisplayResult = DisplayResult.Substring(0, DisplayResult.Length - 1);
            if (DisplayResult == "-") DisplayResult = "0";
        }
        else
        {
            DisplayResult = "0";
        }

        NotifyState();
    }

    /// <summary>
    /// C (Clear) tuşu: Tüm işlem geçmişini ve ekranı sıfırlar.
    /// </summary>
    public void ResetAll()
    {
        DisplayResult = "0";
        DisplayExpression = "";
        _firstOperand = 0;
        _pendingOperator = "";
        _isNewEntry = true;
        _isCalculated = false;
        HasError = false;
        NotifyState();
    }

    /// <summary>
    /// CE (Clear Entry) tuşu: Sadece ekrandaki son girilen sayıyı sıfırlar.
    /// </summary>
    public void ClearEntry()
    {
        if (HasError)
        {
            ResetAll();
            return;
        }

        DisplayResult = "0";
        _isNewEntry = true;
        NotifyState();
    }

    /// <summary>
    /// İkili işlem operatörleri (+, -, ×, ÷, x^y, mod).
    /// </summary>
    public void SetOperator(string op)
    {
        if (HasError) return;

        // Kullanıcı en başta henüz sayı girmeden eksi (-) tuşuna basarsa, bunu negatif sayı girişi yap:
        if ((op == "-" || op == "−") && _isNewEntry && string.IsNullOrEmpty(_pendingOperator) && !_isCalculated && (DisplayResult == "0" || string.IsNullOrEmpty(DisplayResult)))
        {
            DisplayResult = "-";
            _isNewEntry = false;
            NotifyState();
            return;
        }

        double currentVal = ParseCurrent(DisplayResult);

        if (!string.IsNullOrEmpty(_pendingOperator) && !_isNewEntry)
        {
            // Bekleyen önceki işlemi tamamla (Örn: 5 + 3 + basıldığında 8 yapar)
            ExecutePendingOperation(currentVal);
            if (HasError) return;
            currentVal = _firstOperand;
        }
        else
        {
            _firstOperand = currentVal;
        }

        _pendingOperator = op;
        DisplayExpression = $"{FormatNumber(_firstOperand)} {FormatOperator(op)}";
        _isNewEntry = true;
        _isCalculated = false;

        NotifyState();
    }

    /// <summary>
    /// Eşittir (=) tuşu: Bekleyen işlemi sonuçlandırır.
    /// </summary>
    public void CalculateEquals()
    {
        if (HasError || string.IsNullOrEmpty(_pendingOperator)) return;

        double secondOperand = ParseCurrent(DisplayResult);
        DisplayExpression = $"{FormatNumber(_firstOperand)} {FormatOperator(_pendingOperator)} {FormatNumber(secondOperand)} =";

        ExecutePendingOperation(secondOperand);

        _pendingOperator = "";
        _isNewEntry = true;
        _isCalculated = true;

        NotifyState();
    }

    /// <summary>
    /// Tek operandlı (Unary) matematiksel fonksiyonlar.
    /// Karekök, kare, 1/x, trigonometri, logaritma, faktöriyel vb.
    /// </summary>
    public void PerformUnary(string function)
    {
        if (HasError) return;

        double value = ParseCurrent(DisplayResult);
        double result = 0;
        string exprFormat = "";

        switch (function)
        {
            // Standart Mod Fonksiyonları
            case "sqrt": // 2√x
                if (value < 0)
                {
                    SetError("Geçersiz giriş");
                    return;
                }
                result = Math.Sqrt(value);
                exprFormat = $"√({FormatNumber(value)})";
                break;

            case "sqr": // x²
                result = Math.Pow(value, 2);
                exprFormat = $"sqr({FormatNumber(value)})";
                break;

            case "recip": // 1/x
                if (value == 0)
                {
                    SetError("Sıfıra bölünemez");
                    return;
                }
                result = 1.0 / value;
                exprFormat = $"1/({FormatNumber(value)})";
                break;

            case "percent": // %
                result = (_firstOperand != 0) ? (_firstOperand * value / 100.0) : (value / 100.0);
                exprFormat = $"{FormatNumber(result)}";
                break;

            // Bilimsel Mod Fonksiyonları
            case "sin":
                double sinAngle = IsDegree ? (value * Math.PI / 180.0) : value;
                result = Math.Sin(sinAngle);
                // Çok küçük sayıları (örneğin sin(180) = 0 olmalı) sıfıra yuvarla
                if (Math.Abs(result) < 1e-15) result = 0;
                exprFormat = $"sin({FormatNumber(value)})";
                break;

            case "cos":
                double cosAngle = IsDegree ? (value * Math.PI / 180.0) : value;
                result = Math.Cos(cosAngle);
                if (Math.Abs(result) < 1e-15) result = 0;
                exprFormat = $"cos({FormatNumber(value)})";
                break;

            case "tan":
                double tanAngle = IsDegree ? (value * Math.PI / 180.0) : value;
                // tan(90) tanımsızdır
                if (IsDegree && Math.Abs(value % 180) == 90)
                {
                    SetError("Geçersiz giriş");
                    return;
                }
                result = Math.Tan(tanAngle);
                exprFormat = $"tan({FormatNumber(value)})";
                break;

            case "log": // log10
                if (value <= 0)
                {
                    SetError("Geçersiz giriş");
                    return;
                }
                result = Math.Log10(value);
                exprFormat = $"log({FormatNumber(value)})";
                break;

            case "ln": // ln (doğal logaritma)
                if (value <= 0)
                {
                    SetError("Geçersiz giriş");
                    return;
                }
                result = Math.Log(value);
                exprFormat = $"ln({FormatNumber(value)})";
                break;

            case "fact": // n! (Faktöriyel)
                if (value < 0 || value != Math.Floor(value) || value > 170)
                {
                    SetError("Geçersiz giriş");
                    return;
                }
                result = Factorial((int)value);
                exprFormat = $"fact({FormatNumber(value)})";
                break;

            case "abs": // |x| Mutlak Değer
                result = Math.Abs(value);
                exprFormat = $"abs({FormatNumber(value)})";
                break;

            case "10pow": // 10^x
                result = Math.Pow(10, value);
                exprFormat = $"10^({FormatNumber(value)})";
                break;

            case "exp": // e^x
                result = Math.Exp(value);
                exprFormat = $"e^({FormatNumber(value)})";
                break;

            default:
                return;
        }

        DisplayExpression = exprFormat;
        DisplayResult = FormatNumber(result);
        _isNewEntry = true;
        _isCalculated = true;

        NotifyState();
    }

    /// <summary>
    /// Pi (π) veya Euler (e) sabitini ekrana yazar.
    /// </summary>
    public void InsertConstant(string constant)
    {
        if (HasError) ResetAll();

        double val = (constant == "pi") ? Math.PI : Math.E;
        DisplayResult = FormatNumber(val);
        _isNewEntry = true;
        _isCalculated = true;

        NotifyState();
    }

    // ========================================================
    // BELLEK (MEMORY) İŞLEVLERİ (MC, MR, M+, M-, MS)
    // ========================================================
    public void MemoryClear()
    {
        Memory = 0;
        NotifyState();
    }

    public void MemoryRecall()
    {
        if (HasError) return;
        DisplayResult = FormatNumber(Memory);
        _isNewEntry = true;
        NotifyState();
    }

    public void MemoryAdd()
    {
        if (HasError) return;
        Memory += ParseCurrent(DisplayResult);
        _isNewEntry = true;
        NotifyState();
    }

    public void MemorySubtract()
    {
        if (HasError) return;
        Memory -= ParseCurrent(DisplayResult);
        _isNewEntry = true;
        NotifyState();
    }

    public void MemoryStore()
    {
        if (HasError) return;
        Memory = ParseCurrent(DisplayResult);
        _isNewEntry = true;
        NotifyState();
    }

    // ========================================================
    // YARDIMCI VE HATA YÖNETİM METOTLARI
    // ========================================================
    private void ExecutePendingOperation(double secondOperand)
    {
        double result = 0;

        switch (_pendingOperator)
        {
            case "+":
                result = _firstOperand + secondOperand;
                break;
            case "-":
                result = _firstOperand - secondOperand;
                break;
            case "*":
            case "×":
                result = _firstOperand * secondOperand;
                break;
            case "/":
            case "÷":
                // 1. ve 7. Ödev Kriteri: Sıfıra bölme hatası yönetimi
                if (secondOperand == 0)
                {
                    SetError("Sıfıra bölünemez");
                    return;
                }
                result = _firstOperand / secondOperand;
                break;
            case "^":
            case "pow":
                result = Math.Pow(_firstOperand, secondOperand);
                break;
            case "mod":
                if (secondOperand == 0)
                {
                    SetError("Sıfıra bölünemez");
                    return;
                }
                result = _firstOperand % secondOperand;
                break;
            default:
                result = secondOperand;
                break;
        }

        if (double.IsInfinity(result) || double.IsNaN(result))
        {
            SetError("Taşma hatası");
            return;
        }

        _firstOperand = result;
        DisplayResult = FormatNumber(result);
    }

    private void SetError(string message)
    {
        HasError = true;
        DisplayResult = message;
        _pendingOperator = "";
        _isNewEntry = true;
        NotifyState();
    }

    private double ParseCurrent(string text)
    {
        if (string.IsNullOrWhiteSpace(text) || text == "-" || text == "-0") return 0;
        // Hem virgülü hem noktayı güvenli parse et
        string clean = text.Replace(".", ",");
        if (double.TryParse(clean, NumberStyles.Any, new CultureInfo("tr-TR"), out double res))
            return res;
        if (double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out double invRes))
            return invRes;
        return 0;
    }

    private string FormatNumber(double val)
    {
        // 15 basamağa kadar yuvarlama ile hassasiyet kontrolü
        val = Math.Round(val, 12);
        string formatted = val.ToString("G15", new CultureInfo("tr-TR"));
        return formatted;
    }

    private string FormatOperator(string op)
    {
        return op switch
        {
            "*" => "×",
            "/" => "÷",
            "pow" => "^",
            _ => op
        };
    }

    private double Factorial(int n)
    {
        if (n <= 1) return 1;
        double res = 1;
        for (int i = 2; i <= n; i++)
            res *= i;
        return res;
    }

    private void NotifyState()
    {
        StateChanged?.Invoke();
    }
}
