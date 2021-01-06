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

namespace Palak_Arora_Exercise02
{
    public partial class DisplayPlayers : Form
    {
        static string playerName;
        public DisplayPlayers()
        {
            InitializeComponent();
        }
        private Palak_Arora_BaseballExamples.BaseballEntities dbcontext = new Palak_Arora_BaseballExamples.BaseballEntities();
        //1.Create an object of DbContextClass 
        //load event handler for the form
        
        private void playerBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            Validate();
            playerBindingSource.EndEdit();
            try
            {
                dbcontext.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Firtst Name and Last Name must contain Values", "Entity Validation Exception");
            }
            //cod efor the saving changes to the rows of data players data table int the data grid 
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

       
        
       
           
        

        private void playerBindingNavigator_RefreshItems(object sender, EventArgs e)
        {

        }

        private void DisplayPlayers_Load(object sender, EventArgs e)
        {
            //Using the DbContext class.... load the datagrid from the rows of the players' table 

            dbcontext.Players.Load();
            //specify Datasource for playerBindingSource
            playerBindingSource.DataSource = dbcontext.Players.Local;
        }

        private void searchPlayer_Click(object sender, EventArgs e)
        {

            playerName = txtSearchPlayr.Text.ToString();
            var r = from player in dbcontext.Players where player.LastName == playerName select player;
            //specify Datasource for playerBindingSource
            playerBindingSource.DataSource = r.ToList();
        }

        private void btnallPlayer_Click(object sender, EventArgs e)
        {
            txtSearchPlayr.Text = null;
            //specify Datasource for playerBindingSource
            playerBindingSource.DataSource = dbcontext.Players.Local;
        }

       
    }
}
