using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DOANLTHDT_1988216.Entities;

namespace DOANLTHDT_1988216.Models
{
    public class m_HoaDonBanHang
    {
        private void writeToFile(List<HoaDonBanHang> dsHD)
        {
            string filePath = HttpContext.Current.Server.MapPath("~/Models/DB_HoaDonBanHang.txt");
            StreamWriter file = new StreamWriter(filePath);

            // Đếm lại tổng hóa đơn
            int TongHoaDon = dsHD.Count;

            // Cập nhật lại vào file
            file.WriteLine(TongHoaDon);

            // Cập nhật mặt hàng lại vào file
            foreach (var m in dsHD)
            {
                string textToWrite =
                    m.MA_HOA_DON + "," +
                    m.MA_MAT_HANG + "," +
                    m.SO_LUONG + "," +
                    m.DON_GIA + "," +
                    m.PHI_SHIP + "," +
                    m.NGAY_BAN;

                file.WriteLine(textToWrite);
            }

            file.Close();
        }
        public List<HoaDonBanHang> getAllHoaDonBanHang()
        {
            string filePath = HttpContext.Current.Server.MapPath("~/Models/DB_HoaDonBanHang.txt");
            StreamReader file = new StreamReader(filePath);
            List<HoaDonBanHang> dsHD = new List<HoaDonBanHang>();
            int numOfHoaDon = int.Parse(file.ReadLine());
            for (int i = 0; i < numOfHoaDon; i++)
            {
                string dataFromLine = file.ReadLine();
                string[] dataArr = dataFromLine.Split(',');
                HoaDonBanHang HD = new HoaDonBanHang();

                HD.MA_HOA_DON = int.Parse(dataArr[0]);
                HD.MA_MAT_HANG = int.Parse(dataArr[1]);
                HD.SO_LUONG = int.Parse(dataArr[2]);
                HD.DON_GIA = int.Parse(dataArr[3]);
                HD.PHI_SHIP = int.Parse(dataArr[4]);
                HD.NGAY_BAN = DateTime.Parse(dataArr[5]);

                dsHD.Add(HD);
            }

            file.Close();

            return dsHD;
        }

        public void addHoaDonBanHang(HoaDonBanHang newHD)
        {
            // Đọc dữ liệu cũ
            List<HoaDonBanHang> dsHD = this.getAllHoaDonBanHang();
            int newId = 0;
            foreach (var m in dsHD)
            {
                if (newId < m.MA_HOA_DON)
                {
                    newId = m.MA_HOA_DON;
                }
            }
            newId++; // Id mới

            // Update lại id cho parameter truyền vào (giá trị mặc định đang là 0)
            newHD.MA_HOA_DON = newId;

            // Thêm vào list hiện tại
            dsHD.Add(newHD);

            // Viết list mới vào file
            this.writeToFile(dsHD);
        }

        public HoaDonBanHang getHoaDonById(int id)
        {
            List<HoaDonBanHang> ds = this.getAllHoaDonBanHang();

            foreach (var d in ds)
            {
                if (d.MA_HOA_DON == id)
                {
                    return d;
                }
            }

            return null;
        }

        public List<HoaDonBanHang> getListHDBanHangByMatHangID(int id)
        {
            List<HoaDonBanHang> ds = this.getAllHoaDonBanHang();
            List<HoaDonBanHang> res = new List<HoaDonBanHang>();
            foreach (var d in ds)
            {
                if (d.MA_MAT_HANG == id)
                {
                    res.Add(d);
                }
            }
            return res;
        }

        public bool updateHoaDon(HoaDonBanHang newHD)
        {
            List<HoaDonBanHang> ds = this.getAllHoaDonBanHang();
            bool flag = false;
            foreach (var d in ds)
            {
                if (d.MA_HOA_DON == newHD.MA_HOA_DON)
                {
                    // Tìm thấy thì update thông tin mới
                    d.MA_MAT_HANG = newHD.MA_MAT_HANG;
                    d.SO_LUONG = newHD.SO_LUONG;
                    d.DON_GIA = newHD.DON_GIA;
                    d.PHI_SHIP = newHD.PHI_SHIP;
                    d.NGAY_BAN = newHD.NGAY_BAN;
                    flag = true;
                    break;
                }
            }

            // Nếu tìm thấy id MatHang này trong DB thì tiến hành update
            if (flag)
            {
                this.writeToFile(ds);
                return true;
            }
            else
            {
                // Không thấy thì return false
                return false;
            }

        }

        public bool deleteHoaDon(int id)
        {
            List<HoaDonBanHang> ds = this.getAllHoaDonBanHang();
            HoaDonBanHang temp = new HoaDonBanHang();
            bool flag = false;

            foreach (var d in ds)
            {
                if (d.MA_HOA_DON == id)
                {
                    temp = d;
                    flag = true;
                    break;
                }
            }

            if (flag)
            {
                ds.Remove(temp);
                // Viết lại ra file
                this.writeToFile(ds);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}