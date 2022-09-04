using Orleans;
using OrlensDemo.GrainInterfacesLib;

namespace OrlensDemo.GrainsLib
{
    public class DeviceGrain : Grain<DeviceState>, IDeviceGrain
    {
        public string Model { get; set; } = "";


        public Task<CallDeviceResponse> Invoke(CallDeviceCommand callInfo)
        {
            throw new NotImplementedException();
        }

        public Task<DeviceState> GetState()
        {
            var did = this.GetPrimaryKeyString();
            var state = new DeviceState()
            {
                IsOnLine = (did.GetHashCode() % 2) == 0
            };
            return Task.FromResult(state);
        }
    }
}