using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GUI
{
    public partial class CanBoQuanLy : Form
    {
        public CanBoQuanLy()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaCanBo.Text == "" || cbbMaChucVu.SelectedValue == null || txtHoTen.Text == "")
            {
                MessageBox.Show("Không được để trống dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            QuanLyCanBo_DTO nv = new QuanLyCanBo_DTO();
            nv.Ma_CanBo1 = txtMaCanBo.Text;
            nv.Ma_chuc_vu1 = cbbMaChucVu.SelectedValue.ToString();
            nv.Ho_ten1 = txtHoTen.Text;
            nv.So_dien_thoai1 = txtSdt.Text;
            if (rdNam.Checked == true)
            {
                nv.Gioi_tinh1 = "Nam";
            }
            else
            {
                nv.Gioi_tinh1 = "Nữ";
            }
            nv.Dia_chi1 = txtDiaChi.Text;
            nv.Ngay_sinh1 = DateTimeNgSinh.Text;
            nv.Luong1 = float.Parse(txtLuong.Text);

            if (CanBoQuanLy_BUS.ThemCanBo(nv) == false)
            {
                MessageBox.Show("Không thêm được", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            QuanLyCanBo_Load(sender, e);
            MessageBox.Show("Đã thêm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaCanBo.Text == "" || cbbMaChucVu.SelectedValue == null || txtHoTen.Text == "")
            {
                MessageBox.Show("Chọn nhân viên cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            NhanVien_DTO nv = new NhanVien_DTO();
            nv.Ma_CanBo1 = txtMaCanBo.Text;
            if (NhanVien_BUS.XoaNhanVien(nv) == true)
            {
                QuanLyCanBo_Load(sender, e);
                MessageBox.Show("Đã xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Không xóa được", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaCanBo.Text == "" || cbbMaChucVu.SelectedValue == null || txtHoTen.Text == "")
            {
                MessageBox.Show("Chọn cán bộ cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            QuanLyCanBo_DTO nv = new QuanLyCanBo_DTO();
            nv.Ma_CanBo1 = txtMaCanBo.Text;
            nv.Ma_chuc_vu1 = cbbMaChucVu.SelectedValue.ToString();
            nv.Ho_ten1 = txtHoTen.Text;
            nv.So_dien_thoai1 = txtSdt.Text;
            if (rdNam.Checked == true)
            {
                nv.Gioi_tinh1 = "Nam";
            }
            else
            {
                nv.Gioi_tinh1 = "Nữ";
            }
            nv.Dia_chi1 = txtDiaChi.Text;
            nv.Ngay_sinh1 = DateTimeNgSinh.Text;
            nv.Luong1 = float.Parse(txtLuong.Text);

            if (QuanLyCanBo_BUS.SuaCanBo(nv) == true)
            {
                QuanLyCanBo_Load(sender, e);
                MessageBox.Show("Đã sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Không sửa được", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaCanBo.Text = "";
            cbbMaChucVu.SelectedIndex = -1;
            txtHoTen.Text = "";
            txtDiaChi.Text = "";
            txtSdt.Text = "";
            txtLuong.Text = "";
        }

        private void QuanLyCanBo_Load(object sender, EventArgs e)
        {
            List<QuanLyCanBo_DTO> lstnhanvien = QuanLyCanBo_BUS.LayCanBo();
            dtvNhanVien.DataSource = lstnhanvien;
            dtvNhanVien.Columns["Ma_CanBo1"].HeaderText = "Mã Cán Bộ ";
            dtvNhanVien.Columns["Ma_chuc_vu1"].HeaderText = "Mã chức vụ";
            dtvNhanVien.Columns["Ho_ten1"].HeaderText = "Họ tên";
            dtvNhanVien.Columns["Ho_ten1"].Width = 150;
            dtvNhanVien.Columns["So_dien_thoai1"].HeaderText = "Số điện thoại";
            dtvNhanVien.Columns["Gioi_tinh1"].HeaderText = "Giới tính";
            dtvNhanVien.Columns["Dia_chi1"].HeaderText = "Địa chỉ";
            dtvNhanVien.Columns["Ngay_sinh1"].HeaderText = "Ngày sinh";
            dtvNhanVien.Columns["Luong1"].HeaderText = "Lương";
            List<ChucVu_DTO> dsChucVu = ChucVu_BUS.LayChucVu();
            cbbMaChucVu.DataSource = dsChucVu;
            cbbMaChucVu.DisplayMember = "Ten_chuc_vu"; // Tên hiển thị
            cbbMaChucVu.ValueMember = "Ma_chuc_vu1";    // Giá trị thực
        }

        private void dtvNhanVien_Click(object sender, EventArgs e)
        {
            if (dtvNhanVien.SelectedRows.Count > 0)
            {
                DataGridViewRow dr = dtvNhanVien.SelectedRows[0];
                txtMaNhanVien.Text = dr.Cells["Ma_nhan_vien1"].Value.ToString();
                cbbMaChucVu.SelectedValue = dr.Cells["Ma_chuc_vu1"].Value.ToString();
                txtHoTen.Text = dr.Cells["Ho_ten1"].Value.ToString();
                txtSdt.Text = dr.Cells["So_dien_thoai1"].Value.ToString();
                if (dr.Cells["Gioi_tinh1"].Value.ToString() == "Nam")
                {
                    rdNam.Checked = true;
                }
                else
                {
                    rdNu.Checked = true;
                }
                txtDiaChi.Text = dr.Cells["Dia_chi1"].Value.ToString();
                DateTimeNgSinh.Text = dr.Cells["Ngay_sinh1"].Value.ToString();
                txtLuong.Text = dr.Cells["Luong1"].Value.ToString();
            }

        }
    }
}
