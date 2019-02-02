using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_XML
{

    public struct Chapter
    {
        public List<Position> Positions { get; set; }

        public string Сaption { get; set; }
    }

    public struct Position
    {
        public List<Resource> Resources { get; set; }

        public string Number { get; set; }
        public string Code { get; set; }
        public string Сaption { get; set; }
        public string Units { get; set; }
        public string Fx { get; set; }
    }

    public struct Resource
    {
        public string Code { get; set; }
        public string Сaption { get; set; }
        public string Quantity { get; set; }
    }

}
