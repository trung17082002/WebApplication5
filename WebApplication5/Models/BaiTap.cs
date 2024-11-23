using System;
using System.Collections.Generic;

namespace WebApplication5.Models
{
    public class BaiTap
    {
        public BaiTap()
        {
        }

        public BaiTap(int id, string ten, string filePath, string nhanXet, DateTime ngayNop, DateTime ngayBatDau, DateTime ngayKetThuc, bool hoanThanh, int sinhVienId, SinhVien sinhVien)
        {
            Id = id;
            Ten = ten;
            FilePath = filePath;
            NhanXet = nhanXet;
            NgayNop = ngayNop;
            NgayBatDau = ngayBatDau;
            NgayKetThuc = ngayKetThuc;
            HoanThanh = hoanThanh;
            SinhVienId = sinhVienId;
            SinhVien = sinhVien;
        }

        public int Id { get; set; }
        public string Ten { get; set; }
        public string FilePath { get; set; }
        public string NhanXet { get; set; }
        public DateTime NgayNop { get; set; } // Nullable
        public DateTime NgayBatDau { get; set; } // Nullable
        public DateTime NgayKetThuc { get; set; } // Nullable
        public bool HoanThanh { get; set; } // Nullable

        public int SinhVienId { get; set; }
        public SinhVien SinhVien { get; set; } // Liên kết với SinhVien

    }
}
