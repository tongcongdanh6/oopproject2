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
        private m_MatHang _m_mathang;
        private m_HoaDonNhapHang _m_HoaDonNhapHang;
        private m_HoaDonBanHang _m_HoaDonBanHang;
        public c_MatHang()
        {
            this._m_HoaDonNhapHang = new m_HoaDonNhapHang();
            this._m_HoaDonBanHang = new m_HoaDonBanHang();
            this._m_mathang = new m_MatHang();
        }

        private List<Message> validateUserInput(string id, string TenMatHang, string HanSD, string CongTySX, string NamSX, string LoaiHang)
        {
            List<Message> listMsg = new List<Message>();

            if (String.IsNullOrEmpty(id))
            {
                listMsg.Add(new Message("danger", String.Format("{0} {1} {2}", Constants.MA, Constants.MAT_HANG, Constants.KHONG_DUOC_RONG)));
            }
            else if (!int.TryParse(id, out int n))
            {
                listMsg.Add(new Message("danger", String.Format("{0} {1} {2}", Constants.MA, Constants.MAT_HANG, Constants.KHONG_HOP_LE)));
            }

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

            if (String.IsNullOrEmpty(HanSD))
            {
                Message msg = new Message();
                msg.TYPE = "danger";
                msg.CONTENT = String.Format("{0} {1}",
                    Constants.HAN_SD,
                    Constants.KHONG_DUOC_RONG
                    );
                listMsg.Add(msg);
            }
            else if (DateTime.Parse(HanSD) < DateTime.Now)
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

            return listMsg;
        }

        public List<MatHang> getDanhSachMatHang()
        {
            m_MatHang m_MatHang = new m_MatHang();

            return m_MatHang.getAllMatHang();
        }

        public List<Message> themSanPham(string id, string TenMatHang, string HanSD, string CongTySX, string NamSX, string LoaiHang)
        {
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

            // List message validate
            List<Message> listMsg = this.validateUserInput(id, TenMatHang, HanSD, CongTySX, NamSX, LoaiHang);
            if(listMsg.Count == 0)
            {
                isValidData = true; // Validate dữ liệu thành công
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
                mh.MA_MAT_HANG = int.Parse(id); // Gán Default;
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

        public List<Message> capNhatMatHang(string id, string TenMatHang, string CongTySX, string HanSD, string NamSX, string LoaiHang)
        {
            // ======= VALIDATION =========
            // Biến cờ cho kết quả validate dữ liệu
            bool isValidData = false;

            // List message validate
            List<Message> listMsg = this.validateUserInput(id, TenMatHang, HanSD, CongTySX, NamSX, LoaiHang);
            if (listMsg.Count == 0)
            {
                isValidData = true; // Validate dữ liệu thành công
            }

            // ======= VALIDATION =========

            if(isValidData == true)
            {
                // Convert dữ liệu
                DateTime Han_SD = DateTime.Parse(HanSD);
                DateTime Nam_SX = DateTime.Parse(NamSX);
                int Loai_Hang = int.Parse(LoaiHang);

                // Tạo Object Mặt Hàng cần update
                MatHang mh = new MatHang();
                mh.MA_MAT_HANG = int.Parse(id);
                mh.TEN_MAT_HANG = TenMatHang;
                mh.HAN_SU_DUNG = Han_SD;
                mh.CONG_TY_SX = CongTySX;
                mh.NAM_SX = Nam_SX;
                mh.LOAI_HANG = Loai_Hang;

                if(_m_mathang.updateMatHang(mh))
                {
                    Message msg = new Message();
                    msg.TYPE = "success";
                    msg.CONTENT = String.Format("{0} {1} {2}",
                        Constants.CAP_NHAT,
                        Constants.MAT_HANG,
                        Constants.THANH_CONG).ToUpper();
                    listMsg.Add(msg);
                }
                else
                {
                    Message msg = new Message();
                    msg.TYPE = "success";
                    msg.CONTENT = String.Format("{0} {1} {2}",
                        Constants.CAP_NHAT,
                        Constants.MAT_HANG,
                        Constants.THAT_BAI).ToUpper();
                    listMsg.Add(msg);
                }
            }
            return listMsg;
        }

        public List<Message> xoaMatHang(string id)
        {
            // Tiến hành xóa các mặt hàng có trong hóa đơn bán hàng & hóa đơn nhập hàng
            List<HoaDonBanHang> listHDBH_byMH = _m_HoaDonBanHang.getListHDBanHangByMatHangID(int.Parse(id));
            List<HoaDonNhapHang> listHDNH_byMH = _m_HoaDonNhapHang.getListHDNhapHangByMatHangID(int.Parse(id));

            if(listHDBH_byMH.Count > 0 && listHDNH_byMH.Count > 0)
            {
                // Full list
                List<HoaDonBanHang> listHDBH = _m_HoaDonBanHang.getAllHoaDonBanHang();
                List<HoaDonNhapHang> listHDNH = _m_HoaDonNhapHang.getAllHoaDonNhapHang();

                //Remove from full list
                foreach (var l in listHDBH_byMH)
                {
                    HoaDonBanHang itemToRemove = listHDBH.Single(r => r.MA_HOA_DON == l.MA_HOA_DON);
                    listHDBH.Remove(itemToRemove);
                }
                foreach (var l in listHDNH_byMH)
                {
                    HoaDonNhapHang itemToRemove = listHDNH.Single(r => r.MA_HOA_DON == l.MA_HOA_DON);
                    listHDNH.Remove(itemToRemove);
                }

                _m_HoaDonBanHang.writeToFile(listHDBH);
                _m_HoaDonNhapHang.writeToFile(listHDNH);
            }

            List<Message> listMsg = new List<Message>();
            if (_m_mathang.deleteMatHang(int.Parse(id)))
            {
                Message msg = new Message();
                msg.TYPE = "success";
                msg.CONTENT = String.Format("{0} {1} {2}",
                    Constants.XOA,
                    Constants.MAT_HANG,
                    Constants.THANH_CONG).ToUpper();
                listMsg.Add(msg);
            }
            else
            {
                Message msg = new Message();
                msg.TYPE = "danger";
                msg.CONTENT = String.Format("{0} {1} {2}",
                    Constants.XOA,
                    Constants.MAT_HANG,
                    Constants.THAT_BAI).ToUpper();
                listMsg.Add(msg);
            }

            return listMsg;
        }

        public List<MatHang> getListMatHangThuocLoaiHang(string id)
        {
            return _m_mathang.getListMatHangByLoaiHangId(int.Parse(id));
        }


    }

}