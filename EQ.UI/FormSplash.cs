using EQ.Common.Logs;
using EQ.Core.Actions;
using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Interface;
using EQ.Infra.Storage;
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

                                act.Recipe.Initialize("");

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

                                /*
                                //사용 예제

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
        public void EndProgram()
        {
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
