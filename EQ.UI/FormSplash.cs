using EQ.Common.Logs;
using EQ.Core.Actions;
using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Interface;
using EQ.Infra.Storage;
using EQ.UI.Services;
using SQLitePCL;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using static EQ.Core.Sequence.SEQ;
using static EQ.Infra.HW.IO.HardwareIOFactory;
using static EQ.Infra.HW.Motion.HardwareMotionFactory;

namespace EQ.UI
{
    public partial class FormSplash : FormBase
    {
        public FormSplash()
        {
            InitializeComponent();
        }

        private void FormSplash_Shown(object sender, EventArgs e)
        {
            StartProgram();
        }

        private void updateLable(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action(() => richTextBox1.AppendText(text) ));
            }
            else
            {
                lblStatus.Text = text;
            }
        }

        enum LoadStep
        {
            Recipe ,            
            LoadUserOption,
            LoadConfig ,
            InitHardware_IO ,
            InitHardware_Motion,
            Complete 
        }

        public async void StartProgram()
        {
            Log.Instance.Info("프로그램 시작");

            var act = ActManager.Instance.Act;
            // 1. (신규) 단방향 알람은 'HandleCoreNotification' 메서드로 연결
            act.OnNotificationRequest += HandleCoreNotification;

            // 2. (신규) 양방향(Yes/No) 확인 서비스 주입
            act.RegisterConfirmationService(new UIConfirmationService());

            await Task.Run(async () => {
            try
            {
                    LoadStep step = LoadStep.Recipe;
                    Stopwatch sw = new Stopwatch();
                    int _length = Enum.GetNames(typeof(LoadStep)).Length;
                    while (true)
                    {
                        sw.Restart();
                        updateLable($"[{(int)step+1}/{_length}]\t{step} 로드 중...");

                        switch (step)
                        {
                            case LoadStep.Recipe:

                                Batteries.Init();
                                act.Recipe.Initialize();

                                IDataStorage<UserOption1> userOptionStorage1 = new DualStorage<UserOption1>
                                (
                                    new JsonFileStorage<UserOption1>(),
                                    new SqliteStorage<UserOption1>()
                                );
                                IDataStorage<UserOption2> userOptionStorage2 = new DualStorage<UserOption2>
                                (
                                    new JsonFileStorage<UserOption2>(),
                                    new SqliteStorage<UserOption2>()
                                );
                                IDataStorage<UserOption3> userOptionStorage3 = new DualStorage<UserOption3>
                                (
                                    new JsonFileStorage<UserOption3>(),
                                    new SqliteStorage<UserOption3>()
                                );
                                IDataStorage<UserOption4> userOptionStorage4 = new DualStorage<UserOption4>
                                (
                                    new JsonFileStorage<UserOption4>(),
                                    new SqliteStorage<UserOption4>()
                                );
                                IDataStorage<UserOptionUI> userOptionStorageUI = new DualStorage<UserOptionUI>
                               (
                                   new JsonFileStorage<UserOptionUI>(),
                                   new SqliteStorage<UserOptionUI>()
                               );

                                act.Option.RegisterStorageService(userOptionStorage1);
                                act.Option.RegisterStorageService(userOptionStorage2);
                                act.Option.RegisterStorageService(userOptionStorage3);
                                act.Option.RegisterStorageService(userOptionStorage4);
                                act.Option.RegisterStorageService(userOptionStorageUI);

                                act.Option.LoadAllOptionsFromStorage();

                                //Old DB 삭제
                                // 1. 정리할 대상(T)의 SqliteStorage 인스턴스를 생성합니다.
                                var userOption1Storage = new SqliteStorage<UserOption1>();
                                var userOption2Storage = new SqliteStorage<UserOption2>();
                                var userOption3Storage = new SqliteStorage<UserOption3>();
                                var userOption4Storage = new SqliteStorage<UserOption4>();
                                var userOptionUIStorage = new SqliteStorage<UserOptionUI>();

                                // 2. 현재 레시피 경로를 가져옵니다.
                                string currentRecipePath = act.Recipe.GetCurrentRecipePath();

                                // 3. 키(key)를 지정하여 삭제 명령을 호출합니다.
                                // (키 이름은 ActUserOption.cs의 GetStorageKey 로직을 따릅니다)
                                string key1 = nameof(UserOption1);
                                string key2 = nameof(UserOption2);
                                string key3 = nameof(UserOption3);
                                string key4 = nameof(UserOption4);
                                string keyUI = nameof(UserOptionUI);
                                                                
                                userOption1Storage.DeleteOldBackups(currentRecipePath, key1);
                                userOption2Storage.DeleteOldBackups(currentRecipePath, key2);
                                userOption3Storage.DeleteOldBackups(currentRecipePath, key3);
                                userOption4Storage.DeleteOldBackups(currentRecipePath, key4);
                                userOptionUIStorage.DeleteOldBackups(currentRecipePath, keyUI);




                               

                                /*
                                //사용 예제

                                act.Option.Save<UserOption1>();
                                act.Option.Save<UserOptionUI>();

                                //UserOption1 ~4 사용법 예제
                                act.Option.Option1.Chip_Tray_X = 1;
                                                                                               

                                //UI 타입 사용법 예제
                                List<UserOptionUI> uiSettings = act.Option.OptionUI; // 리스트에서 꺼내서 사용

                                var setting = act.Option.OptionUI.FirstOrDefault(x => x.name == "btn_Start"); // 이름으로 검색

                                // 이 방법이 가장 편리
                                int delay = act.Option.GetUIValueByName<int>("txt_Delay", 1000);
                                var str = act.Option.GetUIValueByName<string>("btn_Start");
                                */

                                //SeqManager.Instance.Seq.RunSequence(SeqName.Seq1_시나리오명);

                                break;

                                case LoadStep.InitHardware_IO:
                                {
                                   
                                    string currentHardwareIoType = "WMX"; // 또는 "Simulation"

                                    

                                    // EQ.Infra의 팩토리를 호출하여 "실제" 하드웨어 인스턴스 생성
                                    IIoController mainIoController = IoFactory.CreateIoController(currentHardwareIoType);
                                    act.IO.SetHardwareController(mainIoController);                                   
                                }
                                   
                                break;

                            case LoadStep.LoadUserOption:
                                {
                                   

                                   
                                                                        
                                   
                                }
                                break;

                            case LoadStep.InitHardware_Motion:
                                {
                                  
                                    string currentHardwareIoType = "WMX"; // 또는 "Simulation"

                                    var act = ActManager.Instance.Act;                                  

                                    IMotionController mainMotionController = MotionFactory.CreateIoController(currentHardwareIoType);
                                    act.Motion.SetHardwareController(mainMotionController);
                                }
                               
                                break;
                        }
                        updateLable($"  Done  Elsp:{sw.ElapsedMilliseconds}ms \n");
                        step++;
                       // enum 길이보다 크면 종료
                        if ((int)step >= Enum.GetNames(typeof(LoadStep)).Length)
                            break;
                    }


                updateLable("111");
                     await Task.Delay(10);

                    updateLable("222");
                    await Task.Delay(10);
                }
                catch (Exception ex)
                {
                   Log.Instance.Error(ex.ToString());
                    MessageBox.Show("프로그램 실행 중 오류가 발생하여 프로그램을 종료합니다.\n" + ex.Message, "프로그램 실행 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit(); // 프로그램 종료
                }
            });


            ShowMainForm();


        }

        /// <summary>
        /// 알림 팝업 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleCoreNotification(object? sender, NotifyEventArgs e)
        {
            // 백그라운드 스레드에서 호출될 수 있으므로 UI 스레드로 전환
            Action showAction = () =>
            {
                var popupGroup = new List<FormNotify>();

                foreach (Screen screen in Screen.AllScreens)
                {
                    FormNotify notifyForm = new FormNotify(e.Title, e.Message, e.Type);

                    // 팝업 위치를 모니터 중앙으로 설정
                    Rectangle bounds = screen.WorkingArea;
                    notifyForm.StartPosition = FormStartPosition.Manual;
                    notifyForm.Left = bounds.Left + (bounds.Width - notifyForm.Width) / 2;
                    notifyForm.Top = bounds.Top + (bounds.Height - notifyForm.Height) / 2;

                    popupGroup.Add(notifyForm);
                }

                // 모든 팝업에게 "그룹" 정보 전달
                foreach (var form in popupGroup)
                {
                    form.SetSiblingGroup(popupGroup);
                }

                // 모든 팝업 표시
                foreach (var form in popupGroup)
                {
                    form.Show();
                }
            };

            // UI 스레드에서 실행
            Form mainForm = Application.OpenForms.Cast<Form>().FirstOrDefault();
            if (mainForm != null && mainForm.InvokeRequired)
            {
                mainForm.Invoke(showAction);
            }
            else
            {
                showAction();
            }
        }

        public void EndProgram()
        {
            ActManager.Instance.Act.OnNotificationRequest -= HandleCoreNotification;
            Log.Instance.Info("프로그램 종료");
        }

        private void ShowMainForm()
        {
          
            FormMain mainForm = new FormMain();           
            mainForm.FormClosed += (s, args) => this.Close();           
            mainForm.Show();          
            this.Hide();
        }
    }
}
