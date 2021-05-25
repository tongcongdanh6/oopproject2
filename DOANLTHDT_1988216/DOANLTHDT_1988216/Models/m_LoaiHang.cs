using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DOANLTHDT_1988216.Entities;

namespace DOANLTHDT_1988216.Models
{
    public class m_LoaiHang
    {
        public List<LoaiHang> getAllLoaiHang()
        {
            string filePath = HttpContext.Current.Server.MapPath("~/Models/DB_LoaiHang.txt");
            StreamReader file = new StreamReader(filePath);
            List<LoaiHang> dsLH = new List<LoaiHang>();
            int numOfLoaiHang = int.Parse(file.ReadLine());
            for (int i = 0; i < numOfLoaiHang; i++)
            {
                string dataFromLine = file.ReadLine();
                string[] dataArr = dataFromLine.Split(',');
                LoaiHang LH = new LoaiHang();

                LH.MA_LOAI_HANG = int.Parse(dataArr[0]);
                LH.TEN_LOAI_HANG = dataArr[1];

                dsLH.Add(LH);
            }

            file.Close();

            return dsLH;
        }
    }
}