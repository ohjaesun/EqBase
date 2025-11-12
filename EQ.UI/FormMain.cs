using EQ.Core.Actions;
using EQ.Core.Sequence;
using EQ.Core.Service;
using EQ.Domain.Interface;
using static EQ.Core.Sequence.SEQ;
using static EQ.Infra.HW.IO.HardwareIOFactory;

namespace EQ.UI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            ACT act = new ACT();

            SEQ seq = new SEQ(act);
            seq.InitSequence();

            var x =seq.GetSequence(SEQ.SeqName.Seq14_시나리오명);

            act.Motion.HomeSearchAsync();

            act.IO.CylinderPushAsync();
            */
            string currentHardwareIoType = "WMX"; // 또는 "Simulation"

            // 2. 🔌 EQ.Infra의 팩토리를 호출하여 "실제" 하드웨어 인스턴스 생성
            IIoController mainIoController = IoFactory.CreateIoController(currentHardwareIoType);
            var act = ActManager.Instance.Act;
            act.IO.SetHardwareController(mainIoController);
            
            SeqManager.Instance.Seq.RunSequence(SeqName.Seq1_시나리오명);
        }
    }
}
