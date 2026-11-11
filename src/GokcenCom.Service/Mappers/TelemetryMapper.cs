namespace GokcenCom.Service.Mappers
{
    public static class TelemetryMapper
    {
        public static Domain.SihaCustom.Telemetry toTelemetryMsg(this Domain.Models.Telemetry telemetry)
        {
            return new Domain.SihaCustom.Telemetry
            {
                TakimNumarasi = telemetry.takim_numarasi,
                IhaEnlem = telemetry.iha_enlem,
                IhaBoylam = telemetry.iha_boylam,
                IhaIrtifa = telemetry.iha_irtifa,
                IhaDikilme = telemetry.iha_dikilme,
                IhaYonelme = telemetry.iha_yonelme,
                IhaYatis = telemetry.iha_yatis,
                IhaHiz = telemetry.iha_hiz,
                IhaBatarya = telemetry.iha_batarya,
                IhaOtonom = telemetry.iha_otonom,
                IhaKilitlenme = telemetry.iha_kilitlenme,
                HedefMerkezX = telemetry.hedef_merkez_X,
                HedefMerkezY = telemetry.hedef_merkez_Y,
                HedefGenislik = telemetry.hedef_genislik,
                HedefYukseklik = telemetry.hedef_yukseklik,
                GpsSaati = telemetry.gps_saati.toTimeMsg(),
            };
        }

        public static Domain.Models.Telemetry toTelemetryModel(this Domain.SihaCustom.Telemetry telemetry)
        {
            return new Domain.Models.Telemetry
            {
                takim_numarasi = telemetry.TakimNumarasi,
                iha_enlem = telemetry.IhaEnlem,
                iha_boylam = telemetry.IhaBoylam,
                iha_irtifa = telemetry.IhaIrtifa,
                iha_dikilme = telemetry.IhaDikilme,
                iha_yonelme = telemetry.IhaYonelme,
                iha_yatis = telemetry.IhaYatis,
                iha_hiz = telemetry.IhaHiz,
                iha_batarya = telemetry.IhaBatarya,
                iha_otonom = telemetry.IhaOtonom,
                iha_kilitlenme = telemetry.IhaKilitlenme,
                hedef_merkez_X = telemetry.HedefMerkezX,
                hedef_merkez_Y = telemetry.HedefMerkezY,
                hedef_genislik = telemetry.HedefGenislik,
                hedef_yukseklik = telemetry.HedefYukseklik,
                gps_saati = telemetry.GpsSaati.toTimeModel(),
            };
        }

        public static Domain.Models.Telemetry toTelemetryModel(this Domain.Gokcen.Telemetry telemetry)
        {
            return new Domain.Models.Telemetry
            {
                takim_numarasi = 1,
                iha_enlem = telemetry.LatitudeCurrentPos,
                iha_boylam = telemetry.LongitudeCurrentPos,
                iha_irtifa = (int)telemetry.AltitudeCurrentPos,
                iha_dikilme = (int)telemetry.Pitch,
                iha_yonelme = (int)telemetry.Yaw,
                iha_yatis = (int)telemetry.Roll,
                //iha_hiz = telemetry.VelocityLocalLinear,
                //iha_batarya = telemetry,
                //iha_otonom = telemetry,
                //iha_kilitlenme = telemetry,
                //hedef_merkez_X = telemetry,
                //hedef_merkez_Y = telemetry,
                //hedef_genislik = telemetry,
                //hedef_yukseklik = telemetry,
                gps_saati = new Domain.Models.Time{}
            };
        }
    }
}