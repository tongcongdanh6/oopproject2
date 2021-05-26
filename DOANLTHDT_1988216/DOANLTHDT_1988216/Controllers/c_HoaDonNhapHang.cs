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

        private MyUltilities _ultilities;
        private m_HoaDonNhapHang _m_HoaDonNhapHang;
        public c_HoaDonNhapHang()
        {
            this._ultilities = new MyUltilities();
            this._m_HoaDonNhapHang = new m_HoaDonNhapHang();
        }

        public List<HoaDonNhapHang> getDanhSachHoaDonNhapHang()
        {
            return _m_HoaDonNhapHang.getAllHoaDonNhapHang();
        }
    }
}