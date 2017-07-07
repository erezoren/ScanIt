using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScanIt
{
    class Invitor
    {

        private string id;
        private string name;

        public Invitor(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public string getId()
        {
            return id;
        }

        public string getName()
        {
            return name;
        }

    }
}
