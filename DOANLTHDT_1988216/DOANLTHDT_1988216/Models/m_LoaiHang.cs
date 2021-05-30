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

        public void writeToFile(List<LoaiHang> dsLH)
        {
            string filePath = HttpContext.Current.Server.MapPath("~/Models/DB_LoaiHang.txt");
            StreamWriter file = new StreamWriter(filePath);

            // Đếm lại tổng loại hàng
            int TongLoaiHang = dsLH.Count;

            // Cập nhật lại vào file
            file.WriteLine(TongLoaiHang);

            // Cập nhật mặt hàng lại vào file
            foreach (var m in dsLH)
            {
                string textToWrite = m.MA_LOAI_HANG + "," + m.TEN_LOAI_HANG;
                file.WriteLine(textToWrite);
            }

            file.Close();
        }

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

        public LoaiHang getLoaiHangById(int id)
        {
            List<LoaiHang> dsLH = this.getAllLoaiHang();
            foreach(var lh in dsLH)
            {
                if(lh.MA_LOAI_HANG == id)
                {
                    return lh;
                }
            }

            return null;
        }

        public void addLoaiHang(LoaiHang newLH)
        {
            // Đọc dữ liệu cũ
            List<LoaiHang> dsLH = this.getAllLoaiHang();
            int newId = 0;
            foreach (var m in dsLH)
            {
                if (newId < m.MA_LOAI_HANG)
                {
                    newId = m.MA_LOAI_HANG;
                }
            }
            newId++; // Mã loại hàng mới

            LoaiHang MH = new LoaiHang();
            MH.MA_LOAI_HANG = newId;
            MH.TEN_LOAI_HANG = newLH.TEN_LOAI_HANG;
            // Thêm vào list hiện tại
            dsLH.Add(MH);

            // Viết list mới vào file
            this.writeToFile(dsLH);
        }

        public bool updateLoaiHang(LoaiHang newLH)
        {
            List<LoaiHang> dsLH = this.getAllLoaiHang();
            bool flag = false;
            foreach (var lh in dsLH)
            {
                if (lh.MA_LOAI_HANG == newLH.MA_LOAI_HANG)
                {
                    // Tìm thấy thì update thông tin mới
                    lh.TEN_LOAI_HANG = newLH.TEN_LOAI_HANG;
                    flag = true;
                    break;
                }
            }

            // Nếu tìm thấy id MatHang này trong DB thì tiến hành update
            if (flag)
            {
                this.writeToFile(dsLH);
                return true;
            }
            else
            {
                // Không thấy thì return false
                return false;
            }
        }

        public bool deleteLoaiHang(int id)
        {
            List<LoaiHang> dsLH = this.getAllLoaiHang();
            m_MatHang m_mh = new m_MatHang();

            // Lấy danh sách mặt hàng thuộc thể loại hàng cần xóa
            List<MatHang> dsMHThuocLoaiHang = m_mh.getListMatHangByLoaiHangId(id);


            // Nếu tồn tại ít nhất 1 mặt hàng trong thể loại này thì tiến hành duyệt mảng
            if(dsMHThuocLoaiHang.Count > 0)
            {
                // Loop duyệt ds mặt hàng để đưa giá trị loại hàng = 0 nếu như mặt hàng này thuộc thể loại cần xóa
                foreach(var d in dsMHThuocLoaiHang)
                {
                    d.LOAI_HANG = 0;
                    m_mh.updateMatHang(d);
                }
            }

            // Xóa loại hàng
            LoaiHang temp = new LoaiHang();
            bool flag = false;

            foreach (var lh in dsLH)
            {
                if (lh.MA_LOAI_HANG == id)
                {
                    temp = lh;
                    flag = true;
                    break;
                }
            }

            if (flag)
            {
                dsLH.Remove(temp);
                // Viết lại ra file
                this.writeToFile(dsLH);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}