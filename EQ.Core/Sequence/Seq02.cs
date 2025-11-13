using EQ.Core.Actions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

using static EQ.Core.Sequence.seq02Step;
namespace EQ.Core.Sequence
{
    public enum seq02Step
    {
        Start,
        SEQ1_분기체크,
        Step2,
        Step3,
        Step4,
        End
    }

    public class Seq02 : AbstractSeqBase<seq02Step>
    {
        string className = (nameof(Seq01));

        // 1. 생성자: 부모(AbstractSeqBase)에게 ACT/SEQ 전달 (필수)
        public Seq02(SEQ seqManager, ACT actManager) : base(seqManager, actManager)
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

                case SEQ1_분기체크:
                    var seq1 = _seq.GetSequence(SEQ.SeqName.Seq1_시나리오명);
                   if(seq1._Step ==3)
                        Step++;
                    break;

                case Step2:
                    await Task.Delay(1000);
                    Step++;
                    break;

                case Step3:
                    await Task.Delay(1000);
                    Step++;
                    break;

                case Step4:
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
