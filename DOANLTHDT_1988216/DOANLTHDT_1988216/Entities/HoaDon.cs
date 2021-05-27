using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOANLTHDT_1988216.Entities
{
    public class HoaDon
    {
        protected int _MaHoaDon;
        public int MA_HOA_DON { get; set; }

        protected int _MaMatHang;
        public int MA_MAT_HANG { get; set; }

        protected int _SoLuong;
        public int SO_LUONG { get; set; }

        protected int _DonGia;
        public int DON_GIA { get; set; }

        protected int _PhiShip;
        public int PHI_SHIP { get; set; }

        public virtual int TongHoaDon()
        {
            return this.SO_LUONG * this.DON_GIA;
        }
    }
}