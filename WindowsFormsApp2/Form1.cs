using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            CheckVersionAndUpdate();
        }

        private async Task<string> GetLatestVersionFromGitHub() //버전확인
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "https://raw.githubusercontent.com/woodlePlatForm/WindowsFormsApp2/Test/version.txt"; //테스트용 버전관리프로그램
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string version = await response.Content.ReadAsStringAsync();
                    return version.Trim();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("버전 확인 실패: " + ex.Message);
                    return null;
                }
            }
        }

        private async void CheckVersionAndUpdate()
        {
            string latestVersion = await GetLatestVersionFromGitHub(); // 위에서 만든 메서드
            string currentVersion = Application.ProductVersion;

            if (latestVersion == null)
                return;

            if (latestVersion != currentVersion)
            {
                DialogResult result = MessageBox.Show(
                    $"현재 버전({currentVersion})보다 최신 버전({latestVersion})이 존재합니다.\n업데이트 페이지로 이동할까요?",
                    "업데이트 안내",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(new ProcessStartInfo
                    {
                        FileName = "https://github.com/woodlePlatForm/WindowsFormsApp2/releases/latest",
                        UseShellExecute = true
                    });
                }

                Application.Exit(); // 실행 차단
            }
        }



        
    }
}
