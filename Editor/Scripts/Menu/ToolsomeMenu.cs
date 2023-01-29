using UnityEditor;
using UnityEngine;

namespace Toolsome.Editor.Menu
{
    /// <summary>
    /// Class that handles all of the Toolsome Menu setup.
    /// </summary>
    internal static class ToolsomeMenu
    {
        [MenuItem("Tools/Toolsome/Manager")]
        public static void OpenPackageManager()
        {
        }

        [MenuItem("Tools/Toolsome/Settings")]
        public static void OpenSettings()
        {
            SettingsService.OpenProjectSettings("Tools/Toolsome");
        }
    }
}