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
        private m_LoaiHang _m_loaihang;
        private m_MatHang _m_mathang;
        public c_LoaiHang()
        {
            this._m_loaihang = new m_LoaiHang();
            this._m_mathang = new m_MatHang();
        }

        private List<Message> validateUserInput(string id, string TenLoaiHang)
        {
            List<Message> listMsg = new List<Message>();

            if (String.IsNullOrEmpty(id))
            {
                listMsg.Add(new Message("danger", String.Format("{0} {1} {2}", Constants.MA, Constants.LOAI_HANG, Constants.KHONG_DUOC_RONG)));
            }
            else if (!int.TryParse(id, out int n))
            {
                listMsg.Add(new Message("danger", String.Format("{0} {1} {2}", Constants.MA, Constants.LOAI_HANG, Constants.KHONG_HOP_LE)));
            }

            if (String.IsNullOrEmpty(TenLoaiHang))
            {
                Message msg = new Message();
                msg.TYPE = "danger";
                msg.CONTENT = String.Format("{0} {1} {2}",
                    Constants.TEN,
                    Constants.LOAI_HANG,
                    Constants.KHONG_DUOC_RONG
                    );
                listMsg.Add(msg);
            }
            else if (MyUltilities.hasSpecialChar(TenLoaiHang))
            {
                Message msg = new Message();
                msg.TYPE = "danger";
                msg.CONTENT = String.Format("{0} {1} {2}",
                    Constants.TEN,
                    Constants.LOAI_HANG,
                    Constants.KHONG_DUOC_CHUA_KY_TU_DAC_BIET
                    );
                listMsg.Add(msg);
            }

            return listMsg;
        }
        public List<LoaiHang> getDanhSachLoaiHang()
        {

            return _m_loaihang.getAllLoaiHang();
        }

        public LoaiHang getLoaiHangById(string id)
        {
            return _m_loaihang.getLoaiHangById(int.Parse(id));
        }

        public List<Message> themLoaiHang(string id, string TenLH)
        {
            // Lưu dữ liệu người dùng vừa nhập qua Cookie để giữ lại trên FrontEnd cho đỡ phải nhập lại
            var dictionary = new Dictionary<string, string> {
                {"TenLH", TenLH}
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
            List<Message> listMsg = this.validateUserInput(id, TenLH);
            if (listMsg.Count == 0)
            {
                isValidData = true; // Validate dữ liệu thành công
            }
            // ======= VALIDATION =========

            if(isValidData == true)
            {
                LoaiHang newLH = new LoaiHang();
                newLH.MA_LOAI_HANG = 0; // Assign default
                newLH.TEN_LOAI_HANG = TenLH;

                // Gọi method ở Model
                _m_loaihang.addLoaiHang(newLH);

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
                        if (MyUltilities.isInt(keyword, ref n))
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

        public List<Message> capNhatLoaiHang(string id, string TenLH)
        {
            // ======= VALIDATION =========
            // Biến cờ cho kết quả validate dữ liệu
            bool isValidData = false;

            // List message validate
            List<Message> listMsg = this.validateUserInput(id, TenLH);
            if (listMsg.Count == 0)
            {
                isValidData = true; // Validate dữ liệu thành công
            }
            // ======= VALIDATION =========

            if(isValidData == true)
            {
                // Tạo Object Loại Hàng cần update
                LoaiHang lh = new LoaiHang();
                lh.MA_LOAI_HANG = int.Parse(id);
                lh.TEN_LOAI_HANG = TenLH;

                if(_m_loaihang.updateLoaiHang(lh))
                {
                    Message msg = new Message();
                    msg.TYPE = "success";
                    msg.CONTENT = String.Format("{0} {1} {2}",
                        Constants.THEM,
                        Constants.LOAI_HANG,
                        Constants.THANH_CONG).ToUpper();
                    listMsg.Add(msg);
                }
                else
                {
                    Message msg = new Message();
                    msg.TYPE = "success";
                    msg.CONTENT = String.Format("{0} {1} {2}",
                        Constants.THEM,
                        Constants.LOAI_HANG,
                        Constants.THAT_BAI).ToUpper();
                    listMsg.Add(msg);
                }
                
            }

            return listMsg;
        }

        public List<Message> xoaLoaiHang(string id)
        {
            List<Message> listMsg = new List<Message>();

            if (_m_loaihang.deleteLoaiHang(int.Parse(id)))
            {
                Message msg = new Message();
                msg.TYPE = "success";
                msg.CONTENT = String.Format("{0} {1} {2}",
                    Constants.XOA,
                    Constants.LOAI_HANG,
                    Constants.THANH_CONG).ToUpper();
                listMsg.Add(msg);
            }
            else
            {
                Message msg = new Message();
                msg.TYPE = "danger";
                msg.CONTENT = String.Format("{0} {1} {2}",
                    Constants.XOA,
                    Constants.LOAI_HANG,
                    Constants.THAT_BAI).ToUpper();
                listMsg.Add(msg);
            }

            return listMsg;
        }
    }
}