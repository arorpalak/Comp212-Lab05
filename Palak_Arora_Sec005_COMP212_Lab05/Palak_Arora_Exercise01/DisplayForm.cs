using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palak_Arora_Exercise01
{
    public partial class DisplayForm : Form
    {
        public DisplayForm()
        {
            InitializeComponent();
        }

        private Palak_Arora_BooksExamples.BooksEntities dbcontext = new Palak_Arora_BooksExamples.BooksEntities();


        private void titleBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            Validate();
            authorBindingSource.EndEdit();
            try
            {
                dbcontext.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Firtst Name and Last Name must contain Values", "Entity Validation Exception");
            }
        }

      

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void DisplayForm_Load(object sender, EventArgs e)
        {
            var result =
                 from title in dbcontext.Titles
                 from author in title.Authors
                 orderby title.Title1    // Sort by Title
                  select new
                 {
                     Title = title.Title1,
                     Edition = title.EditionNumber,
                     AuthorID = author.AuthorID,
                     AuthorName = author.FirstName
                 };
            // specify DataSource for authorBindingSource
            dataGridView1.DataSource = result.ToList();
            var titlesAndAuthors =
                from title in dbcontext.Titles
                orderby title.Title1
                select new
                {
                    Title = title.Title1,
                    AuthorsName =
                        from author in title.Authors
                        orderby author.LastName, author.FirstName
                        select author.FirstName + " " + author.LastName
                };

            txtResult.AppendText("******* List of all the authors grouped by titles, sorted by title***********");
            foreach (var book in titlesAndAuthors)
            {
                txtResult.AppendText(Environment.NewLine);
                txtResult.AppendText($"\r\n\t*************Book Title***************");
                txtResult.AppendText(Environment.NewLine);
                txtResult.AppendText($"\r\n\t{book.Title}:");
                txtResult.AppendText($"\r\n\t***************Authors******************");
                foreach (var author in book.AuthorsName)
                {
                    txtResult.AppendText($"\r\n\t\t{author}");
                }
                txtResult.AppendText(Environment.NewLine);
            }
        }


    }
}
