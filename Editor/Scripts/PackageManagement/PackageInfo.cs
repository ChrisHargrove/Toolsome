using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

namespace Toolsome.Editor.PackageManagement
{
    [Serializable]
    internal class PackageInfo
    {
        public string Name { get; }
        public string Version { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public string DocumentationUrl { get; }
        public string ChangelogUrl { get; }
        public IDictionary<string, string> Dependencies { get; }
        public IDictionary<string, string> GitDependencies { get; }
        public List<string> Keywords { get; }
        public PackageAuthor Author { get; }
    }

    [Serializable]
    internal class PackageAuthor
    {
        public string Name { get; }
        public string Email { get; }
        public Uri Url { get; set; }
    }
}