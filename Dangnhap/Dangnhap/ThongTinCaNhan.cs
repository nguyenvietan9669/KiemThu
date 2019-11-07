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
            if (!newpass.Equals(nhaplai))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới !");
            }
            else
            {
                if (AccountDAO.Instance.UpdateAccount(userName, disPlayName, password, newpass))
                {
                    MessageBox.Show("Cập nhật thành công ");
                }
                else
                {
                    MessageBox.Show("vui lòng điền đúng mật khẩu ");
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
