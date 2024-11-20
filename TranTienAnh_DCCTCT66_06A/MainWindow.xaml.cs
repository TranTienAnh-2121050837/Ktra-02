using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace QLShippers
{
    public partial class frmShippers_2121050837_TranTienAnh : Window
    {
        private string connectionString = "Server=LAPTOP-SRO5BP0A\\MINOS;Database=Northwind;Trusted_Connection=True;";

        public frmShippers_2121050837_TranTienAnh()
        {
            InitializeComponent();
            LoadShippers();
        }

        private void LoadShippers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Shippers", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvShippers.ItemsSource = dt.DefaultView;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Shippers WHERE 1=1";
                if (!string.IsNullOrEmpty(txtSearchName.Text))
                    query += $" AND CompanyName LIKE '%{txtSearchName.Text}%'";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvShippers.ItemsSource = dt.DefaultView;
            }
        }

        private void dgvShippers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvShippers.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dgvShippers.SelectedItem;
                txtShipperIDDetail.Text = row["ShipperID"].ToString();
                txtShipperNameDetail.Text = row["CompanyName"].ToString();
                txtAgeDetail.Text = row["Age"].ToString();  // Giả sử có trường "Age" trong cơ sở dữ liệu

                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            txtShipperIDDetail.Clear();
            txtShipperNameDetail.Clear();
            txtAgeDetail.Clear();

            txtShipperIDDetail.IsReadOnly = true;
            btnSave.Tag = "Add";
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgvShippers.SelectedItem != null)
            {
                btnSave.Tag = "Edit";
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgvShippers.SelectedItem != null)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    DataRowView row = (DataRowView)dgvShippers.SelectedItem;
                    string query = $"DELETE FROM Shippers WHERE ShipperID = {row["ShipperID"]}";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xoá thành công!");
                    LoadShippers();
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query;
                if (btnSave.Tag.ToString() == "Add")
                {
                    query = $"INSERT INTO Shippers (CompanyName, Age) VALUES ('{txtShipperNameDetail.Text}', {txtAgeDetail.Text})";
                }
                else // Edit
                {
                    query = $"UPDATE Shippers SET CompanyName = '{txtShipperNameDetail.Text}', Age = {txtAgeDetail.Text} WHERE ShipperID = {txtShipperIDDetail.Text}";
                }
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Lưu thành công!");
                LoadShippers();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtShipperIDDetail.Clear();
            txtShipperNameDetail.Clear();
            txtAgeDetail.Clear();
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }
    }
}
