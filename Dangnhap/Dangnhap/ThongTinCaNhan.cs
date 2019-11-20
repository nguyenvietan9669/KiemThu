using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dangnhap.DTO;
using Dangnhap.DAO;

namespace Dangnhap
{
    public partial class ThongTinCaNhan : Form
    {
         
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount); }
        }
        public ThongTinCaNhan(Account acc )
        {
            InitializeComponent();
            LoginAccount = acc;
        }
        void ChangeAccount(Account acc)
        {
            txtTen.Text = LoginAccount.UserName;
            txtTentk.Text = LoginAccount.DisplayName;
        }
        void UpDateAccount()
        {
            string disPlayName = txtTentk.Text;
            string password = txtPass.Text;
            string newpass = txtMKmoi.Text;
            string nhaplai = txtNhaplai.Text;
            string userName = txtTen.Text;
            if (newpass.Contains("!") || newpass.Contains("@") || newpass.Contains("#") || newpass.Contains("$")
                || newpass.Contains("%") || newpass.Contains("^") || newpass.Contains("&") || newpass.Contains("*")
                || newpass.Contains("(") || newpass.Contains(")") || newpass.Contains("_") || newpass.Contains("+")
                || newpass.Contains("-") || newpass.Contains("+"))
            {
                MessageBox.Show("Không thể cập nhật,có ký tự đặc biệt trong mật khẩu");
                return;
            }
            if (newpass.Contains(" "))
            {
                MessageBox.Show(" không thể cập nhật,có khoảng trắng trong mật khẩu");
                return;
            }
            if (!newpass.Equals(nhaplai))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới !");
                return;
            }
                if (newpass.Length <= 0)
                {
                    MessageBox.Show("vui lòng nhập mật khẩu mới và nhập lại");
                    return;
                }
                if (nhaplai.Length <= 0)
                {
                    MessageBox.Show("vui lòng nhập lại mật khẩu");
                    return;
                }
                else
                {
                    if (AccountDAO.Instance.UpdateAccount(userName, disPlayName, password, newpass))
                    {
                        MessageBox.Show("Cập nhật mật khẩu thành công ");
                    }
                    else
                    {
                        MessageBox.Show("vui lòng điền đúng mật khẩu hiện tại ");
                    }
                }
        }

        private void btncapnhat_Click(object sender, EventArgs e)
        {
             UpDateAccount();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            FDangNhap dn = new FDangNhap();
        }

     
    }
}
