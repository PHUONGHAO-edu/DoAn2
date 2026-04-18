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
    public partial class DangVien : Form
    {
        public DangVien()
        {
            InitializeComponent();
        }
        public void AnChucNang()
        {
            btnThem.Visible = false;
            btnXoa.Visible = false;
            btnLamMoi.Visible = false;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaDangVien.Text == "" || txtHoTen.Text == "")
            {
                MessageBox.Show("Không được để trống dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            BenhNhan_DTO bn = new BenhNhan_DTO();
            bn.Ma_benh_nhan1 = int.Parse(txtMaDangVien.Text);
            bn.Ho_tenBn1 = txtHoTen.Text;
            bn.Dia_chi1 = txtDiaChi.Text;
            bn.Ma_benh1 = cbbChiBo.SelectedValue.ToString();
            if (rdNam.Checked == true)
            {
                bn.Gioi_tinh1 = "Nam";
            }
            else
            {
                bn.Gioi_tinh1 = "Nữ";
            }

            bn.So_dien_thoai1 = txtDiaChi.Text;


            if (BenhNhan_BUS.ThemBenhNhan(bn) == true)
            {
                BenhNhan_Load(sender, e);
                MessageBox.Show("Đã thêm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show("Không thêm được", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void BenhNhan_Load(object sender, EventArgs e)
        {
            List<ChiBo_DTO> benh = ChiBo_BUS.LayChiBo();
            cbbChiBo.DataSource = benh;
            cbbChiBo.DisplayMember = "TenChiBo";
            cbbChiBo.ValueMember = "MaChiBo";

            List<BenhNhan_DTO> bn = BenhNhan_BUS.LayBenhNhan();
            dtvBenhNhan.DataSource = bn;
            dtvBenhNhan.Columns["Ma_benh_nhan1"].HeaderText = "Mã bệnh nhân";
            dtvBenhNhan.Columns["Ma_benh1"].HeaderText = "Chẩn đoán";
            dtvBenhNhan.Columns["Ho_tenBn1"].HeaderText = "Họ tên";
            dtvBenhNhan.Columns["So_dien_thoai1"].HeaderText = "Số điện thoại";
            dtvBenhNhan.Columns["Gioi_tinh1"].HeaderText = "Giới tính";
            dtvBenhNhan.Columns["Dia_chi1"].HeaderText = "Địa chỉ";
            dtvBenhNhan.Columns["Nam_sinh1"].HeaderText = "Năm";
            dtvBenhNhan.Columns["Ten_benh1"].HeaderText = "Tên bệnh";
        }

        private void dtvBenhNhan_Click(object sender, EventArgs e)
        {
            if (dtvBenhNhan.SelectedRows.Count > 0)
            {
                DataGridViewRow dr = dtvBenhNhan.SelectedRows[0];
                txtMaDangVien.Text = dr.Cells["Ma_benh_nhan1"].Value.ToString();
                cbbChiBo.SelectedValue = dr.Cells["Ma_benh1"].Value;
                txtHoTen.Text = dr.Cells["Ho_tenBn1"].Value.ToString();
                txtDiaChi.Text = dr.Cells["Dia_chi1"].Value.ToString();
                txtSodt.Text = dr.Cells["So_dien_thoai1"].Value.ToString();
                if (dr.Cells["Gioi_tinh1"].Value.ToString() == "Nam")
                {
                    rdNam.Checked = true;
                }
                else
                {
                    rdNu.Checked = true;
                }
                DateTimeNgSinh.Text = dr.Cells["Nam_sinh1"].Value.ToString();

            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            {
                if (txtMaDangVien.Text == "" || txtHoTen.Text == "")
                {
                    MessageBox.Show("Không được để trống dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                BenhNhan_DTO bn = new BenhNhan_DTO();
                bn.Ma_benh_nhan1 = int.Parse(txtMaDangVien.Text);
                if (BenhNhan_BUS.XoaBenhNhan(bn) == true)
                {
                    BenhNhan_Load(sender, e);
                    MessageBox.Show("Đã xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show("Không xóa được", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaDangVien.Text == "" || txtHoTen.Text == "")
            {
                MessageBox.Show("Không được để trống dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            BenhNhan_DTO bn = new BenhNhan_DTO();
            bn.Ma_benh_nhan1 = int.Parse(txtMaDangVien.Text);
            bn.Ho_tenBn1 = txtHoTen.Text;
            bn.Dia_chi1 = txtDiaChi.Text;
            bn.Ma_benh1 = cbbChiBo.SelectedValue.ToString();
            if (rdNam.Checked == true)
            {
                bn.Gioi_tinh1 = "Nam";
            }
            else
            {
                bn.Gioi_tinh1 = "Nữ";
            }

            bn.So_dien_thoai1 = txtDiaChi.Text;


            if (BenhNhan_BUS.SuaBenhNhan(bn) == true)
            {
                BenhNhan_Load(sender, e);
                MessageBox.Show("Đã sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show("Không sửa được", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaDangVien.Text = "";
            txtHoTen.Text = "";
            txtDiaChi.Text = "";
            txtSodt.Text = "";

        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string ma = txtMaDangVien.Text;
            List<BenhNhan_DTO> lstbn = BenhNhan_BUS.TimBenhNhan(ma);
            if (lstbn == null)
            {
                MessageBox.Show("Không tìm thấy!");
                return;
            }
            dtvBenhNhan.DataSource = lstbn;

        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            BenhNhan_Load(sender, e);
        }
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
