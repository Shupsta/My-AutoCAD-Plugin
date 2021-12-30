using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using WarmBoardTools.Interfaces;

namespace WBPlugin.Zone_Tools.Old_Support
{
    public class OldDeserializer
    {
        public static Object Deserialize(ResultBuffer xrecordData)
        {
            if (xrecordData == null) return null;

            Object obj2 = null;

            BinaryFormatter formatter = new BinaryFormatter() { Binder = new DomainBinder() };

            MemoryStream serializationStream = new MemoryStream();

            try
            {
                TypedValue[] valueArray = xrecordData.AsArray();
                int index = 1;
                while (true)
                {
                    if(index >= valueArray.Length)
                    {
                        serializationStream.Position = 0L;
                        obj2 = formatter.Deserialize(serializationStream);
                        break;
                    }

                    if(valueArray[index].TypeCode == 310)
                    {
                        byte[] buffer = (byte[])valueArray[index].Value;
                        serializationStream.Write(buffer, 0, buffer.Length);
                    }
                    index++;
                }
            }
            finally
            {
                if(serializationStream is object)
                {
                    serializationStream.Dispose();
                }
            }

            return obj2;
        }
    }

    class DomainBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            string str = assemblyName.Split(new char[] { ',' })[0];
            
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            if (str.Equals("WarmBoardTools")){
                for (int i = 0; i < assemblies.Length; i++)
                {
                    char[] seperator = new char[] { ',' };
                    if (assemblies[i].FullName.Split(seperator)[0] == "WBPlugin")
                    {
                        return assemblies[i].GetType(typeName);
                    }
                }
            }

            int index = 0;
            while (true)
            {
                Type type;
                if(index >= assemblies.Length)
                {
                    type = null;
                }
                else
                {
                    char[] seperator = new char[] { ',' };
                    if(str != assemblies[index].FullName.Split(seperator)[0])
                    {
                        index++;
                        continue;
                    }
                    type = assemblies[index].GetType(typeName);
                }
                return type;
            }
        }
    }
}
