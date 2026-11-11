namespace GokcenCom.Service.Mappers
{
    public static class TimeMapper
    {
        public static Domain.Models.Time toTimeModel(this Domain.SihaCustom.Time time)
        {
            return new Domain.Models.Time
            {
                saat = time.Hour,
                dakika = time.Minute,
                saniye = time.Second,
                milisaniye = time.Millisecond,
            };
        }

        public static Domain.SihaCustom.Time toTimeMsg(this Domain.Models.Time time)
        {
            return new Domain.SihaCustom.Time
            {
                Hour = time.saat,
                Minute = time.dakika,
                Second = time.saniye,
                Millisecond = time.milisaniye,
            };
        }

        public static Domain.Models.ServerTime toServerTimeModel(this Domain.SihaCustom.ServerTime time)
        {
            return new Domain.Models.ServerTime
            {
                gun = time.Day,
                saat = time.Hour,
                dakika = time.Minute,
                saniye = time.Second,
                milisaniye = time.Millisecond
            };
        }

        public static Domain.SihaCustom.ServerTime toServerTimeMsg(this Domain.Models.ServerTime time)
        {
            return new Domain.SihaCustom.ServerTime
            {
                Day = time.gun,
                Hour = time.saat,
                Minute = time.dakika,
                Second = time.saniye,
                Millisecond = time.milisaniye
            };
        }
    }
}