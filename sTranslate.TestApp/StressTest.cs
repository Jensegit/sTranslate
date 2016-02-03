using System;
using System.IO; 
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using sTranslate.Tools; 

namespace sTranslate.TestApp
{
    class StressTest
    {
        public static void DoTest(string fileName)
        {
            DateTime startTime = DateTime.Now;

            // Get my name
            string[] strArr = Assembly.GetCallingAssembly().FullName.Split(',');
            string myName = strArr[0];

            try
            {
                if (!File.Exists(fileName))
                {
                    Console.WriteLine("{0}: Failed to read file {1}; File does not exist!", myName, fileName);
                    return; 
                }

                // Read data from file    
                string[] lines = System.IO.File.ReadAllLines(fileName, Encoding.GetEncoding("ISO-8859-1"));

                foreach (string line in lines)
                {
                    // Use keyValue class to helpe us parsing line by field names   
                    KeyValue kv = new KeyValue("tmp", line);
                    string fromText = kv.Param("FromText","", true);
                    string property = kv.Param("Property", "", true);
                    string context = kv.Param("Context", "", true);
                    string toText = XltTool.ToText(fromText, EnumsXlt.ToPropertyType(property), context);

                    Console.WriteLine("{0}: English: \"{1}\" translated to: \"{2}\"", myName, fromText, toText);
                }

                Console.WriteLine("{0}: Duration: {1}", myName, DateTime.Now.Subtract(startTime));
                Console.WriteLine("Press any key to end ...");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", XltTool.ExceptionMsg(ex, true));
            }
            Console.ReadKey();
        }
    }
}
