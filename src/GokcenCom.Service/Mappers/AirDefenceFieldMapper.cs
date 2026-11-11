namespace GokcenCom.Service.Mappers
{
    public static class AirDefenceFieldMapper
    {
        public static Domain.Models.AirDefenceField toAirDefenceFieldModel(this Domain.SihaCustom.AirDefenceField airDefenceField)
        {
            return new Domain.Models.AirDefenceField
            {
                sunucusaati = airDefenceField.ServerTime.toServerTimeModel(),
                hss_koordinat_bilgileri = airDefenceField.AdfCoordinates.Select(adf => adf.toADFCoordinateModel()).ToList()
            };
        }

        public static Domain.SihaCustom.AirDefenceField toAirDefenceFieldMsg(this Domain.Models.AirDefenceField airDefenceField)
        {
            return new Domain.SihaCustom.AirDefenceField
            {
                ServerTime = airDefenceField.sunucusaati.toServerTimeMsg(),
                AdfCoordinates = airDefenceField.hss_koordinat_bilgileri.Select(adf => adf.toADFCoordinateMsg()).ToArray()
            };
        }

        public static Domain.SihaCustom.ADFCoordinate toADFCoordinateMsg(this Domain.Models.ADFCoordinate adfCoordinate)
        {
            return new Domain.SihaCustom.ADFCoordinate
            {
                Id = adfCoordinate.id,
                AdfLatitude = adfCoordinate.hssEnlem,
                AdfLongitude = adfCoordinate.hssBoylam,
                AdfRadius = adfCoordinate.hssYaricap
            };
        }

        public static Domain.Models.ADFCoordinate toADFCoordinateModel(this Domain.SihaCustom.ADFCoordinate ADFCoordinate)
        {
            return new Domain.Models.ADFCoordinate
            {
                id = ADFCoordinate.Id,
                hssEnlem = ADFCoordinate.AdfLongitude,
                hssBoylam = ADFCoordinate.AdfLatitude,
                hssYaricap = ADFCoordinate.AdfRadius
            };
        }


    }
}