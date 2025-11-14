using EQ.Domain.Entities;
using EQ.Infra.Storage;

namespace EQ.UI.UserViews
{
    public partial class DB_Export_View : UserControlBase
    {
        public DB_Export_View()
        {
            InitializeComponent();

        }

        private void _Button1_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "DB 파일 (*.db)|*.db|모든 파일 (*.*)|*.*";
                dialog.Title = "DB 파일을 선택하세요.";

                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
                {
                    string fullFilePath = dialog.FileName; // 예: "C:\MyProject\Data\recipe.json"

                    // 1. 파일의 상위 폴더 경로 가져오기
                    string directoryPath = Path.GetDirectoryName(fullFilePath); // "C:\MyProject\Data"

                    // 2. 상위 폴더 경로에서 마지막 폴더명만 추출
                    string folderName = Path.GetFileName(directoryPath); // "Data"

                    _Label1.Text = fullFilePath;

                }
            }
        }

        /// <summary>
        /// 유저 데이터에 대해 30일치 변경분은 백업 시켜 놓음
        /// 필요시 이를 추출하여 사용
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Button2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(_Label1.Text))
                return;

            var storageInstances = new dynamic[]
             {
                new SqliteStorage<UserOption1>(),
                new SqliteStorage<UserOption2>(),
                new SqliteStorage<UserOption3>(),
                new SqliteStorage<UserOption4>(),
                new SqliteStorage<UserOptionUI>()
             };

            var keys = new string[] { nameof(UserOption1), nameof(UserOption2), nameof(UserOption3), nameof(UserOption4), nameof(UserOptionUI) };


            string basePath = Path.GetDirectoryName(_Label1.Text);
            string historyPath = Path.Combine(basePath, "History");
            Directory.CreateDirectory(historyPath);

            for (int i = 0; i < keys.Length; i++)
            {
                dynamic currentStorage = storageInstances[i]; 
                string currentKey = keys[i];                  

                string exportFolder = Path.Combine(historyPath, currentKey);
                Directory.CreateDirectory(exportFolder);
                
                int count = currentStorage.ExportAllByKey(basePath, currentKey, exportFolder);
            }

            MessageBox.Show("복원 완료");

        }
    }
}
