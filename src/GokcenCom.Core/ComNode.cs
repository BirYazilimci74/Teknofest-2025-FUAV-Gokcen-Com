using Rcl;
using Rcl.Logging;
using System.Text.Json;
using GokcenCom.Service.Server;
using GokcenCom.Service.Mappers;
using GokcenCom.Service.Database;
using GokcenCom.Repository.Server;
using GokcenCom.Repository.Database;
using GokcenCom.Repository.DBContext;
using GokcenCom.Domain.Interfaces.Server;
using GokcenCom.Domain.Interfaces.Database;

namespace GokcenCom.Core
{
    internal class ComNode
    {
        private RclContext context;
        private IRclNode node;

        //--------------------------Server Services----------------------------
        private readonly IGETService<Domain.Models.ServerTime> _serverTimeService;
        private readonly IGETService<Domain.Models.AirDefenceField> _adfService;
        private readonly IGETService<Domain.Models.QRCoordinate> _qrService;
        private readonly IPOSTService<Domain.Models.Telemetry> _telemetryService;
        private readonly IPOSTService<Domain.Models.LoginRequest> _loginRequestService;
        private readonly IPOSTService<Domain.Models.KamikazeInfo> _kamikazeInfoService;
        private readonly IPOSTService<Domain.Models.LockInfo> _lockInfoService;

        //------------------------Database Services---------------------------
        private readonly IService<Domain.Models.UAVLocationHistory> _locationHistoryService;
        private readonly IService<Domain.Models.UAVCurrentLocation> _currentLocationService;

        //-------------------------------Publishers--------------------------------
        private readonly IRclPublisher<Domain.Std.String> _serverTimePublisher;
        private readonly IRclPublisher<Domain.Std.String> _qrPublisher;
        private readonly IRclPublisher<Domain.Std.String> _uavLocationListPublisher;
        private readonly IRclPublisher<Domain.Std.String> _lockPublisher;

        //------------------------------Subscriptions------------------------------
        private readonly IRclSubscription<Domain.SihaCustom.Kamikaze> _kamikazeSubscription;
        private readonly IRclSubscription<Domain.SihaCustom.Lock> _lockSubscription;
        private readonly IRclSubscription<Domain.Gokcen.Telemetry> _rawTelemetrySubscription;
        private readonly IRclSubscription<Domain.SihaCustom.Telemetry> _telemetrySubscription;

        public ComNode(string[] args)
        {
            context = new RclContext(args);
            node = context.CreateNode("com_node");

            //--------------------------Server Services----------------------------
            _serverTimeService = new ServerTimeService(new ServerTimeSR());
            _adfService = new AirDefenceFieldService(new AirDefenceFieldSR());
            _telemetryService = new TelemetryService(new TelemetrySR());
            _loginRequestService = new LoginService(new LoginSR());
            _qrService = new QRCoordinateService(new QRCoordinateSR());
            _kamikazeInfoService = new KamikazeInfoService(new KamikazeInfoSR());
            _lockInfoService = new LockInfoService(new LockInfoSR());

            //------------------------Database Services---------------------------
            _locationHistoryService = new UAVLocationHistoryService(new UAVLocationHistoryRepository(new GokcenDBContext()));
            _currentLocationService = new UAVCurrentLocationService(new UAVCurrentLocationRepository(new GokcenDBContext()));

            //-------------------------------Publishers--------------------------------
            _serverTimePublisher = node.CreatePublisher<Domain.Std.String>("/chatter");
            _qrPublisher = node.CreatePublisher<Domain.Std.String>("/chatter");
            _lockPublisher = node.CreatePublisher<Domain.Std.String>("/chatter");
            _uavLocationListPublisher = node.CreatePublisher<Domain.Std.String>("/chatter");

            //------------------------------Subscriptions------------------------------
            _kamikazeSubscription = node.CreateSubscription<Domain.SihaCustom.Kamikaze>("/kamikaze_pack");
            _lockSubscription = node.CreateSubscription<Domain.SihaCustom.Lock>("/lock_pack");
            _rawTelemetrySubscription = node.CreateSubscription<Domain.Gokcen.Telemetry>("/obafc/telemetry");
            _telemetrySubscription = node.CreateSubscription<Domain.SihaCustom.Telemetry>("/telemetry");
        }

        public async Task PublishServerTimeAsync(CancellationToken token)
        {
            node.Logger.LogInformation($"Hello ROS2, {node.Name} was created! For ServerTime");

            var msg = new Domain.Std.String();
            while (!token.IsCancellationRequested)
            {
                var time = await _serverTimeService.GetAsync();
                msg.Data = $"Server Time: {time.gun}:{time.saat}:{time.dakika}:{time.saniye}:{time.milisaniye}";

                _serverTimePublisher.Publish(msg);
                await Task.Delay(1000, token);
            }
        }

        public async Task GetAirDefenceFieldAsync(CancellationToken token)
        {
            //to gcs and obafp
            node.Logger.LogInformation($"Hello ROS2, {node.Name} was created! For ADF");

            using var server = node.CreateService<
                Domain.SihaCustom.ADFCoordinatesService,
                Domain.SihaCustom.ADFCoordinatesServiceRequest,
                Domain.SihaCustom.ADFCoordinatesServiceResponse>("/adf_coordinates",
                    (request, state) =>
                    {
                        var adf = _adfService.GetAsync().GetAwaiter().GetResult();
                        return new Domain.SihaCustom.ADFCoordinatesServiceResponse
                        {
                            AdfCoordinates = adf.toAirDefenceFieldMsg()
                        };
                    });
            await Task.Delay(-1,token);
        }

        public async Task PublishOtherUAVLocationListAsync(CancellationToken token)
        {
            //to gcs
            node.Logger.LogInformation($"Hello ROS2, {node.Name} was created! For OtherUAVLocationList");

            var msg = new Domain.Std.String();
            while (!token.IsCancellationRequested)
            {
                var uavLocations = await _currentLocationService.GetAllAsync();
                var json = JsonSerializer.Serialize(uavLocations);
                msg.Data = $"UAV: {json}\n";
                
                _uavLocationListPublisher.Publish(msg);
                await Task.Delay(1000,token);
            }
        }

        public async Task PublishQRCoordinateAsync(CancellationToken token)
        {
            //to gcs and obafc
            node.Logger.LogInformation($"Hello ROS2, {node.Name} was created! For QR");

            var msg = new Domain.Std.String();
            while (!token.IsCancellationRequested)
            {
                var qr = await _qrService.GetAsync();
                msg.Data = $"Qr Coordinate: {qr.qrEnlem},{qr.qrBoylam}";
                
                _qrPublisher.Publish(msg);
                await Task.Delay(1000,token);
            }
        }

        //TODO: raw olarak gelecek burda çevirme yapılcak
        public async Task PostTelemetry(CancellationToken token)
        {
            //to cs
            node.Logger.LogInformation($"Hello ROS2, {node.Name} was created! For Telemetry");

            while (!token.IsCancellationRequested)
            {
                await foreach (var telemetry in _telemetrySubscription.ReadAllAsync())
                {
                    if (telemetry == null)
                    {
                        node.Logger.LogWarning("No telemetry data received, skipping...");
                        continue;
                    }
                    await _telemetryService.PostAsync(telemetry.toTelemetryModel());
                }
            }
        }

        public async Task PostKamikazeInfo(CancellationToken token)
        {
            //to cs
            node.Logger.LogInformation($"Hello ROS2, {node.Name} was created! For Kamikaze");

            await foreach (var kamikazePack in _kamikazeSubscription.ReadAllAsync(token))
            {
                await _kamikazeInfoService.PostAsync(kamikazePack.toKamikazeModel());
            }
        }

        public async Task PostLockInfo(CancellationToken token)
        {
            //to cs
            node.Logger.LogInformation($"Hello ROS2, {node.Name} was created! For Lock");

            await foreach (var lockPack in _lockSubscription.ReadAllAsync(token))
            {
                await _lockInfoService.PostAsync(lockPack.toLockModel());
            }
        }

        public async Task Login(CancellationToken token)
        {
            node.Logger.LogInformation($"Hello ROS2, {node.Name} was created! For LoginService");
            using var server = node.CreateService<
                Domain.SihaCustom.LoginService,
                Domain.SihaCustom.LoginServiceRequest,
                Domain.SihaCustom.LoginServiceResponse>("/cs_login",
                    (request, state) =>
                    {
                        Domain.Models.LoginResponse response = new Domain.Models.LoginResponse { };
                        if (_loginRequestService is LoginService loginService)
                        {
                            response = loginService.Login(new Domain.Models.LoginRequest { kadi = request.Username, sifre = request.Password })
                            .GetAwaiter().GetResult();
                            Console.WriteLine(response.takimNumarasi.ToString());
                        }
                        return new Domain.SihaCustom.LoginServiceResponse
                        {
                            LoginStatus = response.takimNumarasi.ToString(),
                        };
                    });
            await Task.Delay(-1,token);
        }

        public async Task GetTargetUAVInfo(CancellationToken token)
        {
            node.Logger.LogInformation($"Hello ROS2, {node.Name} was created! For TargetUAVService");
            using var server = node.CreateService<
                Domain.SihaCustom.TargetUAVLocationService,
                Domain.SihaCustom.TargetUAVLocationServiceRequest,
                Domain.SihaCustom.TargetUAVLocationServiceResponse>("/target_uav_location",
                    (request, state) =>
                    {
                        Domain.Models.LoginResponse response = new Domain.Models.LoginResponse { };
                        var targetUAV = _currentLocationService.GetByIdAsync(request.TargetUavId).GetAwaiter().GetResult();
                        return new Domain.SihaCustom.TargetUAVLocationServiceResponse
                        {
                            TargetUavLocation = targetUAV.toOtherUAVLocationMsg()
                        };
                    });
            await Task.Delay(-1,token);
        }

        public async Task CheckNumberOfUAV(CancellationToken token)
        {
            node.Logger.LogInformation($"Hello ROS2, {node.Name} was created! For CheckingUAVService");
            using var server = node.CreateService<
                Domain.SihaCustom.CheckUAVNumberService,
                Domain.SihaCustom.CheckUAVNumberServiceRequest,
                Domain.SihaCustom.CheckUAVNumberServiceResponse>("/check_uav_number_cmd",
                    (request, state) =>
                    {
                        Domain.Models.LoginResponse response = new Domain.Models.LoginResponse { };
                        int numberOfUAV = _currentLocationService.GetAllAsync().GetAwaiter().GetResult().Count();
                        
                        if (numberOfUAV > 0)
                        {
                            return new Domain.SihaCustom.CheckUAVNumberServiceResponse
                            {
                                Success = true
                            };
                        }

                        return new Domain.SihaCustom.CheckUAVNumberServiceResponse
                        {
                            Success = false
                        };      
                    });
            await Task.Delay(-1,token);
        }

        public async Task CheckNumberOfADF(CancellationToken token)
        {
            node.Logger.LogInformation($"Hello ROS2, {node.Name} was created! For CheckingADFService");
            using var server = node.CreateService<
                Domain.SihaCustom.CheckADFNumberService,
                Domain.SihaCustom.CheckADFNumberServiceRequest,
                Domain.SihaCustom.CheckADFNumberServiceResponse>("/check_adf_number_cmd",
                    (request, state) =>
                    {
                        Domain.Models.LoginResponse response = new Domain.Models.LoginResponse { };
                        int numberOfADF = _adfService.GetAsync().GetAwaiter().GetResult().hss_koordinat_bilgileri.Count;
                        
                        if (numberOfADF > 0)
                        {
                            return new Domain.SihaCustom.CheckADFNumberServiceResponse
                            {
                                Success = true
                            };
                        }

                        return new Domain.SihaCustom.CheckADFNumberServiceResponse
                        {
                            Success = false
                        };
                    });
            await Task.Delay(-1,token);
        }
    }
}