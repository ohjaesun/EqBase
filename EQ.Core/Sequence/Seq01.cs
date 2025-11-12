using EQ.Core.Actions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

using static EQ.Core.Sequence.seq01Step;
namespace EQ.Core.Sequence
{
    public enum seq01Step
    {
        Start,
        인터락체크,
        Step2,
        Step3,
        단위_시퀀스_실행,
        End
    }

    public class Seq01 : AbstractSeqBase<seq01Step>
    {
        string className = (nameof(Seq01));

        // 1. 생성자: 부모(AbstractSeqBase)에게 ACT/SEQ 전달 (필수)
        public Seq01(SEQ seqManager, ACT actManager) : base(seqManager, actManager)
        {
            // 이 클래스 내에서 _seq, _act 사용 가능
        }

        // 2. 핵심 로직 구현 (필수)
        public override async Task doSequence()
        {
            // 'Step' 대신 'StepEnum'을 사용하면 switch가 더 명확해집니다.
            switch (Step)
            {
                case Start:
                    // 'Step++' 대신 Enum을 사용하면 점프/관리가 명확합니다.
                    Step++;// = 인터락체크;
                    break;

                case 인터락체크:
                    // 예: 주입받은 _act 인스턴스 사용
                    // if (await _act.Motion.IsServoOnAsync(...)) { ... }
                    await _act.IO.CylinderPushAsync();
                    await Task.Delay(1000);
                    Step = Step2;
                    break;

                case Step2:
                    await Task.Delay(1000);
                    Step = Step3;
                    break;

                case Step3:
                    await Task.Delay(1000);
                    Step = 단위_시퀀스_실행;
                    break;

                case 단위_시퀀스_실행:
                    await Task.Delay(1000);
                    _Step++;
                    break;

                case End:
                    _Step++; // Enum의 끝을 초과 (루프 종료)
                    break;

                default:
                    // 정의되지 않은 스텝
                    _Status = SeqStatus.ERROR;
                    break;
            }
        }
    }
}
