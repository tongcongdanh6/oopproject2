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
    }
}