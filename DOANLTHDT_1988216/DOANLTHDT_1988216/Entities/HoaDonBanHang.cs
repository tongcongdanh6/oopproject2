using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOANLTHDT_1988216.Entities
{
    public class HoaDonBanHang : HoaDon
    {
        private DateTime _NgayBan;

        public DateTime NGAY_BAN { get; set; }

        public override int TongHoaDon()
        {
            return (int) Math.Round(base.TongHoaDon() * 1.1 + this.PHI_SHIP);
        }
    }
}