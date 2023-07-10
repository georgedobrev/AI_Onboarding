using System.Diagnostics;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.ResponseModels;

namespace AI_Onboarding.Services.Implementation
{
    public class AIService : IAIService
    {
        public ScriptResponseViewModel RunScript(string relativePath, string argument = "")
        {
            int maxArgumentLength = 30000;

            if (argument.Length <= maxArgumentLength)
            {
                return ExecuteScript(relativePath, argument);
            }
            else
            {
                int argumentParts = (int)Math.Ceiling(argument.Length / (double)maxArgumentLength);
                string output = string.Empty;
                string error = string.Empty;

                for (int i = 0; i < argumentParts; i++)
                {
                    int startIndex = i * maxArgumentLength;
                    int length = Math.Min(maxArgumentLength, argument.Length - startIndex);
                    string partialArgument = argument.Substring(startIndex, length);

                    ScriptResponseViewModel response = ExecuteScript(relativePath, partialArgument);
                    output += response.Output;
                    error += response.ErrorMessage;

                    if (!response.Success)
                        break;
                }

                if (string.IsNullOrEmpty(error))
                {
                    return new ScriptResponseViewModel { Success = true, Output = output };
                }
                else
                {
                    return new ScriptResponseViewModel { Success = false, ErrorMessage = error };
                }
            }
        }

        private ScriptResponseViewModel ExecuteScript(string relativePath, string argument)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/opt/homebrew/Cellar/python@3.11/3.11.4/bin/python3",
                    Arguments = $"\"{relativePath}\" \"{argument.Replace("\"", "\\\"")}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            int exitCode = process.ExitCode;

            if (exitCode == 0)
            {
                return new ScriptResponseViewModel { Success = true, Output = output };
            }
            else
            {
                return new ScriptResponseViewModel { Success = false, ErrorMessage = error };
            }
        }
    }
}
