namespace WebApplication5.Models
{
    public interface IBaiTap
    {
        string FilePath { get; set; }
        bool HoanThanh { get; set; }
        int Id { get; set; }
        DateTime NgayBatDau { get; set; }
        DateTime NgayKetThuc { get; set; }
        DateTime NgayNop { get; set; }
        string NhanXet { get; set; }
        SinhVien SinhVien { get; set; }
        int SinhVienId { get; set; }
        string Ten { get; set; }
    }
}