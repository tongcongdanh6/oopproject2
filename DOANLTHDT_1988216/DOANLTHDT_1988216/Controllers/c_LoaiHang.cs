using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOANLTHDT_1988216.Models;
using DOANLTHDT_1988216.Entities;
using DOANLTHDT_1988216.Functions;

namespace DOANLTHDT_1988216.Controllers
{
    public class c_LoaiHang
    {
        private MyUltilities _ultilities;
        private m_LoaiHang _m_loaihang;
        public c_LoaiHang()
        {
            this._ultilities = new MyUltilities();
            this._m_loaihang = new m_LoaiHang();
        }
        public List<LoaiHang> getDanhSachLoaiHang()
        {

            return _m_loaihang.getAllLoaiHang();
        }

        public LoaiHang getLoaiHangById(string id)
        {
            return _m_loaihang.getLoaiHangById(int.Parse(id));
        }

        public void themLoaiHang(string TenLH)
        {
            LoaiHang newLH = new LoaiHang();
            newLH.MA_LOAI_HANG = 0; // Assign default
            newLH.TEN_LOAI_HANG = TenLH;
            
            // Gọi method ở Model
            _m_loaihang.addLoaiHang(newLH);
        }

        public List<LoaiHang> TimKiemLoaiHang(string keyword, string type)
        {
            List<LoaiHang> dsLH = this.getDanhSachLoaiHang();
            List<LoaiHang> result = new List<LoaiHang>();
            switch (type)
            {
                case "ten_loai_hang":
                    foreach (var lh in dsLH)
                    {
                        if (lh.TEN_LOAI_HANG.ToLower().Contains(keyword.ToLower()))
                        {
                            result.Add(lh);
                        }
                    }
                    break;
                case "ma_loai_hang":
                    foreach (var lh in dsLH)
                    {
                        // Kiểm tra tính hợp lệ số nguyên
                        int n = 0;
                        if (_ultilities.isInt(keyword, ref n))
                        {
                            if (lh.MA_LOAI_HANG == n)
                            {
                                result.Add(lh);
                            }
                        }
                    }
                    break;
            }
            return result;
        }
    }
}