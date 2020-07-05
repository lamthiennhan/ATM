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
        public ID(string id, string ten, int soDu, string tienTe)
        {
            this.id = id;
            this.ten = ten;
            this.soDu = soDu;
            this.tienTe = tienTe;
        }
    }
}
