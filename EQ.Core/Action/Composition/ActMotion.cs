using EQ.Core.Actions;
using EQ.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Core.Action
{
    public class ActMotion : ActComponent
    {
        private IMotionController _ioHardware;
        public ActMotion(ACT act) : base(act) { }

        public void SetHardwareController(IMotionController controller)
        {
            this._ioHardware = controller;

            _ioHardware.Init("ModelData/wmx_parameters.xml");
        }

        public async Task<ActionStatus> HomeSearchAsync()
        {
            // ACT의 공통 실행 함수 호출 (_act 접근 가능)
            return await _act.ExecuteAction(
                title: "Motion_HomeSearch",
                stepNames: new List<string> { "Start", "ServoOn", "Homing", "End" },
                stepLogic: async (context, stepName) =>
                {
                    int nextStep = context.StepIndex + 1;

                    switch (stepName)
                    {
                        case "Start":
                            // 로그 등 초기화
                            break;
                        case "ServoOn":
                            // 모터 서보 온 로직
                            await Task.Delay(100);
                            break;
                        case "Homing":
                            // 원점 복귀 로직
                            await Task.Delay(500);
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
