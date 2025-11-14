using EQ.Core.Actions;
using EQ.Core.Sequence;
using EQ.Core.Service;
using EQ.Domain.Enums;
using EQ.Domain.Interface;
using static EQ.Core.Sequence.SEQ;
using static EQ.Infra.HW.IO.HardwareIOFactory;

namespace EQ.UI
{
    public partial class FormMain : FormBase
    {
        Point formMove = new Point();
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            var act = ActManager.Instance.Act;

            act.PopupNoti("시퀀스 시작", "Seq01이 시작되었습니다.", NotifyType.Info);
            act.PopupYesNo.Confirm(
                        "시퀀스 확인",
                        "A 실린더가 ON 되었습니다. 다음 스텝으로 진행할까요?",
                        NotifyType.Warning // 경고 타입
                    );
            act.IO.GetIoStatus();

            var t = act.Option.Option1.Chip_Tray_X;

            //    SeqManager.Instance.Seq.RunSequence(SeqName.Seq1_시나리오명);
            return;
            /*
            ACT act = new ACT();

            SEQ seq = new SEQ(act);
            seq.InitSequence();

            var x =seq.GetSequence(SEQ.SeqName.Seq14_시나리오명);

            act.Motion.HomeSearchAsync();

            act.IO.CylinderPushAsync();
            */




            SeqManager.Instance.Seq.RunSequence(SeqName.Seq1_시나리오명);
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {




            Form fm = new FormTest();
            fm.TopLevel = false;
            fm.Dock = DockStyle.Fill;
            fm.Show();

            panelMain.Controls.Add(fm);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //타이틀 영역을 이용해 메인폼 마우스로 이동 
            _Label_Title.MouseDown += (s, e1) =>
            {
                formMove = new Point(e1.X, e1.Y);
            };
            _Label_Title.MouseDoubleClick += (s, e1) =>
            {
                this.Location = new Point(0, 0);
            };
            _Label_Title.MouseMove += (s, e1) =>
            {
                if ((e1.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    this.Location = new Point(this.Left - (formMove.X - e1.X), this.Top - (formMove.Y - e1.Y));
                }
            };
        }
    }
}
