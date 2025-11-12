using EQ.Common.Logs;
using System;

using System.Windows.Forms;

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
            None = 0,
            LoadConfig = 1,
            InitHardware = 2,
            Complete = 3
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
                                updateLable("환경설정 파일 로드 중...");
                                break;
                        }
                        step++;
                       // enum 길이보다 크면 종료
                        if ((int)step >= Enum.GetNames(typeof(LoadStep)).Length)
                            break;
                    }


                updateLable("111");
                     await Task.Delay(5000);

                    updateLable("222");
                    await Task.Delay(5000);
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
