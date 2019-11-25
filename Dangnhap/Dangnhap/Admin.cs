using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Dangnhap.DAO;
using Dangnhap.DTO;


namespace Dangnhap
{
    public partial class Admin : Form
    {
        BindingSource foodlist = new BindingSource();
        BindingSource accountList = new BindingSource();
        BindingSource tablelist = new BindingSource();
        BindingSource categorylist = new BindingSource();
        public Account loginAccount;
        public Admin()
        {
            InitializeComponent();
            load();
        }
        void load()
        {
            dtgvMonAn.DataSource = foodlist;
            dtgvTk.DataSource = accountList;
            dtgvBan.DataSource = tablelist;
            dtgvDM.DataSource = categorylist;
            LoadListBillByDate(dtpkOpen.Value, dtpkOver.Value);
            LoadListFood();
            LoadAccount();
            LoadListTable();
            LoadListCategory();
            AddFoodBinDing();
            addAcounBinding();
            AddTableBinDing();
            AddCategoryBinDing();
            LoadCategoryIntoComboBox(cbDanhMuc);
        }
        void addAcounBinding()
        {
            txtTenTK.DataBindings.Add(new Binding("Text", dtgvTk.DataSource, "userName",true,DataSourceUpdateMode.Never));
            txtTenHT.DataBindings.Add(new Binding("Text", dtgvTk.DataSource, "displayName", true, DataSourceUpdateMode.Never));
            nmBLTK.DataBindings.Add(new Binding("Text", dtgvTk.DataSource, "type", true, DataSourceUpdateMode.Never));
        }
        void loadDatetimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkOpen.Value = new DateTime(today.Year, today.Month, 1);
            dtpkOver.Value = dtpkOpen.Value.AddMonths(1).AddDays(-1);
        }
        void LoadListBillByDate(DateTime CheckIn, DateTime CheckOut)
        {
            dtgvBill.DataSource= BillDAO.Instance.GetBillListByDate(CheckIn, CheckOut);
        }
        List<Food> SearchFoodByName(string name)
        {
            List<Food> listfood = FoodDAO.Instance.SearchFoodByName(name);
            return listfood;
        }
        private void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkOpen.Value, dtpkOver.Value);
        }
        void AddFoodBinDing()
        {
            txtTenMon.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txtID.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmBGia.DataBindings.Add(new Binding("value", dtgvMonAn.DataSource, "price", true, DataSourceUpdateMode.Never));
        }
        void AddTableBinDing()
        {
            txtTenBan.DataBindings.Add(new Binding("Text", dtgvBan.DataSource, "name", true, DataSourceUpdateMode.Never));
            txtIDBan.DataBindings.Add(new Binding("Text", dtgvBan.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtstatus.DataBindings.Add(new Binding("Text", dtgvBan.DataSource, "status", true, DataSourceUpdateMode.Never));
        }
        void AddCategoryBinDing()
        {
            txtTenDM.DataBindings.Add(new Binding("Text", dtgvDM.DataSource, "name", true, DataSourceUpdateMode.Never));
            txtIDDM.DataBindings.Add(new Binding("Text", dtgvDM.DataSource, "ID", true, DataSourceUpdateMode.Never));
        }
        void LoadCategoryIntoComboBox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }
        void LoadListFood()
        {
            foodlist.DataSource = FoodDAO.Instance.GetListFood();
        }
        void LoadListTable()
        {
            tablelist.DataSource = TableDAO.Instance.GetListTable();
        }
        void LoadListCategory()
        {
            categorylist.DataSource = CategoryDAO.Instance.GetListDanhMuc();
        }
        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvMonAn.SelectedCells.Count > 0)
                {
                    int id = (int)dtgvMonAn.SelectedCells[0].OwningRow.Cells["categoryid"].Value;

                    Category category = CategoryDAO.Instance.GetCategoryById(id);

                    cbDanhMuc.SelectedItem = category;
                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbDanhMuc.Items)
                    {
                        if (item.ID == category.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }
                    cbDanhMuc.SelectedItem = index;
                }
            }
            catch { }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string name = txtTenMon.Text;
            int categoryID = (cbDanhMuc.SelectedItem as Category).ID;
            float price = (float)nmBGia.Value;
            if (name.Length > 200)
            {
                MessageBox.Show("Tên món ăn quá dài không thể thêm !!");
                return;
            }
            if (name.Length <= 0)
            {
                MessageBox.Show("Chưa điền tên món ăn, không thể thêm !!");
                return;
            }
            if (price > 10000000000000 || price <= 0)
            {
                MessageBox.Show("giá tiền không hợp lệ,không thể thêm !!");
                return;
            }
            if (check() == false)
            {
                MessageBox.Show("Món ăn đã tồn tại ");
                return;
            }
            if (FoodDAO.Instance.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm món " + name + " với giá " + price.ToString() + "(Đ) thành công ");
                LoadListFood();
                if (insertFood != null)
                {
                    insertFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm");
            }
        }
        public bool Them(string ten, int categoryid, float gia)
        {
            string name = ten;
            int categoryID = categoryid;
            float price = gia;
            {
                if (FoodDAO.Instance.InsertFood(name, categoryID, price))
                {
                    LoadListFood();
                    if (insertFood != null)
                    {
                        insertFood(this, new EventArgs());
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string name = txtTenMon.Text;
            int categoryID = (cbDanhMuc.SelectedItem as Category).ID;
            float price = (float)nmBGia.Value;
            int id = Convert.ToInt32(txtID.Text);
            if (name.Length > 200)
            {
                MessageBox.Show("Tên món ăn quá dài không thể sửa");
                return;
            }
            if (checkUpdate() == false)
            {
                MessageBox.Show("Tên món đã tồn tại, Mời nhập tên khác");
                return;
            }
            if (FoodDAO.Instance.UpdateFood(id,name, categoryID, price))
            {
                MessageBox.Show("Sửa món thành công");
                LoadListFood();
                if (updateFood != null)
                {
                    updateFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa");
            }
        }
        public bool sua(string Ten,int loai,float Gia,int ID)
        {
            string name = Ten;
            int categoryID = loai;
            float price = Gia;
            int id = ID;
            if (FoodDAO.Instance.UpdateFood(id, name, categoryID, price))
            {
                LoadListFood();
                if (updateFood != null)
                {
                    updateFood(this, new EventArgs());
                }
                return true;
            }
            else
            {
                return false;
            }
         
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            
            int id = Convert.ToInt32(txtID.Text);
            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Bạn có chắc chắn muốn xóa món này");
                LoadListFood();
                MessageBox.Show("Đã xóa");
                if (deleteFood != null)
                {
                    deleteFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi Xóa");
            }
        }
        public bool xoa(int iD)
        {
            int id = iD;
            if(id == null || id == ' ')
            {
                return false;
            }
            if (FoodDAO.Instance.DeleteFood(id))
            {
                LoadListFood();
                if (deleteFood != null)
                {
                    deleteFood(this, new EventArgs());
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }
        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }
        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
           foodlist.DataSource = SearchFoodByName(txtSearch.Text);
        }

        private void btnXemt_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }
        void AddAccount(string userName,string displayName,int type)
        {
            if (AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm thành công");
            }
            else
            {
                MessageBox.Show("Thất Bại");
            }
            LoadAccount();
        }
        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show("Sửa thành công");
            }
            else
            {
                MessageBox.Show("Thất Bại");
            }
            LoadAccount();
        }
        void DeleteAccount(string userName)
        {
            if (loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Tài khoản đang hoạt động không thể xóa");
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Bạn có thực sự muốn xóa");
                MessageBox.Show("Xóa thành công");
            }
            else
            {
                MessageBox.Show("Thất Bại");
            }
            LoadAccount();
        }
        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetAccount(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Thất Bại");
            }
        }
        private void btnThemT_Click(object sender, EventArgs e)
        {
            string userName = txtTenTK.Text;
            string displayName = txtTenHT.Text;
            int type = (int)nmBLTK.Value;
            if (userName.Contains("!") || userName.Contains("@") || userName.Contains("#") || userName.Contains("$")
                || userName.Contains("%") || userName.Contains("^") || userName.Contains("&") || userName.Contains("*")
                || userName.Contains("(") || userName.Contains(")") || userName.Contains("_") || userName.Contains("+")
                || userName.Contains("-") || userName.Contains("+"))
            {
                MessageBox.Show("Tên tài khoản có ký tự đặc biệt không thể thêm!!");
                return;
            }
            if (userName.Length > 100)
            {
                MessageBox.Show("Tên tài khoản quá dài không thể thêm ");
                return;
            }
            if (userName.Contains(" "))
            {
                MessageBox.Show("Tên tài khoản có khoảng trắng không thể thêm !!");
                return;
            }
            AddAccount(userName, displayName, type);
        }

        private void btnXoaT_Click(object sender, EventArgs e)
        {
            string userName = txtTenTK.Text;
            DeleteAccount(userName);      
        }

        private void btnSuaT_Click(object sender, EventArgs e)
        {
            string userName = txtTenTK.Text;
            if (userName.Contains("!") || userName.Contains("@") || userName.Contains("#") || userName.Contains("$")
                || userName.Contains("%") || userName.Contains("^") || userName.Contains("&") || userName.Contains("*")
                || userName.Contains("(") || userName.Contains(")") || userName.Contains("_") || userName.Contains("+")
                || userName.Contains("-") || userName.Contains("+"))
            {
                MessageBox.Show("trong tên tài khoản có ký tự đặc biệt,không thể sửa");
                return;
            }
            if (userName.Length > 100)
            {
                MessageBox.Show("Tên tài khoản quá dài không thể thêm ");
                return;
            }
            if (userName.Contains(" "))
            {
                MessageBox.Show("trong tên tài khoản có khoảng trống,không thể sửa");
                return;
            }
            string displayName = txtTenHT.Text;
            int type = (int)nmBLTK.Value;
            EditAccount(userName, displayName, type);
        }

        private void btnXemTk_Click(object sender, EventArgs e)
        {
            string userName = txtTenTK.Text;
            ResetPass(userName);
        }

        private void btnXemb_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }

        private void btnSuad_Click(object sender, EventArgs e)
        {
            string name = txtTenDM.Text;
            int id = Convert.ToInt32(txtIDDM.Text);
            if (CategoryDAO.Instance.UpdateCategory(name, id))
            {
                MessageBox.Show("Sửa danh mục thành công");
                LoadListCategory();
            }
            else
            {
                MessageBox.Show("Có lỗi khi Sửa");
            }
        }
        private bool check()
        {
            foodlist.DataSource = FoodDAO.Instance.SearchFoodByNametrue(txtTenMon.Text);
            if (dtgvMonAn.RowCount > 0)
            {
                return false;
            }
            return true;
        }
        private bool checkUpdate()
        {
            foodlist.DataSource = FoodDAO.Instance.SearchFoodByNametrue(txtTenMon.Text);
            if (dtgvMonAn.RowCount > 0)
            {
                LoadListFood();
                return false;
            }
            return true;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            QuanLy ql = new QuanLy(loginAccount);
            ql.ShowDialog();
            this.Show();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            QuanLy ql = new QuanLy(loginAccount);
            ql.ShowDialog();
            this.Show();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            QuanLy ql = new QuanLy(loginAccount);
            ql.ShowDialog();
            this.Show();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            QuanLy ql = new QuanLy(loginAccount);
            ql.ShowDialog();
            this.Show();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            QuanLy ql = new QuanLy(loginAccount);
            ql.ShowDialog();
            this.Show();
        }
     
    }
}
