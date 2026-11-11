namespace GokcenCom.Service.Mappers
{
    public static class LockMapper
    {
        public static Domain.Models.LockInfo toLockModel(this Domain.SihaCustom.Lock lockInfo)
        {
            return new Domain.Models.LockInfo
            {
                kilitlenmeBaslangicZamani = lockInfo.LockStartTime.toTimeModel(),
                kilitlenmeBitisZamani = lockInfo.LockEndTime.toTimeModel(),
                otonom_kilitlenme = lockInfo.Autonomous
            };
        }
    }
}