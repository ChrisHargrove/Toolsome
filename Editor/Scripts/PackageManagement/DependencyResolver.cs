using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Toolsome.Editor.PackageManagement
{
    [InitializeOnLoad]
    internal static class DependencyResolver
    {
        private const string CachedPackagesDirectory = "./Library/PackageCache";
        private const string LocalPackagesDirectory = "./Packages";
        private const string ManifestPath = LocalPackagesDirectory + "manifest.json";

        static DependencyResolver()
        {
            Resolve();
        }

        public static void Resolve()
        {
            if (TryGetUnresolvedGitDependencies(out var unresolvedGitDependencies))
            {
                try
                {
                    AssetDatabase.StartAssetEditing();

                    Debug.Log($"Starting to resolve not installed Git Dependencies.");

                    if (TryLoadManifest(out ManifestInfo manifest))
                    {
                        List<KeyValuePair<string, string>> dependencies = manifest.Dependencies.ToList();

                        foreach (KeyValuePair<string, string> item in unresolvedGitDependencies)
                        {
                            Debug.Log($"Dependency: {item.Key} : {item.Value}");
                            dependencies.Insert(0, new KeyValuePair<string, string>(item.Key, item.Value));
                        }

                        manifest.Dependencies = dependencies.Distinct().ToDictionary(pair => pair.Key, pair => pair.Value);
                        SaveManifest(manifest);

                        Debug.Log($"Git Dependencies resolved.");
                    }
                }
                finally
                {
                    AssetDatabase.StopAssetEditing();
                    AssetDatabase.Refresh();
                }
            }
        }

        private static bool TryGetUnresolvedGitDependencies(out KeyValuePair<string, string>[] result)
        {
            if (TryGetInstalledPackages(out var installedPackages)
                && TryGetGitDependencies(installedPackages, out var gitDependencies))
            {
                result = gitDependencies
                    .Where(x => !installedPackages.Exists(i => i.Name.Equals(x.Key)))
                    .ToArray();

                return result?.Length > 0;
            }

            result = default;
            return false;
        }

        private static bool TryGetGitDependencies(List<PackageInfo> installedPackages, out KeyValuePair<string, string>[] result)
        {
            result = installedPackages
                .Where(x => !x.Equals(default) && x.GitDependencies?.Count > 0)
                .SelectMany(x => x.GitDependencies)
                .Where(x => !string.IsNullOrEmpty(x.Value))
                .ToArray();

            return result?.Length > 0;
        }

        private static bool TryGetInstalledPackages(out List<PackageInfo> result)
        {
            result = Directory.GetDirectories(CachedPackagesDirectory)
                .Concat(Directory.GetDirectories(LocalPackagesDirectory))
                .Select(GetPackageFromDirectory)
                .Where(x => !x.Equals(default))
                .ToList();

            return result?.Count > 0;
        }

        private static PackageInfo GetPackageFromDirectory(string path)
        {
            string fullPath = Path.Combine(path, "package.json");
            if (!File.Exists(fullPath))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<PackageInfo>(File.ReadAllText(fullPath));
        }

        private static bool TryLoadManifest(out ManifestInfo manifest)
        {
            if (!File.Exists(ManifestPath))
            {
                manifest = null;
                return false;
            }
            manifest = JsonConvert.DeserializeObject<ManifestInfo>(File.ReadAllText(ManifestPath));
            return true;
        }

        private static void SaveManifest(ManifestInfo manifest)
        {
            File.WriteAllText(ManifestPath, JsonConvert.SerializeObject(manifest));
        }
    }
}
