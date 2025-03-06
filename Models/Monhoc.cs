using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace WPF_FE.Models
{
    public partial class Monhoc
    {
        public Monhoc()
        {
            Diemthis = new HashSet<Diemthi>();
        }

        public string Msmh { get; set; }
        public string Tenmh { get; set; }
        public int? Sotiet { get; set; }
        [JsonIgnore]

        public virtual ICollection<Diemthi> Diemthis { get; set; }
    }
}
