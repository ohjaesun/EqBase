using EQ.Core.Actions;
using EQ.Domain.Enums;
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
        InterLockCheck,
        SY_UP,
        Step3,
        NEXT_STEP,
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
                  
                    Step++;// = 인터락체크;
                    break;

                case InterLockCheck:                 
                    await _act.IO.doubleTypeOn(IO_OUT.TRAY_FEEDER_Front_Clamp_ForWard_ON);
                   
                    Step = SY_UP;
                    break;

                case SY_UP:
                    await Task.Delay(1000);
                    Step = Step3;
                    break;

                case Step3:
                    await Task.Delay(500);
                    Step = NEXT_STEP;
                    break;

                case NEXT_STEP:
                    await Task.Delay(1000);
                    _Step++;
                    break;

                case End:
                    _Step++; 
                    break;

                default:                   
                    _Status = SeqStatus.ERROR;
                    break;
            }
        }
    }
}
