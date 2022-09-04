using Orleans;

namespace OrlensDemo.GrainInterfacesLib
{

    public class CallDeviceCommand
    {
        public string? Did { get; set; }
        public ulong TraceId { get; set; }

        public string? CmdName { get; set; }
        public string? CmdProprtyies { get; set; }
    }

    public class CallDeviceResponse
    {
        public ulong TraceId { get; set; }
        public string? Result { get; set; }
    }
     

    public class DeviceState
    {
        public string Model { get; set; } = "";

        public bool IsOnLine { get; set; }
    }

    public interface IDeviceGrain :  IGrainWithStringKey
    {
        Task<CallDeviceResponse> Invoke(CallDeviceCommand callInfo);


        Task<DeviceState> GetState();
    }
}