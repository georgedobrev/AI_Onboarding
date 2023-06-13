using System;
using System.Data;
using System.Diagnostics;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.ResponseModels;

namespace AI_Onboarding.Services.Implementation
{
    public class AIService : IAIService
    {
        public ScriptResponseViewModel RunScript(string relativePath, string argument)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string fullPythonFilePath = System.IO.Path.Combine(baseDirectory, relativePath);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/opt/homebrew/Cellar/python@3.11/3.11.3/bin/python3",
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

