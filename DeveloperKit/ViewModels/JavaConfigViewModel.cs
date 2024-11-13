using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;

namespace DeveloperKit.ViewModels
{
    public class JavaConfigViewModel : ViewModelBase
    {
        private string _jdkVersion;
        private string _jdkPath;
        /// <summary>
        /// jdk版本
        /// </summary>
        public string JdkVersion
        {
            get => _jdkVersion;
            set
            {
                _jdkVersion = value;
                NotifyPropertyChanged("JdkVersion");
            }
        }
        /// <summary>
        /// jdk路径
        /// </summary>
        public string JdkPath
        {
            get => _jdkPath;
            set
            {
                _jdkPath = value;
                NotifyPropertyChanged("JdkPath");
            }
        }

        /// <summary>
        /// 窗口启动时发生
        /// </summary>
        public JavaConfigViewModel()
        {

            bool b = RegistryHelper.IsRegistryExist(RegistryHive.LocalMachine, @"SOFTWARE\JavaSoft\Java Development Kit", RegistryView.Registry64);
            if (b)  // 安装jdk
            {
                string[] subKeys = RegistryHelper.GetSubKeyNames(RegistryHive.LocalMachine, @"SOFTWARE\JavaSoft\Java Development Kit", RegistryView.Registry64);
                if (subKeys != null)
                {
                    _jdkVersion = subKeys[0];
                    _jdkPath = RegistryHelper.GetRegistryData(RegistryHive.LocalMachine, @"SOFTWARE\JavaSoft\Java Development Kit\" + subKeys[0], "JavaHome", RegistryView.Registry64);
                    return;
                }
            }
            _jdkVersion = "没有查找到JDK";
            _jdkPath = "没有查找到JDK路径！";
        }

        /// <summary>
        /// 退出按钮单击时发生
        /// </summary>
        public ICommand ExitCommand
        {
            get
            {
                return new DelegateCommand<object>((obj) =>
                {
                    ((Window)obj).Close();
                });
            }
        }
        /// <summary>
        /// 配置按钮单击时发生
        /// </summary>
        public ICommand DeployCommand
        {
            get
            {
                return new DelegateCommand<object>((obj) =>
                {
                    string jdkPath = (string)obj;

                    if (FileHelper.FindDirectories(jdkPath).Contains("bin"))
                    {
                        MessageBox.Show("选择的文件夹不是JDK目录（如：C:\\Program Files\\Java\\jdk1.8.0_101）");
                        return;
                    }
                    if (Environment.GetEnvironmentVariable("JAVA_HOME", EnvironmentVariableTarget.Machine) != null)
                    {
                        System.SystemHelper.SetVariable("JAVA_HOME", jdkPath);
                        System.SystemHelper.SetVariable("Path", @"%JAVA_HOME%\bin;%JAVA_HOME%\jre\bin");
                        System.SystemHelper.SetVariable("CLASSPATH", @".;%JAVA_HOME%\lib;%JAVA_HOME%\lib\tools.jar");
                    }
                    MessageBox.Show("配置成功！注销或者重启生效！");
                });
            }
        }

        public ICommand DialogCommand
        {
            get
            {
                return new DelegateCommand<object>((obj) =>
                {
                    Label label2 = (Label)obj;
                    System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
                    System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                    label2.Content = dialog.SelectedPath.Trim();
                });
            }
        }
    }
}