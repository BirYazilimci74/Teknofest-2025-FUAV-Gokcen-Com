namespace GokcenCom.Domain.Models
{
    public class LockInfo
    {
        public required Time kilitlenmeBaslangicZamani { get; set; }
        public required Time kilitlenmeBitisZamani { get; set; }
        public required int otonom_kilitlenme { get; set; }
    }
}