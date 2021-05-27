﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOANLTHDT_1988216.Entities
{
    public class HoaDonNhapHang : HoaDon
    {
        private DateTime _NgayNhap;

        public DateTime NGAY_NHAP { get; set; }

        public override int TongHoaDon()
        {
            return base.TongHoaDon() + this.PHI_SHIP;
        }
    }
}