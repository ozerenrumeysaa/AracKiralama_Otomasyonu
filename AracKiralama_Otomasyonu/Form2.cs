using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace AracKiralama_Otomasyonu
{
    public partial class Form2 : Form
    {
        CarRentalDbContext db = new CarRentalDbContext();

        public Form2()
        {
           
            System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<CarRentalDbContext>());

            InitializeComponent();

            
            if (db.Cars.Count() <= 3)
            {
                foreach (var car in db.Cars.ToList())
                {
                    db.Cars.Remove(car);
                }
                db.SaveChanges();

                db.Cars.Add(new Car { Car_BrandModel = "Ford Transit (Panelvan)", Car_Plate = "34 AVC 01", Car_DailyPrice = 1500 });
                db.Cars.Add(new Car { Car_BrandModel = "Fiat Ducato (Kamyonet)", Car_Plate = "41 AVC 02", Car_DailyPrice = 1800 });
                db.Cars.Add(new Car { Car_BrandModel = "Mercedes Sprinter", Car_Plate = "06 AVC 03", Car_DailyPrice = 2500 });
                db.Cars.Add(new Car { Car_BrandModel = "Iveco Daily", Car_Plate = "34 AVC 04", Car_DailyPrice = 2200 });
                db.Cars.Add(new Car { Car_BrandModel = "Renault Master", Car_Plate = "16 AVC 05", Car_DailyPrice = 1900 });
                db.Cars.Add(new Car { Car_BrandModel = "Volkswagen Crafter", Car_Plate = "35 AVC 06", Car_DailyPrice = 2600 });
                db.Cars.Add(new Car { Car_BrandModel = "Fiat Egea", Car_Plate = "34 AVC 07", Car_DailyPrice = 1000 });
                db.Cars.Add(new Car { Car_BrandModel = "Renault Megane", Car_Plate = "41 AVC 08", Car_DailyPrice = 1200 });
                db.Cars.Add(new Car { Car_BrandModel = "Toyota Corolla", Car_Plate = "06 AVC 09", Car_DailyPrice = 1300 });
                db.Cars.Add(new Car { Car_BrandModel = "Dacia Duster", Car_Plate = "34 AVC 10", Car_DailyPrice = 1400 });

                db.SaveChanges();
            }

            comboBox1.DataSource = db.Customers.ToList();
            comboBox1.DisplayMember = "Customer_Name";
            comboBox1.ValueMember = "Customer_Id";

            comboBox2.DataSource = db.Cars.ToList();
            comboBox2.DisplayMember = "Car_BrandModel";
            comboBox2.ValueMember = "Car_Id";
        }

        private void TabloyuYenile()
        {
            var kiralamaListesi = db.Rentals.Select(k => new
            {
                Kayıt_No = k.Rental_Id,
                Müşteri = k.Customer.Customer_Name,
                Kiralanan_Araç = k.Car.Car_BrandModel,
                Tarih = k.RentDate,
                Gün_Sayısı = k.RentDays,
                Günlük_Ücret = k.Car.Car_DailyPrice.ToString() + " ₺",
                Toplam_Tutar = (k.RentDays * k.Car.Car_DailyPrice).ToString() + " ₺"
            }).ToList();

            dataGridView1.DataSource = kiralamaListesi;

           
            dataGridView1.Columns["Kayıt_No"].Visible = false;

            
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

           
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

           
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.ColumnHeadersHeight = 45;

             
            dataGridView1.BackgroundColor = Color.WhiteSmoke;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 53, 65);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(212, 175, 55);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

            
            rd_1.ForeColor = Color.White;
            rd_2.ForeColor = Color.White;

            
            lbl_1.ForeColor = Color.FromArgb(212, 175, 55);
            lbl_2.ForeColor = Color.FromArgb(212, 175, 55);
        }
            
private void KoseYuvarlat(Control nesne, int cap)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, cap, cap, 180, 90);
            path.AddArc(nesne.Width - cap, 0, cap, cap, 270, 90);
            path.AddArc(nesne.Width - cap, nesne.Height - cap, cap, cap, 0, 90);
            path.AddArc(0, nesne.Height - cap, cap, cap, 90, 90);
            path.CloseFigure();
            nesne.Region = new Region(path);
        }
        

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null || comboBox2.SelectedValue == null)
            {
                MessageBox.Show("Lütfen önce listeden bir Müşteri ve Araç seçin!", "Eksik Seçim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Lütfen kiralama gün sayısını boş bırakmayın!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Rental yeniKiralama = new Rental();
            yeniKiralama.Customer_Id = (int)comboBox1.SelectedValue;
            yeniKiralama.Car_Id = (int)comboBox2.SelectedValue;
            yeniKiralama.RentDays = Convert.ToInt32(textBox1.Text);

            
            yeniKiralama.RentDate = dateTimePicker1.Value;

            db.Rentals.Add(yeniKiralama);
            db.SaveChanges();

            MessageBox.Show("Başarıyla kaydedildi!", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            TabloyuYenile();
        }

        private void btn_listele_Click(object sender, EventArgs e)
        {
            TabloyuYenile();
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int secilenID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                var silinecekKiralama = db.Rentals.Find(secilenID);

                if (silinecekKiralama != null)
                {
                    db.Rentals.Remove(silinecekKiralama);
                    db.SaveChanges();

                    TabloyuYenile();
                }
            }
        }

        
        private void rd_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_1.Checked)
            {
                lbl_2.Text = "..."; 

                var enCokKiralanan = db.Rentals
                    .GroupBy(k => k.Car.Car_BrandModel)
                    .Select(grup => new { Arac = grup.Key, KiralamaSayisi = grup.Count() })
                    .OrderByDescending(grup => grup.KiralamaSayisi)
                    .FirstOrDefault();

                if (enCokKiralanan != null)
                {
                    lbl_1.Text = enCokKiralanan.Arac + " (" + enCokKiralanan.KiralamaSayisi + " Kez)";
                }
                else
                {
                    lbl_1.Text = "Henüz kiralama yok.";
                }
            }
        }

        
        private void rd_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_2.Checked)
            {
                lbl_1.Text = "..."; 

                var enCokKazandiran = db.Rentals
                    .GroupBy(k => k.Customer.Customer_Name)
                    .Select(grup => new
                    {
                        Musteri = grup.Key,
                        ToplamCiro = grup.Sum(k => k.RentDays * k.Car.Car_DailyPrice)
                    })
                    .OrderByDescending(grup => grup.ToplamCiro)
                    .FirstOrDefault();

                if (enCokKazandiran != null)
                {
                    lbl_2.Text = enCokKazandiran.Musteri + " (" + enCokKazandiran.ToplamCiro + " ₺)";
                }
                else
                {
                    lbl_2.Text = "Henüz kiralama yok.";
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            lblGunSayisi.Visible = true;
            lblGunSayisi.ForeColor = Color.Black;
            
            panel1.BackColor = Color.FromArgb(100, 245, 245, 250); 

            
            KoseYuvarlat(btn_ekle, 15);
            KoseYuvarlat(btn_listele, 15);
            KoseYuvarlat(btn_sil, 15);
           

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lbl_2_Click(object sender, EventArgs e)
        {

        }

        
        private void rd_1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rd_1.Checked)
            {
                lbl_2.Text = "..."; 

                var enCokKiralanan = db.Rentals
                    .GroupBy(k => k.Car.Car_BrandModel)
                    .Select(grup => new { Arac = grup.Key, KiralamaSayisi = grup.Count() })
                    .OrderByDescending(grup => grup.KiralamaSayisi)
                    .FirstOrDefault();

                if (enCokKiralanan != null)
                {
                    lbl_1.Text = enCokKiralanan.Arac + " (" + enCokKiralanan.KiralamaSayisi + " Kez)";
                }
            }
        }

        
        private void rd_2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rd_2.Checked)
            {
                lbl_1.Text = "..."; 

                var enCokKazandiran = db.Rentals
                    .GroupBy(k => k.Customer.Customer_Name)
                    .Select(grup => new
                    {
                        Musteri = grup.Key,
                        ToplamCiro = grup.Sum(k => k.RentDays * k.Car.Car_DailyPrice)
                    })
                    .OrderByDescending(grup => grup.ToplamCiro)
                    .FirstOrDefault();

                if (enCokKazandiran != null)
                {
                    lbl_2.Text = enCokKazandiran.Musteri + " (" + enCokKazandiran.ToplamCiro + " ₺)";
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}