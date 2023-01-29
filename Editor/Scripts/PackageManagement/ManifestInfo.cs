using System;
using System.Collections.Generic;

namespace Toolsome.Editor.PackageManagement
{
    [Serializable]
    internal class ManifestInfo
    {
        public IDictionary<string, string> Dependencies { get; set; }


    }

}