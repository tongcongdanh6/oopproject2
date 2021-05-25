using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOANLTHDT_1988216.Models;
using DOANLTHDT_1988216.Entities;

namespace DOANLTHDT_1988216.Controllers
{
    public class c_LoaiHang
    {
        public List<LoaiHang> getDanhSachLoaiHang()
        {
            m_LoaiHang m_LoaiHang = new m_LoaiHang();

            return m_LoaiHang.getAllLoaiHang();
        }
    }
}