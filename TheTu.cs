using System;
using System.Collections.Generic;
using System.Text;

namespace Do_An2
{
    class TheTu
    {
        public string tinhTrang, id, maPin;
        
        public TheTu()
        {
            id = null;
            maPin = null;
            tinhTrang = "1";
        }
        public TheTu(string id, string maPin)
        {
            this.id = id;
            this.maPin = maPin;
            this.tinhTrang = "1";
        }
    }
}
