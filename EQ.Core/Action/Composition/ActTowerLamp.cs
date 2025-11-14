// EQ.Core/Action/Composition/ActTowerLamp.cs
using EQ.Core.Actions;
using EQ.Domain.Enums;
using System.Threading.Tasks;

namespace EQ.Core.Action
{
    /// <summary>
    /// 타워 램프 및 부저를 FSM 상태에 맞게 제어하는 모듈
    /// </summary>
    public class ActTowerLamp : ActComponent
    {
        public ActTowerLamp(ACT act) : base(act) { }

        // FSM 상태에 따른 램프/부저 설정
        public void SetState(EqState state)
        {
            // 모든 램프/부저를 끈다
            AllOff();

            switch (state)
            {
                case EqState.Init:
                    // 초기화/부팅 중: 옐로우 점등
                    SetLamp(IO_OUT.Tower_Lamp_Yellow, true);
                    // (참고: 점멸 로직은 별도 Task가 필요하며, 현재는 점등으로 구현됨)
                    break;

                case EqState.Idle:
                    // 대기 중: 그린 점등
                    SetLamp(IO_OUT.Tower_Lamp_Green, true);
                    break;

                case EqState.Running:
                    // 실행 중: 옐로우 점등
                    // (참고: 기존 Init과 동일하게 옐로우지만, 공정 중임을 나타냄)
                    SetLamp(IO_OUT.Tower_Lamp_Yellow, true);
                    break;

                case EqState.Error:
                    // 오류: 레드 점등 + 부저 울림
                    SetLamp(IO_OUT.Tower_Lamp_Red, true);
                    SetBuzzer(true);
                    break;
            }
        }

        private void SetLamp(IO_OUT lamp, bool on)
        {
            _act.IO.WriteOutput(lamp, on);
        }

        private void SetBuzzer(bool on)
        {
            // IO.cs에 정의된 부저 사용
            _act.IO.WriteOutput(IO_OUT.BUZZER_1, on);
            // _act.IO.WriteOutput(IO_OUT.BUZZER_2, on);
            // _act.IO.WriteOutput(IO_OUT.BUZZER_3, on);
        }

        private void AllOff()
        {
            SetLamp(IO_OUT.Tower_Lamp_Red, false);
            SetLamp(IO_OUT.Tower_Lamp_Yellow, false);
            SetLamp(IO_OUT.Tower_Lamp_Green, false);
            SetBuzzer(false);
        }

        /// <summary>
        /// (FSM과 별개) 부저만 강제로 끄는 메서드 (예: UI에서 Mute 버튼 클릭)
        /// </summary>
        public void SilenceBuzzer()
        {
            SetBuzzer(false);
        }
    }
}