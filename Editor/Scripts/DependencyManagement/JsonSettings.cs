using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Toolsome.Editor.DependencyManagement
{
    internal static class JsonSettings
    {
        private static JsonSerializerSettings defaultSettings;
        internal static JsonSerializerSettings DefaultSettings => defaultSettings ??= CreateDefaultSettings();

        private static JsonSerializerSettings CreateDefaultSettings()
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            return new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            };
        }
    }
}