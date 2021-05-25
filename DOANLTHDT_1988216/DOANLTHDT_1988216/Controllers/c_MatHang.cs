using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOANLTHDT_1988216.Models;
using DOANLTHDT_1988216.Entities;
using System.Globalization;

namespace DOANLTHDT_1988216.Controllers
{
    public class c_MatHang
    {
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

            m_MatHang m_MatHang = new m_MatHang();
            m_MatHang.themMatHang(mh);
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
            }

            return result;
        }
    }

}