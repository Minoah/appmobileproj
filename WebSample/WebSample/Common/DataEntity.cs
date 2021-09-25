using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace WebSample.Common
{
    public class DataEntity
    {
        private static Dictionary<string, JObject> _ErrorResource;
        public static Dictionary<string, JObject> ErrorResource
        {
            get
            {
                if (_ErrorResource == null)
                {
                    _ErrorResource = new Dictionary<string, JObject>();
                    List<string> languages = new List<string> { "vi", "en" };
                    foreach (string language in languages)
                    {
                        string folder = Path.Combine(Directory.GetCurrentDirectory(), "Resources/" + language);
                        if (Directory.Exists(folder))
                        {
                            List<string> files = Directory.GetFiles(folder).ToList();
                            foreach (string file in files)
                            {
                                string content = File.ReadAllText(file);
                                ErrorResource.Add(language + "." + Path.GetFileNameWithoutExtension(file), JObject.Parse(content));
                            }
                        }
                    }
                }
                return _ErrorResource;
            }
        }
        private string _DataPath;
        private string DataPath
        {
            get
            {
                if (string.IsNullOrEmpty(_DataPath))
                    return this.GetType().Name;
                else
                    return _DataPath;
            }
            set
            {
                _DataPath = value + "." + this.GetType().Name;
            }
        }
        private bool _IsValidated = true;
        internal bool IsValidated
        {
            get
            {
                if (Errors != null && Errors.Count > 0) return _IsValidated = false;
                List<PropertyInfo> PropertiesInfo = this.GetType().GetProperties().ToList();
                foreach (PropertyInfo PropertyInfo in PropertiesInfo)
                {
                    if (PropertyInfo.PropertyType.IsGenericType && PropertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        if (PropertyInfo.GetValue(this) is IEnumerable<DataEntity> DataEntities)
                            foreach (DataEntity DataEntity in DataEntities)
                            {
                                this._IsValidated &= DataEntity.IsValidated;
                            }
                    }
                    if (PropertyInfo.PropertyType.IsSubclassOf(typeof(DataEntity)))
                    {
                        if (PropertyInfo.GetValue(this) is DataEntity DataEntity)
                        {
                            this._IsValidated &= DataEntity.IsValidated;
                        }
                    }
                }
                return this._IsValidated;
            }
        }

        public Dictionary<string, string> Errors { get; private set; }
        public void AddError(string className, string Key, Enum Value)
        {

            if (Errors == null) Errors = new Dictionary<string, string>();

            string file = string.Format("{0}", className);
            string path = string.Format("{0}.{1}.{2}", DataPath, Key, Value.ToString());
            JToken token = ErrorResource.GetValueOrDefault(file)?.SelectToken(path);
            string content = token == null ? Value.ToString() : token.ToString();
            Key = ToCamel(Key);
            if (Errors.ContainsKey(Key))
            {
                if (!Errors[Key].Contains(content))
                    Errors[Key] += content;
            }
            else
                Errors.Add(Key, content);
        }

        private string ToCamel(string name)
        {
            return Char.ToLowerInvariant(name[0]) + name.Substring(1);
        }
    }
}
