using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CommonValues
{
    public static class ObjectSerlialize
    {
        public static byte[] Serialize(this object obj)
        {
            if (obj == null)
                return null;

            var json = JsonConvert.SerializeObject(obj);
            return Encoding.ASCII.GetBytes(json);
        }

        public static object DeSerialize(this byte[] obj , Type type)
        {
            var json = Encoding.Default.GetString(obj);

            return JsonConvert.DeserializeObject(json , type);
        }

        public static string DeSerializeText(this byte[] obj)
        {
            return Encoding.Default.GetString(obj);
        }
    }
}
