using System;
using System.Reflection;
using System.Resources;
namespace WebToEpubKindle.Core.Properties
{
    public static class CoreStrings
    {
        private static readonly ResourceManager _resourceManager 
            = new ResourceManager("namespace WebToEpubKindle.Core.Properties.CoreStrings", typeof(CoreStrings).GetTypeInfo().Assembly);

        public static string NullPage => GetString("NullPage");

        private static string GetString(string name, params  string[] formatterNames)
        {
            var value = _resourceManager.GetString(name);
            for(var i =0; i<formatterNames.Length;i++)
            {
                value = value.Replace("{" + formatterNames[i] + "}", "{" + i + "}");
            }
            return value;
        }
    }
} 