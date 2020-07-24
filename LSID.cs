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
        public LSID(string loaiGD, string id, int soTien, string tG)
        {
            this.loaiGD = loaiGD;
            this.id = id;
            this.tG = tG;
            this.soTien = soTien;
        }
    }
}
