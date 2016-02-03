using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sTranslate.Tools
{
    [Serializable]
    public class KeyValue
    {
        public string Key {get; set;}
        public string Value { get; set; }
        public List<KeyValue> Params { get; set; }
        public List<string> Constants { get; set; }

        /// <summary>
        /// Constructors
        /// </summary>
        public KeyValue()
        {
        }
        public KeyValue(string key, string value)
        {
            this.Key = key;
            this.Value = value;
            Constants = ToConstants(value);
            Params = ToKeyValues(value);
        }

        /// <summary>
        /// Read Only properties
        /// </summary>
        public string KeySufix
        {
            get
            {
                string[] arr = Key.Split('.') ?? new string[] {""};
                return arr[arr.Length-1];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public string Param(string parameterName, string defaultValue = "", bool skipQuotationMarks = false, bool treatEmptyAsNull = false)
        {
            foreach (KeyValue kv in Params)
            {
                if (kv.Key.Trim().ToLower() == parameterName.ToLower())
                {
                    if (treatEmptyAsNull && string.IsNullOrEmpty(kv.Value))
                        return defaultValue;
                    if (skipQuotationMarks)
                    {
                        string val = kv.Value; 
                        if (val.StartsWith("\""))
                            val = val.Substring(1);
                        if (val.EndsWith("\""))
                            val = val.Substring(0, val.Length-1);
                        return val; 
                    }
                    return kv.Value;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueString"></param>
        /// <returns></returns>
        public List<KeyValue> ToKeyValues(string valueString)
        {
            List<KeyValue> coll = new List<KeyValue>();
            if (valueString != null)
            {
                string[] arr = valueString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in arr)
                {
                    int pos = s.IndexOf('=');
                    if (pos > 0)
                    {
                        string key = s.Substring(0, pos).Trim();
                        string val = "";
                        if (pos < s.Length - 1)
                            val = s.Substring(pos + 1);
                        coll.Add(new KeyValue(key, val));
                    }
                }
            }
            return coll; 
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueString"></param>
        /// <returns></returns>
        public List<string> ToConstants(string valueString)
        {
            List<string> coll = new List<string>();
            if (valueString != null)
            {
                string[] arr = valueString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in arr)
                {
                    coll.Add(s);
                }
            }
            return coll;
        }

        public static string GetParam(string parameterKey, string parameterKeyValueString, string defaultValue = "", string separator = ";")
        {
            if (string.IsNullOrEmpty(parameterKeyValueString) == false)
            {
                string[] arr = parameterKeyValueString.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in arr)
                {
                    int pos = s.IndexOf('=');
                    if (pos > 0)
                    {
                        string key = s.Substring(0, pos).Trim();
                        string val = "";
                        if (pos < s.Length - 1)
                            val = s.Substring(pos + 1);
                        if (key.ToLower() == parameterKey.ToLower())
                            return val;
                    }
                }
            }
            return defaultValue;
        }

    }
}
