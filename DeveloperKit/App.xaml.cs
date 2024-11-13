using System.Windows;
using System.Windows.Threading;

namespace DeveloperKit
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Current.DispatcherUnhandledException += App_OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            //CalculatorView view = new CalculatorView();
            //view.DataContext = new CalculatorViewModel();
            //view.Show();
        }
        /// <summary>
        /// 非UI线程抛出全局异常事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var exception = e.ExceptionObject as Exception;
                if (exception != null)
                {
                    //LogHelper.WriteError(exception, "非UI线程全局异常");
                }
            }
            catch (Exception ex)
            {
                //LogHelper.WriteError(ex, "不可恢复的非UI线程全局异常");
                MessageBox.Show("应用程序发生不可恢复的异常，将要退出！");
            }
        }
        /// <summary>
        /// UI线程抛出全局异常事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                //LogHelper.WriteError(e.Exception, "UI线程全局异常");
                e.Handled = true;
            }
            catch (Exception ex)
            {
                //LogHelper.WriteError(ex, "不可恢复的UI线程全局异常");
                MessageBox.Show("应用程序发生不可恢复的异常，将要退出！");
            }
        }
    }
}
