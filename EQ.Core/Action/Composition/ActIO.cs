using EQ.Core.Actions;
using EQ.Domain.Enums;
using EQ.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Core.Action
{
    public class ActIO : ActComponent
    {
        private IIoController _ioHardware;

        public ActIO(ACT act) : base(act) { }

        public void SetHardwareController(IIoController controller)
        {
            this._ioHardware = controller;

            _ioHardware.Init("Config/IOConfig.json");
        }

        public (byte[] _input , byte[] _output) GetIoStatus()
        {
            if (this._ioHardware == null)
                return (null , null);

            return _ioHardware.GetCachedData();
        }

        public bool ReadInput(IO_IN address)
        {
            return _ioHardware.ReadInput((int)address);
        }
        public bool ReadOutput(IO_OUT address)
        {
            return _ioHardware.ReadOutput((int)address);
        }

        public void WriteInput(IO_IN address, bool On)
        {
            _ioHardware.WriteInput((int)address, On ? (byte)1 : (byte)0);
        }
        public void WriteOutput(IO_OUT address, bool On)
        {
            _ioHardware.WriteOutput((int)address, On ? (byte)1 : (byte)0);
        }        
       

        public async Task<ActionStatus> CylinderPushAsync()
        {
            _ioHardware.WriteOutput(0, 1);

            return await _act.ExecuteStepBasedAction(
                title: "IO_CylinderPush",
                stepNames: new List<string> { "Start", "SolenoidOn", "WaitSensor", "End" },
                stepLogic: async (context, stepName) =>
                {
                    int nextStep = context.StepIndex + 1;

                    switch (stepName)
                    {
                        case "SolenoidOn":
                            // IO Write 로직
                            break;
                        case "WaitSensor":
                            // 센서 대기 로직
                            // if (SensorOn) nextStep++; else nextStep = context.StepIndex; (대기)
                            await Task.Delay(1000);
                            break;
                        case "End":
                            context.Status = ActionStatus.Finished;
                            break;
                    }
                    return nextStep;
                }
            );
        }
    }
}
