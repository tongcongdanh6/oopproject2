using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOANLTHDT_1988216.Models;
using DOANLTHDT_1988216.Entities;
using DOANLTHDT_1988216.Functions;
using System.Globalization;


namespace DOANLTHDT_1988216.Controllers
{
    public class c_MatHang
    {
        private MyUltilities _ultilities;
        private m_MatHang _m_mathang;
        public c_MatHang()
        {
            this._ultilities = new MyUltilities();
            this._m_mathang = new m_MatHang();
        }

        public List<MatHang> getDanhSachMatHang()
        {
            m_MatHang m_MatHang = new m_MatHang();

            return m_MatHang.getAllMatHang();
        }

        public void themSanPham(string TenMatHang, string HanSD, string CongTySX, string NamSX, string LoaiHang)
        {
            // Convert dữ liệu
            DateTime Han_SD = DateTime.Parse(HanSD);
            DateTime Nam_SX = DateTime.Parse(NamSX);
            int Loai_Hang = int.Parse(LoaiHang);

            // Thêm
            MatHang mh = new MatHang();
            mh.MA_MAT_HANG = 0; // Gán Default;
            mh.TEN_MAT_HANG = TenMatHang;
            mh.HAN_SU_DUNG = Han_SD;
            mh.CONG_TY_SX = CongTySX;
            mh.NAM_SX = Nam_SX;
            mh.LOAI_HANG = Loai_Hang;

            _m_mathang.themMatHang(mh);
            
        }

        public List<MatHang> TimKiemMatHang(string keyword, string type)
        {
            List<MatHang> dsMH = this.getDanhSachMatHang();
            List<MatHang> result = new List<MatHang>();
            switch(type)
            {
                case "ten_mat_hang":
                    foreach (var mh in dsMH)
                    {
                        if(mh.TEN_MAT_HANG.ToLower().Contains(keyword.ToLower()))
                        {
                           result.Add(mh);
                        } 
                    }
                break;
                case "ma_mat_hang":
                    foreach (var mh in dsMH)
                    {
                        // Kiểm tra tính hợp lệ số nguyên
                        int n = 0;
                        if(_ultilities.isInt(keyword, ref n))
                        {
                            if (mh.MA_MAT_HANG == n)
                            {
                                result.Add(mh);
                            }
                        }
                    }
                    break;
            }
            return result;
        }

        public MatHang getMatHangById(string id)
        {
            return _m_mathang.getMatHangById(int.Parse(id));
        }

        public bool capNhatMatHang(string id, string TenMH, string NhaSX, string HanSD, string NamSX, string LoaiHang)
        {
            // Convert dữ liệu
            DateTime Han_SD = DateTime.Parse(HanSD);
            DateTime Nam_SX = DateTime.Parse(NamSX);
            int Loai_Hang = int.Parse(LoaiHang);

            // Tạo Object Mặt Hàng cần update
            MatHang mh = new MatHang();
            mh.MA_MAT_HANG = int.Parse(id);
            mh.TEN_MAT_HANG = TenMH;
            mh.HAN_SU_DUNG = Han_SD;
            mh.CONG_TY_SX = NhaSX;
            mh.NAM_SX = Nam_SX;
            mh.LOAI_HANG = Loai_Hang;

           
            return _m_mathang.updateMatHang(mh);
        }

        public bool xoaMatHang(string id)
        {
            return _m_mathang.deleteMatHang(int.Parse(id));
        }


    }

}