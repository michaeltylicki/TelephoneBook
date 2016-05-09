using System;
using System.Windows.Forms;
#pragma warning disable CS0105 // Using directive appeared previously in this namespace
#pragma warning restore CS0105 // Using directive appeared previously in this namespace
using System.Data;
using System.Data.SqlClient;

namespace TelephoneBook
{
    public partial class Phone : Form
    {

        SqlConnection con = new SqlConnection("Data Source=IDEA-PC;Initial Catalog=Phone;Integrated Security=True");
        public Phone()
        {
            InitializeComponent();
        }

        private void Phone_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox3.Text = "";
            textBox4.Clear();
            comboBox1.SelectedIndex = -1;
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"Insert into Mobiles 
                (Imię,Nazwisko,Telefon,Email,Kategoria) 
                values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox1.Text + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Poprawnie zapisano!");
            Display();
        }

        void Display()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Mobiles", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
            }

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
           textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
           textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
           textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
           textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
           comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"Delete from Mobiles where (Telefon = '" + textBox3.Text + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Poprawnie usunięto!");
            Display();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"Update Mobiles set 
            Imię = '" + textBox1.Text + "',Nazwisko = '" + textBox2.Text + "',Telefon = '" + textBox3.Text + "',Email = '" + textBox4.Text + "',Kategoria = '" + comboBox1.Text + "' where (Telefon = '" + textBox3.Text + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Poprawnie zaaktualizowano!");
            Display();
        }
    }
}
