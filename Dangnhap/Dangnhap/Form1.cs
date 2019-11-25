using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dangnhap.DAO;
using Dangnhap.DTO;
using System.Data.SqlClient;

namespace Dangnhap
{
    public partial class FDangNhap : Form
    {
        public string userName;
        public string passWord;        
        public FDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            userName = txtTen.Text;
            passWord = txtpass.Text;
            if (userName.Contains(" ")|| passWord.Contains(" "))
            {
                MessageBox.Show("có khoảng trắng trong tên tài khoản hoặc mật khẩu");
                return;
            }
            if (userName.Contains("!") || userName.Contains("@") || userName.Contains("#") || userName.Contains("$")
                || userName.Contains("%") || userName.Contains("^") || userName.Contains("&") || userName.Contains("*")
                || userName.Contains("(") || userName.Contains(")") || userName.Contains("_") || userName.Contains("+")
                || userName.Contains("-") || userName.Contains("+") || passWord.Contains("!") || passWord.Contains("@") || passWord.Contains("#") || passWord.Contains("$")
                || passWord.Contains("%") || passWord.Contains("^") || passWord.Contains("&") || passWord.Contains("*")
                || passWord.Contains("(") || passWord.Contains(")") || passWord.Contains("_") || passWord.Contains("+")
                || passWord.Contains("-") || passWord.Contains("+"))
            {
                MessageBox.Show("Có kí tự đặc biệt trong tên tài khoản hoặc mật khẩu");
                return;
            }
            if (userName.Length > 100 || passWord.Length > 100)
            {
                MessageBox.Show("Tên tài khoản hoặc mật khẩu quá dài");
                return;
            }
            if (userName.Length <= 0 || passWord.Length <= 0)
            {
                MessageBox.Show("Chưa điền tên tài khoản, mật khẩu !");
                return;
            }
                if (Login(userName,passWord))
                {
                    Account loginAccount = AccountDAO.Instance.GetAccountByUserName(userName);
                    QuanLy ql = new QuanLy(loginAccount);
                    this.Hide();
                    ql.ShowDialog();
                    this.Show();
                }
                    else
                {
                    MessageBox.Show("Sai tên tài khoản hoặc mật khẩu");
                }
        }
        bool Login(string userName, string passWord)
        {
            return AccountDAO.Instance.Login(userName, passWord);
        }
        private void FDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public bool isAccepted()
        {
            if (Login(userName, passWord))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            txtpass.UseSystemPasswordChar = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txtpass.UseSystemPasswordChar = true;
        }

      
    }
}
