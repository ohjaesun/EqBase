using EQ.Core.Actions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Core.Sequence
{
    public enum SeqStatus { STOP, RUN, SEQ_STOPPING, ERROR, TIMEOUT, }
    public interface ISeqInterface
    {
        // SEQ 매니저가 사용할 속성
        SeqStatus _Status { get; set; }
        string _StepString { get; }
        int _Step { get; set; }
        int _StepMax { get; }

        // SEQ 매니저가 호출할 메서드
        Task doSequence();

        // UI 또는 데이터 표시용
        DataTable GetDataTable();
        ConcurrentDictionary<string, Stopwatch> _StepTime { get; }

        // 상태 제어용
        void _StepTimeAllStop();
        void _StepTimeClear();
    }

    public abstract class AbstractSeqBase<T> : ISeqInterface where T : Enum
    {
        // --- A. 의존성 주입 (공통) ---
        protected readonly SEQ _seq; // SEQ 매니저 접근용
        protected readonly ACT _act; // ACT 기능(Motion/IO) 접근용

        public AbstractSeqBase(SEQ seqManager, ACT actManager)
        {
            _seq = seqManager;
            _act = actManager;
            _Status = SeqStatus.STOP;
            _StepTimes = new ConcurrentDictionary<string, Stopwatch>();
        }

       
        public abstract Task doSequence();      

       
        public SeqStatus _Status { get; set; }

        protected int _StepIndex;
        public int _Step
        {
            get => _StepIndex;
            set
            {
                _StepIndex = value;
                Step = (T)(object)value; // Enum 타입도 함께 업데이트
            }
        }

        public string _StepString => ((T)(object)_StepIndex).ToString();

        public int _StepMax => Enum.GetNames(typeof(T)).Length;

        // --- D. 공통 스텝(Enum) 관리 기능 ---

        // (T StepEnum)
        public T Step
        {
            get => (T)(object)_StepIndex;
            set
            {
                _StepIndex = (int)(object)value; // 숫자 인덱스도 함께 업데이트

                // 스텝 변경 시 시간 측정기 재시작
                foreach (var p in _StepTimes)
                    p.Value.Stop();

                var name = ((T)(object)_StepIndex).ToString();
                if (_StepTimes.ContainsKey(name) == false)
                    _StepTimes.TryAdd(name, new Stopwatch());
            }
        }

        // --- E. 공통 시간 관리 기능 ---

        protected ConcurrentDictionary<string, Stopwatch> _StepTimes;
        public ConcurrentDictionary<string, Stopwatch> _StepTime => _StepTimes;

        public void _StepTimeAllStop()
        {
            foreach (var p in _StepTimes) p.Value.Stop();
        }

        public void _StepTimeClear()
        {
            foreach (var p in _StepTimes) p.Value.Reset();
        }

        // --- F. 공통 UI 데이터 기능 ---
        public DataTable GetDataTable()
        {
            var columnName = typeof(T).Name;
            DataTable dt = new DataTable();
            dt.Columns.Add(columnName);
            dt.Columns.Add("Elsp", typeof(string));
            foreach (var p in Enum.GetValues(typeof(T)))
            {
                dt.Rows.Add(p.ToString());
            }
            return dt;
        }
    }
}
