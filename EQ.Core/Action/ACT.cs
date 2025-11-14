
using EQ.Common.Logs;
using EQ.Core.Action;
using EQ.Domain.Enums;
using EQ.Domain.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Core.Actions
{
    /// <summary>
    /// (신규) 단방향 알림 이벤트용 데이터 클래스
    /// </summary>
    public class NotifyEventArgs : EventArgs
    {
        public string Title { get; }
        public string Message { get; }
        public NotifyType Type { get; }

        public NotifyEventArgs(string title, string message, NotifyType type)
        {
            Title = title;
            Message = message;
            Type = type;
        }
    }

    /// <summary>
    /// 모든 기능별 클래스(Motion, IO 등)가 상속받을 기본 클래스
    /// ACT 메인 인스턴스에 접근할 수 있게 해줍니다.
    /// </summary>
    public abstract class ActComponent
    {
        protected readonly ACT _act;

        public ActComponent(ACT act)
        {
            _act = act;
        }
    }

    public partial class ACT
    {

        // --- 컴포지션: 기능별 모듈 선언 ---

        /// <summary>
        /// 모터, 로봇 등 모션(Motion) 관련 기능을 제어합니다.
        /// </summary>
        public ActMotion Motion { get; private set; }
        /// <summary>
        /// 실린더, 센서 등 입출력(IO) 관련 기능을 제어합니다.
        /// </summary>
        public ActIO IO { get; private set; }

        public ActUserOption Option { get; private set; }
        public ActRecipe Recipe { get; private set; }
      
        public ActTowerLamp TowerLamp { get; private set; }

        public event EventHandler<NotifyEventArgs> OnNotificationRequest;
        public IConfirmationService PopupYesNo { get; private set; }
        public void RegisterConfirmationService(IConfirmationService service)
        {
            this.PopupYesNo = service;
        }
        /// <summary>
        /// UI 레이어에 알림 팝업을 요청합니다. (Fire and Forget)
        /// (Core는 UI를 모르므로 이벤트를 발생시킴)
        /// </summary>
        public void PopupNoti(string title, string message, NotifyType type)
        {
            // UI(FormSplash)에 구독자가 있으면 이벤트 발생
            OnNotificationRequest?.Invoke(this, new NotifyEventArgs(title, message, type));
        }


        public Action_Sample action_Sample { get; private set; } // 샘플 액션 추가

        public ACT()
        {
            // 'this'를 넘겨주어 모듈들이 ACT의 기능(ExecuteStepBasedAction)을 쓰게 함
            this.Motion = new ActMotion(this);
            this.IO = new ActIO(this);
            this.action_Sample = new Action_Sample(this); // 샘플 액션 초기화
            this.Option = new ActUserOption(this);
            this.Recipe = new ActRecipe(this);
            this.TowerLamp = new ActTowerLamp(this);// FSM이 TowerLamp를 사용하므로 TowerLamp를 먼저 생성
           
        }


        // 'static' 제거 (인스턴스 멤버로 변경)
        public ConcurrentDictionary<string, int> ACT_TimeOut = new ConcurrentDictionary<string, int>();
        public ConcurrentDictionary<string, ActionState> ACT_STATUS = new ConcurrentDictionary<string, ActionState>();

        public delegate void Msg(string msg);
        public event Msg OnMsg; // 'static' 제거

        #region Action 상태 관리 (구 static 메서드들)

        public ActionStatus GetActionStatus()
        {
            foreach (var item in this.ACT_STATUS)
            {
                if (item.Value.Status == ActionStatus.Timeout || item.Value.Status == ActionStatus.Error)
                    return ActionStatus.Error;
                if (item.Value.Status == ActionStatus.Running)
                    return ActionStatus.Running;
            }
            return ActionStatus.Finished;
        }

        public void SetActionError(List<string> errorSeq)
        {
            foreach (var s in this.ACT_STATUS.Values)
            {
                if (s.Status == ActionStatus.Running)
                {
                    if (errorSeq.Count <= 0 || errorSeq.Contains(s.CallSequenceName))
                    {
                        s.Status = ActionStatus.Error;
                    }
                }
            }
        }

        public void ActErrorReset()
        {
            foreach (var s in this.ACT_STATUS.Values)
            {
                if (s.Status == ActionStatus.Error || s.Status == ActionStatus.Timeout)
                {
                    s.Status = ActionStatus.Finished;
                }
            }
        }

        #endregion

        #region Action 생성 및 실행 (리팩토링 핵심)

        /// <summary>
        /// Action 상태 객체를 생성하고 등록합니다. (구 SetTitle)
        /// </summary>
        private ActionState CreateState(string title, string subTitle = "")
        {
            bool err = GetActionStatus() == ActionStatus.Error;
            if (err)
            {
               
            }

            ActionState state = new ActionState(title);
           

            // --- 타임아웃 로드 로직 (구 SetTitle) ---
            string iniSection = "Timeout";
            if (ACT_TimeOut.Count == 0)
            {
                
                // DEPENDENCY: CIni ini = new CIni("ActionTimeout"); ...
            }
            if (ACT_TimeOut.ContainsKey(state.Title) == false)
            {
                // DEPENDENCY: CIni ini = new CIni("ActionTimeout"); ...
                ACT_TimeOut.TryAdd(state.Title, 10); // 기본값 10
            }
            state.Timeout = ACT_TimeOut[state.Title];
            // --- ---

            if (!this.ACT_STATUS.TryAdd(state.Uid, state))
            {
                // DEPENDENCY: Log.Instance.Error($"Action 실행 실패: {state.Title}");
            }

            // DEPENDENCY: TimeCheck.start(state.Title.ToString());

            return state;
        }

        /// <summary>
        /// 비동기 작업을 타임아웃과 함께 실행합니다. (구 DoWork)
        /// </summary>
        private async Task DoWork(Func<Task> action, System.Action timeoutAction, TimeSpan timeout, CancellationTokenSource cancel)
        {
            if (cancel.IsCancellationRequested) return;

            var task = Task.Run(action, cancel.Token);
            var delayTask = Task.Delay(timeout, cancel.Token);

            var completedTask = await Task.WhenAny(task, delayTask);

            if (completedTask == delayTask)
            {
                // Timeout
                cancel.Cancel(); // 진행 중인 action 취소
                timeoutAction();
            }
            // 'task'가 먼저 완료되면 (정상 종료) 아무것도 하지 않음
        }

        /// <summary>
        /// (신규) Step 기반 Action 실행을 위한 템플릿 메서드
        /// </summary>
        /// <param name="title">Action 제목</param>
        /// <param name="stepNames">Step 이름 목록</param>
        /// <param name="stepLogic">
        /// [입력] (Context, 현재 Step이름), 
        /// [출력] (다음 Step의 인덱스)를 반환하는 로직
        /// </param>
        /// <returns></returns>
        public async Task<ActionStatus> ExecuteAction(string title, List<string> stepNames, Func<ActionState, string, Task<int>> stepLogic)
        {
            // 1. Context 생성 (구 SetTitle)
            var context = CreateState(title);
            if (context.Status != ActionStatus.Running) return context.Status;

            int currentStepIndex = 0;

            // 2. DoWork를 사용한 실행 (타임아웃 래퍼)
            await DoWork(
                async () => // 실행할 Action (State Machine)
                {
                    while (context.Status == ActionStatus.Running)
                    {
                        if (context.cancellatinSource.IsCancellationRequested) return;

                        if (currentStepIndex < 0 || currentStepIndex >= stepNames.Count)
                        {
                            // DEPENDENCY: Alarm.setAlarm(ErrorList.ID.SEQUENCE_NOT_DEFINE, ...
                            context.Status = ActionStatus.Error;
                            return;
                        }

                        // 현재 Step 정보 업데이트
                        string currentStepName = stepNames[currentStepIndex];
                        context.StepName = currentStepName;
                        context.StepIndex = currentStepIndex;

                        // 3. 외부에서 주입된 Step 로직 실행
                        int nextStepIndex = await stepLogic(context, currentStepName);

                        if (context.Status != ActionStatus.Running) return; // 로직이 상태를 변경함 (Finished, Error)

                        // 4. 다음 Step으로 이동
                        currentStepIndex = nextStepIndex;
                    }
                },
                () => // 타임아웃 시 실행할 Action
                {
                    context.Status = ActionStatus.Timeout;
                    // DEPENDENCY: Alarm.setAlarm(ErrorList.ID.ACT_TIMEOUT, $"{context.Title} ...
                },
                TimeSpan.FromSeconds(context.Timeout), // 타임아웃 시간
                context.cancellatinSource // 취소 토큰
            );


            //실행 결과
            if (context.Status == ActionStatus.Error || context.Status == ActionStatus.Timeout)
            {
                
                Log.Instance.Error($"Action {context.Title} ended with status: {context.Status}");
                // 에러 알람
                // (stepLogic 람다에서 context.Status = Error로 설정한 경우)
                // DEPENDENCY: Alarm.setAlarm(ErrorList.ID.SEQUENCE_NOT_DEFINE, $"{context.Title} Error at step {context.StepName}");
            }

            return context.Status;
        }

        #endregion
    }
}
