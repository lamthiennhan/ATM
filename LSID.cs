using System;
using System.Collections.Generic;
using System.Text;

namespace Do_An2
{
    class LSID
    {
        public string loaiGD, id, tG;
        public int soTien;

        public LSID()
        {
            loaiGD = null;
            id = null;
            tG = null;
            soTien = 0;
        }
        public LSID(string loaiGD, string tG, string id, int soTien)
        {
            this.loaiGD = loaiGD;
            this.id = id;
            this.tG = tG;
            this.soTien = soTien;
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
