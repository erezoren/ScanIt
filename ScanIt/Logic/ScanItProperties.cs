using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
namespace ScanIt
{
    class ScanItProperties
    {
        private static ScanItProperties instance;

        private ScanItProperties() { init(); }

        public static ScanItProperties Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScanItProperties();
                }
                return instance;
            }
        }

        Dictionary<string, string> data = new Dictionary<string, string>();

        public void init()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"reasources\scanprops.txt");

            foreach (var row in File.ReadAllLines(path))
                data.Add(row.Split('=')[0].Trim(), string.Join("=", row.Split('=').Skip(1).ToArray()));
        }

        public string getPropValue(string propName)
        {
            return data[propName];
        }
    }
 
}
