using EQ.Domain.Interface;
using WMX3ApiCLR;
using WMX3ApiCLR.EcApiCLR;
using static WMX3ApiCLR.EventControl;

namespace Hardware.Infra.Motion.WMX
{
    public class WMX_Motion : IMotionController
    {
        readonly int coefficient = 1; // Interface 호출부에서 적용 함

        protected WMX3Api Wmx3Lib = new WMX3Api();
        CoreMotionStatus CmStatus;
        CoreMotionStatus CmStatus2;

        CoreMotion Wmx3Lib_cm;

        EventControl CmControl;
        ComparatorSource ComSrc;
        PSOOutput PsoOut;

        Ecat EcLib;
        EcMasterInfo MotorInfo;

        //  WMX3ApiCLR.ProfileType profile = ProfileType.Trapezoidal;

        ProfileType[] MotorProfile;
        double[] MotorJerkRatio;
        public string LastError { get; private set; }
        public string getErrorStatus() => LastError;




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
            CmControl = new EventControl();
            ComSrc = new ComparatorSource();
            PsoOut = new PSOOutput();

            Wmx3Lib = new WMX3Api();
            CmStatus = new CoreMotionStatus();
            CmStatus2 = new CoreMotionStatus();
            Wmx3Lib_cm = new CoreMotion(Wmx3Lib);

            EcLib = new Ecat(Wmx3Lib);
            MotorInfo = new EcMasterInfo();

            int ret = 0;

            // Create device.
            ret += Wmx3Lib.CreateDevice("C:\\Program Files\\SoftServo\\WMX3\\",
                DeviceType.DeviceTypeNormal,
                0xFFFFFFFF);

            // Set Device Name.
            ret += Wmx3Lib.SetDeviceName("MotorControl");

            var path = configPath;//"d:\\wmx_parameters.xml";
            Config.AxisParam Parm = new Config.AxisParam();
            ret += Wmx3Lib_cm.Config.Import(path, ref Parm);
            ret += Wmx3Lib_cm.Config.SetAxisParam(Parm);

            //전체 파라메터 업로드
            ret += Wmx3Lib_cm.Config.ImportAndSetAll(path);

            ret += Wmx3Lib.StartCommunication(0xFFFFFFFF);

            //API Buffer Init
       //     apiBufferInit();

            //앱솔루트 모터에 대해 포지션 잡아 줌
            //Absolut_Home();          

            EcMasterInfo ec = new EcMasterInfo();
            EcLib.GetMasterInfo(ec);
            MotorProfile = new ProfileType[ec.Slaves.Length];
            MotorJerkRatio = new double[ec.Slaves.Length];


            if (ret != 0)
            {              
                Wmx3Lib.CloseDevice();
                return false;
            }

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