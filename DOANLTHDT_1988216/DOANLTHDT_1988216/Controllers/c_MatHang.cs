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
        //private MyUltilities _ultilities;
        private m_MatHang _m_mathang;
        public c_MatHang()
        {
            //this._ultilities = new MyUltilities();
            this._m_mathang = new m_MatHang();
        }

        public List<MatHang> getDanhSachMatHang()
        {
            m_MatHang m_MatHang = new m_MatHang();

            return m_MatHang.getAllMatHang();
        }

        public List<Message> themSanPham(string TenMatHang, string HanSD, string CongTySX, string NamSX, string LoaiHang)
        {
            // Khởi tạo list message kết quả
            List<Message> listMsg = new List<Message>();

            // Lưu dữ liệu người dùng vừa nhập qua Cookie để giữ lại trên FrontEnd cho đỡ phải nhập lại
            var dictionary = new Dictionary<string, string> {
                {"TenMatHang", TenMatHang},
                {"HanSD", HanSD},
                {"CongTySX", CongTySX},
                {"NamSX", NamSX},
                {"LoaiHang", LoaiHang}
            };
            foreach(var d in dictionary)
            {
                HttpCookie cookieItem = new HttpCookie(d.Key);
                cookieItem.Value = d.Value;
                cookieItem.Expires.AddSeconds(30);
                HttpContext.Current.Response.Cookies.Set(cookieItem);
            }


            // ======= VALIDATION =========
            // Biến cờ cho kết quả validate dữ liệu
            bool isValidData = false;

            // Kiểm tra nếu có ký tự đặc biệt trong Tên Mặt Hàng hay Công Ty SX thì trả message lỗi

            if (String.IsNullOrEmpty(TenMatHang))
            {
                Message msg = new Message();
                msg.TYPE = "danger";
                msg.CONTENT = String.Format("{0} {1} {2}",
                    Constants.TEN,
                    Constants.MAT_HANG,
                    Constants.KHONG_DUOC_RONG
                    );
                listMsg.Add(msg);
            }
            else if (MyUltilities.hasSpecialChar(TenMatHang))
            {
                Message msg = new Message();
                msg.TYPE = "danger";
                msg.CONTENT = String.Format("{0} {1} {2}",
                    Constants.TEN,
                    Constants.MAT_HANG,
                    Constants.KHONG_DUOC_CHUA_KY_TU_DAC_BIET
                    );
                listMsg.Add(msg);
            }

            if (String.IsNullOrEmpty(CongTySX))
            {
                Message msg = new Message();
                msg.TYPE = "danger";
                msg.CONTENT = String.Format("{0} {1}",
                    Constants.CONG_TY_SX,
                    Constants.KHONG_DUOC_RONG
                    );
                listMsg.Add(msg);
            }
            else if (MyUltilities.hasSpecialChar(CongTySX))
            {
                Message msg = new Message();
                msg.TYPE = "danger";
                msg.CONTENT = String.Format("{0} {1}",
                    Constants.CONG_TY_SX,
                    Constants.KHONG_DUOC_CHUA_KY_TU_DAC_BIET
                    );
                listMsg.Add(msg);
            }

            if(String.IsNullOrEmpty(HanSD))
            {
                Message msg = new Message();
                msg.TYPE = "danger";
                msg.CONTENT = String.Format("{0} {1}",
                    Constants.HAN_SD,
                    Constants.KHONG_DUOC_RONG
                    );
                listMsg.Add(msg);
            }
            else if(DateTime.Parse(HanSD) < DateTime.Now)
            {
                Message msg = new Message();
                msg.TYPE = "warning";
                msg.CONTENT = String.Format("{0} {1} ({2} {3})",
                    Constants.HAN_SD,
                    Constants.KHONG_HOP_LE,
                    Constants.PHAI_SAU_NGAY,
                    DateTime.Now.ToString("dd-MM-yyyy")
                    );
                listMsg.Add(msg);
            }

            if (String.IsNullOrEmpty(NamSX))
            {
                Message msg = new Message();
                msg.TYPE = "danger";
                msg.CONTENT = String.Format("{0} {1}",
                    Constants.NAM_SX,
                    Constants.KHONG_DUOC_RONG
                    );
                listMsg.Add(msg);
            }
            else if (DateTime.Parse(NamSX) > DateTime.Now)
            {
                Message msg = new Message();
                msg.TYPE = "warning";
                msg.CONTENT = String.Format("{0} {1} ({2} {3})",
                    Constants.NAM_SX,
                    Constants.KHONG_HOP_LE,
                    Constants.PHAI_TRUOC_HOAC_BANG_NGAY,
                    DateTime.Now.ToString("dd-MM-yyyy")
                    );
                listMsg.Add(msg);
            }

            if(listMsg.Count == 0)
            {
                isValidData = true;
            }

            // ======= VALIDATION =========

            if (isValidData == true)
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

                Message msg = new Message();
                msg.TYPE = "success";
                msg.CONTENT = String.Format("{0} {1} {2}",
                    Constants.THEM,
                    Constants.MAT_HANG,
                    Constants.THANH_CONG).ToUpper();

                listMsg.Add(msg);
            }

            return listMsg;
            
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
                        if(MyUltilities.isInt(keyword, ref n))
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