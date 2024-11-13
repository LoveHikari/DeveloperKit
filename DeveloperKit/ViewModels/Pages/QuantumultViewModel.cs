using System.Text.RegularExpressions;
using DeveloperKit.Models.Pages;
using System.Windows.Input;
using Hikari.Mvvm.Command;

namespace DeveloperKit.ViewModels.Pages;

public class QuantumultViewModel
{
    public QuantumultModel Model { get; }

    public QuantumultViewModel()
    {
        Model = new QuantumultModel();
    }

    public ICommand ConvertCommand => new DelegateCommand(o =>
    {
        Model.ShadowrocketText = ConvertQuantumultXToShadowrocket(Model.QuantumultXText);
    });
    private string ConvertQuantumultXToShadowrocket(string input)
    {
        // 按行处理
        var lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        string hostnameResult = "[MITM]\n";
        string scriptResult = "[Script]\n";
        string rewriteResult = "[URL Rewrite]\n";

        string str = "";
        // 逐行解析
        for (int i = 0; i < lines.Length; i++)
        {
            var trimmedLine = lines[i].Trim();

            if (Regex.IsMatch(trimmedLine, @"// @ScriptName\s+(.*?)$"))
            {
                var match = Regex.Match(trimmedLine, @"// @ScriptName\s+(.*?)$");
                string s = match.Groups[1].Value.Trim();
                str += $"#!name={s}\n";
                str += $"#!desc={s}\n";
            }
            else if (Regex.IsMatch(trimmedLine, @"// @Author\s+(.*?)$"))
            {
                var match = Regex.Match(trimmedLine, @"// @Author\s+(.*?)$");
                string s = match.Groups[1].Value.Trim();
                str += $"#!author={s}\n";
            }


            // 解析hostname行
            if (Regex.IsMatch(trimmedLine, @"^hostname\s=\s(.*?)$"))
            {
                var match = Regex.Match(trimmedLine, @"^hostname\s=\s(.*?)$");
                string scriptPath = match.Groups[1].Value.Trim();
                hostnameResult += $"hostname = %APPEND% {scriptPath}" + "\n";
            }
            // 解析script-response-body行
            else if (Regex.IsMatch(trimmedLine, @"^(.*?)\surl\s+script-response-body\s+(.*?)$"))
            {
                var previousLine = lines[i - 1].Trim();  // 上一行说明行
                var match = Regex.Match(trimmedLine, @"^(.*?)\surl\s+script-response-body\s+(.*?)$");
                var m = Regex.Match(previousLine, @"^# > (.*?)$");
                if (match.Success)
                {
                    string name = m.Groups[1].Value.Trim();
                    string pattern = match.Groups[1].Value.Trim();
                    string scriptPath = match.Groups[2].Value.Trim();
                    string shadowrocketScript = $"{name}=type=http-response,pattern={pattern},requires-body=1,script-path={scriptPath}";
                    scriptResult += shadowrocketScript + "\n";
                }
            }
            // 解析url规则
            else if (Regex.IsMatch(trimmedLine, @"^(.*?)\surl\s+(.*)$"))
            {
                var previousLine = lines[i - 1].Trim();  // 上一行说明行
                var match = Regex.Match(trimmedLine, @"^(.*?)\surl\s+(.*)$");
                var m = Regex.Match(previousLine, @"^# > (.*?)$");
                if (match.Success)
                {
                    string name = m.Groups[1].Value.Trim();
                    string pattern = match.Groups[1].Value.Trim();
                    string action = match.Groups[2].Value.Trim(); // 保留url后面的部分
                    string shadowrocketRewrite = $"{pattern} - {action}";
                    rewriteResult += $"# ～ {name}\n{shadowrocketRewrite}\n";
                }
            }
        }

        // 组合所有部分
        return $"{str}\n{rewriteResult}\n{scriptResult}\n{hostnameResult}";
    }

}