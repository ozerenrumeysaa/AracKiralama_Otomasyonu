using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AracKiralama_Otomasyonu
{
    public partial class Form1 : Form
     
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        CarRentalDbContext db = new CarRentalDbContext();

        private void btn_listele_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = db.Customers.ToList();

                dataGridView1.Columns["Customer_Id"].HeaderText = "Müşteri ID";
                dataGridView1.Columns["Customer_Name"].HeaderText = "Müşteri Adı";
                dataGridView1.Columns["Customer_Surname"].HeaderText = "Müşteri Soyadı";
                dataGridView1.Columns["Customer_Telephone"].HeaderText = "Telefon";

                
                dataGridView1.Columns["Customer_Id"].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            try
            {
                Customer newCustomer = new Customer()
                {
                    Customer_Name = txt_isim.Text,
                    Customer_Surname = txt_soyisim.Text,
                    Customer_Telephone = txt_telefon.Text
                };

                db.Customers.Add(newCustomer);
                db.SaveChanges();

                MessageBox.Show("Yeni Müşteri Eklendi!");
                btn_listele.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txt_isim.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer_Name"].Value.ToString();
                txt_soyisim.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer_Surname"].Value.ToString();
                txt_telefon.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer_Telephone"].Value.ToString();
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Customer_Id"].Value);
                    Customer customer = db.Customers.Find(selectedId);

                    if (customer != null)
                    {
                        customer.Customer_Name = txt_isim.Text;
                        customer.Customer_Surname = txt_soyisim.Text;
                        customer.Customer_Telephone = txt_telefon.Text;

                        db.SaveChanges();
                        MessageBox.Show("Müşteri güncellendi!");
                        btn_listele.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult sonuc = MessageBox.Show("Silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {
                    if (dataGridView1.CurrentRow != null)
                    {
                        int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Customer_Id"].Value);
                        Customer customer = db.Customers.Find(selectedId);

                        if (customer != null)
                        {
                            db.Customers.Remove(customer);
                            db.SaveChanges();
                            MessageBox.Show("Müşteri Silindi!");
                            btn_listele.PerformClick();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        
        private void btn_form_kiralama_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;

            
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

           
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White;


            
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Goldenrod;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
           

            
            btn_form_kiralama.FlatAppearance.BorderSize = 1;
            btn_form_kiralama.FlatAppearance.BorderColor = Color.White;

            
            btn_form_kiralama.BackColor = Color.FromArgb(40, 255, 255, 255);

           
            btn_form_kiralama.ForeColor = Color.White;
           
            btn_form_kiralama.FlatAppearance.MouseOverBackColor = Color.FromArgb(70, 255, 255, 255);

            
            btn_form_kiralama.FlatAppearance.MouseDownBackColor = Color.FromArgb(100, 255, 255, 255);
        }

        private void btn_listele_Click_1(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Customers.ToList();
            dataGridView1.Columns["Customer_Name"].HeaderText = "Müşteri Adı";
            dataGridView1.Columns["Customer_Surname"].HeaderText = "Müşteri Soyadı";
            dataGridView1.Columns["Customer_Telephone"].HeaderText = "Müşteri Numarası";
            dataGridView1.Columns["Rentals"].Visible = false;

            dataGridView1.Columns["Customer_Id"].HeaderText = "No";
            dataGridView1.Columns["Customer_Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void btn_ekle_Click_1(object sender, EventArgs e)
        {
            Customer newCustomer = new Customer()
            {
                Customer_Name = txt_isim.Text,
                Customer_Surname = txt_soyisim.Text,
                Customer_Telephone = txt_telefon.Text
            };
            db.Customers.Add(newCustomer);
            db.SaveChanges();
            MessageBox.Show("Yeni Müşteri Eklendi!");
            btn_listele.PerformClick(); 
        }

        private void btn_sil_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Customer_Id"].Value);
                Customer customer = db.Customers.Find(selectedId);
                if (customer != null)
                {
                    db.Customers.Remove(customer);
                    db.SaveChanges();
                    MessageBox.Show("Müşteri Silindi!");
                    btn_listele.PerformClick();
                }
            }
        }

        private void btn_guncelle_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Customer_Id"].Value);
                Customer customer = db.Customers.Find(selectedId);
                if (customer != null)
                {
                    customer.Customer_Name = txt_isim.Text;
                    customer.Customer_Surname = txt_soyisim.Text;
                    customer.Customer_Telephone = txt_telefon.Text;
                    db.SaveChanges();
                    MessageBox.Show("Müşteri güncellendi!");
                    btn_listele.PerformClick();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.DataSource = db.Customers.ToList();
            {
                txt_isim.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer_Name"].Value.ToString();
                txt_soyisim.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer_Surname"].Value.ToString();
                txt_telefon.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer_Telephone"].Value.ToString();
            }
            
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            
            dataGridView1.RowTemplate.Height = 35;
        }

        private void btn_form_kiralama_Click_1(object sender, EventArgs e)
        {
            Form2 kiralamaEkrani = new Form2();
            kiralamaEkrani.Show();
        }

        private void txt_telefon_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_isim_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}