using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CinemaProject
{
	public partial class MainGUI : Form
	{
		private static List<UserDetails> _users = new List<UserDetails>();

		public MainGUI()
		{
			InitializeComponent();
		}

		private void MainGUI_Load( object sender, EventArgs e )
		{
			#region Debugging code.  FIXME: Remove.

			_users.Add( new UserDetails( "TestName1", "TestSurname1", DateTime.Today.AddYears( -05 ), true, true ) );
			_users.Add( new UserDetails( "TestName2", "TestSurname2", DateTime.Today.AddYears( -10 ), true, false ) );
			_users.Add( new UserDetails( "TestName3", "TestSurname3", DateTime.Today.AddYears( -15 ), false, true ) );
			_users.Add( new UserDetails( "TestName4", "TestSurname4", DateTime.Today.AddYears( -20 ), false, false ) );

			#endregion Debugging code.  FIXME: Remove.

			foreach ( UserDetails user in _users )
			{
				dropdownSelectUsers.Items.Add( user.ToString() );
			}

            MovieButtonControl(0);
		}

		private void btnAddUser_Click( object sender, EventArgs e )
		{
			// Enable/disable controls.
			dropdownSelectUsers.Enabled = false;
			btnAddUser.Enabled = false;
			txtName.Enabled = true;
			txtSurname.Enabled = true;
			dateDateOfBirth.Enabled = true;
			chkAdmin.Enabled = true;
			chkActive.Enabled = true;
			btnSave.Enabled = true;
			btnCancelUserDetailsChange.Enabled = true;

			// Set default values.
			grpUserDetails.Text = "New User";
			txtName.Text = String.Empty;
			txtSurname.Text = String.Empty;
			dateDateOfBirth.Value = DateTime.Today;
			chkAdmin.Checked = false;
			chkActive.Checked = true;
			btnSave.Text = "Add User";
		}

		private void btnCancelUserDetailsChange_Click( object sender, EventArgs e )
		{
			// Enable/disable controls.
			dropdownSelectUsers.Enabled = true;
			btnAddUser.Enabled = true;
			txtName.Enabled = false;
			txtSurname.Enabled = false;
			dateDateOfBirth.Enabled = false;
			chkAdmin.Enabled = false;
			chkActive.Enabled = false;
			btnSave.Enabled = false;
			btnCancelUserDetailsChange.Enabled = false;

			// Set default values.
			btnSave.Text = "Save";
		}

		private void dropdownSelectUsers_SelectedIndexChanged( object sender, EventArgs e )
		{
			// Enable/disable controls.
			txtName.Enabled = true;
			txtSurname.Enabled = true;
			dateDateOfBirth.Enabled = true;
			chkAdmin.Enabled = true;
			chkActive.Enabled = true;
			btnSave.Enabled = true;
			btnCancelUserDetailsChange.Enabled = true;

			UserDetails selectedUser = ( UserDetails )dropdownSelectUsers.SelectedItem;

			txtName.Text = selectedUser.Name;
			txtSurname.Text = selectedUser.Surname;
			dateDateOfBirth.Value = selectedUser.DateOfBirth;
			chkAdmin.Checked = selectedUser.IsAdmin;
			chkActive.Checked = selectedUser.IsActive;

			btnSave.Text = "Update User";
		}

        private void cbbSession_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbMovieName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbMovieName.SelectedItem != "")
            {
                List<string> sessionList = new List<string>();
                //Load Movie Session Info from tInkScreenMovie table
                //Load items into the session list

                foreach (string item in sessionList)
                {
                    cbbSession.Items.Add(sessionList);
                }
                MovieButtonControl(1);
            }
            else
            {
                MovieButtonControl(0);
            }
        }

        private void btnMovieDetails_Click(object sender, EventArgs e)
        {
            if (cbbMovieName.SelectedText != "")
            {
                
            }
            //Load Movie info from tblMovies
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            int NumOfTickets;
            if (cbbMovieName.SelectedText == "")
            {
                MessageBox.Show("No movie selected for booking!");
            }
            else if (cbbSession.SelectedText == "" )
            {
                MessageBox.Show("No session selected for booking!");
            }
            else if (!int.TryParse(txtNumOfTickets.Text, out NumOfTickets))
            {
                MessageBox.Show("Please enter a correct number of tickets");
            }
            else
            {
                //Write to database
            }
        }

        public void MovieButtonControl(int Mode)
        {
            //Mode 0: Disable, Mode 1: Enable 
            if (Mode == 0)
            {
                cbbMovieName.Enabled = false;
                cbbSession.Enabled = false;
                txtNumOfTickets.Enabled = false;
                btnMovieDetails.Enabled = false;
                btnBook.Enabled = false;
            }
            else
            {
                cbbMovieName.Enabled = true;
                cbbSession.Enabled = true;
                txtNumOfTickets.Enabled = true;
                btnMovieDetails.Enabled = true;
                btnBook.Enabled = true;
            }
            
        }
	}
}
