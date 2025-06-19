using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        }

        private async Task<string> GetLatestVersionFromGitHub() //버전확인
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "https://raw.githubusercontent.com/사용자명/저장소명/브랜치명/version.txt";
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
    }
}
