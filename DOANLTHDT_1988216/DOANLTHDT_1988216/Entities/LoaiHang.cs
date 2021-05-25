using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOANLTHDT_1988216.Entities
{
    public class LoaiHang
    {
        private int _MaLoaiHang;

        public int MA_LOAI_HANG {
            get
            {
                return this._MaLoaiHang;
            }
            set
            {
                // Kiểm tra phải số nguyên hợp lệ hay không
                if (int.TryParse(value.ToString(), out int n))
                {
                    // Nếu là số nguyên thì kiểm tra tiếp phải là số > 0 thì mới set
                    if (n > 0) this._MaLoaiHang = n;
                    else this._MaLoaiHang = 1;
                }
                else
                {
                    this._MaLoaiHang = 1;
                }
            }
        }

        private string _TenLoaiHang;

        public string TEN_LOAI_HANG
        {
            get
            {
                return this._TenLoaiHang;
            }
            set
            {
                this._TenLoaiHang = value;
            }
        }
    }
}