using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOANLTHDT_1988216.Entities;
using DOANLTHDT_1988216.Models;

namespace DOANLTHDT_1988216.Controllers
{
    public class c_ThongKe
    {
        private m_LoaiHang _m_lh;
        private m_MatHang _m_mh;
        private m_HoaDonBanHang _m_hdbh;
        private m_HoaDonNhapHang _m_hdnh;

        public c_ThongKe()
        {
            this._m_lh = new m_LoaiHang();
            this._m_mh = new m_MatHang();
            this._m_hdbh = new m_HoaDonBanHang();
            this._m_hdnh = new m_HoaDonNhapHang();
        }

        public List<ThongKe> SoLuongHangTheoLoaiHang()
        {
            List<LoaiHang> lh = _m_lh.getAllLoaiHang();
            List<MatHang> mh = _m_mh.getAllMatHang();
            List<HoaDonNhapHang> hdnh = _m_hdnh.getAllHoaDonNhapHang();
            List<HoaDonBanHang> hdbh = _m_hdbh.getAllHoaDonBanHang();
            List<ThongKe> tk = new List<ThongKe>();


            foreach(var l in lh)
            {
                int sum = 0;
                ThongKe newRecord = new ThongKe();
                newRecord.ID = l.MA_LOAI_HANG;

                foreach(var m in mh)
                {
                    if(m.LOAI_HANG == l.MA_LOAI_HANG)
                    {
                        foreach(var hd in hdnh)
                        {
                            if(hd.MA_MAT_HANG == m.MA_MAT_HANG)
                            {
                                sum += hd.SO_LUONG;
                            }
                        }

                        foreach (var hd in hdbh)
                        {
                            if (hd.MA_MAT_HANG == m.MA_MAT_HANG)
                            {
                                sum -= hd.SO_LUONG;
                            }
                        }
                    }
                }

                newRecord.SO_LUONG = sum;
                tk.Add(newRecord);
            }

            return tk;
        }

        public List<ThongKe> SoLuongHangHetHanTheoMatHang()
        {
            List<MatHang> mh = _m_mh.getAllMatHang();
            List<HoaDonNhapHang> hdnh = _m_hdnh.getAllHoaDonNhapHang();
            List<HoaDonBanHang> hdbh = _m_hdbh.getAllHoaDonBanHang();

            List<ThongKe> tk = new List<ThongKe>();
            foreach(var m in mh)
            {
                if(m.HAN_SU_DUNG < DateTime.Now)
                {
                    ThongKe record = new ThongKe();
                    record.ID = m.MA_MAT_HANG;
                    

                    int sum = 0;
                    foreach(var hd in hdnh)
                    {
                        if(hd.MA_MAT_HANG == m.MA_MAT_HANG)
                        {
                            sum += hd.SO_LUONG;
                        }
                    }

                    foreach (var hd in hdbh)
                    {
                        if (hd.MA_MAT_HANG == m.MA_MAT_HANG)
                        {
                            sum -= hd.SO_LUONG;
                        }
                    }

                    record.SO_LUONG = sum;
                    tk.Add(record);
                }
            }

            return tk;
        }
    }
}