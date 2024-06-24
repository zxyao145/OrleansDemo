using Orleans;

namespace OrlensDemo.GrainInterfacesLib
{
    [GenerateSerializer]
    public class CallDeviceCommand
    {
        [Id(0)]
        public string? Did { get; set; }
        [Id(1)]
        public ulong TraceId { get; set; }

        [Id(2)]
        public string? CmdName { get; set; }
        [Id(3)]
        public string? CmdProprtyies { get; set; }
    }


    [GenerateSerializer]
    public class CallDeviceResponse
    {
        [Id(0)]
        public ulong TraceId { get; set; }
        [Id(1)]
        public string? Result { get; set; }
    }

    [GenerateSerializer]
    public class DeviceState
    {
        [Id(0)]
        public string Model { get; set; } = "";

        [Id(1)]
        public bool IsOnLine { get; set; }
    }

    public interface IDeviceGrain :  IGrainWithStringKey
    {
        Task<CallDeviceResponse> Invoke(CallDeviceCommand callInfo);


        Task<DeviceState> GetState();
    }
}