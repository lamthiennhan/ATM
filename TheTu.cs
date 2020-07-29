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

        public void Xuat()
        {
            string tinhTrang;

            Console.WriteLine("{0,10}{1,-20}{2,-20}", "", "ID:", id);
            Console.WriteLine("{0,10}{1,-20}{2,-20}", "", "Ma Pin:", maPin);
            Console.Write("{0,10}{1,-20}", "", "Tinh trang:");

            if (string.Compare(this.tinhTrang, "0") == 0)
            {
                tinhTrang = "Khoa";
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                tinhTrang = "Binh thuong";
            }

            Console.WriteLine("{0,-20}", tinhTrang);
            Console.ResetColor();
            Console.WriteLine("{0,15}{1}", "", "--------------------");
        }
    }
}
