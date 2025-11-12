using EQ.Core.Actions;
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

        public async Task<ActionStatus> CylinderPushAsync()
        {
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
