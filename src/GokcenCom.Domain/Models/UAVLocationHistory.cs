using System.ComponentModel.DataAnnotations;

namespace GokcenCom.Domain.Models
{
    public class UAVLocationHistory
    {
        [Key]
        public int Id { get; set; }
        public int takim_numarasi { get; set; }
        public double iha_enlem { get; set; }
        public double iha_boylam { get; set; }
        public double iha_irtifa { get; set; }
        public double iha_dikilme { get; set; }
        public int iha_yonelme { get; set; }
        public double iha_yatis { get; set; }
        public double iha_hizi { get; set; }
        public int zaman_farki { get; set; }
    }
}