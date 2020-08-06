using System;
using System.Collections.Generic;
using System.Text;

namespace Do_An2
{
    class ID
    {
        public string id, ten, tienTe;
        public int soDu;

        public ID()
        {
            id = null;
            ten = null;
            soDu = 0;
            tienTe = null;
        }

        public void Xuat()
        {        
            Console.WriteLine("{0,10}{1,-20}{2,-20}", "", "ID:", id);
            Console.WriteLine("{0,10}{1,-20}{2,-20}", "", "Ten:", ten);
            Console.WriteLine("{0,10}{1,-20}{2,-20}", "", "So du:", soDu + " " + tienTe);
            Console.WriteLine("{0,15}{1}", "", "--------------------");
        }
    }
}
