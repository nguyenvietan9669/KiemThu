using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using Dangnhap.DAO;
using Dangnhap.DTO;

namespace Dangnhap
{
    public partial class QuanLy : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value;  }
        }
        public QuanLy(Account acc)
        {
            InitializeComponent();
            this.loginAccount = acc;
            LoadTable();
            loadCategory();
            LoadComboBoxTable(cbChuyenBan);
            int type = LoginAccount.Type;
            if (type == 1)
            {
                adminToolStripMenuItem.Enabled = true;
            }
            if(type == 0)
            {
                adminToolStripMenuItem.Enabled = false;
            }

            thôngTinCáNhânToolStripMenuItem1.Text += "(" + LoginAccount.DisplayName + ")";
        }

        public QuanLy()
        {
            // TODO: Complete member initialization
        }

      

        public static int TableWidth = 80;
        public static int TableHeight = 80;
        #region Method
        void loadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbDanhMuc.DataSource = listCategory;
            cbDanhMuc.DisplayMember = "name";
        }
        void LoadFoodByCatagoryID(int id )
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbMonAn.DataSource = listFood;
            cbMonAn.DisplayMember = "name";
        }
        void LoadTable()
        {
          flPDsBan.Controls.Clear();
          List<Table> TableList = TableDAO.Instance.LoadTableList();
          foreach (Table item in TableList)
          {
              Button btn = new Button() { Width = TableWidth, Height = TableHeight };
              btn.Text = item.Name + Environment.NewLine + item.Status;
              btn.Click += btn_Click;
              btn.Tag = item;
              switch (item.Status)
              {
                  case "Trống":
                      btn.BackColor = Color.Aqua;
                      break;
                  default:
                      btn.BackColor = Color.LightPink;
                      break;
              }
              flPDsBan.Controls.Add(btn);
          }
        }
        void ShowBill(int id)
        {
            lsWMonAn.Items.Clear();
            List<MENU> ListBillinfo = MENUDAO.Instance.GetListMenuByTable(id);
            float totalPrice = 0;
            foreach (MENU item in ListBillinfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lsWMonAn.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            txttotalPrice.Text = totalPrice.ToString("c",culture);
            LoadTable();
        }
        void LoadComboBoxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "Name";
        }
        #endregion 
        #region even 
        void btn_Click(object sender, EventArgs e)
        {
            int tableid= ((sender as Button).Tag as Table).ID;
            lsWMonAn.Tag = (sender as Button).Tag;
            ShowBill(tableid);
        }
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            FDangNhap dn = new FDangNhap();
            dn.Show();
        }

        private void thôngTinCáNhânToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ThongTinCaNhan tt = new ThongTinCaNhan(LoginAccount);
            this.Hide();
            tt.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin ad = new Admin();
            ad.loginAccount = loginAccount;
            ad.InsertFood += ad_InsertFood;
            ad.DeleteFood += ad_DeleteFood;
            ad.UpdateFood += ad_UpdateFood;
            ad.ShowDialog();
        }
        private void cbDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;
            Category selected = cb.SelectedItem as Category;
            id = selected.ID;
            LoadFoodByCatagoryID(id);
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            Table table = lsWMonAn.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Mời chọn bàn");
                return;
            }
            int idBill = BillDAO.Instance.GetUnBillByTableID(table.ID);
            int foodID=(cbMonAn.SelectedItem as Food).ID;
            int Count = (int)nmBSoMon.Value;
            if (Count < 0)
            {
                MessageBox.Show("Số món không hợp lệ");
                return;
            }
            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillinfoDAO.Instance.InsertBillinfo(BillDAO.Instance.GetMaxIDBill(), foodID, Count);
            }
            else 
            {
                BillinfoDAO.Instance.InsertBillinfo(idBill, foodID, Count);
            }
            ShowBill(table.ID);
            LoadTable();
        }
        public bool ThemMon(int idbill, int foodid,int idtable, int somon)
        {
            {
                if (idtable < 0)
                {
                    return false;
                }
                if (somon <= 0)
                {
                    return false;
                }
                int idBill = idbill;
                int foodID = foodid;
                int Count = somon;
                if (idBill == -1)
                {
                    BillDAO.Instance.InsertBill(idtable);
                    //BillinfoDAO.Instance.InsertBillinfo(BillDAO.Instance.GetMaxIDBill(), foodID, Count);
                    return true;
                }
                else
                {
                    //BillinfoDAO.Instance.InsertBillinfo(idBill, foodID, Count);
                    return true;
                }
            }
            return false;
        }
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
                Table table = lsWMonAn.Tag as Table;
                int idBill = BillDAO.Instance.GetUnBillByTableID(table.ID);
                int discount = (int)nmDGiamGia.Value;
                double totalPrice = Convert.ToDouble(txttotalPrice.Text.Split(',')[0]);
                double final = totalPrice - (totalPrice / 100) * discount;
                final = final * 1000;
                CultureInfo culture = new CultureInfo("vi-VN");
                if (final <= 0||discount <0)
                {
                    MessageBox.Show("giá trị nhập vào chưa đúng mời nhập lại");
                    return;
                }
                else
                    if (idBill != -1)
                    {
                        if (MessageBox.Show("Bạn có chắc chắn muốn thanh toán cho " + table.Name, "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                        {
                            MessageBox.Show("Tổng tiền của bàn " + table.Name + "Là :" + final + " (VNĐ)");
                            BillDAO.Instance.CheckOut(idBill, discount, (float)final);
                            ShowBill(table.ID);
                            LoadTable();
                        }
            }
        }
        public bool ThanhCong(int id, int dis, double fl)
        {
                int idBill = id;
                int discount = dis;
                double totalPrice = fl;
                double final = totalPrice - (totalPrice / 100) * discount;
                final = final * 1000;
                if (final <= 0 || discount < 0)
                {
                    return false;
                }
                else
                {
                    if (idBill != -1)
                    {
                        BillDAO.Instance.CheckOut(idBill, discount, (float)final);
                        return true;
                    }
                    return false;
                }
                
        }
        #endregion


        private void btnChuyenBan_Click(object sender, EventArgs e)
        {
           
            int table1 = (lsWMonAn.Tag as Table).ID;
            int table2 = (cbChuyenBan.SelectedItem as Table).ID;
            if (MessageBox.Show(String.Format("Bạn có thực sự muốn chuyển từ {0} qua {1}", (lsWMonAn.Tag as Table).Name, (cbChuyenBan.SelectedItem as Table).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(table1, table2);
                LoadTable();
            }
        }
        public bool thuchuyen(int ban1,int ban2)
        {
            int table1 = ban1;
            int table2 = ban2;
            if (table1 != null && table2 != null)
            {
                TableDAO.Instance.SwitchTable(table1, table2);
                //LoadTable();
                return true;
            }
            return false;
        }

        void ad_InsertFood(object sender, EventArgs e)
        {
            LoadFoodByCatagoryID((cbDanhMuc.SelectedItem as Category).ID);
            if (lsWMonAn.Tag != null)
            {
                ShowBill((lsWMonAn.Tag as Table).ID);
            }
        }
        void ad_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodByCatagoryID((cbDanhMuc.SelectedItem as Category).ID);
            if (lsWMonAn.Tag != null)
            {
                ShowBill((lsWMonAn.Tag as Table).ID);
            }
            LoadTable();
        }

        void ad_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodByCatagoryID((cbDanhMuc.SelectedItem as Category).ID);
            if (lsWMonAn.Tag != null)
            {
                ShowBill((lsWMonAn.Tag as Table).ID);
            }
        }

        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnThanhToan_Click(this, new EventArgs());
        }

        private void thêmMónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnThemMon_Click(this, new EventArgs());
        }

        private void chuyểnBànToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnChuyenBan_Click(this, new EventArgs());
        }
    }
}
