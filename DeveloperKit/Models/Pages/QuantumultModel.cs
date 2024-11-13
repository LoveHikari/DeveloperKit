using Hikari.Mvvm;

namespace DeveloperKit.Models.Pages;

public class QuantumultModel : NotificationObject
{
    private string _quantumultXText;
    public string QuantumultXText
    {
        get => _quantumultXText;
        set
        {
            _quantumultXText = value; NotifyPropertyChanged();
        }
    }
    private string _shadowrocketText;
    public string ShadowrocketText
    {
        get => _shadowrocketText;
        set
        {
            _shadowrocketText = value; NotifyPropertyChanged();
        }
    }
}