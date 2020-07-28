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
                    matKhau = Console.ReadLine();

                    for (int i = 0; i < a.Count; i++)
                    {
                        if (string.Compare(a[i].user, taiKhoan) == 0 && string.Compare(a[i].pass, matKhau) == 0)
                        {
                            kT++;
                        }
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
                    matKhau = Console.ReadLine();

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
                                    Console.WriteLine("Mat khau sai. Nhap lai!!!");
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
                                        matKhau = Console.ReadLine();

                                        if (string.Compare(b[i].maPin, matKhau) == 0)
                                        {
                                            kT++; //Trường hợp thỏa tất cả điều kiện
                                        }
                                        else
                                        {
                                            lanNhap++; //Trường hợp sai mật khẩu
                                            if (lanNhap != 3)
                                            {
                                                Console.WriteLine("Mat khau sai. Nhap lai!!!");
                                                Console.ReadKey();
                                            }
                                        }
                                    } while (kT == 0 && lanNhap != 3);
                                }
                            }
                            else //Bị khóa
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Tai khoan nay hien dang bi khoa!!!");
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
                            Console.WriteLine("Tai khoan hoac mat khau sai. Nhap lai!!!");
                            Console.ResetColor();
                            Console.ReadKey();
                        }
                    }

                    if (lanNhap == 3 && user != "") //Khóa TK
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Khoa tai khoan!!!");
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

                        //Sửa trong chương trình  
                        for (int i = 0; i < b.Count; i++)
                        {
                            if (string.Compare(user, b[i].id) == 0)
                            {
                                b[i].tinhTrang = "0";
                                break;
                            }
                        }

                        Console.ReadKey();
                        Environment.Exit(0);
                    }

                    if (lanNhap == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Nhap qua so lan quy dinh!!!");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                } while (kT == 0);
            }
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
                    Console.WriteLine("{0,20}{1}{2}", "", "Tai khoan thu ", i + 1);

                    Console.Write("ID: {0}", b[i].id);
                    Console.Write("\tMa PIN: {0}", b[i].maPin);
                    if (string.Compare(b[i].tinhTrang, "0") == 0)
                    {
                        Console.WriteLine("\tTinh trang: KHOA");
                    }
                    else
                    {
                        Console.WriteLine("");
                    }

                    Console.WriteLine("{0,23}{1}", "", "----------");
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
                    Console.WriteLine("{0,20}{1}{2}", "", "Tai khoan thu ", i + 1);

                    Console.Write("ID: {0}", b[i].id);
                    Console.Write("\tMa PIN: {0}", b[i].maPin);
                    if (string.Compare(b[i].tinhTrang, "0") == 0)
                    {
                        Console.WriteLine("\tTinh trang: KHOA");
                    }
                    else
                    {
                        Console.WriteLine("");
                    }

                    Console.WriteLine("{0,23}{1}", "", "----------");
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
                            Console.WriteLine("{0,20}{1}{2}", "", "Tai khoan thu ", i + 1);

                            Console.Write("ID: {0}", b[i].id);
                            Console.Write("\tMa PIN: {0}", b[i].maPin);
                            if (string.Compare(b[i].tinhTrang, "0") == 0)
                            {
                                Console.WriteLine("\tTinh trang: KHOA");
                            }
                            else
                            {
                                Console.WriteLine("");
                            }

                            Console.WriteLine("{0,23}{1}", "", "----------");
                        }
                        Console.Write("Nhap ID muon xoa: ");
                        ID = Console.ReadLine();

                        for (int i = 0; i < b.Count; i++)
                        {
                            if (string.Compare(ID, b[i].id) == 0)
                            {
                                //Xóa file
                                File.Delete(@"ID\" + ID + ".txt");
                                File.Delete(@"LSID\" + ID + ".txt");
 
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
                        MoKhoa(a, b, c, d);
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
                        Console.WriteLine("{0,20}{1}{2}", "", "Tai khoan thu ", ++j);
                        Console.WriteLine("\t\tID: {0}", b[i].id);
                        Console.WriteLine("{0,23}{1}", "", "----------");
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
                            Console.WriteLine("{0,20}{1}{2}", "", "Tai khoan thu ", i + 1);
                            Console.WriteLine("\t\tID: {0}", b[i].id);
                            Console.WriteLine("{0,23}{1}", "", "----------");

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

            }
            if (chucNang == 2)
            {
                ConsoleColor foreground = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                SoTienRut(a,b,c,d,user);
            }
            if (chucNang == 3)
            {

            }
            if (chucNang == 4)
            {

            }
            if (chucNang == 5)
            {

            }
            if (chucNang == 6)
            {
                KT_Thoat(a, b, c, d, 2, user);
            }
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

        //Xuất dữ liệu
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
                Console.WriteLine(d[i].loaiGD);
                Console.WriteLine(d[i].soTien);
                Console.WriteLine(d[i].tG);
            }

        }

        //Nạp dữ liệu từ file
        static void DocFile(List<Admin> a, List<TheTu> b, List<ID> c, List<LSID> d)
        {
            //Khai báo
            string file1 = "Admin", file2 = "TheTu", file3 = "";
            string dauCach = "#";

            //Đọc file Admin
            try
            {
                StreamReader sr = new StreamReader(file1 + ".txt");
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
                sr.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //Đọc file TheTu và những file còn lại
            try
            {
                //File TheTu
                StreamReader srTheTu = new StreamReader(file2 + ".txt");
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
                srTheTu.Close();

                for (int i = 0; i < b.Count; i++)
                {
                    //File ID
                    file3 = b[i].id;

                    StreamReader srID = new StreamReader(@"ID\" + file3 + ".txt");

                    ID id = new ID();
                    line = srID.ReadLine();
                    string[] a2 = line.Split(a1, StringSplitOptions.RemoveEmptyEntries);

                    id.id = a2[0];
                    id.ten = a2[1];
                    int.TryParse(a2[2], out id.soDu);
                    id.tienTe = a2[3];

                    c.Add(id);
                    srID.Close();

                    //File LSID
                    StreamReader srLSID = new StreamReader(@"LSID\" + file3 + ".txt");

                    LSID lsid = new LSID();
                    line = srLSID.ReadLine();
                    string[] a3 = line.Split(a1, StringSplitOptions.RemoveEmptyEntries);

                    lsid.id = a3[0];
                    lsid.loaiGD = a3[1];
                    int.TryParse(a3[2], out lsid.soTien);
                    lsid.tG = a3[3];

                    d.Add(lsid);
                    srLSID.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void SoTienRut(List<Admin> a, List<TheTu> b, List<ID> c, List<LSID> d, string user)
        {
            //string file = "";
            //StreamReader sr = new StreamReader(@"ID\" + "" + ".txt");
            Console.Clear();
            int soDu = 0;
            int tienRut;
            tienrut:
            Console.Write("nhap so tien ban muon rut: ");
            int.TryParse(Console.ReadLine(), out tienRut);
            for(int i = 0; i < c.Count; i++)
            {
                if (tienRut > c[i].soDu)
                {
                    Console.WriteLine("Rut tien khong thanh cong. so du khong du.");
                    goto tienrut;
                }
                else if (tienRut <= 49999)
                {
                    Console.WriteLine("So tien ban muon rut phai lon hon 50000");
                    goto tienrut;
                }
                else if (tienRut > c[i].soDu - 50000)
                {
                    Console.WriteLine("So du khong du. So du trong tai khoang phai >= 50000");
                    goto tienrut;
                }
                else
                {
                    soDu = c[i].soDu - tienRut;
                    Console.WriteLine("Rut tien thanh cong !");
                    Console.WriteLine("So du con lai la: {0}", soDu);
                }
            }
            int chon;
            do
            {

                Console.WriteLine("1.Quay lai");
                Console.WriteLine("2.Thoat");
                Console.Write("Chon chac nang: ");
                int.TryParse(Console.ReadLine(), out chon);
                if(chon == 1)
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
                        Menu_User(a, b, c, d, user);
                    }
                }
            } while (chon != 1 && chon != 2);

        }
    }
}

