using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniPosApp
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblTotal.Visible = false;

            dataCart.BorderStyle = BorderStyle.None;
            dataCart.EnableHeadersVisualStyles= false;
            dataCart.ColumnHeadersDefaultCellStyle.BackColor= Color.Black;
            dataCart.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataCart.DefaultCellStyle.SelectionBackColor = Color.FromArgb(80,80,80);
            dataCart.DefaultCellStyle.SelectionForeColor=Color.White;
                
            dataCart.RowHeadersVisible = false;
            dataCart.AllowUserToAddRows = false;
            dataCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataCart.Columns.Clear();
            dataCart.Columns.Add("Ad", "ürün");
            dataCart.Columns.Add("Fiyat", "Fiyat");
            dataCart.Columns.Add("Adet", "Adet");
            dataCart.Columns.Add("Toplam", "Toplam");

            
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.DefaultCellStyle.BackColor= Color.Red;
            btn.DefaultCellStyle.ForeColor= Color.White;
            btn.Name = "Sil";
            btn.HeaderText = "Sil";
            btn.Text = "X";
            btn.UseColumnTextForButtonValue = true;
           

            dataCart.Columns.Add(btn);
            dataCart.Columns["Sil"].Width = 50;
            dataCart.Columns["Adet"].Width = 60;

            dataCart.Columns["Fiyat"].DefaultCellStyle.Format = "0.00";
            dataCart.Columns["Toplam"].DefaultCellStyle.Format = "0.00";



            UrunEkle("Burger", 50, "burger.jpg");
            UrunEkle("Kola", 20, "cola.jpg");
            UrunEkle("Pizza", 80, "pizza.jpg");





        }

        void UrunEkle(string ad, decimal fiyat, string resimYolu)
        {
           
        

           Panel p= new Panel();
            p.BackColor = Color.White;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Margin = new Padding(10);
            p.Width = 100;
            p.Height = 130;
            
            PictureBox pic = new PictureBox();
            pic.Image = Image.FromFile(resimYolu);
            pic.SizeMode=PictureBoxSizeMode.StretchImage;
            pic.Width = 80;
            pic.Height = 60;
            pic.Top = 10;
            pic.Left = 10;



            

            Label lbl = new Label();
            lbl.Text = ad;
            lbl.Top = 10;
            lbl.Left = 10;
            lbl.AutoSize = true;

            Label lblFiyat = new Label();

            lblFiyat.Text = fiyat + "₺";

            lblFiyat.Top = 35;

            lblFiyat.Left = 10;

            lblFiyat.AutoSize = true;

            Button btn= new Button();
            btn.Text = "Ekle";
            btn.Width = 80;
            btn.Height = 30;
            btn.Top = 80;
            btn.Left = 20;

            btn.Click += (s, e) =>
            {

                SepeteEkle(ad, fiyat);
            };
            p.Controls.Add(pic);
            p.Controls.Add(lbl);
            p.Controls.Add(lblFiyat);
            p.Controls.Add(btn);

            flowProducts.Controls.Add(p);
             }


        void SepeteEkle(string ad, decimal fiyat)
        {
            bool bulundu = false;

            foreach (DataGridViewRow row in dataCart.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == ad)
                {
                    int adet = Convert.ToInt32(row.Cells[2].Value);
                    adet++;

                    row.Cells[2].Value = adet;
                    row.Cells[3].Value = adet * fiyat;

                    bulundu = true;
                    break;
                }
            }

            if (!bulundu)
            {
                dataCart.Rows.Add(ad, fiyat, 1, fiyat);
            }

            ToplamHesapla();
        }

        void ToplamHesapla()
        {
            decimal toplam = 0;

            foreach (DataGridViewRow row in dataCart.Rows) 
            {
               if (row.Cells[3].Value != null)
                {
                    toplam += Convert.ToDecimal(row.Cells[3].Value);
                }

                lblTotal.Text = "Toplam:" + toplam + "₺";
                lblTotal.Visible = true;
            }

            lblTotal.Text = "Toplam: " + toplam.ToString("0.00") + "₺";
        }

        private void dataCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dataCart.Columns["Sil"].Index && e.RowIndex >= 0)
            {
                dataCart.Rows.RemoveAt(e.RowIndex);
                ToplamHesapla();
            }
        }
    }
}
