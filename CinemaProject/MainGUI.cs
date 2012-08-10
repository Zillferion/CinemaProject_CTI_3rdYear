using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CinemaProject
{
	public partial class MainGUI : Form
	{
		#region Member Variables
		private static List<UserDetails> _users = new List<UserDetails>();
		private static List<MovieDetails> _movies = new List<MovieDetails>();
		private static List<MovieDetails> _nonArchivedMovies = new List<MovieDetails>();
		private static String connectionString = @"server=.\SQLEXPRESS; Database=CinemaDB; Integrated Security=SSPI;";
		#endregion

		#region General Events
		public MainGUI()
		{
			//TODO:  Create logout.
			InitializeComponent();
		}


		/// <summary>
		/// Occurs when the form loads.
		/// </summary>
		private void MainGUI_Load( object sender, EventArgs e )
		{
			//Hide tabs based on Admin Level.
			switch ( UserInformation.AdminLevel )
			{
				//User is an Employee.
				case 0:
					tbcUserRoles.TabPages.Remove( tbUserSettings );
					tbcUserRoles.TabPages.Remove( tbMovieSettings );
					tbcUserRoles.TabPages.Remove( tbDistributors );
                    FillMovieDropDown();
					break;
				//User is a Manager.
				case 1:
					tbcUserRoles.TabPages.Remove( tbUserSettings );
					tbcUserRoles.TabPages.Remove( tbDistributors );
					FillMovieDropDown();
					break;
				//User is an Admin.
				case 2:
					FillUserDropDown();
					FillMovieDropDown();
					break;
				//User is a Distributor.
				case 3:
					tbcUserRoles.TabPages.Remove( tbBookings );
					tbcUserRoles.TabPages.Remove( tbUserSettings );
					tbcUserRoles.TabPages.Remove( tbMovieSettings );
					break;

				default:
					break;
			}
		}


		/// <summary>
		/// When the form closes the application is exited.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainGUI_FormClosed( object sender, FormClosedEventArgs e )
		{
			//Exit the application.
			Application.Exit();
		}
		#endregion

		#region User Setting Events
		private void btnAddUser_Click( object sender, EventArgs e )
		{
			// Enable/disable controls.
			dropdownSelectUsers.Enabled = false;
			btnAddUser.Enabled = false;
			btnCancelUserDetailsChange.Enabled = true;

			// Set default values.
			grpUserDetails.Text = "New User";
			txtName.Text = String.Empty;
			txtSurname.Text = String.Empty;
			dateDateOfBirth.Value = DateTime.Today;
			cbAdminLevel.SelectedIndex = 0;
			chkActive.Checked = true;
			txtContactNumber.Text = String.Empty;
			txtConfirmPassword.Text = String.Empty;
			txtPassword.Text = String.Empty;
			txtLoginUserName.Text = String.Empty;
			btnSave.Text = "Add User";
		}


		private void btnCancelUserDetailsChange_Click( object sender, EventArgs e )
		{
			// Enable/disable controls.
			dropdownSelectUsers.Enabled = true;
			btnAddUser.Enabled = true;
			btnCancelUserDetailsChange.Enabled = false;
			//Refills the users list.
			FillUserDropDown();
		}


		private void dropdownSelectUsers_SelectedIndexChanged( object sender, EventArgs e )
		{
			UserDetails selectedUser = _users[ dropdownSelectUsers.SelectedIndex ];

			txtName.Text = selectedUser.Name;
			txtSurname.Text = selectedUser.Surname;
			dateDateOfBirth.Value = selectedUser.DateOfBirth;
			cbAdminLevel.SelectedIndex = selectedUser.adminLevel;
			chkActive.Checked = selectedUser.IsActive;
			txtConfirmPassword.Text = selectedUser.userPassword;
			txtPassword.Text = selectedUser.userPassword;
			txtLoginUserName.Text = selectedUser.userName;
			txtContactNumber.Text = selectedUser.contactNumber;
			btnSave.Text = "Update User";
		}


		private void btnSave_Click( object sender, EventArgs e )
		{
			if ( txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim() )
			{
				MessageBox.Show( "Passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
				return;
			}
			if ( btnSave.Text.Equals( "Update User" ) )
			{
				using ( SqlConnection cn = new SqlConnection( connectionString ) )
				{
					if ( cn.State == ConnectionState.Closed ) cn.Open();

					using ( SqlCommand cmd = new SqlCommand( "UpdateUser", cn ) )
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Add( "@userGuid", SqlDbType.UniqueIdentifier ).Value = _users[ dropdownSelectUsers.SelectedIndex ].userGuid;
						cmd.Parameters.Add( "@name", SqlDbType.NVarChar ).Value = txtName.Text.Trim();
						cmd.Parameters.Add( "@surname", SqlDbType.NVarChar ).Value = txtSurname.Text.Trim();
						cmd.Parameters.Add( "@adminLevel", SqlDbType.Int ).Value = cbAdminLevel.SelectedIndex;
						cmd.Parameters.Add( "@contactNumber", SqlDbType.NVarChar ).Value = txtContactNumber.Text.Trim();
						cmd.Parameters.Add( "@password", SqlDbType.NVarChar ).Value = txtPassword.Text.Trim();
						cmd.Parameters.Add( "@loginName", SqlDbType.NVarChar ).Value = txtLoginUserName.Text.Trim();
						cmd.Parameters.Add( "@dateOfBirth", SqlDbType.DateTime ).Value = dateDateOfBirth.Text;
						cmd.Parameters.Add( "@active", SqlDbType.Bit ).Value = chkActive.Checked;
						cmd.ExecuteNonQuery();

						MessageBox.Show( "User successfully updated.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information );
					}
				}
			}
			else
			{
				using ( SqlConnection cn = new SqlConnection( connectionString ) )
				{
					if ( cn.State == ConnectionState.Closed ) cn.Open();

					using ( SqlCommand cmd = new SqlCommand( "CreateUser", cn ) )
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Add( "@name", SqlDbType.NVarChar ).Value = txtName.Text.Trim();
						cmd.Parameters.Add( "@surname", SqlDbType.NVarChar ).Value = txtSurname.Text.Trim();
						cmd.Parameters.Add( "@adminLevel", SqlDbType.Int ).Value = cbAdminLevel.SelectedIndex;
						cmd.Parameters.Add( "@contactNumber", SqlDbType.NVarChar ).Value = txtContactNumber.Text.Trim();
						cmd.Parameters.Add( "@password", SqlDbType.NVarChar ).Value = txtPassword.Text.Trim();
						cmd.Parameters.Add( "@loginName", SqlDbType.NVarChar ).Value = txtLoginUserName.Text.Trim();
						cmd.Parameters.Add( "@dateOfBirth", SqlDbType.DateTime ).Value = dateDateOfBirth.Text;
						cmd.Parameters.Add( "@active", SqlDbType.Bit ).Value = chkActive.Checked;
						cmd.ExecuteNonQuery();

						MessageBox.Show( "User successfully created.", "Create Success", MessageBoxButtons.OK, MessageBoxIcon.Information );

						//Re-enables buttons.
						btnCancelUserDetailsChange.Enabled = false;
						btnAddUser.Enabled = true;
						dropdownSelectUsers.Enabled = true;
						//Fills the dropdown again.
						FillUserDropDown();
					}
				}
			}
		}
		#endregion

		#region Booking Events
		private void cbbSession_SelectedIndexChanged( object sender, EventArgs e )
		{

		}


		private void cbbMovieName_SelectedIndexChanged( object sender, EventArgs e )
		{
			if ( cbbMovieName.SelectedItem != "" )
			{
				List<string> sessionList = new List<string>();
				//Load Movie Session Info from tInkScreenMovie table
				//Load items into the session list

				foreach ( string item in sessionList )
				{
					cbbSession.Items.Add( sessionList );
				}
				MovieButtonControl( 1 );
			}
			else
			{
				MovieButtonControl( 0 );
			}
		}


		private void btnMovieDetails_Click( object sender, EventArgs e )
		{
			if ( cbbMovieName.SelectedText != "" )
			{

			}
			//Load Movie info from tblMovies
		}


		private void btnBook_Click( object sender, EventArgs e )
		{
			int NumOfTickets;
			if ( cbbMovieName.SelectedText == "" )
			{
				MessageBox.Show( "No movie selected for booking!" );
			}
			else if ( cbbSession.SelectedText == "" )
			{
				MessageBox.Show( "No session selected for booking!" );
			}
			else if ( !int.TryParse( txtNumOfTickets.Text, out NumOfTickets ) )
			{
				MessageBox.Show( "Please enter a correct number of tickets" );
			}
			else
			{
				//Write to database
			}
		}


		public void MovieButtonControl( int Mode )
		{
			//Mode 0: Disable, Mode 1: Enable 
			if ( Mode == 0 )
			{
				cbbMovieName.Enabled = false;
				cbbSession.Enabled = false;
				txtNumOfTickets.Enabled = false;
				btnBook.Enabled = false;
			}
			else
			{
				cbbMovieName.Enabled = true;
				cbbSession.Enabled = true;
				txtNumOfTickets.Enabled = true;
				btnBook.Enabled = true;
			}
		}
		#endregion

		#region Movie Setting Events

		#endregion

		#region Methods
		/// <summary>
		/// Clears the current User list, refills it and then goes to the 1st item.
		/// </summary>
		private void FillUserDropDown()
		{
			try
			{
				//Clears the current list.
				dropdownSelectUsers.Items.Clear();
				//Clear the List object declared at the top.
				_users.Clear();

				//Create new connection.
				using ( SqlConnection cn = new SqlConnection( connectionString ) )
				{
					//If the connection is closed, open it.
					if ( cn.State == ConnectionState.Closed ) cn.Open();

					//Gets all the users that the logged in user is allowed to view and adds it to the dropdown list.
					using ( SqlCommand cmd = new SqlCommand( "GetUserList", cn ) )
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Add( "@adminLevel", SqlDbType.Int ).Value = UserInformation.AdminLevel;

						using ( SqlDataReader dr = cmd.ExecuteReader() )
						{
							while ( dr.Read() )
							{
								String name = dr.GetString( dr.GetOrdinal( "UserName" ) );
								String surname = dr.GetString( dr.GetOrdinal( "UserSurname" ) );
								DateTime doB = dr.GetDateTime( dr.GetOrdinal( "UserDoB" ) );
								Int32 adminLvl = dr.GetInt32( dr.GetOrdinal( "AdminLevel" ) );
								Boolean active = dr.GetBoolean( dr.GetOrdinal( "bActive" ) );
								DateTime registered = dr.GetDateTime( dr.GetOrdinal( "Created" ) );
								String username = dr.GetString( dr.GetOrdinal( "LoginUsername" ) );
								String password = dr.GetString( dr.GetOrdinal( "LoginPassword" ) );
								Guid userGuid = dr.GetGuid( dr.GetOrdinal( "UserGuid" ) );
								String contactNumber = dr[ 5 ].ToString();

								UserDetails user = new UserDetails
								(
									name
									, surname
									, doB
									, adminLvl
									, active
									, registered
									, username
									, password
									, userGuid
									, contactNumber
								);
								//Adds the user to the list declared at the top.
								_users.Add( user );
								//Adds the user to the dropdown list.
								dropdownSelectUsers.Items.Add( user.ToString() );
							}
							//Always select the 1st item in the dropdown as default.
							if ( dropdownSelectUsers.Items.Count > 0 )
							{
								dropdownSelectUsers.SelectedIndex = 0;
							}
						}
					}
				}
			}
			catch ( Exception ex )
			{
				MessageBox.Show( "Error:  " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}


		/// <summary>
		/// Clears the current Movie list, refills it and then goes to the 1st item.
		/// </summary>
		private void FillMovieDropDown()
		{
			try
			{
                //Clears the current list.
                ddlMovies.Items.Clear();
                //Clear the List object declared at the top.
                _movies.Clear();

                //Clears the current list.
                cbbMovieName.Items.Clear();
                //Clear the List object declared at the top.
                _nonArchivedMovies.Clear();

				//Create new connection.
				using ( SqlConnection cn = new SqlConnection( connectionString ) )
				{
					//If the connection is closed, open it.
					if ( cn.State == ConnectionState.Closed ) cn.Open();

					//Gets all the users that the logged in user is allowed to view and adds it to the dropdown list.
					using ( SqlCommand cmd = new SqlCommand( "GetMovieList", cn ) )
					{
						cmd.CommandType = CommandType.StoredProcedure;

						using ( SqlDataReader dr = cmd.ExecuteReader() )
						{
							while ( dr.Read() )
							{
								String name = dr.GetString( dr.GetOrdinal( "Name" ) );
								Int32 duration = dr.GetInt32( dr.GetOrdinal( "Duration" ) );
								Int32 releaseYear = dr.GetInt32( dr.GetOrdinal( "YearProduced" ) );
								String director = dr.GetString( dr.GetOrdinal( "Director" ) );
								String producer = dr.GetString( dr.GetOrdinal( "Producer" ) );
								String type = dr.GetString( dr.GetOrdinal( "Type" ) );
								String expectedAudience = dr.GetString( dr.GetOrdinal( "ExpectedAudience" ) );
								String rating = dr.GetString( dr.GetOrdinal( "BBFC_Rate" ) );
								Boolean archived = dr.GetBoolean( dr.GetOrdinal( "IsArchived" ) );
								String description = dr.GetString( dr.GetOrdinal( "Description" ) );

								//Create new movie instance
								MovieDetails movie = new MovieDetails();
								//Sets the movies details.
								movie.MovieName = name;
								movie.Duration = duration;
								movie.YearReleased = releaseYear;
								movie.Director = director;
								movie.Producer = producer;
								movie.Type = type;
								movie.ExpectedAudience = expectedAudience;
								movie.BbfcRate = rating;
								movie.IsArchived = archived;
								movie.Description = description;

								//Adds non-archived movies to the list created above.
								if ( !archived )
								{
									_nonArchivedMovies.Add( movie );
									cbbMovieName.Items.Add( movie.MovieName );
								}
								//Adds the user to the list declared at the top.
								_movies.Add( movie );
								//Adds the user to the dropdown list.
								ddlMovies.Items.Add( movie.MovieName );
							}
							//Always select the 1st item in the dropdown as default.
							if ( ddlMovies.Items.Count > 0 )
							{
								ddlMovies.SelectedIndex = 0;
							}
						}
					}
				}
			}
			catch ( Exception ex )
			{
				MessageBox.Show( "Error:  " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}

        public void SetMovieDetailsBlank()
        {
            lblMovie.Text = "";
            lblMovieDesc.Text = "";
            lblMovieDirector.Text = "";
            lblMovieDuration.Text = "";
            lblMovieProducer.Text = "";
            lblMovieTitle.Text = "";
            lblMovieType.Text = "";
        }

        public void SetMovieDetails(int movieID)
        {

        }

		#endregion
	}
}
