namespace GokcenCom.Domain.Models
{
    public class KamikazeInfo
    {
        public required Time kamikazeBaslangicZamani { get; set; }
        public required Time kamikazeBitisZamani { get; set; }
        public required string qrMetni { get; set; }
    }
}