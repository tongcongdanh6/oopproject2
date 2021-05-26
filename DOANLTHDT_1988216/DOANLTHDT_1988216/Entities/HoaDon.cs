using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOANLTHDT_1988216.Entities
{
    public class HoaDon
    {
        private int _MaHoaDon;
        public int MA_HOA_DON { get; set; }

        private int _MaMatHang;
        public int MA_MAT_HANG { get; set; }

        private int _SoLuong;
        public int SO_LUONG { get; set; }

        private int _DonGia;
        public int DON_GIA { get; set; }

        public int TongHoaDon()
        {
            return this._SoLuong * this._DonGia;
        }
    }
}