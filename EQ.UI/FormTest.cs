using EQ.Core.Service;
using EQ.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EQ.Core.Sequence.SEQ;

namespace EQ.UI
{
    public partial class FormTest : FormBase
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private async void _Button1_Click(object sender, EventArgs e)
        {
            var act = ActManager.Instance.Act;

            act.PopupNoti("시퀀스 시작", "Seq01이 시작되었습니다.", NotifyType.Info);

            var r = await act.PopupYesNo.Confirm(
                        "시퀀스 확인",
                        "A 실린더가 ON 되었습니다. 다음 스텝으로 진행할까요?",
                        NotifyType.Error
                    );
            if (r == YesNoResult.Yes)
            {

            }
        }

        private void _Button2_Click(object sender, EventArgs e)
        {
            var act = ActManager.Instance.Act;
            SeqManager.Instance.Seq.RunSequence(SeqName.Seq1_시나리오명);

            var seq = SeqManager.Instance.Seq;
            //시퀀스의 현재 상태 가져오기
            var status = seq.GetSequence(SeqName.Seq1_시나리오명)._Status;
        }

        private void _Button3_Click(object sender, EventArgs e)
        {
            var act = ActManager.Instance.Act;
            act.IO.GetIoStatus();
        }

        private void _Button4_Click(object sender, EventArgs e)
        {
            var act = ActManager.Instance.Act;
            act.TowerLamp.SetState(EqState.Running);
        }
    }
}
