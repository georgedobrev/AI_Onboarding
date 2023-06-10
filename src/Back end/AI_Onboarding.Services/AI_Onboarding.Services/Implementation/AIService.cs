using System;
using System.Data;
using System.Diagnostics;
using AI_Onboarding.Services.Interfaces;

namespace AI_Onboarding.Services.Implementation
{
    public class AIService : IAIService
    {
        public string RunScript(string relativePath, string argument)
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
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();

            string output = process.StandardOutput.ReadToEnd();

            process.WaitForExit();

            return output;
        }
    }
}

