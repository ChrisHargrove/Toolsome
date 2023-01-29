using System;
using System.Collections.Generic;

namespace Toolsome.Editor.DependencyManagement
{
    [Serializable]
    public class ManifestInfo
    {
        public IDictionary<string, string> Dependencies { get; set; }
    }
}