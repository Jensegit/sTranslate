using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using sTranslate.Tools; 

namespace sTranslate.TestApp
{
    class Program
    {
        /// <summary>
        /// Using: TestApp Context Property Criteria ToLang FromText  
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            DateTime startTime = DateTime.Now;
            // Get my name
            string[] strArr = Assembly.GetCallingAssembly().FullName.Split(',');
            string myName = strArr[0];
 
            // Check command syntax
            if (args.Length < 5)
            {
                if (args.Length == 1 && args[0].ToLower() == "stresstest")
                {
                    StressTest.DoTest(@"C:\Dev\sTranslate\sTranslate.TestApp\StressTest.dat"); 
                    return; 
                }
                
                Console.WriteLine("Command syntax error!\n");
                Console.WriteLine("Using: {0}.exe Context Property Criteria ToLang Text", myName);
                Console.WriteLine("\nExample: {0}.exe string text contains no Add Service", myName);
                return; 
            }

            string context = args[0];
            string property = args[1];
            string criteria = args[2];
            string toLang = args[3];
            string text = "";
            for (int i = 4; i < args.Length; i++)
                text += (text == "") ? args[i] : " " + args[i];

            try
            {
                // Call translation an print output
                string toText = Tools.XltTool.GetToText(EnumsXlt.ToCriteria(criteria), text, EnumsXlt.ToPropertyType(property), context, toLang);
                Console.WriteLine("{0}: English: \"{1}\" translated to: \"{2}\"", myName, text, toText);
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
