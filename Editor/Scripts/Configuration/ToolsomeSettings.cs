using System;
using UnityEditor;
using UnityEngine;

namespace Toolsome.Editor.Configuration
{
    [FilePath("ProjectSettings/ToolsomeSettings.asset", FilePathAttribute.Location.ProjectFolder)]
    public class ToolsomeSettings : ScriptableSingleton<ToolsomeSettings>
    {
        private readonly string[] toolsomePackages = {
        };
        public string[] ToolsomePackages => toolsomePackages;

        [SerializeField]
        private bool autoUpdatePackages;
        public bool AutoUpdatePackages
        {
            get => autoUpdatePackages;
            set => SetAndSave(ref autoUpdatePackages, value);
        }

        private void SetAndSave<T>(ref T field, T value)
        {
            if (field.Equals(value)) return;
                
            field = value;
            Save(false);
        }
    }
}