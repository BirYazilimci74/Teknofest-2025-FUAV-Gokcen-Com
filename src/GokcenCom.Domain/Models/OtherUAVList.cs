namespace GokcenCom.Domain.Models
{
    public class OtherUAVList
    {
        public required ServerTime sunucusaati { get; set; }
        public required List<UAVLocation> konumBilgileri { get; set; }
    }
}