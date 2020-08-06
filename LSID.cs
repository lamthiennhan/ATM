using System;
using System.Collections.Generic;
using System.Text;

namespace Do_An2
{
    class LSID
    {
        public DateTime tG;
        public string loaiGD, id; 
        public int soTien;

        public LSID()
        {
            loaiGD = null;
            id = null;
            soTien = 0;
        }

        public void Xuat()
        {
            Console.WriteLine("{0,10}{1,-20}{2,-20}", "", "Ngay:", tG);
            Console.WriteLine("{0,10}{1,-20}{2,-20}", "", "Loai Giao dich:", loaiGD);
            Console.WriteLine("{0,10}{1,-20}{2,-20}", "", "So tien:", soTien);
            Console.WriteLine("{0,15}{1}", "", "--------------------");
        }
    }
}
