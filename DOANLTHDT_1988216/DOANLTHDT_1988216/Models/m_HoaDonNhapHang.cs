using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DOANLTHDT_1988216.Entities;

namespace DOANLTHDT_1988216.Models
{
    public class m_HoaDonNhapHang
    {
        private void writeToFile(List<HoaDonNhapHang> dsHD)
        {
            string filePath = HttpContext.Current.Server.MapPath("~/Models/DB_HoaDonNhapHang.txt");
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
                    m.NGAY_NHAP;

                file.WriteLine(textToWrite);
            }

            file.Close();
        }
        public List<HoaDonNhapHang> getAllHoaDonNhapHang()
        {
            string filePath = HttpContext.Current.Server.MapPath("~/Models/DB_HoaDonNhapHang.txt");
            StreamReader file = new StreamReader(filePath);
            List<HoaDonNhapHang> dsHD = new List<HoaDonNhapHang>();
            int numOfHoaDon = int.Parse(file.ReadLine());
            for (int i = 0; i < numOfHoaDon; i++)
            {
                string dataFromLine = file.ReadLine();
                string[] dataArr = dataFromLine.Split(',');
                HoaDonNhapHang HD = new HoaDonNhapHang();

                HD.MA_HOA_DON = int.Parse(dataArr[0]);
                HD.MA_MAT_HANG = int.Parse(dataArr[1]);
                HD.SO_LUONG = int.Parse(dataArr[2]);
                HD.DON_GIA = int.Parse(dataArr[3]);
                HD.PHI_SHIP = int.Parse(dataArr[4]);
                HD.NGAY_NHAP = DateTime.Parse(dataArr[5]);

                dsHD.Add(HD);
            }

            file.Close();

            return dsHD;
        }

        public void addHoaDonNhapHang(HoaDonNhapHang newHD)
        {
            // Đọc dữ liệu cũ
            List<HoaDonNhapHang> dsHD = this.getAllHoaDonNhapHang();
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

        public HoaDonNhapHang getHoaDonById(int id)
        {
            List<HoaDonNhapHang> ds = this.getAllHoaDonNhapHang();

            foreach (var d in ds)
            {
                if (d.MA_HOA_DON == id)
                {
                    return d;
                }
            }

            return null;
        }
    }
}