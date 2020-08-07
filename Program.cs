using System;
using System.Collections;
using System;
using System.Runtime.CompilerServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Threading;

namespace Do_An2
{
    class Program
    {
        static void Main(string[] args)
        {
            string user = ""; //Dùng để xác định user

            //Khởi tạo các ds
            List<Admin> ds_admin = new List<Admin>();
            List<TheTu> ds_thetu = new List<TheTu>();
            List<ID> ds_id = new List<ID>();
            List<LSID> ds_lsid = new List<LSID>();

            //Nạp dữ liệu
            DocFile(ds_admin, ds_thetu, ds_id, ds_lsid); //Đây là hàm đọc dữ liệu từ file, vị trí hàm nằm ở cuối cùng.

            //Chọn loại tài khoản
            int loaiTaiKhoan;

            do
            {
                Console.Clear();
                Console.Write("Admin nhap 1, User nhap 2: ");
                int.TryParse(Console.ReadLine(), out loaiTaiKhoan);
            } while (loaiTaiKhoan != 1 && loaiTaiKhoan != 2);

            //Chương trình
            KTra_DangNhap(ds_admin, ds_thetu, loaiTaiKhoan, ref user);
            if (loaiTaiKhoan == 1)
            {
                Menu_Admin(ds_admin, ds_thetu, ds_id, ds_lsid);
            }
            else
            {
                Menu_User(ds_admin, ds_thetu, ds_id, ds_lsid, user);
            }
            Console.ReadKey();
        }

        //Thành phần của Admin
        static void Logo_DangNhapAdmin()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.Write("{0,15}{1}", "", "*");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("    DANG NHAP ADMIN    ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*");
            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.ResetColor();
        }

        //1_Admin
        static void Logo_XemDS()
        {
            ConsoleColor foreground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.Write("{0,15}{1}", "", "*");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  DANH SACH TAI KHOAN  ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*");
            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.ResetColor();
        }
        static void XemDS(List<Admin> a, List<TheTu> b, List<ID> c, List<LSID> d)
        {
            int chon;
            do
            {
                Console.Clear();
                Logo_XemDS();

                for (int i = 0; i < b.Count; i++)
                {
                    b[i].Xuat();
                }

                Console.WriteLine("1.Quay lai");
                Console.WriteLine("2.Thoat");
                Console.Write("Chon chac nang: ");
                int.TryParse(Console.ReadLine(), out chon);
                if (chon == 1)
                {
                    Menu_Admin(a, b, c, d);
                }
                if (chon == 2)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine("{0,5}{1}", "", "BAN MUON THOAT?");
                        Console.WriteLine("{0}{1,10}{2}", "1.Co", "", "2.Quay lai");
                        int.TryParse(Console.ReadLine(), out chon);
                    } while (chon != 1 && chon != 2);
                    if (chon == 1)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        XemDS(a, b, c, d);
                    }
                }
            } while (chon != 1 && chon != 2);
        }

        //2_Admin
        static void Logo_Them()
        {
            ConsoleColor foreground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.Write("{0,15}{1}", "", "*");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("    THEM TAI KHOAN     ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*");
            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.ResetColor();
        }
        static void Them(List<Admin> a, List<TheTu> b, List<ID> c, List<LSID> d)
        {
            int kT = 0, chon;
            string[] dauCach = new string[] { " " };
            ID moiID = new ID();
            TheTu moiTheTu = new TheTu();

            do
            {
                Console.Clear();
                Logo_Them();

                Console.WriteLine("1.Them tai khoan");
                Console.WriteLine("2.Quay lai");
                Console.WriteLine("3.Thoat");
                Console.Write("Chon chac nang: ");
                int.TryParse(Console.ReadLine(), out chon);
                if (chon == 1)
                {
                    do
                    {
                        kT = 0;

                        Console.Clear();
                        Logo_Them();

                        Console.WriteLine("{0,15}{1}", "", "Nhap thong tin tai khoan moi");
                        Console.Write("{0,10}{1,-20}", "", "ID:");
                        moiID.id = Console.ReadLine();

                        //Cắt dấu cách ra
                        string[] arrayMa = moiID.id.Split(dauCach, StringSplitOptions.RemoveEmptyEntries);

                        moiID.id = "";

                        //Ghi lại không có dấu cách
                        for (int i = 0; i < arrayMa.Length; i++)
                        {
                            moiID.id += arrayMa[i];
                        }

                        //KT trùng ID
                        if (KT_Trung(b, moiID.id) == false)
                        {
                            kT = 1;
                        }
                    } while (kT == 0 || moiID.id.Length != 14);

                    Console.Clear();
                    Logo_Them();

                    Console.WriteLine("{0,15}{1}", "", "Nhap thong tin tai khoan moi");
                    Console.WriteLine("{0,10}{1,-20}{2,-20}", "", "ID:", moiID.id);

                    //Sửa trong chương trình
                    //ID
                    Console.Write("{0,10}{1,-20}", "", "Ten:");
                    moiID.ten = Console.ReadLine();
                    Console.Write("{0,10}{1,-20}", "", "So du:");
                    int.TryParse(Console.ReadLine(), out moiID.soDu);
                    Console.Write("{0,10}{1,-20}", "", "Tien te:");
                    moiID.tienTe = Console.ReadLine();

                    c.Add(moiID);

                    //TheTu
                    moiTheTu.id = moiID.id;
                    moiTheTu.maPin = "123456";
                    moiTheTu.tinhTrang = "1";

                    b.Add(moiTheTu);

                    //Sửa trong file
                    //TheTu
                    using (StreamWriter sw = new StreamWriter("TheTu.txt"))
                    {
                        sw.WriteLine(b.Count);
                        for (int j = 0; j < b.Count; j++)
                        {
                            sw.WriteLine(b[j].tinhTrang + "#" + b[j].id + "#" + b[j].maPin);
                        }
                    }
                    //ID
                    using (StreamWriter sw = new StreamWriter(moiID.id + ".txt"))
                    {
                        sw.WriteLine(moiID.id + "#" + moiID.ten + "#" + moiID.soDu + "#" + moiID.tienTe);
                    }

                    //LSID
                    using (StreamWriter sw = new StreamWriter("LichSu" + moiID.id + ".txt"))
                    {
                    }

                    ConsoleColor foreground = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nSua thanh cong");
                    Console.ResetColor();
                    Console.ReadKey();

                    Them(a, b, c, d);
                }
                if (chon == 2)
                {
                    Menu_Admin(a, b, c, d);
                }
                if (chon == 3)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine("{0,5}{1}", "", "BAN MUON THOAT?");
                        Console.WriteLine("{0}{1,10}{2}", "1.Co", "", "2.Quay lai");
                        int.TryParse(Console.ReadLine(), out chon);
                    } while (chon != 1 && chon != 2);
                    if (chon == 1)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Them(a, b, c, d);
                    }
                }
            } while (chon != 1 && chon != 2 && chon != 3);
        }

        //KT trùng ID
        static bool KT_Trung(List<TheTu> b, string ma)
        {
            for (int i = 0; i < b.Count; i++)
            {
                if (b[i].id == ma)
                {
                    return true;
                }
            }
            return false;
        }

        //3_Admin
        static void Logo_Xoa()
        {
            ConsoleColor foreground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.Write("{0,15}{1}", "", "*");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("     XOA TAI KHOAN     ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*");
            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.ResetColor();
        }
        static void Xoa(List<Admin> a, List<TheTu> b, List<ID> c, List<LSID> d)
        {
            string ID;
            int chon, kT = 0; ;

            do
            {
                Console.Clear();
                Logo_Xoa();

                for (int i = 0; i < b.Count; i++)
                {
                    b[i].Xuat();
                }

                Console.WriteLine("1.Xoa tai khoan");
                Console.WriteLine("2.Quay lai");
                Console.WriteLine("3.Thoat");
                Console.Write("Chon chac nang: ");
                int.TryParse(Console.ReadLine(), out chon);
                if (chon == 1)
                {

                    do
                    {
                        Console.Clear();
                        Logo_Xoa();

                        for (int i = 0; i < b.Count; i++)
                        {
                            b[i].Xuat();
                        }
                        Console.Write("Nhap ID muon xoa: ");
                        ID = Console.ReadLine();

                        for (int i = 0; i < b.Count; i++)
                        {
                            if (string.Compare(ID, b[i].id) == 0)
                            {
                                //Xóa trong file
                                File.Delete(ID + ".txt");

                                if (File.Exists("LichSu" + ID + ".txt"))
                                {
                                    File.Delete("LichSu" + ID + ".txt");
                                }

                                using (StreamWriter sw = new StreamWriter("TheTu.txt"))
                                {
                                    sw.WriteLine(b.Count - 1);
                                    for (int j = 0; j < b.Count; j++)
                                    {
                                        if (ID != b[j].id)
                                        {
                                            sw.WriteLine(b[j].tinhTrang + "#" + b[j].id + "#" + b[j].maPin);
                                        }
                                    }
                                }

                                //Xóa trong chương trình
                                for (int j = 0; j < b.Count; j++)
                                {
                                    if (ID == b[j].id)
                                    {
                                        b.Remove(b[j]);
                                    }
                                }

                                kT++;

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Xoa thanh cong");
                                Console.ResetColor();
                                Console.ReadKey();
                                break;
                            }
                        }
                        if (kT == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Nhap sai ID. Nhap lai!!!");
                            Console.ResetColor();
                            Console.ReadKey();
                        }
                    } while (kT == 0);
                    if (kT != 0)
                    {
                        Xoa(a, b, c, d);
                    }
                }
                if (chon == 2)
                {
                    Menu_Admin(a, b, c, d);
                }
                if (chon == 3)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine("{0,5}{1}", "", "BAN MUON THOAT?");
                        Console.WriteLine("{0}{1,10}{2}", "1.Co", "", "2.Quay lai");
                        int.TryParse(Console.ReadLine(), out chon);
                    } while (chon != 1 && chon != 2);
                    if (chon == 1)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Xoa(a, b, c, d);
                    }
                }
            } while (chon != 1 && chon != 2);
        }

        //4_Admin
        static void Logo_MoKhoa()
        {
            ConsoleColor foreground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.Write("{0,15}{1}", "", "*");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("   MO KHOA TAI KHOAN   ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*");
            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.ResetColor();
        }
        static void MoKhoa(List<Admin> a, List<TheTu> b, List<ID> c, List<LSID> d)
        {
            int chon, kT = 0, dong = 0;
            string ID;
            List<TheTu> khoa = new List<TheTu>();
            do
            {
                Console.Clear();
                Logo_MoKhoa();

                for (int i = 0, j = 0; i < b.Count; i++)
                {
                    if (string.Compare(b[i].tinhTrang, "0") == 0)
                    {
                        b[i].Xuat();
                        khoa.Add(b[i]);
                    }
                }

                Console.WriteLine("1.Mo khoa");
                Console.WriteLine("2.Quay lai");
                Console.WriteLine("3.Thoat");
                Console.Write("Chon chac nang: ");
                int.TryParse(Console.ReadLine(), out chon);
                if (chon == 1)
                {

                    do
                    {
                        Console.Clear();
                        Logo_MoKhoa();

                        for (int i = 0; i < khoa.Count; i++)
                        {
                            khoa[i].Xuat();

                        }
                        Console.Write("Nhap ID muon mo khoa: ");
                        ID = Console.ReadLine();

                        for (int i = 0; i < khoa.Count; i++)
                        {
                            if (string.Compare(ID, khoa[i].id) == 0)
                            {
                                for (int j = 0; j < b.Count; j++)
                                {
                                    if (string.Compare(ID, b[j].id) == 0)
                                    {
                                        dong = j;
                                    }
                                }
                                //Sửa trong file
                                using (StreamWriter sw = new StreamWriter("TheTu.txt"))
                                {
                                    sw.WriteLine(b.Count);
                                    for (int j = 0; j < b.Count; j++)
                                    {
                                        if (j == dong)
                                        {
                                            sw.WriteLine("1#" + b[j].id + "#" + b[j].maPin);
                                        }
                                        else
                                        {
                                            sw.WriteLine(b[j].tinhTrang + "#" + b[j].id + "#" + b[j].maPin);
                                        }
                                    }
                                }

                                //Sửa trong chương trình  
                                b[dong].tinhTrang = "1";

                                kT++;

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Mo khoa thanh cong");
                                Console.ResetColor();
                                Console.ReadKey();
                            }
                        }
                        if (kT == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Nhap sai ID. Nhap lai!!!");
                            Console.ResetColor();
                            Console.ReadKey();
                        }
                    } while (kT == 0);
                    if (kT != 0)
                    {
                        MoKhoa(a, b, c, d);
                    }
                }
                if (chon == 2)
                {
                    Menu_Admin(a, b, c, d);
                }
                if (chon == 3)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine("{0,5}{1}", "", "BAN MUON THOAT?");
                        Console.WriteLine("{0}{1,10}{2}", "1.Co", "", "2.Quay lai");
                        int.TryParse(Console.ReadLine(), out chon);
                    } while (chon != 1 && chon != 2);
                    if (chon == 1)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        MoKhoa(a, b, c, d);
                    }
                }
            } while (chon != 1 && chon != 2);
        }

        //Menu
        static void Menu_Admin(List<Admin> a, List<TheTu> b, List<ID> c, List<LSID> d)
        {
            int chucNang;
            string user = "";

            do
            {
                Console.Clear();
                ConsoleColor foreground = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("");
                Console.WriteLine("{0,12}{1}", "", "*************MENU**************");
                Console.WriteLine("{0,15}{1}", "", "1.Xem danh sach tai khoan");
                Console.WriteLine("{0,15}{1}", "", "2.Them tai khoan");
                Console.WriteLine("{0,15}{1}", "", "3.Xoa tai khoan");
                Console.WriteLine("{0,15}{1}", "", "4.Mo khoa tai khoan");
                Console.WriteLine("{0,15}{1}", "", "5.Thoat");
                Console.WriteLine("{0,12}{1}", "", "*******************************");
                Console.ResetColor();
                Console.Write("Chon chac nang: ");
                int.TryParse(Console.ReadLine(), out chucNang);
            } while (chucNang < 1 || chucNang > 5);

            if (chucNang == 1)
            {
                XemDS(a, b, c, d);
            }
            if (chucNang == 2)
            {
                Them(a, b, c, d);
            }
            if (chucNang == 3)
            {
                Xoa(a, b, c, d);
            }
            if (chucNang == 4)
            {
                MoKhoa(a, b, c, d);
            }
            if (chucNang == 5)
            {
                KT_Thoat(a, b, c, d, 1, user);
            }
        }


        //Thành phần của User
        static void Logo_DangNhapUser()
        {
            ConsoleColor foreground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.Write("{0,15}{1}", "", "*");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("    DANG NHAP USER     ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*");
            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.ResetColor();
        }

        //1_User
        static void Logo_XemTT()
        {
            ConsoleColor foreground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.Write("{0,15}{1}", "", "*");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("     XEM THONG TIN     ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*");
            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.ResetColor();
        }
        static void XemTT(List<Admin> a, List<TheTu> b, List<ID> c, List<LSID> d, string user)
        {
            int chon;
            do
            {
                Console.Clear();
                Logo_XemTT();

                for (int i = 0; i < c.Count; i++)
                {
                    if (string.Compare(c[i].id, user) == 0)
                    {
                        c[i].Xuat();
                    }

                }

                Console.WriteLine("1.Quay lai");
                Console.WriteLine("2.Thoat");
                Console.Write("Chon chac nang: ");
                int.TryParse(Console.ReadLine(), out chon);
                if (chon == 1)
                {
                    Menu_User(a, b, c, d, user);
                }
                if (chon == 2)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine("{0,5}{1}", "", "BAN MUON THOAT?");
                        Console.WriteLine("{0}{1,10}{2}", "1.Co", "", "2.Quay lai");
                        int.TryParse(Console.ReadLine(), out chon);
                    } while (chon != 1 && chon != 2);
                    if (chon == 1)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        XemTT(a, b, c, d, user);
                    }
                }
            } while (chon != 1 && chon != 2);
        }

        static void Logo_Rut()
        {
            ConsoleColor foreground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.Write("{0,15}{1}", "", "*");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("       RUT TIEN        ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*");
            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.ResetColor();
        }
        static void Rut(List<Admin> a, List<TheTu> b, List<ID> c, List<LSID> d, string user)
        {
            int chon, kT, tienRut;

            do
            {
                Console.Clear();
                Logo_Rut();
                Console.WriteLine("1.Rut tien");
                Console.WriteLine("2.Quay lai");
                Console.WriteLine("3.Thoat");
                Console.Write("Chon chac nang: ");
                int.TryParse(Console.ReadLine(), out chon);
                if (chon == 1)
                {
                    do
                    {
                        kT = 0;

                        Console.Clear();
                        Logo_Rut();
                        Console.Write("{0,5}{1,-20}", "", "Nhap so tien can rut:");
                        int.TryParse(Console.ReadLine(), out tienRut);

                        //Dk <= so du -50
                        for (int i = 0; i < c.Count; i++)
                        {
                            if (string.Compare(user, c[i].id) == 0)
                            {
                                if (tienRut <= c[i].soDu - 50000)
                                {
                                    kT++;
                                }
                            }
                        }

                        //Dk >= 50000
                        if (tienRut >= 50000)
                        {
                            kT++;
                        }

                        //Boi cua 50
                        if (tienRut % 50 == 0)
                        {
                            kT++;
                        }

                        if (kT != 3)
                        {
                            Console.WriteLine("Nhap lai!!!");
                            Console.ReadKey();
                        }
                    } while (kT != 3);


                    DateTime time = DateTime.Now;

                    //Sua trong chuong trinh
                    //ID
                    for (int i = 0; i < c.Count; i++)
                    {
                        if (string.Compare(user, c[i].id) == 0)
                        {
                            c[i].soDu = c[i].soDu - tienRut;
                            break;
                        }
                    }
                    //LS
                    LSID lsNew = new LSID();
                    lsNew.id = user;
                    lsNew.tG = time;
                    lsNew.loaiGD = "Rut tien ";
                    lsNew.soTien = tienRut;
                    d.Add(lsNew);
                    //Sua trong file
                    //ID
                    using (StreamWriter sw = new StreamWriter(user + ".txt"))
                    {
                        for (int i = 0; i < c.Count; i++)
                        {
                            if (string.Compare(user, c[i].id) == 0)
                            {
                                sw.WriteLine(c[i].id + "#" + c[i].ten + "#" + c[i].soDu + "#" + c[i].tienTe);
                                break;
                            }
                        }
                    }
                    //LSID
                    using (StreamWriter sw = new StreamWriter("LichSu" + user + ".txt", true))
                    {
                        sw.WriteLine(lsNew.id + "#" + lsNew.tG + "#" + lsNew.loaiGD + "#" + lsNew.soTien);
                    }

                    ConsoleColor foreground = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nRut tien thanh cong");
                    Console.ResetColor();
                    Console.ReadKey();
                    Rut(a, b, c, d, user);
                }
                if (chon == 2)
                {
                    Menu_User(a, b, c, d, user);
                }
                if (chon == 3)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine("{0,5}{1}", "", "BAN MUON THOAT?");
                        Console.WriteLine("{0}{1,10}{2}", "1.Co", "", "2.Quay lai");
                        int.TryParse(Console.ReadLine(), out chon);
                    } while (chon != 1 && chon != 2);
                    if (chon == 1)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Rut(a, b, c, d, user);
                    }
                }
            } while (chon != 1 && chon != 2 && chon != 3);
        }

        //3_User
        static void Logo_Chuyen()
        {
            ConsoleColor foreground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.Write("{0,15}{1}", "", "*");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("      CHUYEN TIEN      ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*");
            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.ResetColor();
        }
        static void soTienChuyen(List<Admin> a, List<TheTu> b, List<ID> c, List<LSID> d, string user)
        {
            int chon;

            do
            {
                Console.Clear();
                Logo_Chuyen();
                Console.WriteLine("1.Chuyen tien");
                Console.WriteLine("2.Quay lai");
                Console.WriteLine("3.Thoat");
                Console.Write("Chon chac nang: ");
                int.TryParse(Console.ReadLine(), out chon);
                if (chon == 1)
                {
                    string soTK = "";

                    int kT = 0;
                    int tienChuyen = 0;


                    do
                    {
                        kT = 0;
                        Console.Clear();
                        Logo_Chuyen();

                        Console.Write("{0,5}{1,-20}", "", "Nhap so tai khoan can chuyen:");

                        soTK = Console.ReadLine();

                        for (int i = 0; i < b.Count; i++)
                        {
                            //Kt 1 tk ton tai va khac tai khoan hien tai
                            if (string.Compare(b[i].id, soTK) == 0 && user != soTK)
                            {
                                kT = 1;
                            }
                        }

                        if (kT != 1)
                        {
                            Console.WriteLine("Nhap lai!!!");
                            Console.ReadKey();
                        }
                    } while (kT != 1);

                    do
                    {
                        kT = 0;

                        Console.Clear();
                        Logo_Chuyen();

                        Console.WriteLine("{0,5}{1,-20}{2}", "", "Nhap so tai khoan can chuyen:", soTK);

                        Console.Write("{0,5}{1,-20}", "", "Nhap so tien can chuyen:");
                        int.TryParse(Console.ReadLine(), out tienChuyen);

                        //Dk <= so du -50
                        for (int i = 0; i < c.Count; i++)
                        {
                            if (string.Compare(user, c[i].id) == 0)
                            {
                                if (tienChuyen <= c[i].soDu - 50000)
                                {
                                    kT++;
                                }
                            }
                        }

                        //Dk >= 50000
                        if (tienChuyen >= 50000)
                        {
                            kT++;
                        }

                        //Boi cua 50
                        if (tienChuyen % 50 == 0)
                        {
                            kT++;
                        }

                        if (kT != 3)
                        {
                            Console.WriteLine("Nhap lai!!!");
                            Console.ReadKey();
                        }
                    } while (kT != 3);


                    DateTime time = DateTime.Now;

                    //Tk chuyen
                    //Sua trong chuong trinh
                    //ID
                    for (int i = 0; i < c.Count; i++)
                    {
                        if (string.Compare(user, c[i].id) == 0)
                        {
                            c[i].soDu = c[i].soDu - tienChuyen;
                            break;
                        }
                    }
                    //LS
                    LSID lsNew = new LSID();
                    lsNew.id = user;
                    lsNew.tG = time;
                    lsNew.loaiGD = "Chuyen tien cho tai khoan " + soTK;
                    lsNew.soTien = tienChuyen;
                    d.Add(lsNew);
                    //Sua trong file
                    //ID
                    using (StreamWriter sw = new StreamWriter(user + ".txt"))
                    {
                        for (int i = 0; i < c.Count; i++)
                        {
                            if (string.Compare(user, c[i].id) == 0)
                            {
                                sw.WriteLine(c[i].id + "#" + c[i].ten + "#" + c[i].soDu + "#" + c[i].tienTe);
                                break;
                            }
                        }
                    }
                    //LSID
                    using (StreamWriter sw = new StreamWriter("LichSu" + user + ".txt", true))
                    {
                        sw.WriteLine(lsNew.id + "#" + lsNew.tG + "#" + lsNew.loaiGD + "#" + lsNew.soTien);
                    }

                    //Tk nhan
                    //Sua trong chuong trinh
                    //ID
                    for (int i = 0; i < c.Count; i++)
                    {
                        if (string.Compare(user, c[i].id) == 0)
                        {
                            c[i].soDu = c[i].soDu + tienChuyen;
                            break;
                        }
                    }
                    //LS
                    LSID lsNew2 = new LSID();
                    lsNew2.id = soTK;
                    lsNew2.tG = time;
                    lsNew2.loaiGD = "Nhan tien tu tai khoan " + user;
                    lsNew2.soTien = tienChuyen;
                    d.Add(lsNew2);
                    //Sua trong file
                    //ID
                    using (StreamWriter sw = new StreamWriter(soTK + ".txt"))
                    {
                        for (int i = 0; i < c.Count; i++)
                        {
                            if (string.Compare(soTK, c[i].id) == 0)
                            {
                                sw.WriteLine(c[i].id + "#" + c[i].ten + "#" + c[i].soDu + "#" + c[i].tienTe);
                                break;
                            }
                        }
                    }
                    //LSID
                    using (StreamWriter sw = new StreamWriter("LichSu" + soTK + ".txt", true))
                    {
                        sw.WriteLine(lsNew2.id + "#" + lsNew2.tG + "#" + lsNew2.loaiGD + "#" + lsNew2.soTien);
                    }

                    ConsoleColor foreground = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nChuyen tien thanh cong");
                    Console.ResetColor();
                    Console.ReadKey();
                    soTienChuyen(a, b, c, d, user);
                }
                if (chon == 2)
                {
                    Menu_User(a, b, c, d, user);
                }
                if (chon == 3)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine("{0,5}{1}", "", "BAN MUON THOAT?");
                        Console.WriteLine("{0}{1,10}{2}", "1.Co", "", "2.Quay lai");
                        int.TryParse(Console.ReadLine(), out chon);
                    } while (chon != 1 && chon != 2);
                    if (chon == 1)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        soTienChuyen(a, b, c, d, user);
                    }
                }
            } while (chon != 1 && chon != 2 && chon != 3);
        }

        //4_User
        static void Logo_GiaoDich()
        {
            ConsoleColor foreground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.Write("{0,15}{1}", "", "*");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("     XEM GIAO DICH     ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*");
            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.ResetColor();
        }
        static void GiaoDich(List<Admin> a, List<TheTu> b, List<ID> c, List<LSID> d, string user)
        {
            int chon, kT = 0;
            do
            {
                Console.Clear();
                Logo_GiaoDich();

                for (int i = 0; i < d.Count; i++)
                {
                    if (String.Compare(d[i].id, user) == 0)
                    {
                        d[i].Xuat();
                        kT++;
                    }
                }

                if(kT == 0)
                {
                    Console.WriteLine("{0,10}{1,-20}", "", "Chua co giao dich nao");
                }

                Console.WriteLine("1.Quay lai");
                Console.WriteLine("2.Thoat");
                Console.Write("Chon chac nang: ");
                int.TryParse(Console.ReadLine(), out chon);
                if (chon == 1)
                {
                    Menu_User(a, b, c, d, user);
                }
                if (chon == 2)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine("{0,5}{1}", "", "BAN MUON THOAT?");
                        Console.WriteLine("{0}{1,10}{2}", "1.Co", "", "2.Quay lai");
                        int.TryParse(Console.ReadLine(), out chon);
                    } while (chon != 1 && chon != 2);
                    if (chon == 1)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        XemTT(a, b, c, d, user);
                    }
                }
            } while (chon != 1 && chon != 2);
            
        }

        //5_User
        static void Logo_DoiPin()
        {
            ConsoleColor foreground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.Write("{0,15}{1}", "", "*");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("      DOI MA PIN       ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*");
            Console.WriteLine("{0,15}{1}", "", "*************************");
            Console.ResetColor();
        }
        static void DoiPin(List<Admin> a, List<TheTu> b, List<ID> c, List<LSID> d, string user)
        {
            int chon, kT = 0, lanNhap = 0;
            string maCu, maMoi, maLai;

            do
            {
                Console.Clear();
                Logo_DoiPin();

                Console.WriteLine("1.Doi ma Pin");
                Console.WriteLine("2.Quay lai");
                Console.WriteLine("3.Thoat");
                Console.Write("Chon chac nang: ");
                int.TryParse(Console.ReadLine(), out chon);
                if (chon == 1)
                {
                    do
                    {
                        kT = 0;
                        Console.Clear();
                        Logo_DoiPin();

                        Console.Write("{0,10}{1,-20}", "", "Nhap Mat khau cu:");
                        maCu = GetPassword();

                        for (int i = 0; i < b.Count; i++)
                        {
                            //Kiem tra ma duoc nhap
                            if (string.Compare(b[i].id, user) == 0 && string.Compare(b[i].maPin, maCu) == 0)
                            {
                                kT = 1;
                            }
                        }
                        lanNhap++;

                        if (kT != 1)
                        {
                            Console.WriteLine("\nNhap sai!!!");
                            Console.ReadKey();
                        }

                        //Nhap 3l van ko dung thi khoa tai khoan
                        if (lanNhap == 3 && kT != 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nKhoa tai khoan!!!");
                            Console.ResetColor();

                            //Sửa trong file
                            using (StreamWriter sw = new StreamWriter("TheTu.txt"))
                            {
                                sw.WriteLine(b.Count);
                                for (int i = 0; i < b.Count; i++)
                                {
                                    if (string.Compare(user, b[i].id) == 0)
                                    {
                                        sw.WriteLine("0#" + b[i].id + "#" + b[i].maPin);
                                    }
                                    else
                                    {
                                        sw.WriteLine(b[i].tinhTrang + "#" + b[i].id + "#" + b[i].maPin);
                                    }
                                }
                            }
                            Environment.Exit(0);
                        }
                    } while (kT != 1);

                    //Sau khi nhap dung mk cu
                    //nhap mk moi
                    do
                    {
                        Console.Clear();
                        Logo_DoiPin();

                        //Hien thi cho dep
                        Console.Write("{0,10}{1,-20}", "", "Nhap Mat khau cu:");
                        for (int i = 0; i < maCu.Length; i++)
                        {
                            Console.Write("*");
                        }

                        Console.Write("\n{0,10}{1,-20}", "", "Nhap Mat khau moi:");
                        maMoi = GetPassword();
                    } while (maMoi.Length != 6);

                    //Nhap lai mk moi
                    do
                    {
                        Console.Clear();
                        Logo_DoiPin();

                        //Hien thi cho dep
                        Console.Write("{0,10}{1,-20}", "", "Nhap Mat khau cu:");
                        for (int i = 0; i < maCu.Length; i++)
                        {
                            Console.Write("*");
                        }
                        Console.Write("\n{0,10}{1,-20}", "", "Nhap Mat khau moi:");
                        for (int i = 0; i < maMoi.Length; i++)
                        {
                            Console.Write("*");
                        }

                        //Nhap lai mk moi
                        Console.Write("\n{0,10}{1,-20}", "", "Nhap lai:");
                        maLai = GetPassword();

                    } while (!maMoi.Equals(maLai) || maMoi.Length != 6);


                    //Sửa trong file
                    using (StreamWriter sw = new StreamWriter("TheTu.txt"))
                    {
                        sw.WriteLine(b.Count);
                        for (int i = 0; i < b.Count; i++)
                        {
                            if (string.Compare(user, b[i].id) == 0)
                            {
                                sw.WriteLine(b[i].tinhTrang + "#" + b[i].id + "#" + maMoi);
                            }
                            else
                            {
                                sw.WriteLine(b[i].tinhTrang + "#" + b[i].id + "#" + b[i].maPin);
                            }
                        }
                    }

                    //Sửa trong chuong trinh
                    for (int i = 0; i < b.Count; i++)
                    {
                        if (string.Compare(user, b[i].id) == 0)
                        {
                            b[i].maPin = maMoi;
                        }
                    }

                    ConsoleColor foreground = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nSua thanh cong");
                    Console.ResetColor();
                    Console.ReadKey();

                    DoiPin(a, b, c, d, user);
                }
                if (chon == 2)
                {
                    Menu_User(a, b, c, d, user);
                }
                if (chon == 3)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine("{0,5}{1}", "", "BAN MUON THOAT?");
                        Console.WriteLine("{0}{1,10}{2}", "1.Co", "", "2.Quay lai");
                        int.TryParse(Console.ReadLine(), out chon);
                    } while (chon != 1 && chon != 2);
                    if (chon == 1)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        DoiPin(a, b, c, d, user);
                    }
                }
            } while (chon != 1 && chon != 2 && chon != 3);
        }

        //Menu
        static void Menu_User(List<Admin> a, List<TheTu> b, List<ID> c, List<LSID> d, string user)
        {
            int chucNang;
            do
            {
                Console.Clear();
                ConsoleColor foreground = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("");
                Console.WriteLine("{0,12}{1}", "", "*************MENU**************");
                Console.WriteLine("{0,15}{1}", "", "1.Xem thong tin tai khoan");
                Console.WriteLine("{0,15}{1}", "", "2.Rut tien");
                Console.WriteLine("{0,15}{1}", "", "3.Chuyen tien");
                Console.WriteLine("{0,15}{1}", "", "4.Xem noi dung giao dich");
                Console.WriteLine("{0,15}{1}", "", "5.Doi ma Pin");
                Console.WriteLine("{0,15}{1}", "", "6.Thoat");
                Console.WriteLine("{0,12}{1}", "", "*******************************");
                Console.ResetColor();
                Console.Write("Chon chac nang: ");
                int.TryParse(Console.ReadLine(), out chucNang);
            } while (chucNang < 1 || chucNang > 6);

            if (chucNang == 1)
            {
                XemTT(a, b, c, d, user);
            }
            if (chucNang == 2)
            {
                Rut(a, b, c, d, user);
            }
            if (chucNang == 3)
            {
                soTienChuyen(a, b, c, d, user);
            }
            if (chucNang == 4)
            {
                GiaoDich(a, b, c, d, user);
            }
            if (chucNang == 5)
            {
                DoiPin(a, b, c, d, user);
            }
            if (chucNang == 6)
            {
                KT_Thoat(a, b, c, d, 2, user);
            }
        }

        //Kiểm tra đăng nhập cho cả Admin và User
        static void KTra_DangNhap(List<Admin> a, List<TheTu> b, int loai, ref string user) //Nếu loai = 1 là Admin, = 2 là User
        {
            string taiKhoan, matKhau;
            int kT = 0, lanNhap = 0;

            //Kiểm tra của Admin
            if (loai == 1)
            {
                //Kiểm tra theo kT đúng thì kT thành 1 và thoát vòng lập
                do
                {
                    Console.Clear();
                    Logo_DangNhapAdmin();

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("{0,15}{1}", "", "User: ");
                    Console.ResetColor();
                    taiKhoan = Console.ReadLine();

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("{0,15}{1}", "", "Pin: ");
                    Console.ResetColor();
                    matKhau = GetPassword();

                    for (int i = 0; i < a.Count; i++)
                    {
                        if (string.Compare(a[i].user, taiKhoan) == 0 && string.Compare(a[i].pass, matKhau) == 0)
                        {
                            kT++;
                        }
                    }

                    lanNhap++;

                    if (lanNhap == 3 && kT == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nNhap qua so lan quy dinh!!!");
                        Console.ResetColor();
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                } while (kT == 0);
            }
            //Kiểm tra của User
            else
            {
                do
                {
                    Console.Clear();
                    Logo_DangNhapUser();

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("{0,15}{1}", "", "User: ");
                    Console.ResetColor();
                    taiKhoan = Console.ReadLine();

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("{0,15}{1}", "", "Pin: ");
                    Console.ResetColor();
                    matKhau = GetPassword();

                    for (int i = 0; i < b.Count; i++)
                    {
                        if (string.Compare(b[i].id, taiKhoan) == 0) //Kiểm tra tài khoản tồn tại hay không
                        {
                            //Rồi kiểm tra trình trạng tài khoản
                            if (string.Compare(b[i].tinhTrang, "1") == 0) //Không bị khóa
                            {
                                user = b[i].id; //Xác định tài khoản
                                                //Sau cùng kiểm tra mật khẩu đã nhập
                                if (string.Compare(b[i].maPin, matKhau) == 0) //Nếu đúng
                                {
                                    kT++; //Trường hợp thỏa tất cả điều kiện
                                    break;
                                }
                                else //Nếu sai
                                {
                                    Console.WriteLine("\nMat khau sai. Nhap lai!!!");
                                    Console.ReadKey();
                                    lanNhap++;

                                    //Cho nhập lại mk 2 lần
                                    do
                                    {
                                        Console.Clear();
                                        Logo_DangNhapUser();

                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        Console.Write("{0,15}{1}", "", "User: ");
                                        Console.ResetColor();
                                        Console.WriteLine(taiKhoan);

                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        Console.Write("{0,15}{1}", "", "Pin: ");
                                        Console.ResetColor();
                                        matKhau = GetPassword();

                                        if (string.Compare(b[i].maPin, matKhau) == 0)
                                        {
                                            kT++; //Trường hợp thỏa tất cả điều kiện
                                        }
                                        else
                                        {
                                            lanNhap++; //Trường hợp sai mật khẩu
                                            if (lanNhap != 3)
                                            {
                                                Console.WriteLine("\nMat khau sai. Nhap lai!!!");
                                                Console.ReadKey();
                                            }
                                        }
                                    } while (kT == 0 && lanNhap != 3);
                                }
                            }
                            else //Bị khóa
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nTai khoan nay hien dang bi khoa!!!");
                                Console.ResetColor();
                                Console.ReadKey();
                                break;
                            }
                        }
                    }
                    if (string.Compare(user, "") == 0)
                    {
                        lanNhap++;
                        if (lanNhap != 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nTai khoan hoac mat khau sai. Nhap lai!!!");
                            Console.ResetColor();
                            Console.ReadKey();
                        }
                    }

                    if (lanNhap == 3 && user != "") //Khóa TK
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nKhoa tai khoan!!!");
                        Console.ResetColor();

                        //Sửa trong file
                        using (StreamWriter sw = new StreamWriter("TheTu.txt"))
                        {
                            sw.WriteLine(b.Count);
                            for (int i = 0; i < b.Count; i++)
                            {
                                if (string.Compare(user, b[i].id) == 0)
                                {
                                    sw.WriteLine("0#" + b[i].id + "#" + b[i].maPin);
                                }
                                else
                                {
                                    sw.WriteLine(b[i].tinhTrang + "#" + b[i].id + "#" + b[i].maPin);
                                }
                            }
                        }

                        Console.ReadKey();
                        Environment.Exit(0);
                    }

                    if (lanNhap == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nNhap qua so lan quy dinh!!!");
                        Console.ResetColor();
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                } while (kT == 0);
            }
        }

        //Ẩn Pass
        static string GetPassword()
        {
            string temp = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                //Kiểm tra có phải là phím Backspace hay không
                if (info.Key != ConsoleKey.Backspace)
                {
                    temp += info.KeyChar;
                    Console.Write("*");

                }
                else
                {
                    if (temp.Length > 0)
                    {
                        //Bỏ ký tự cuối cùng
                        temp = temp.Substring(0, temp.Length - 1);
                    }
                }

                //Đọc ký tự tiếp theo để xử lý
                info = Console.ReadKey(true);
            }

            return temp;
        }

        //Kiểm tra thoát
        static void KT_Thoat(List<Admin> a, List<TheTu> b, List<ID> c, List<LSID> d, int loai, string user)
        {
            int kTThoat;

            do
            {
                Console.Clear();
                Console.WriteLine("{0,5}{1}", "", "BAN MUON THOAT?");
                Console.WriteLine("{0}{1,10}{2}", "1.Co", "", "2.Quay lai");
                int.TryParse(Console.ReadLine(), out kTThoat);
            } while (kTThoat != 1 && kTThoat != 2);
            if (kTThoat == 1)
            {
                Environment.Exit(0);
            }
            else
            {
                if (loai == 1)
                {
                    Menu_Admin(a, b, c, d);
                }
                if (loai == 2)
                {
                    Menu_User(a, b, c, d, user);
                }
            }
        }



        //Hàm kiểm để kiểm tra DL khi cần
        static void Xuat(List<Admin> a, List<TheTu> b, List<ID> c, List<LSID> d)
        {
            Console.WriteLine("Admin");
            for (int i = 0; i < a.Count; i++)
            {
                Console.WriteLine(a[i].user);
                Console.WriteLine(a[i].pass);
            }
            Console.WriteLine("TheTu");
            for (int i = 0; i < b.Count; i++)
            {
                Console.WriteLine(b[i].tinhTrang);
                Console.WriteLine(b[i].id);
                Console.WriteLine(b[i].maPin);
            }
            Console.WriteLine("ID");
            for (int i = 0; i < c.Count; i++)
            {
                Console.WriteLine(c[i].id);
                Console.WriteLine(c[i].ten);
                Console.WriteLine(c[i].soDu);
                Console.WriteLine(c[i].tienTe);
            }
            Console.WriteLine("LSID");
            for (int i = 0; i < d.Count; i++)
            {
                Console.WriteLine(d[i].id);
                Console.WriteLine(d[i].tG);
                Console.WriteLine(d[i].loaiGD);
                Console.WriteLine(d[i].soTien);
            }
        }

        //Nạp dữ liệu từ file
        static void DocFile(List<Admin> a, List<TheTu> b, List<ID> c, List<LSID> d)
        {
            //Khai báo
            string file1 = "Admin", file2 = "TheTu", file3 = "";
            string dauCach = "#";

            //Đọc file Admin
            using (StreamReader sr = new StreamReader(file1 + ".txt"))
            {
                int soLuong;

                string[] a1 = new string[] { dauCach };
                string line;

                line = sr.ReadLine();
                int.TryParse(line, out soLuong);

                for (int i = 0; i < soLuong; i++)
                {
                    Admin admin = new Admin();
                    line = sr.ReadLine();
                    string[] a2 = line.Split(a1, StringSplitOptions.RemoveEmptyEntries);
                    admin.user = a2[0];
                    admin.pass = a2[1];
                    a.Add(admin);
                }
            }

            //Đọc file TheTu
            using (StreamReader srTheTu = new StreamReader(file2 + ".txt"))
            {
                int soLuong;

                string[] a1 = new string[] { dauCach };
                string line;

                line = srTheTu.ReadLine();
                int.TryParse(line, out soLuong);

                for (int i = 0; i < soLuong; i++)
                {
                    TheTu theTu = new TheTu();
                    line = srTheTu.ReadLine();
                    string[] a2 = line.Split(a1, StringSplitOptions.RemoveEmptyEntries);
                    theTu.tinhTrang = a2[0];
                    theTu.id = a2[1];
                    theTu.maPin = a2[2];

                    b.Add(theTu);
                }
            }

            for (int i = 0; i < b.Count; i++)
            {
                file3 = b[i].id;

                //Đọc file các ID
                using (StreamReader srID = new StreamReader(file3 + ".txt"))
                {
                    ID id = new ID();
                    string[] a1 = new string[] { dauCach };
                    string line;

                    line = srID.ReadLine();
                    string[] a2 = line.Split(a1, StringSplitOptions.RemoveEmptyEntries);

                    id.id = a2[0];
                    id.ten = a2[1];
                    int.TryParse(a2[2], out id.soDu);
                    id.tienTe = a2[3];

                    c.Add(id);
                }


                //Tạo các file LS chưa có
                if (!File.Exists("LichSu" + file3 + ".txt"))
                {
                    using (StreamWriter sw = new StreamWriter("LichSu" + file3 + ".txt"))
                    {
                    }
                }
            }

            for (int i = 0; i < b.Count; i++)
            {
                file3 = b[i].id;

                //Đọc các file LS
                using (StreamReader srLSID = new StreamReader("LichSu" + file3 + ".txt"))
                {
                    LSID lsid = new LSID();

                    string[] a1 = new string[] { dauCach };
                    string line;

                    line = srLSID.ReadLine();

                    //Có ghi mới đọc
                    if (line != null)
                    {
                        string[] a2 = line.Split(a1, StringSplitOptions.RemoveEmptyEntries);

                        lsid.id = a2[0];
                        DateTime.TryParse(a2[1], out lsid.tG);
                        lsid.loaiGD = a2[2];
                        int.TryParse(a2[3], out lsid.soTien);

                        d.Add(lsid);
                    }
                }
            }
        }
    }
}
