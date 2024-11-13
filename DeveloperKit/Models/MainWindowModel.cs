using Hikari.Mvvm;

namespace DeveloperKit.Models;

public class MainWindowModel : NotificationObject
{
    private Uri _frameUri;
    public Uri FrameUri
    {
        get => _frameUri;
        set
        {
            _frameUri = value; NotifyPropertyChanged();
        }
    }

}