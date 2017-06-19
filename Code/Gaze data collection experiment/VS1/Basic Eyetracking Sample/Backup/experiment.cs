using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicEyetrackingSample
{
    class experiment
    {
        List<List<string>> compare = new List<List<string>>();

        public List<List<string>> Compare
        {
            get { return compare; }
            set { compare = value; }
        }
        List<string> record = new List<string>();

        public List<string> Record
        {
            get { return record; }
            set { record = value; }
        }

    }
}
