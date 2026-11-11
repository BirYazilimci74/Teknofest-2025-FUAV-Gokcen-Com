namespace GokcenCom.Service.Mappers
{
    public static class UAVLocationMapper
    {
        public static Domain.Models.UAVLocationHistory toUAVLocationHistory(this Domain.Models.UAVLocation location)
        {
            return new Domain.Models.UAVLocationHistory
            {
                takim_numarasi = location.takim_numarasi,
                iha_enlem = location.iha_enlem,
                iha_boylam = location.iha_boylam,
                iha_irtifa = location.iha_irtifa,
                iha_dikilme = location.iha_dikilme,
                iha_yonelme = location.iha_yonelme,
                iha_yatis = location.iha_yatis,
                iha_hizi = location.iha_hizi,
                zaman_farki = location.zaman_farki
            };
        }

        public static Domain.Models.UAVCurrentLocation toUAVCurrentLocation(this Domain.Models.UAVLocation location)
        {
            return new Domain.Models.UAVCurrentLocation
            {
                takim_numarasi = location.takim_numarasi,
                iha_enlem = location.iha_enlem,
                iha_boylam = location.iha_boylam,
                iha_irtifa = location.iha_irtifa,
                iha_dikilme = location.iha_dikilme,
                iha_yonelme = location.iha_yonelme,
                iha_yatis = location.iha_yatis,
                iha_hizi = location.iha_hizi,
                zaman_farki = location.zaman_farki
            };
        }

        public static Domain.SihaCustom.OtherUAVLocation toOtherUAVLocationMsg(this Domain.Models.UAVCurrentLocation location)
        {
            return new Domain.SihaCustom.OtherUAVLocation
            {
                TakimNumarasi = location.takim_numarasi,
                IhaEnlem = location.iha_enlem,
                IhaBoylam = location.iha_boylam,
                IhaIrtifa = location.iha_irtifa,
                IhaDikilme = location.iha_dikilme,
                IhaYonelme = location.iha_yonelme,
                IhaYatis = location.iha_yatis,
                IhaHiz = location.iha_hizi,
                ZamanFarki = location.zaman_farki
            };
        }
    }
}