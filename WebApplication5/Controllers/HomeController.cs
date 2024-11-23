using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PagedList.Core;
using System.Diagnostics;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private static List<SinhVien> sinhViens = new List<SinhVien>
        {
            new SinhVien { Id = 1, HoTen = "Nguyễn Văn A", Lop = "IT01", Email = "a@student.com" },
            new SinhVien { Id = 2, HoTen = "Trần Thị B", Lop = "IT02", Email = "b@student.com" },
            new SinhVien { Id = 3, HoTen = "Phạm Văn C", Lop = "IT03", Email = "c@student.com" }
        };

        private static List<BaiTap> baiTaps = new List<BaiTap>
        {
            new BaiTap
            {
                Id = 1,
                Ten = "Bài tập 1",
                FilePath = "/uploads/file1.pdf",
                NhanXet = "Tốt",
                NgayNop = DateTime.Now.AddDays(-3),
                NgayBatDau = DateTime.Now.AddDays(-7),
                NgayKetThuc = DateTime.Now.AddDays(-1),
                HoanThanh = true,
                SinhVienId = 1
            },
            new BaiTap
            {
                Id = 2,
                Ten = "Bài tập 2",
                FilePath = "/uploads/file2.pdf",
                NhanXet = "Cần cải thiện",
                NgayNop = DateTime.Now.AddDays(-2),
                NgayBatDau = DateTime.Now.AddDays(-6),
                NgayKetThuc = DateTime.Now.AddDays(-1),
                HoanThanh = false,
                SinhVienId = 2
            },
            new BaiTap
            {
                Id = 3,
                Ten = "Bài tập 3",
                FilePath = "/uploads/file3.pdf",
                NhanXet = "Đạt yêu cầu",
                NgayNop = DateTime.Now.AddDays(-1),
                NgayBatDau = DateTime.Now.AddDays(-5),
                NgayKetThuc = DateTime.Now.AddDays(0),
                HoanThanh = true,
                SinhVienId = 3
            }
        };
        public IActionResult Index(int? page)
        {
            // Giả lập lấy danh sách bài tập và sinh viên liên quan
            var baiTapsWithSinhVien = baiTaps.Select(bt => new BaiTap
            {
                Id = bt.Id,
                Ten = bt.Ten,
                FilePath = bt.FilePath,
                NhanXet = bt.NhanXet,
                NgayNop = bt.NgayNop,
                NgayBatDau = bt.NgayBatDau,
                NgayKetThuc = bt.NgayKetThuc,
                HoanThanh = bt.HoanThanh,
                SinhVien = sinhViens.FirstOrDefault(sv => sv.Id == bt.SinhVienId) // Kết nối sinh viên với bài tập
            }).AsQueryable(); // Chuyển sang IQueryable

            // Số lượng bài tập trên mỗi trang
            int pageSize = 5;

            // Trang hiện tại (mặc định là 1 nếu không có page)
            int pageNumber = page ?? 1;

            // Sử dụng PagedList để tạo danh sách phân trang
            var pagedList = baiTapsWithSinhVien.ToPagedList(pageNumber, pageSize);

            return View(pagedList);
        }


        [HttpGet]
        public IActionResult View(int id)
        {
            // Giả lập lấy bài tập theo ID
            var baiTap = baiTaps.FirstOrDefault(b => b.Id == id);

            if (baiTap == null) return NotFound(); // Nếu không tìm thấy bài tập

            // Lấy thông tin sinh viên tương ứng
            baiTap.SinhVien = sinhViens.FirstOrDefault(sv => sv.Id == baiTap.SinhVienId);

            return View(baiTap); // Trả về bài tập cùng sinh viên cho view
        }
        [HttpPost]
        public IActionResult View(int id, string nhanXet, IFormFile uploadedFile)
        {
            // Lấy bài tập theo ID
            var baiTap = baiTaps.FirstOrDefault(b => b.Id == id);
            if (baiTap == null) return NotFound();

            // Cập nhật nhận xét
            baiTap.NhanXet = nhanXet;

            // Xử lý file tải lên (nếu có)
            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                // Đặt đường dẫn lưu file (thư mục `uploads` trong ứng dụng)
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                Directory.CreateDirectory(uploadsFolder); // Đảm bảo thư mục tồn tại

                var filePath = Path.Combine(uploadsFolder, uploadedFile.FileName);

                // Lưu tệp lên server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    uploadedFile.CopyTo(stream);
                }

                // Cập nhật đường dẫn file trong bài tập
                baiTap.FilePath = $"/uploads/{uploadedFile.FileName}";
            }

            // Quay lại màn Index
            return RedirectToAction("Index");
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}