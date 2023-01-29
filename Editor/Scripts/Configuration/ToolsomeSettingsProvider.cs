using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Toolsome.Editor.Configuration
{
    public class ToolsomeSettingsProvider : SettingsProvider
    {
        public ToolsomeSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path, scopes, keywords)
        {
        }

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new ToolsomeSettingsProvider("Tools/Toolsome", SettingsScope.Project);
        }
    }
}