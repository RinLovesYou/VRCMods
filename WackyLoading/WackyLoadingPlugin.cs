using MelonLoader;
using System;
using System.IO;
using System.Linq;

namespace VRCMods
{
    public class WackyLoading : MelonPlugin
    {
        public static MelonLogger.Instance MyLogger => new("WackyLoading", System.ConsoleColor.Cyan);

        internal static bool IsInVR = false;

        internal static string ModsPath = $"{Directory.GetCurrentDirectory()}/Mods";

        public override void OnApplicationStart()
        {
            if (!Directory.Exists("Mods/Desktop"))
                Directory.CreateDirectory("Mods/Desktop");

            if (!Directory.Exists("Mods/VR"))
                Directory.CreateDirectory("Mods/VR");

            IsInVR = !Environment.CommandLine.Contains("no-vr");

            string vrOrDesktop = IsInVR ? "VR" : "Desktop";

            ModsPath = $"{ModsPath}/{vrOrDesktop}";

            MyLogger.Msg($"Loading {vrOrDesktop} Mods!");

            ScanMods();
        }

        public void ScanMods()
        {
            LoadModsFolder(ModsPath);
        }

        public void LoadModsFolder(string path)
        {
            string[] dir = Directory.GetFiles(ModsPath);

            foreach (string file in dir)
            {
                if (!file.EndsWith(".dll"))
                    continue;

                MelonHandler.LoadFromFile(file);

                string modName = file.Split('\\').Last().Replace(".dll", "");

                MyLogger.Msg($"Loaded {modName}!");
            }

        }
    }
}
