using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace BrightnessController
{
    class Program
    {
        static void Main(string[] args)
        {
            // Parse
            string dbPath = args[0];
            string sign = args[1];
            double delta = double.Parse(args[2]);
            // Read
            StreamReader reader = new(dbPath);
            double currentBrightness = double.Parse(reader.ReadToEnd());
            reader.Close();
            // Calculate
            delta = sign == "+" ? delta : -delta;
            double newBrightness = Math.Clamp(currentBrightness + delta, 0.2, 1);
            // Set
            string arguments = $"--output DVI-D-0 --brightness {newBrightness}";
            ProcessStartInfo startInfo = new () { FileName = "xrandr", Arguments = arguments, }; 
            Process proc = new () { StartInfo = startInfo, };
            proc.Start();
            // Save
            StreamWriter writer = new(dbPath);
            writer.Write(newBrightness.ToString("F1"));
            writer.Close();
        }
    }
}
