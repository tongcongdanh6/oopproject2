using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOANLTHDT_1988216.Entities;
using DOANLTHDT_1988216.Models;
using DOANLTHDT_1988216.Functions;

namespace DOANLTHDT_1988216.Controllers
{
    public class c_HoaDonBanHang
    {

        private m_HoaDonBanHang _m_HoaDonBanHang;
        private m_HoaDonNhapHang _m_HoaDonNhapHang;
        private m_MatHang _m_MatHang;
        public c_HoaDonBanHang()
        {
            this._m_HoaDonBanHang = new m_HoaDonBanHang();
            this._m_HoaDonNhapHang = new m_HoaDonNhapHang();
            this._m_MatHang = new m_MatHang();
        }

        private List<Message> validateUserInput(string idHoaDon, string idMatHang, string SoLuong, string DonGia, string PhiVanChuyen, string NgayBan)
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
            else if (int.Parse(SoLuong) < 0)
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


            if (String.IsNullOrEmpty(NgayBan))
            {
                Message msg = new Message();
                msg.TYPE = "danger";
                msg.CONTENT = String.Format("{0} {1}",
                    Constants.HAN_SD,
                    Constants.KHONG_DUOC_RONG
                    );
                listMsg.Add(msg);
            }
            else if (DateTime.Parse(NgayBan) > DateTime.Now)
            {
                Message msg = new Message();
                msg.TYPE = "warning";
                msg.CONTENT = String.Format("{0} {1} ({2} {3})",
                    Constants.NGAY_BAN_HANG,
                    Constants.KHONG_HOP_LE,
                    Constants.PHAI_TRUOC_HOAC_BANG_NGAY,
                    DateTime.Now.ToString("dd-MM-yyyy")
                    );
                listMsg.Add(msg);
            }

            return listMsg;
        }

        private bool isValidLogic(HoaDonBanHang hd, ref int sumbh, ref int sumnh)
        {
            // Nếu như số lượng bán đã nhập sẽ làm hiệu số TỔNG NHẬP của 1 mặt hàng - TỔNG BÁN của một 1 mặt hàng < 0 là không hợp lệ
            bool isValidLogic = false;

            // Lấy list hóa đơn nhập hàng
            List<HoaDonNhapHang> listHDNH = _m_HoaDonNhapHang.getListHDNhapHangByMatHangID(hd.MA_MAT_HANG);
            List<HoaDonBanHang> listHDBH = _m_HoaDonBanHang.getListHDBanHangByMatHangID(hd.MA_MAT_HANG);
            listHDBH.Add(hd);
            int sumBH = 0;
            foreach (var d in listHDBH)
            {
                sumBH += d.SO_LUONG;
            }

            sumbh = sumBH;

            int sumNH = 0;
            foreach (var d in listHDNH)
            {
                sumNH += d.SO_LUONG;
            }

            sumnh = sumNH;

            if (sumBH <= sumNH)
            {
                isValidLogic = true;
            }


            return isValidLogic;
        }

        public List<HoaDonBanHang> getDanhSachHoaDonBanHang()
        {
            return _m_HoaDonBanHang.getAllHoaDonBanHang();
        }

        public List<Message> themHoaDonBanHang(string idHoaDon ,string idMatHang, string SoLuong, string DonGia, string PhiVanChuyen, string NgayBan)
        {
            // Lưu dữ liệu người dùng vừa nhập qua Cookie để giữ lại trên FrontEnd cho đỡ phải nhập lại
            var dictionary = new Dictionary<string, string> {
                {"idMatHang", idMatHang},
                {"SoLuong", SoLuong},
                {"DonGia", DonGia},
                {"PhiVanChuyen", PhiVanChuyen},
                {"NgayNhap", NgayBan}
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
            List<Message> listMsg = this.validateUserInput(idHoaDon, idMatHang, SoLuong, DonGia, PhiVanChuyen, NgayBan);
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
                DateTime NgayBanHang = DateTime.Parse(NgayBan);

                // Tạo Object HoaDonNhapHang mới
                HoaDonBanHang hd = new HoaDonBanHang();
                hd.MA_HOA_DON = 0; // Gán default
                hd.MA_MAT_HANG = idMH;
                hd.SO_LUONG = sl;
                hd.DON_GIA = dg;
                hd.PHI_SHIP = pvc;
                hd.NGAY_BAN = NgayBanHang;


                int sumBH = 0, sumNH = 0;
                if(!this.isValidLogic(hd, ref sumBH, ref sumNH))
                {
                    Message msg2 = new Message();
                    msg2.TYPE = "danger";
                    msg2.CONTENT = String.Format("Không thể thêm hóa đơn bán hàng này vì tổng số lượng hàng bán sau khi thêm" +
                        "là {0} > tổng số lượng hàng đã nhập là {1}. Vui lòng cập nhật lại số lượng nhập.",
                        sumBH, sumNH);
                    listMsg.Add(msg2);
                }
                else
                {
                    // Gọi Models
                    _m_HoaDonBanHang.addHoaDonBanHang(hd);

                    Message msg = new Message();
                    msg.TYPE = "success";
                    msg.CONTENT = String.Format("{0} {1} {2}",
                        Constants.THEM,
                        Constants.HOA_DON,
                        Constants.THANH_CONG).ToUpper();

                    listMsg.Add(msg);
                }

            }
            return listMsg;
        }

        public List<HoaDonBanHang> TimKiemHoaDon(string keyword, string type)
        {
            List<HoaDonBanHang> ds = this.getDanhSachHoaDonBanHang();
            List<HoaDonBanHang> result = new List<HoaDonBanHang>();
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

        public HoaDonBanHang getHoaDonById(string id)
        {
            return _m_HoaDonBanHang.getHoaDonById(int.Parse(id));
        }

        public List<Message> suaHoaDonBanHang(string idHoaDon, string MaMH, string SoLuong, string DonGia, string PhiVanChuyen, string NgayBan)
        {
            // ======= VALIDATION =========
            // Biến cờ cho kết quả validate dữ liệu
            bool isValidData = false;

            // List message validate
            List<Message> listMsg = this.validateUserInput(idHoaDon, MaMH, SoLuong, DonGia, PhiVanChuyen, NgayBan);
            if (listMsg.Count == 0)
            {
                isValidData = true; // Validate dữ liệu thành công
            }
            // ======= VALIDATION =========

            if(isValidData == true)
            {
                // Convert dữ liệu
                int idHD = int.Parse(idHoaDon);
                int idMH = int.Parse(MaMH);
                int sl = int.Parse(SoLuong);
                int dg = int.Parse(DonGia);
                int pvc = int.Parse(PhiVanChuyen);
                DateTime NgayBanHang = DateTime.Parse(NgayBan);

                // Tạo Object HoaDonNhapHang mới
                HoaDonBanHang hd = new HoaDonBanHang();
                hd.MA_HOA_DON = idHD;
                hd.MA_MAT_HANG = idMH;
                hd.SO_LUONG = sl;
                hd.DON_GIA = dg;
                hd.PHI_SHIP = pvc;
                hd.NGAY_BAN = NgayBanHang;

                // Kiểm tra Logic
                int sumBH = 0, sumNH = 0;
                if (!this.isValidLogic(hd, ref sumBH, ref sumNH))
                {
                    Message msg2 = new Message();
                    msg2.TYPE = "danger";
                    msg2.CONTENT = String.Format("Không thể thêm hóa đơn bán hàng này vì tổng số lượng hàng bán sau khi thêm" +
                        "là {0} > tổng số lượng hàng đã nhập là {1}. Vui lòng cập nhật lại số lượng nhập.",
                        sumBH, sumNH);
                    listMsg.Add(msg2);
                }
                else
                {
                    // Gọi model
                    if (_m_HoaDonBanHang.updateHoaDon(hd))
                    {
                        Message msg = new Message();
                        msg.TYPE = "success";
                        msg.CONTENT = String.Format("{0} {1} {2}",
                            Constants.CAP_NHAT,
                            Constants.HOA_DON,
                            Constants.THANH_CONG).ToUpper();
                        listMsg.Add(msg);
                    }
                    else
                    {
                        Message msg = new Message();
                        msg.TYPE = "warning";
                        msg.CONTENT = String.Format("{0} {1} {2}",
                            Constants.CAP_NHAT,
                            Constants.HOA_DON,
                            Constants.THAT_BAI).ToUpper();
                        listMsg.Add(msg);
                    }
                }
            }

            return listMsg;
        }

        public List<Message> xoaHoaDon(string id)
        {
            List<Message> listMsg = new List<Message>();

            if (_m_HoaDonBanHang.deleteHoaDon(int.Parse(id)))
            {
                Message msg = new Message();
                msg.TYPE = "success";
                msg.CONTENT = String.Format("{0} {1} {2}",
                    Constants.XOA,
                    Constants.HOA_DON,
                    Constants.THANH_CONG).ToUpper();
                listMsg.Add(msg);
            }
            else
            {
                Message msg = new Message();
                msg.TYPE = "danger";
                msg.CONTENT = String.Format("{0} {1} {2}",
                    Constants.XOA,
                    Constants.HOA_DON,
                    Constants.THAT_BAI).ToUpper();
                listMsg.Add(msg);
            }

            return listMsg;
        }
    }
}