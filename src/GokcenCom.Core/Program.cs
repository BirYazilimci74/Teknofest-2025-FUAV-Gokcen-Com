namespace GokcenCom.Core
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var comNode = new ComNode(args);
            var cts = new CancellationTokenSource();

            Task serverTimeTask = comNode.PublishServerTimeAsync(cts.Token);
            Task airDefenceFieldTask = comNode.GetAirDefenceFieldAsync(cts.Token);
            Task uavLocationListTask = comNode.PublishOtherUAVLocationListAsync(cts.Token);
            Task telemetryTask = comNode.PostTelemetry(cts.Token);
            Task loginTask = comNode.Login(cts.Token);
            Task qrTask = comNode.PublishQRCoordinateAsync(cts.Token);
            Task lockTask = comNode.PostLockInfo(cts.Token);
            Task kamikazeTask = comNode.PostKamikazeInfo(cts.Token);

            Task targetUAVTask = comNode.GetTargetUAVInfo(cts.Token);
            Task checkUAVTask = comNode.CheckNumberOfUAV(cts.Token);
            Task checkADFTask = comNode.CheckNumberOfADF(cts.Token);

            await Task.WhenAll(
                serverTimeTask,
                airDefenceFieldTask,
                uavLocationListTask,
                telemetryTask,
                loginTask,
                qrTask,
                lockTask,
                kamikazeTask,
                targetUAVTask,
                checkUAVTask,
                checkADFTask
            );
        }
    }
}