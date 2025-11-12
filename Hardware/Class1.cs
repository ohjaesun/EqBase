using EQ.Domain.Interface;
using WMX3ApiCLR;
using WMX3ApiCLR.SimuApiCLR;

namespace Hardware.Infra.IO.WMX
{
    public class WMX_IO : IIoController
    {
        protected WMX3Api Wmx3Lib = new WMX3Api();
        Io _io;
        Simu simu = new Simu();

        private byte[] InByte;
        private byte[] OutByte;

        int nNumChIn = 16;
        int nNumChOut = 16;

        

        public void Close()
        {
            Wmx3Lib.StopCommunication(0xFFFFFFFF);

            //Quit device.
            Wmx3Lib.CloseDevice();
            Wmx3Lib.Dispose();
        }





        public Task<double> ReadAnalogInAsync(int index)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ReadInAsync(int index)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ReadOutAsync(int index)
        {
            throw new NotImplementedException();
        }

        public Task<bool> WriteOutAsync(int index, bool onOff)
        {
            throw new NotImplementedException();
        }

        public bool Init(string configPath)
        {
            _io = new Io(Wmx3Lib);
            Wmx3Lib.CreateDevice("C:\\Program Files\\SoftServo\\WMX3\\", DeviceType.DeviceTypeNormal, 0xFFFFFFFF);

            // Set Device Name.
            Wmx3Lib.SetDeviceName("ControlIO");
            simu = new Simu(Wmx3Lib);

            // Start Communication.
            Wmx3Lib.StartCommunication(0xFFFFFFFF);

            nNumChIn = 16;
            nNumChOut = 16;

            InByte = new byte[nNumChIn / 8];
            OutByte = new byte[nNumChOut / 8];

            return true;
        }

        public bool ReadInput(int address)
        {
            throw new NotImplementedException();
        }

        public void WriteOutput(int address, bool value)
        {
            throw new NotImplementedException();
        }
    }
}
