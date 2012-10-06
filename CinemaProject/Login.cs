using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CinemaProject.Classes;

namespace CinemaProject
{
	public partial class Login : Form
	{
		public Login()
		{
			InitializeComponent();
		}


		private void btnLogin_Click( object sender, EventArgs e )
		{
			try
			{
				//If either the username or the password is incorrect, throw an error and stop all processes.
				if ( String.IsNullOrEmpty( txtUsername.Text.Trim() ) || String.IsNullOrEmpty( txtPassword.Text.Trim() ) )
				{
					//Modifies the error based on the field that is empty.
					MessageBox.Show( "Please enter your " + ( String.IsNullOrEmpty( txtUsername.Text.Trim() ) && String.IsNullOrEmpty( txtPassword.Text.Trim() ) ? "username and password" : String.IsNullOrEmpty( txtUsername.Text.Trim() ) ? "username" : String.IsNullOrEmpty( txtPassword.Text.Trim() ) ? "password" : "" ) + ".", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
					return;
				}
				using ( SqlConnection cn = new SqlConnection( @"server=.\SQLEXPRESS; Database=CinemaDB; Integrated Security=SSPI;" ) )
				{
					if ( cn.State == ConnectionState.Closed ) cn.Open();

					using ( SqlCommand cmd = new SqlCommand( "GetUserInformation", cn ) )
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Add( "@username", SqlDbType.NVarChar ).Value = txtUsername.Text.Trim();
						cmd.Parameters.Add( "@password", SqlDbType.NVarChar ).Value = txtPassword.Text.Trim();

						using ( SqlDataReader dr = cmd.ExecuteReader() )
						{
							//If any data is returned, set the UserInformation properties.
							if ( dr.Read() )
							{
								if ( dr[ 0 ].ToString().Equals( "That username is invalid!" ) )
								{
									throw new Exception( dr[ 0 ].ToString() );
								}
								else if ( dr[ 0 ].ToString().Equals( "Entered username and password do not match!" ) )
								{
									throw new Exception( dr[ 0 ].ToString() );
								}
								else if ( dr[ 0 ].ToString().Equals( "That acount has been de-activated!" ) )
								{
									throw new Exception( dr[ 0 ].ToString() );
								}
								else
								{
									//Set user details.
									UserInformation.UserID = dr.GetGuid( dr.GetOrdinal( "UserGuid" ) );
									UserInformation.Name = dr.GetString( dr.GetOrdinal( "UserName" ) );
									UserInformation.Surname = dr.GetString( dr.GetOrdinal( "UserSurname" ) );
									UserInformation.DoB = dr.GetDateTime( dr.GetOrdinal( "UserDoB" ) );
									UserInformation.ContactNumber = dr[ 5 ].ToString();
									UserInformation.AdminLevel = dr.GetInt32( dr.GetOrdinal( "AdminLevel" ) );

									//Hides the login panel and shows the main panel.
									this.Visible = false;
									MainGUI mainpage = new MainGUI();
									mainpage.ShowDialog();
								}
							}
						}
					}
				}
			}
			catch ( Exception ex )
			{
				MessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}
	}
}
