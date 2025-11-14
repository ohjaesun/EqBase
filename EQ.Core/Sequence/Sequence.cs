using EQ.Core.Actions;
using EQ.Common.Logs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Core.Sequence
{
    public partial class SEQ
    {
        public enum SeqName
        {
            Seq1_시나리오명, //Seq1.cs .. to Seq[N]
            Seq2_시나리오명,
            Seq3_시나리오명,
            Seq4_시나리오명,
            Seq5_시나리오명,
            Seq6_시나리오명,
            Seq7_시나리오명,
            Seq8_시나리오명,
            Seq9_MotorAllHome,
            Seq10_시나리오명,
            Seq11_시나리오명,
            Seq12_시나리오명,
            Seq13_시나리오명,
            Seq14_시나리오명,
            Seq15_시나리오명,
        }

        private readonly ACT _act;
        private ConcurrentDictionary<SeqName, ISeqInterface> dicSeq = new ConcurrentDictionary<SeqName, ISeqInterface>();
       
        public SEQ(ACT act)
        {
            _act = act;
        }

        /// <summary>
        /// 시퀀스 추가 부분
        /// </summary>
        private ISeqInterface s1;
        private ISeqInterface s2;

        public void InitSequence()
        {           
            s1 = new Seq01(this, _act);

            // 딕셔너리에 연결
            dicSeq.TryAdd(SeqName.Seq1_시나리오명, s1);            
        }

        public ISeqInterface GetSequence(SeqName name)
        {
            if (dicSeq.TryGetValue(name, out ISeqInterface seq))
            {
                return seq;
            }
            
            Log.Instance.Error($"등록되지 않은 시퀀스 요청: {name}");
            
            return null; // 못 찾으면 null 반환
        }

        public void RunSequence(SeqName seqName)
        {

            var p = dicSeq[seqName] ;

            if (p._Status == SeqStatus.STOP)
            {
             

                p._Status = SeqStatus.RUN;
                p._Step = 0;

                //기존 stepTime init
                foreach (var pp in p._StepTime)
                    pp.Value.Reset();


                Task x = Task.Run(async () =>
                {
                    SequenceContext.CurrentSequenceId.Value = seqName.ToString();

                    try
                    {
                        var old_step = -1;

                        while (p._Status == SeqStatus.RUN || p._Status == SeqStatus.SEQ_STOPPING)
                        {
                            //One Cycle Time
                            if (p._Step == 0)
                            {
                                if (p._Status == SeqStatus.SEQ_STOPPING) // SEQ_STOP 버튼 눌렀을 때 
                                {
                                    p._Status = SeqStatus.STOP;
                                    continue;
                                }
                            }

                            if (p._StepMax <= p._Step)
                            {
                                p._Status = SeqStatus.STOP;
                                break;
                            }

                            if (old_step != p._Step)
                            {
                                old_step = p._Step;
                                Log.Instance.Sequence($"Seq,{seqName},Step:[{p._StepString}]");

                                p._StepTime[p._StepString].Restart();
                            }


                            await p.doSequence();
                            //p.doSequence().Wait();

                            //One Cycle Time
                            if (p._StepMax - 1 == old_step)
                            {

                                if (p._Status == SeqStatus.SEQ_STOPPING) // SEQ_STOP 버튼 눌렀을 때
                                    p._Status = SeqStatus.STOP;
                            }

                        }

                        if (p._Status == SeqStatus.STOP) // STOP 버튼 , 시퀀스 끝등 정상 종료
                        {
                            p._StepTimeAllStop();
                            Log.Instance.Sequence($"Seq,{seqName},Status:[{p._Status}]");
                            p._Status = SeqStatus.STOP;
                         
                        }
                        else // 에러 종료
                        {
                            p._StepTimeAllStop();
                            Log.Instance.Error($"Seq: {seqName} Status : {p._Status}");
                            Log.Instance.Sequence($"Seq,{seqName},Step,{p._StepString},Status:[{p._Status}]");
                          
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"Seq,{seqName},Run Start Exception : {ex.Message}");
                    }
                    finally
                    {
                        SequenceContext.CurrentSequenceId.Value = null;
                    }


                 //   Log.Instance.Sequence($"Seq,{seqName},Status,{p._Status}");                   

                });
            }
            else
            {
                Log.Instance.Sequence($"Seq,{seqName},can not Run -> Status,{p._Status}");
            }
        }

        // ... (lstType, dicSeq, SeqTitle 필드) ...

        // ... (GetStatus, GetSeqStatus, SetAllStatusStop 등 모든 나머지 코드는 수정 없이 그대로) ...

    }
}
