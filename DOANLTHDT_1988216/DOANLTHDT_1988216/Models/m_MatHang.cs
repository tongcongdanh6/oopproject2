using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DOANLTHDT_1988216.Entities;

namespace DOANLTHDT_1988216.Models
{
    public class m_MatHang
    {

        private void writeToFile(List<MatHang> dsMH)
        {
            string filePath = HttpContext.Current.Server.MapPath("~/Models/DB_MatHang.txt");
            StreamWriter file = new StreamWriter(filePath);

            // Đếm lại tổng mặt hàng
            int TongMatHang = dsMH.Count;

            // Cập nhật lại vào file
            file.WriteLine(TongMatHang);

            // Cập nhật mặt hàng lại vào file
            foreach (var m in dsMH)
            {
                string textToWrite = m.MA_MAT_HANG + "," + m.TEN_MAT_HANG + "," + m.HAN_SU_DUNG.ToString() + "," + m.CONG_TY_SX + "," + m.NAM_SX.ToString() + "," + m.LOAI_HANG;
                file.WriteLine(textToWrite);
            }

            file.Close();
           
        }
        public List<MatHang> getAllMatHang()
        {
            string filePath = HttpContext.Current.Server.MapPath("~/Models/DB_MatHang.txt");
            StreamReader file = new StreamReader(filePath);
            List<MatHang> dsMH = new List<MatHang>();
            int numOfMatHang = int.Parse(file.ReadLine());
            for(int i = 0; i < numOfMatHang; i++)
            {
                string dataFromLine = file.ReadLine();
                string[] dataArr = dataFromLine.Split(',');
                MatHang MH = new MatHang();

                MH.MA_MAT_HANG = int.Parse(dataArr[0]);
                MH.TEN_MAT_HANG = dataArr[1];
                MH.HAN_SU_DUNG = DateTime.Parse(dataArr[2]);
                MH.CONG_TY_SX = dataArr[3];
                MH.NAM_SX = DateTime.Parse(dataArr[4]);
                MH.LOAI_HANG = int.Parse(dataArr[5]);

                dsMH.Add(MH);
            }

            file.Close();

            return dsMH;
        }

        public void themMatHang(MatHang mh)
        {
            // Đọc dữ liệu cũ
            List<MatHang> dsMH = this.getAllMatHang();
            int newId = 0;
            foreach(var m in dsMH)
            {
                if(newId < m.MA_MAT_HANG)
                {
                    newId = m.MA_MAT_HANG;
                }
            }
            newId++; // Mã mặt hàng mới

            string filePath = HttpContext.Current.Server.MapPath("~/Models/DB_MatHang.txt");
            StreamWriter file = new StreamWriter(filePath);

            MatHang MH = new MatHang();
            MH.MA_MAT_HANG = newId;
            MH.TEN_MAT_HANG = mh.TEN_MAT_HANG;
            MH.HAN_SU_DUNG = mh.HAN_SU_DUNG;
            MH.CONG_TY_SX = mh.CONG_TY_SX;
            MH.NAM_SX = mh.NAM_SX;
            MH.LOAI_HANG = mh.LOAI_HANG;
            // Thêm vào list hiện tại
            dsMH.Add(MH);

            // Đếm lại tổng mặt hàng
            int TongMatHang = dsMH.Count;

            // Cập nhật lại vào file
            file.WriteLine(TongMatHang);

            // Cập nhật mặt hàng lại vào file
            foreach(var m in dsMH)
            {
                string textToWrite = m.MA_MAT_HANG + "," + m.TEN_MAT_HANG + "," + m.HAN_SU_DUNG.ToString() + "," + m.CONG_TY_SX + "," + m.NAM_SX.ToString() + "," + m.LOAI_HANG;
                file.WriteLine(textToWrite);
            }

            file.Close();
        }

        public MatHang getMatHangById(int id)
        {
            List<MatHang> dsMH = this.getAllMatHang();
            foreach(var mh in dsMH)
            {
                if(mh.MA_MAT_HANG == id)
                {
                    return mh;
                }
            }
            return null;
        }

        public bool updateMatHang(MatHang newMH)
        {
            List<MatHang> dsMH = this.getAllMatHang();
            bool flag = false;
            foreach (var mh in dsMH)
            {
                if (mh.MA_MAT_HANG == newMH.MA_MAT_HANG)
                {
                    // Tìm thấy thì update thông tin mới
                    mh.TEN_MAT_HANG = newMH.TEN_MAT_HANG;
                    mh.CONG_TY_SX = newMH.CONG_TY_SX;
                    mh.HAN_SU_DUNG = newMH.HAN_SU_DUNG;
                    mh.NAM_SX = newMH.NAM_SX;
                    mh.LOAI_HANG = newMH.LOAI_HANG;
                    flag = true;
                    break;
                }
            }

            // Nếu tìm thấy id MatHang này trong DB thì tiến hành update
            if(flag)
            {
                string filePath = HttpContext.Current.Server.MapPath("~/Models/DB_MatHang.txt");
                StreamWriter file = new StreamWriter(filePath);

                // Đếm lại tổng mặt hàng
                int TongMatHang = dsMH.Count;

                // Cập nhật lại vào file
                file.WriteLine(TongMatHang);

                // Cập nhật mặt hàng lại vào file
                foreach (var m in dsMH)
                {
                    string textToWrite = m.MA_MAT_HANG + "," + m.TEN_MAT_HANG + "," + m.HAN_SU_DUNG.ToString() + "," + m.CONG_TY_SX + "," + m.NAM_SX.ToString() + "," + m.LOAI_HANG;
                    file.WriteLine(textToWrite);
                }

                file.Close();
                return true;
            }
            else
            {
                // Không thấy thì return false
                return false;
            }
            
        }

        public bool deleteMatHang(int id)
        {
            List<MatHang> dsMH = this.getAllMatHang();
            MatHang temp = new MatHang();
            bool flag = false;

            foreach(var mh in dsMH)
            {
                if(mh.MA_MAT_HANG == id)
                {
                    temp = mh;
                    flag = true;
                    break;
                }
            }

            if(flag)
            {
                dsMH.Remove(temp);
                // Viết lại ra file
                this.writeToFile(dsMH);

                return true;
            }
            else
            {
                return false;
            }
  
        }
    }
}