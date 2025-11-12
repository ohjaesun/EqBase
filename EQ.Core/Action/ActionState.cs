using EQ.Core.Sequence;
using EQ.Common.Logs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Core.Actions
{
    public enum ActionStatus { Running, Error, Timeout, Finished };

    /// <summary>
    /// 개별 Action의 상태와 컨텍스트를 저장하는 클래스 (구 OP)
    /// </summary>
    public class ActionState
    {
        private ActionStatus _status;
        private string _Title;

        public ActionState(string title)
        {
            this._Title = title;
            this.Uid = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 6); 
            this.cancellatinSource = new CancellationTokenSource();
            this.sw = new Stopwatch();
            this.startTime = DateTime.Now;
            this.StepIndex = -1;

            CallSequenceName = SequenceContext.CurrentSequenceId.Value;

            // 시퀀스가 아닌 UI 등에서 직접 호출
            if (string.IsNullOrEmpty(CallSequenceName))
            {
                CallSequenceName = "Manual";
            }
        }

        public string Title => _Title;
        public string CallSequenceName { get; set; }
        public string Uid { get; private set; }

        private string _stepName;
        public string StepName
        {
            get => _stepName;
            set
            {
                if (string.IsNullOrEmpty(value) == false && _stepName != value)
                {
                    // DEPENDENCY: Log.Instance.Action(...);
                }
                _stepName = value;
            }
        }

        public CancellationTokenSource cancellatinSource { get; private set; }

        public ActionStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                if (value != ActionStatus.Running)
                {
                    endTime = sw.ElapsedMilliseconds;
                    sw.Stop();
                    // DEPENDENCY: TimeCheck.end(Title);
                    // DEPENDENCY: Log.Instance.Action($"End:{Title}...");
                    Log.Instance.Action($"Action '{Title}' {CallSequenceName} finished with status: {_status}, Duration: {endTime} ms");
                }
                else
                {
                    sw.Start(); // 상태가 Running으로 설정될 때 타이머 시작
                    // DEPENDENCY: Log.Instance.Action($"Start:{Title}...");
                }
            }
        }

        public DateTime startTime { get; private set; }
        public long endTime { get; set; }
        public Stopwatch sw { get; private set; }
        public int Timeout { get; set; }
        public int StepIndex { get; set; }
    }
}
