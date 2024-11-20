using System;
using System.Data.SqlClient;
using System.Windows;
using System.Configuration;

namespace TranTienAnh_DCCTCT66_06A
{
    public partial class frmShippers_2121050837_TranTienAnh : Window
    {
        // Chuỗi kết nối cơ sở dữ liệu từ App.config
        private string connectionString = ConfigurationManager.ConnectionStrings["QLShipperConnectionString"].ConnectionString;

        public frmShippers_2121050837_TranTienAnh()
        {
            InitializeComponent();
        }

        // Sự kiện Tìm kiếm
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string shipperID = txtShipperID.Text;
            string companyName = txtCompanyName.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Shippers WHERE ShipperID = @ShipperID AND CompanyName LIKE @CompanyName";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ShipperID", shipperID);
                    cmd.Parameters.AddWithValue("@CompanyName", "%" + companyName + "%");

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // Hiển thị kết quả trong các TextBox hoặc DataGrid
                        txtShipperIDDetail.Text = reader["ShipperID"].ToString();
                        txtCompanyNameDetail.Text = reader["CompanyName"].ToString();
                        txtPhone.Text = reader["Phone"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}");
                }
            }
        }

        // Sự kiện Lưu thông tin (Cập nhật thông tin shipper)
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string shipperID = txtShipperIDDetail.Text;
            string companyName = txtCompanyNameDetail.Text;
            string phone = txtPhone.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Shippers SET CompanyName = @CompanyName, Phone = @Phone WHERE ShipperID = @ShipperID";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ShipperID", shipperID);
                    cmd.Parameters.AddWithValue("@CompanyName", companyName);
                    cmd.Parameters.AddWithValue("@Phone", phone);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy ShipperID để cập nhật.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}");
                }
            }
        }

        // Sự kiện Hủy bỏ (Không làm gì, chỉ đóng cửa sổ hoặc reset thông tin)
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Reset thông tin
            txtShipperIDDetail.Clear();
            txtCompanyNameDetail.Clear();
            txtPhone.Clear();
        }

        // Sự kiện Thêm mới Shipper
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string shipperID = txtShipperIDDetail.Text;
            string companyName = txtCompanyNameDetail.Text;
            string phone = txtPhone.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Shippers (ShipperID, CompanyName, Phone) VALUES (@ShipperID, @CompanyName, @Phone)";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ShipperID", shipperID);
                    cmd.Parameters.AddWithValue("@CompanyName", companyName);
                    cmd.Parameters.AddWithValue("@Phone", phone);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thêm mới thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm dữ liệu.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}");
                }
            }
        }

        // Sự kiện Chỉnh sửa thông tin Shipper
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            string shipperID = txtShipperIDDetail.Text;
            string companyName = txtCompanyNameDetail.Text;
            string phone = txtPhone.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Shippers SET CompanyName = @CompanyName, Phone = @Phone WHERE ShipperID = @ShipperID";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ShipperID", shipperID);
                    cmd.Parameters.AddWithValue("@CompanyName", companyName);
                    cmd.Parameters.AddWithValue("@Phone", phone);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy ShipperID để cập nhật.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}");
                }
            }
        }

        // Sự kiện Xóa Shipper
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string shipperID = txtShipperIDDetail.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Shippers WHERE ShipperID = @ShipperID";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ShipperID", shipperID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy ShipperID để xóa.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}");
                }
            }
        }

        // Sự kiện thay đổi lựa chọn trong DataGrid (dùng để hiển thị chi tiết)
        private void dgvShippers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgvShippers.SelectedItem != null)
            {
                // Lấy thông tin từ DataGrid để hiển thị chi tiết
                var selectedRow = dgvShippers.SelectedItem as System.Data.DataRowView;
                txtShipperIDDetail.Text = selectedRow["ShipperID"].ToString();
                txtCompanyNameDetail.Text = selectedRow["CompanyName"].ToString();
                txtPhone.Text = selectedRow["Phone"].ToString();
            }
        }
    }
}
