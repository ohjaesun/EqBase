using EQ.Common.Logs;
using EQ.Core.Service;
using EQ.Domain.Interface;
using System;
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
            if (lblStatus.InvokeRequired)
            {
                lblStatus.Invoke(new Action(() => lblStatus.Text = text));
            }
            else
            {
                lblStatus.Text = text;
            }
        }

        enum LoadStep
        {
            None ,
            LoadConfig ,
            InitHardware_IO ,
            InitHardware_Motion,
            Complete 
        }

        public async void StartProgram()
        {
            Log.Instance.Info("프로그램 시작");

            await Task.Run(async () => {
            try
            {
                    LoadStep step = LoadStep.None;
                    while (true)
                    {
                        switch (step)
                        {
                            case LoadStep.None:
                               

                                //SeqManager.Instance.Seq.RunSequence(SeqName.Seq1_시나리오명);
                                updateLable("환경설정 파일 로드 중...");
                                break;

                                case LoadStep.InitHardware_IO:
                                {
                                    updateLable("하드웨어 IO 초기화 중...");
                                    string currentHardwareIoType = "WMX"; // 또는 "Simulation"

                                    var act = ActManager.Instance.Act;

                                    // EQ.Infra의 팩토리를 호출하여 "실제" 하드웨어 인스턴스 생성
                                    IIoController mainIoController = IoFactory.CreateIoController(currentHardwareIoType);
                                    act.IO.SetHardwareController(mainIoController);                                   
                                }
                                   
                                break;

                            case LoadStep.InitHardware_Motion:
                                {
                                    updateLable("하드웨어 모터 초기화 중...");
                                    string currentHardwareIoType = "WMX"; // 또는 "Simulation"

                                    var act = ActManager.Instance.Act;                                  

                                    IMotionController mainMotionController = MotionFactory.CreateIoController(currentHardwareIoType);
                                    act.Motion.SetHardwareController(mainMotionController);
                                }
                               
                                break;
                        }
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
