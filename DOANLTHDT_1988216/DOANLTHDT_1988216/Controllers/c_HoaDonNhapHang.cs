using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOANLTHDT_1988216.Entities;
using DOANLTHDT_1988216.Models;
using DOANLTHDT_1988216.Functions;

namespace DOANLTHDT_1988216.Controllers
{
    public class c_HoaDonNhapHang
    {

        //private MyUltilities _ultilities;
        private m_HoaDonNhapHang _m_HoaDonNhapHang;
        private m_MatHang _m_MatHang;
        public c_HoaDonNhapHang()
        {
            //this._ultilities = new MyUltilities();
            this._m_HoaDonNhapHang = new m_HoaDonNhapHang();
            this._m_MatHang = new m_MatHang();
        }

        public List<HoaDonNhapHang> getDanhSachHoaDonNhapHang()
        {
            return _m_HoaDonNhapHang.getAllHoaDonNhapHang();
        }

        public void themHoaDonNhapHang(string idMatHang, string SoLuong, string DonGia, string PhiVanChuyen, string NgayNhap)
        {
            // Convert dữ liệu
            int idMH = int.Parse(idMatHang);
            int sl = int.Parse(SoLuong);
            int dg = int.Parse(DonGia);
            int pvc = int.Parse(PhiVanChuyen);
            DateTime NgayNhapHang = DateTime.Parse(NgayNhap);

            // Tạo Object HoaDonNhapHang mới
            HoaDonNhapHang hd = new HoaDonNhapHang();
            hd.MA_HOA_DON = 0; // Gán default
            hd.MA_MAT_HANG = idMH;
            hd.SO_LUONG = sl;
            hd.DON_GIA = dg;
            hd.PHI_SHIP = pvc;
            hd.NGAY_NHAP = NgayNhapHang;

            // Gọi Models
            _m_HoaDonNhapHang.addHoaDonNhapHang(hd);
        }

        public List<HoaDonNhapHang> TimKiemHoaDon(string keyword, string type)
        {
            List<HoaDonNhapHang> ds = this.getDanhSachHoaDonNhapHang();
            List<HoaDonNhapHang> result = new List<HoaDonNhapHang>();
            switch (type)
            {
                case "ten_mat_hang":
                    foreach (var d in ds)
                    {
                        if (_m_MatHang.getMatHangById(d.MA_MAT_HANG).TEN_MAT_HANG.ToLower().Contains(keyword.ToLower()))
                        {
                            result.Add(d);
                        }
                    }
                    break;
                case "ma_hoa_don":
                    foreach (var d in ds)
                    {
                        // Kiểm tra tính hợp lệ số nguyên
                        int n = 0;
                        if (MyUltilities.isInt(keyword, ref n))
                        {
                            if (d.MA_HOA_DON == n)
                            {
                                result.Add(d);
                            }
                        }
                    }
                    break;
                case "ma_mat_hang":
                    foreach (var d in ds)
                    {
                        // Kiểm tra tính hợp lệ số nguyên
                        int n = 0;
                        if (MyUltilities.isInt(keyword, ref n))
                        {
                            if (d.MA_MAT_HANG == n)
                            {
                                result.Add(d);
                            }
                        }
                    }
                    break;
            }
            return result;
        }

        public HoaDonNhapHang getHoaDonById(string id)
        {
            return _m_HoaDonNhapHang.getHoaDonById(int.Parse(id));
        }

        public bool suaHoaDonNhapHang(string id, string MaMH, string SoLuong, string DonGia, string PhiVanChuyen, string NgayNhap)
        {
            // Convert dữ liệu
            int idHD = int.Parse(id);
            int idMH = int.Parse(MaMH);
            int sl = int.Parse(SoLuong);
            int dg = int.Parse(DonGia);
            int pvc = int.Parse(PhiVanChuyen);
            DateTime NgayNhapHang = DateTime.Parse(NgayNhap);

            // Tạo Object HoaDonNhapHang mới
            HoaDonNhapHang hd = new HoaDonNhapHang();
            hd.MA_HOA_DON = idHD;
            hd.MA_MAT_HANG = idMH;
            hd.SO_LUONG = sl;
            hd.DON_GIA = dg;
            hd.PHI_SHIP = pvc;
            hd.NGAY_NHAP = NgayNhapHang;

            // Gọi model
            return _m_HoaDonNhapHang.updateHoaDon(hd);
        }

        public bool xoaHoaDon(string id)
        {
            return _m_HoaDonNhapHang.deleteHoaDon(int.Parse(id));
        }
    }
}