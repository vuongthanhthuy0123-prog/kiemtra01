using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoSpeed
{
    public abstract class PhuongTien
    {
        private string _maPT;
        private string _tenHang;
        private int _namSanXuat;
        private decimal _giaGoc;

        public string MaPT
        {
            get { return _maPT; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _maPT = "PT000";
                }
                else
                {
                    _maPT = value;
                }
            }
        }

        public string TenHang
        {
            get { return _tenHang; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Tên hãng không được để trống!");
                }

                _tenHang = value;
            }
        }

        public int NamSanXuat
        {
            get { return _namSanXuat; }
            set
            {
                int namHienTai = DateTime.Now.Year;

                if (value < 1900 || value > namHienTai)
                {
                    throw new ArgumentException(
                        "Năm sản xuất không hợp lệ!"
                    );
                }

                _namSanXuat = value;
            }
        }

        public decimal GiaGoc
        {
            get { return _giaGoc; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(
                        "Giá gốc phải lớn hơn 0!"
                    );
                }

                _giaGoc = value;
            }
        }

        public PhuongTien(
            string maPT,
            string tenHang,
            int namSanXuat,
            decimal giaGoc)
        {
            MaPT = maPT;
            TenHang = tenHang;
            NamSanXuat = namSanXuat;
            GiaGoc = giaGoc;
        }

        public abstract decimal TinhGiaLanBanh();

        public virtual string GetInfo()
        {
            return $"Mã PT: {MaPT} | " +
                   $"Hãng: {TenHang} | " +
                   $"Năm SX: {NamSanXuat} | " +
                   $"Giá gốc: {GiaGoc:N0} VNĐ";
        }
    }

    public class OTo : PhuongTien
    {
        private int _soChoNgoi;
        private double _dungTichDongCo;

        public int SoChoNgoi
        {
            get { return _soChoNgoi; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(
                        "Số chỗ ngồi phải lớn hơn 0!"
                    );
                }

                _soChoNgoi = value;
            }
        }

        public double DungTichDongCo
        {
            get { return _dungTichDongCo; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(
                        "Dung tích động cơ phải lớn hơn 0!"
                    );
                }

                _dungTichDongCo = value;
            }
        }

        public OTo(
            string maPT,
            string tenHang,
            int namSanXuat,
            decimal giaGoc,
            int soChoNgoi,
            double dungTichDongCo)
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            SoChoNgoi = soChoNgoi;
            DungTichDongCo = dungTichDongCo;
        }

        public override decimal TinhGiaLanBanh()
        {
            if (SoChoNgoi <= 9)
            {
                return GiaGoc
                     + GiaGoc * 0.12m
                     + GiaGoc * 0.30m;
            }
            else
            {
                return GiaGoc
                     + GiaGoc * 0.10m;
            }
        }

        public override string GetInfo()
        {
            return base.GetInfo()
                   + $" | Số chỗ: {SoChoNgoi}"
                   + $" | Dung tích động cơ: {DungTichDongCo} L";
        }
    }

    public class XeMay : PhuongTien
    {
        private int _dungTichXylanh;

        public int DungTichXylanh
        {
            get { return _dungTichXylanh; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(
                        "Dung tích xy-lanh phải lớn hơn 0!"
                    );
                }

                _dungTichXylanh = value;
            }
        }

        public XeMay(
            string maPT,
            string tenHang,
            int namSanXuat,
            decimal giaGoc,
            int dungTichXylanh)
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            DungTichXylanh = dungTichXylanh;
        }

        public override decimal TinhGiaLanBanh()
        {
            if (DungTichXylanh < 175)
            {
                return GiaGoc + GiaGoc * 0.02m;
            }
            else
            {
                return GiaGoc + GiaGoc * 0.05m;
            }
        }

        public override string GetInfo()
        {
            return base.GetInfo()
                   + $" | Dung tích xy-lanh: {DungTichXylanh} cc";
        }
    }

    public class QuanLyPhuongTien
    {
        private List<PhuongTien> danhSach;

        public QuanLyPhuongTien()
        {
            danhSach = new List<PhuongTien>();
        }

        public void AddPhuongTien(PhuongTien pt)
        {
            if (pt == null)
            {
                throw new ArgumentNullException(
                    nameof(pt),
                    "Phương tiện không được null!"
                );
            }

            danhSach.Add(pt);
        }

        public void DisplayAll()
        {
            if (danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách phương tiện đang trống!");
                return;
            }

            Console.WriteLine("\nDANH SÁCH PHƯƠNG TIỆN");

            foreach (PhuongTien pt in danhSach)
            {
                Console.WriteLine(pt.GetInfo());
                Console.WriteLine(
                    $"Giá lăn bánh: {pt.TinhGiaLanBanh():N0} VNĐ"
                );
            }
        }

        public PhuongTien FindMaxGiaLanBanh()
        {
            if (danhSach.Count == 0)
            {
                return null;
            }

            return danhSach
                .OrderByDescending(pt => pt.TinhGiaLanBanh())
                .First();
        }

        public List<PhuongTien> SearchByName(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return new List<PhuongTien>();
            }

            return danhSach
                .Where(pt =>
                    pt.TenHang.Contains(
                        keyword,
                        StringComparison.OrdinalIgnoreCase
                    ))
                .ToList();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("HỆ THỐNG QUẢN LÝ PHƯƠNG TIỆN AUTOSPEED");

            Console.WriteLine("\nTC01: VALIDATION NĂM SẢN XUẤT");

            try
            {
                OTo otoLoi = new OTo(
                    "PT001",
                    "Toyota",
                    1850,
                    1000000000m,
                    5,
                    2.0
                );

                Console.WriteLine("Tạo phương tiện thành công.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Bắt được lỗi: {ex.Message}");
                Console.WriteLine("TC01: PASS");
            }

            OTo oto1 = new OTo(
                "OT001",
                "Toyota",
                2024,
                1000000000m,
                5,
                2.0
            );

            XeMay xeMay1 = new XeMay(
                "XM001",
                "Honda",
                2023,
                50000000m,
                150
            );

            Console.WriteLine("\nTC02: GIÁ LĂN BÁNH Ô TÔ");

            decimal giaOTo = oto1.TinhGiaLanBanh();

            Console.WriteLine(
                $"Giá gốc: {oto1.GiaGoc:N0} VNĐ"
            );

            Console.WriteLine(
                $"Giá lăn bánh: {giaOTo:N0} VNĐ"
            );

            if (giaOTo == 1420000000m)
            {
                Console.WriteLine("TC02: PASS");
            }
            else
            {
                Console.WriteLine("TC02: FAIL");
            }

            Console.WriteLine("\nTC03: GIÁ LĂN BÁNH XE MÁY");

            decimal giaXeMay = xeMay1.TinhGiaLanBanh();

            Console.WriteLine(
                $"Giá gốc: {xeMay1.GiaGoc:N0} VNĐ"
            );

            Console.WriteLine(
                $"Giá lăn bánh: {giaXeMay:N0} VNĐ"
            );

            if (giaXeMay == 51000000m)
            {
                Console.WriteLine("TC03: PASS");
            }
            else
            {
                Console.WriteLine("TC03: FAIL");
            }

            Console.WriteLine("\nTC04: KIỂM TRA ĐA HÌNH");

            List<PhuongTien> danhSach = new List<PhuongTien>();

            danhSach.Add(oto1);
            danhSach.Add(xeMay1);

            foreach (PhuongTien pt in danhSach)
            {
                Console.WriteLine(
                    $"{pt.GetInfo()}"
                );

                Console.WriteLine(
                    $"Giá lăn bánh: {pt.TinhGiaLanBanh():N0} VNĐ"
                );

                Console.WriteLine();
            }

            Console.WriteLine(
                "C# đã tự động gọi đúng TinhGiaLanBanh() " +
                "của từng lớp con."
            );

            Console.WriteLine("TC04: PASS");

            Console.WriteLine("\nTC05: TÌM GIÁ LĂN BÁNH MAX");

            QuanLyPhuongTien quanLy = new QuanLyPhuongTien();

            quanLy.AddPhuongTien(oto1);
            quanLy.AddPhuongTien(xeMay1);

            PhuongTien ptMax = quanLy.FindMaxGiaLanBanh();

            if (ptMax != null)
            {
                Console.WriteLine("Phương tiện có giá lăn bánh cao nhất:");

                Console.WriteLine(ptMax.GetInfo());

                Console.WriteLine(
                    $"Giá lăn bánh: {ptMax.TinhGiaLanBanh():N0} VNĐ"
                );

                if (ptMax == oto1)
                {
                    Console.WriteLine("TC05: PASS");
                }
                else
                {
                    Console.WriteLine("TC05: FAIL");
                }
            }

            quanLy.DisplayAll();

            Console.WriteLine("\nTÌM KIẾM THEO TÊN HÃNG");

            string keyword = "Toyota";

            List<PhuongTien> ketQua =
                quanLy.SearchByName(keyword);

            Console.WriteLine(
                $"Kết quả tìm kiếm với từ khóa: {keyword}"
            );

            if (ketQua.Count == 0)
            {
                Console.WriteLine("Không tìm thấy phương tiện!");
            }
            else
            {
                foreach (PhuongTien pt in ketQua)
                {
                    Console.WriteLine(pt.GetInfo());
                }
            }


            Console.WriteLine("\nNhấn phím bất kỳ để kết thúc...");
            Console.ReadKey();
        }
    }
}