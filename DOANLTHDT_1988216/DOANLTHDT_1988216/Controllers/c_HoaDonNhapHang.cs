﻿using System;
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

        private m_HoaDonNhapHang _m_HoaDonNhapHang;
        private m_MatHang _m_MatHang;
        public c_HoaDonNhapHang()
        {
            this._m_HoaDonNhapHang = new m_HoaDonNhapHang();
            this._m_MatHang = new m_MatHang();
        }

        public List<HoaDonNhapHang> getDanhSachHoaDonNhapHang()
        {
            return _m_HoaDonNhapHang.getAllHoaDonNhapHang();
        }

        private List<Message> validateUserInput(string idHoaDon, string idMatHang, string SoLuong, string DonGia, string PhiVanChuyen, string NgayNhap)
        {
            List<Message> listMsg = new List<Message>();

            if (String.IsNullOrEmpty(idHoaDon))
            {
                listMsg.Add(new Message("danger", String.Format("{0} {1} {2}", Constants.MA, Constants.HOA_DON, Constants.KHONG_DUOC_RONG)));
            }
            else if (!int.TryParse(idHoaDon, out int n))
            {
                listMsg.Add(new Message("danger", String.Format("{0} {1} {2}", Constants.MA, Constants.HOA_DON, Constants.KHONG_HOP_LE)));
            }

            if (String.IsNullOrEmpty(idMatHang))
            {
                listMsg.Add(new Message("danger", String.Format("{0} {1} {2}", Constants.MA, Constants.MAT_HANG, Constants.KHONG_DUOC_RONG)));
            }
            else if (!int.TryParse(idMatHang, out int n))
            {
                listMsg.Add(new Message("danger", String.Format("{0} {1} {2}", Constants.MA, Constants.MAT_HANG, Constants.KHONG_HOP_LE)));
            }

            if (String.IsNullOrEmpty(SoLuong))
            {
                listMsg.Add(new Message("danger", String.Format("{0} {1}", Constants.SO_LUONG, Constants.KHONG_DUOC_RONG)));
            }
            else if (!int.TryParse(SoLuong, out int n))
            {
                listMsg.Add(new Message("danger", String.Format("{0} {1}", Constants.SO_LUONG, Constants.KHONG_HOP_LE)));
            }
            else if(int.Parse(SoLuong) < 0)
            {
                listMsg.Add(new Message("danger", String.Format("{0} {1}", Constants.SO_LUONG, Constants.KHONG_DUOC_BE_HON_0)));
            }

            if (String.IsNullOrEmpty(DonGia))
            {
                listMsg.Add(new Message("danger", String.Format("{0} {1}", Constants.DON_GIA, Constants.KHONG_DUOC_RONG)));
            }
            else if (!int.TryParse(DonGia, out int n))
            {
                listMsg.Add(new Message("danger", String.Format("{0} {1}", Constants.DON_GIA, Constants.KHONG_HOP_LE)));
            }
            else if (int.Parse(DonGia) < 0)
            {
                listMsg.Add(new Message("danger", String.Format("{0} {1}", Constants.DON_GIA, Constants.KHONG_DUOC_BE_HON_0)));
            }

            if (String.IsNullOrEmpty(PhiVanChuyen))
            {
                listMsg.Add(new Message("danger", String.Format("{0} {1}", Constants.PHI_VAN_CHUYEN, Constants.KHONG_DUOC_RONG)));
            }
            else if (!int.TryParse(PhiVanChuyen, out int n))
            {
                listMsg.Add(new Message("danger", String.Format("{0} {1}", Constants.PHI_VAN_CHUYEN, Constants.KHONG_HOP_LE)));
            }
            else if (int.Parse(PhiVanChuyen) < 0)
            {
                listMsg.Add(new Message("danger", String.Format("{0} {1}", Constants.PHI_VAN_CHUYEN, Constants.KHONG_DUOC_BE_HON_0)));
            }


            if (String.IsNullOrEmpty(NgayNhap))
            {
                Message msg = new Message();
                msg.TYPE = "danger";
                msg.CONTENT = String.Format("{0} {1}",
                    Constants.HAN_SD,
                    Constants.KHONG_DUOC_RONG
                    );
                listMsg.Add(msg);
            }
            else if (DateTime.Parse(NgayNhap) > DateTime.Now)
            {
                Message msg = new Message();
                msg.TYPE = "warning";
                msg.CONTENT = String.Format("{0} {1} ({2} {3})",
                    Constants.NGAY_NHAP_HANG,
                    Constants.KHONG_HOP_LE,
                    Constants.PHAI_TRUOC_HOAC_BANG_NGAY,
                    DateTime.Now.ToString("dd-MM-yyyy")
                    );
                listMsg.Add(msg);
            }

            return listMsg;
        }

        public List<Message> themHoaDonNhapHang(string idHoaDon, string idMatHang, string SoLuong, string DonGia, string PhiVanChuyen, string NgayNhap)
        {
            // Lưu dữ liệu người dùng vừa nhập qua Cookie để giữ lại trên FrontEnd cho đỡ phải nhập lại
            var dictionary = new Dictionary<string, string> {
                {"idMatHang", idMatHang},
                {"SoLuong", SoLuong},
                {"DonGia", DonGia},
                {"PhiVanChuyen", PhiVanChuyen},
                {"NgayNhap", NgayNhap}
            };
            foreach (var d in dictionary)
            {
                HttpCookie cookieItem = new HttpCookie(d.Key);
                cookieItem.Value = d.Value;
                cookieItem.Expires.AddSeconds(30);
                HttpContext.Current.Response.Cookies.Set(cookieItem);
            }

            // ======= VALIDATION =========
            // Biến cờ cho kết quả validate dữ liệu
            bool isValidData = false;

            // List message validate
            List<Message> listMsg = this.validateUserInput(idHoaDon, idMatHang, SoLuong, DonGia, PhiVanChuyen, NgayNhap);
            if (listMsg.Count == 0)
            {
                isValidData = true; // Validate dữ liệu thành công
            }
            // ======= VALIDATION =========

            if(isValidData == true)
            {
                // Convert dữ liệu
                int idMH = int.Parse(idMatHang);
                int sl = int.Parse(SoLuong);
                int dg = int.Parse(DonGia);
                int pvc = int.Parse(PhiVanChuyen);
                DateTime NgayNhapHang = DateTime.Parse(NgayNhap);

                // Tạo Object HoaDonNhapHang mới
                HoaDonNhapHang hd = new HoaDonNhapHang();
                hd.MA_HOA_DON = 0; // Gán default
                hd.MA_MAT_HANG = idMH;
                hd.SO_LUONG = sl;
                hd.DON_GIA = dg;
                hd.PHI_SHIP = pvc;
                hd.NGAY_NHAP = NgayNhapHang;

                // Gọi Models
                _m_HoaDonNhapHang.addHoaDonNhapHang(hd);

                Message msg = new Message();
                msg.TYPE = "success";
                msg.CONTENT = String.Format("{0} {1} {2}",
                    Constants.THEM,
                    Constants.HOA_DON,
                    Constants.THANH_CONG).ToUpper();

                listMsg.Add(msg);
            }

            return listMsg;
        }

        public List<HoaDonNhapHang> TimKiemHoaDon(string keyword, string type)
        {
            List<HoaDonNhapHang> ds = this.getDanhSachHoaDonNhapHang();
            List<HoaDonNhapHang> result = new List<HoaDonNhapHang>();
            switch (type)
            {
                case "ten_mat_hang":
                    foreach (var d in ds)
                    {
                        if (_m_MatHang.getMatHangById(d.MA_MAT_HANG).TEN_MAT_HANG.ToLower().Contains(keyword.ToLower()))
                        {
                            result.Add(d);
                        }
                    }
                    break;
                case "ma_hoa_don":
                    foreach (var d in ds)
                    {
                        // Kiểm tra tính hợp lệ số nguyên
                        int n = 0;
                        if (MyUltilities.isInt(keyword, ref n))
                        {
                            if (d.MA_HOA_DON == n)
                            {
                                result.Add(d);
                            }
                        }
                    }
                    break;
                case "ma_mat_hang":
                    foreach (var d in ds)
                    {
                        // Kiểm tra tính hợp lệ số nguyên
                        int n = 0;
                        if (MyUltilities.isInt(keyword, ref n))
                        {
                            if (d.MA_MAT_HANG == n)
                            {
                                result.Add(d);
                            }
                        }
                    }
                    break;
            }
            return result;
        }

        public HoaDonNhapHang getHoaDonById(string id)
        {
            return _m_HoaDonNhapHang.getHoaDonById(int.Parse(id));
        }

        public bool suaHoaDonNhapHang(string id, string MaMH, string SoLuong, string DonGia, string PhiVanChuyen, string NgayNhap)
        {
            // Convert dữ liệu
            int idHD = int.Parse(id);
            int idMH = int.Parse(MaMH);
            int sl = int.Parse(SoLuong);
            int dg = int.Parse(DonGia);
            int pvc = int.Parse(PhiVanChuyen);
            DateTime NgayNhapHang = DateTime.Parse(NgayNhap);

            // Tạo Object HoaDonNhapHang mới
            HoaDonNhapHang hd = new HoaDonNhapHang();
            hd.MA_HOA_DON = idHD;
            hd.MA_MAT_HANG = idMH;
            hd.SO_LUONG = sl;
            hd.DON_GIA = dg;
            hd.PHI_SHIP = pvc;
            hd.NGAY_NHAP = NgayNhapHang;

            // Gọi model
            return _m_HoaDonNhapHang.updateHoaDon(hd);
        }

        public bool xoaHoaDon(string id)
        {
            return _m_HoaDonNhapHang.deleteHoaDon(int.Parse(id));
        }
    }
}