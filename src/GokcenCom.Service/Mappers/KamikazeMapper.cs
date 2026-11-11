namespace GokcenCom.Service.Mappers
{
    public static class KamikazeMapper
    {
        public static Domain.SihaCustom.Kamikaze toKamikazeMsg(this Domain.Models.KamikazeInfo kamikazeInfo)
        {
            return new Domain.SihaCustom.Kamikaze
            {
                KamikazeStartTime = kamikazeInfo.kamikazeBaslangicZamani.toTimeMsg(),
                KamikazeEndTime = kamikazeInfo.kamikazeBitisZamani.toTimeMsg(),
                QrContent = kamikazeInfo.qrMetni
            };
        }

        public static Domain.Models.KamikazeInfo toKamikazeModel(this Domain.SihaCustom.Kamikaze kamikaze)
        {
            return new Domain.Models.KamikazeInfo
            {
                kamikazeBaslangicZamani = kamikaze.KamikazeStartTime.toTimeModel(),
                kamikazeBitisZamani = kamikaze.KamikazeEndTime.toTimeModel(),
                qrMetni = kamikaze.QrContent
            };
        }
    }
}