using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScanIt
{
    class ComboboxItem
    {
        public string name { get; set; }
        public string id { get; set; }

        public override string ToString()
        {
            return name;
        }

       
    }
}
