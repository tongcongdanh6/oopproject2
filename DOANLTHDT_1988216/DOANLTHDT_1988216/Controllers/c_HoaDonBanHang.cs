using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOANLTHDT_1988216.Entities;
using DOANLTHDT_1988216.Models;
using DOANLTHDT_1988216.Functions;

namespace DOANLTHDT_1988216.Controllers
{
    public class c_HoaDonBanHang
    {

        //private MyUltilities _ultilities;
        private m_HoaDonBanHang _m_HoaDonBanHang;
        private m_MatHang _m_MatHang;
        public c_HoaDonBanHang()
        {
            //this._ultilities = new MyUltilities();
            this._m_HoaDonBanHang = new m_HoaDonBanHang();
            this._m_MatHang = new m_MatHang();
        }

        public List<HoaDonBanHang> getDanhSachHoaDonBanHang()
        {
            return _m_HoaDonBanHang.getAllHoaDonBanHang();
        }

        public void themHoaDonBanHang(string idMatHang, string SoLuong, string DonGia, string PhiVanChuyen, string NgayBan)
        {
            // Convert dữ liệu
            int idMH = int.Parse(idMatHang);
            int sl = int.Parse(SoLuong);
            int dg = int.Parse(DonGia);
            int pvc = int.Parse(PhiVanChuyen);
            DateTime NgayBanHang = DateTime.Parse(NgayBan);

            // Tạo Object HoaDonNhapHang mới
            HoaDonBanHang hd = new HoaDonBanHang();
            hd.MA_HOA_DON = 0; // Gán default
            hd.MA_MAT_HANG = idMH;
            hd.SO_LUONG = sl;
            hd.DON_GIA = dg;
            hd.PHI_SHIP = pvc;
            hd.NGAY_BAN = NgayBanHang;

            // Gọi Models
            _m_HoaDonBanHang.addHoaDonBanHang(hd);
        }

        public List<HoaDonBanHang> TimKiemHoaDon(string keyword, string type)
        {
            List<HoaDonBanHang> ds = this.getDanhSachHoaDonBanHang();
            List<HoaDonBanHang> result = new List<HoaDonBanHang>();
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

        public HoaDonBanHang getHoaDonById(string id)
        {
            return _m_HoaDonBanHang.getHoaDonById(int.Parse(id));
        }

        public bool suaHoaDonBanHang(string id, string MaMH, string SoLuong, string DonGia, string PhiVanChuyen, string NgayBan)
        {
            // Convert dữ liệu
            int idHD = int.Parse(id);
            int idMH = int.Parse(MaMH);
            int sl = int.Parse(SoLuong);
            int dg = int.Parse(DonGia);
            int pvc = int.Parse(PhiVanChuyen);
            DateTime NgayBanHang = DateTime.Parse(NgayBan);

            // Tạo Object HoaDonNhapHang mới
            HoaDonBanHang hd = new HoaDonBanHang();
            hd.MA_HOA_DON = idHD;
            hd.MA_MAT_HANG = idMH;
            hd.SO_LUONG = sl;
            hd.DON_GIA = dg;
            hd.PHI_SHIP = pvc;
            hd.NGAY_BAN = NgayBanHang;

            // Gọi model
            return _m_HoaDonBanHang.updateHoaDon(hd);
        }

        public bool xoaHoaDon(string id)
        {
            return _m_HoaDonBanHang.deleteHoaDon(int.Parse(id));
        }
    }
}