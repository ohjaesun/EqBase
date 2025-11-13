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
            var act = ActManager.Instance.Act;
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
    }
}
