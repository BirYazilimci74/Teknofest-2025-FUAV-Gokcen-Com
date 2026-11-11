namespace GokcenCom.Domain.Models
{
    public class AirDefenceField
    {
        public ServerTime sunucusaati { get; set; }
        public List<ADFCoordinate> hss_koordinat_bilgileri { get; set; }
    }
}