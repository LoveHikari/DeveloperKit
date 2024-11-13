using System.Windows.Input;
using DeveloperKit.Models;
using Hikari.Mvvm.Command;

namespace DeveloperKit.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowModel Model { get; set; }

        public MainWindowViewModel()
        {
            this.Model = new MainWindowModel();
        }
        public ICommand JavaConfigCommand
        {
            get
            {
                return new DelegateCommand<object>((obj) =>
                {
                    //Views.JAVAConfig.Window1 view = new Views.JAVAConfig.Window1();
                    //view.DataContext = new JavaConfigViewModel();
                    //view.ShowDialog();
                });
            }
        }

        public ICommand QuantumultToShadowrocketCommand
        {
            get
            {
                return new DelegateCommand<object>((obj) =>
                {
                    Model.FrameUri = new Uri("Views\\Pages\\QuantumultPage.xaml", UriKind.RelativeOrAbsolute);
                });
            }
        }
    }
}