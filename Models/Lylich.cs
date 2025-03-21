﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WPF_FE.Models
{
    public partial class Lylich
    {
        public Lylich()
        {
            Diemthis = new HashSet<Diemthi>();
        }

        public string Mshv { get; set; }
        public string Tenhv { get; set; }
        public DateTime? Ngaysinh { get; set; }
        public bool? Phai { get; set; }
        public string Malop { get; set; }
        public string PhaiFormat
        {
            get
            {
                if (!this.Phai.HasValue) { return string.Empty; }
                return (bool)this.Phai ? "Name" : "Nữ";
            }
        }
        public string NgaySinhFormat
        {
            get
            {
                if (!this.Ngaysinh.HasValue) { return string.Empty; }
                return this.Ngaysinh.Value.ToShortDateString();
            }
        }

        public virtual Lop MalopNavigation { get; set; }
        public virtual ICollection<Diemthi> Diemthis { get; set; }
    }
}
