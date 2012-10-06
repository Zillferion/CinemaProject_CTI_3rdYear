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
using System.Collections;

namespace CinemaProject
{
	/*
	 * 1.  Add comments everywhere.
	 * 2.  Optimize code to get rid of un-needed Member Variables.
	 * 3.  Make new movie/user/distributor the selected user after creation.
	 * 4.  Show list of movies a distributor is linked to so that items can be removed.
	 */
	public partial class MainGUI : Form
	{
		#region Enums
		/// <summary>
		/// 
		/// </summary>
		private enum UserType
		{
			Employee = 0,
			Managrer = 1,
			Admin = 2
		}

		/// <summary>
		/// 
		/// </summary>
		private enum ExpectedAudience
		{
			Mainstream = 0,
			Average = 1,
			Below_Average = 2,
			Specialist = 3,
			Non_Mainstream = 4
		}

		/// <summary>
		/// 
		/// </summary>
		private enum BBFC_Rating
		{
			_U = 0,
			_PG = 1,
			_12 = 2,
			_12A = 3,
			_15 = 4,
			_18 = 5,
			_R18 = 6
		}
		#endregion Enums

		#region Member Variables
		/// <summary>
		/// Contains a list of the current users.
		/// </summary>
		private static List<UserDetails> _users = new List<UserDetails>();

		/// <summary>
		/// Contains a list of all the current movies.
		/// </summary>
		private static List<MovieDetails> _movies = new List<MovieDetails>();

		/// <summary>
		/// Contains a list of all the current distributors.
		/// </summary>
		private static List<DistributorDetails> _distributors = new List<DistributorDetails>();

		/// <summary>
		/// Contains a list of all the current non-archived movies.
		/// </summary>
		private static List<MovieDetails> _nonArchivedMovies = new List<MovieDetails>();

		/// <summary>
		/// ConnectionString for the database.
		/// </summary>
		private const String connectionString = @"server=.\SQLEXPRESS; Database=CinemaDB; Integrated Security=SSPI;";
		#endregion Member Variables

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
					FillDistributorDropDown();
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
		#endregion General Events

		#region User Setting Events
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddUser_Click( object sender, EventArgs e )
		{
			// Enable/disable controls.
			cbSelectUsers.Enabled = false;
			btnAddUser.Enabled = false;
			btnCancelUserDetailsChange.Enabled = true;

			// Set default values.
			grpUserDetails.Text = "New User";
			txtName.Text = String.Empty;
			txtSurname.Text = String.Empty;
			txtContactNumber.Text = String.Empty;
			dateDateOfBirth.Value = DateTime.Today;
			cbAdminLevel.SelectedIndex = 0;
			txtLoginUserName.Text = String.Empty;
			txtPassword.Text = String.Empty;
			txtConfirmPassword.Text = String.Empty;
			chkActive.Checked = true;
			btnSave.Text = "Add User";
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancelUserDetailsChange_Click( object sender, EventArgs e )
		{
			// Enable/disable controls.
			cbSelectUsers.Enabled = true;
			btnAddUser.Enabled = true;
			btnCancelUserDetailsChange.Enabled = false;

			//Refills the users list.
			FillUserDropDown();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dropdownSelectUsers_SelectedIndexChanged( object sender, EventArgs e )
		{
			UserDetails selectedUser = _users[ cbSelectUsers.SelectedIndex ];

			txtName.Text = selectedUser.Name;
			txtSurname.Text = selectedUser.Surname;
			txtContactNumber.Text = selectedUser.contactNumber;
			dateDateOfBirth.Value = selectedUser.DateOfBirth;
			cbAdminLevel.SelectedIndex = selectedUser.adminLevel;
			txtLoginUserName.Text = selectedUser.userName;
			txtPassword.Text = selectedUser.userPassword;
			txtConfirmPassword.Text = selectedUser.userPassword;
			chkActive.Checked = selectedUser.IsActive;
			btnSave.Text = "Update User";
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click( object sender, EventArgs e )
		{
			try
			{
				//Check that required details have been provided.
				if ( String.IsNullOrEmpty( txtName.Text.Trim() ) ) throw new Exception( "Please enter name." );
				if ( String.IsNullOrEmpty( txtSurname.Text.Trim() ) ) throw new Exception( "Please enter surname." );
				if ( String.IsNullOrEmpty( txtLoginUserName.Text.Trim() ) ) throw new Exception( "Please enter login username." );
				if ( String.IsNullOrEmpty( dateDateOfBirth.Text.Trim() ) ) throw new Exception( "Please enter date of birth." );
				if ( String.IsNullOrEmpty( txtContactNumber.Text.Trim() ) ) throw new Exception( "Please enter contact number." );
				if ( String.IsNullOrEmpty( txtPassword.Text.Trim() ) ) throw new Exception( "Please enter password." );
				if ( txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim() )
				{
					txtPassword.Focus();
					throw new Exception( "Passwords do not match." );
				}

				if ( btnSave.Text.Equals( "Update User" ) ) // Update existing user
				{
					using ( SqlConnection cn = new SqlConnection( connectionString ) )
					{
						if ( cn.State == ConnectionState.Closed ) { cn.Open(); }

						using ( SqlCommand cmd = new SqlCommand( "UpdateUser", cn ) )
						{
							cmd.CommandType = CommandType.StoredProcedure;
							cmd.Parameters.Add( "@userGuid", SqlDbType.UniqueIdentifier ).Value = _users[ cbSelectUsers.SelectedIndex ].userGuid;
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

							//Fills the dropdown again.
							FillUserDropDown();
						}
					}
				}
				else // Add new user
				{
					using ( SqlConnection cn = new SqlConnection( connectionString ) )
					{
						if ( cn.State == ConnectionState.Closed ) { cn.Open(); }

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
							cbSelectUsers.Enabled = true;

							//Fills the dropdown again.
							FillUserDropDown();
						}
					}
				}
			}
			catch ( Exception ex )
			{
				MessageBox.Show( "Error:  " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}
		#endregion User Setting Events

		#region Attendance Events
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cbbSession_SelectedIndexChanged( object sender, EventArgs e )
		{

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cbbMovieName_SelectedIndexChanged( object sender, EventArgs e )
		{
			if ( cbSelectMovie.SelectedItem.ToString() != "" )
			{
				//Load Movie Session Info from tInkScreenMovie table
				MovieDetails selectedMovie = null;

				selectedMovie = _nonArchivedMovies[ cbSelectMovie.SelectedIndex ];

				lblMovieTitle.Text = selectedMovie.MovieName;
				lblMovieDuration.Text = selectedMovie.Duration.ToString();
				lblMovieType.Text = selectedMovie.Type;
				lblMovieDirector.Text = selectedMovie.Director;
				lblMovieProducer.Text = selectedMovie.Producer;
				lblMovieBBFCRating.Text = selectedMovie.BbfcRate;
				lblMovieDesc.Text = selectedMovie.Description;

				//Load items into the session list
				List<string> sessionList = new List<string>();

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


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnBook_Click( object sender, EventArgs e )
		{
			int NumOfTickets;
			if ( cbSelectMovie.SelectedText == "" )
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


		/// <summary>
		/// 
		/// </summary>
		/// <param name="Mode"></param>
		public void MovieButtonControl( int Mode )
		{
			//Mode 0: Disable, Mode 1: Enable 
			if ( Mode == 0 )
			{
				cbSelectMovie.Enabled = false;
				cbbSession.Enabled = false;
				txtNumOfTickets.Enabled = false;
				btnBook.Enabled = false;
			}
			else
			{
				cbSelectMovie.Enabled = true;
				cbbSession.Enabled = true;
				txtNumOfTickets.Enabled = true;
				btnBook.Enabled = true;
			}
		}
		#endregion Attendance Events

		#region Movie Setting Events
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddMovie_Click( object sender, EventArgs e )
		{
			// Enable/disable controls.
			cbSelectEditMovie.Enabled = false;
			btnAddMovie.Enabled = false;
			btnCancelMovieCreate.Enabled = true;

			// Set default values.
			grpMovieDetails.Text = "New Movie";
			txtMovieName.Text = String.Empty;
			txtDirector.Text = String.Empty;
			txtProducer.Text = String.Empty;
			txtMovieType.Text = String.Empty;
			cbArchived.Checked = false;
			txtDuration.Text = String.Empty;
			ddlExpectedAudience.SelectedIndex = 0;
			ddlBBFCRating.SelectedIndex = 0;
			txtDescription.Text = String.Empty;
			btnSaveMovieChanges.Text = "Add Movie";
			spnrMovieYear.Value = DateTime.Today.Year;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancelMovieCreate_Click( object sender, EventArgs e )
		{
			// Enable/disable controls.
			cbSelectEditMovie.Enabled = true;
			btnAddMovie.Enabled = true;
			btnCancelMovieCreate.Enabled = false;

			//Refills the users list.
			FillMovieDropDown();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ddlMovies_SelectedIndexChanged( object sender, EventArgs e )
		{
			MovieDetails selectedMovie = _movies[ cbSelectEditMovie.SelectedIndex ];

			txtMovieName.Text = selectedMovie.MovieName;
			txtDirector.Text = selectedMovie.Director;
			txtProducer.Text = selectedMovie.Producer;
			txtMovieType.Text = selectedMovie.Type;
			txtDuration.Text = selectedMovie.Duration.ToString();
			ddlExpectedAudience.SelectedIndex = ddlExpectedAudience.Items.IndexOf( selectedMovie.ExpectedAudience );
			ddlBBFCRating.SelectedIndex = ddlBBFCRating.Items.IndexOf( selectedMovie.BbfcRate );
			cbArchived.Checked = selectedMovie.IsArchived;
			txtDescription.Text = selectedMovie.Description;
			btnSaveMovieChanges.Text = "Update Movie";
			spnrMovieYear.Value = selectedMovie.YearReleased;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSaveMovieChanges_Click( object sender, EventArgs e )
		{
			if ( btnSaveMovieChanges.Text.Equals( "Update Movie" ) ) // Update movie
			{
				using ( SqlConnection cn = new SqlConnection( connectionString ) )
				{
					if ( cn.State == ConnectionState.Closed ) cn.Open();

					using ( SqlCommand cmd = new SqlCommand( "UpdateMovie", cn ) )
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Add( "@movieGuid", SqlDbType.UniqueIdentifier ).Value = _movies[ cbSelectEditMovie.SelectedIndex ].MovieGuid;
						cmd.Parameters.Add( "@name", SqlDbType.NVarChar ).Value = txtMovieName.Text.Trim();
						cmd.Parameters.Add( "@directors", SqlDbType.NVarChar ).Value = txtDirector.Text.Trim();
						cmd.Parameters.Add( "@producers", SqlDbType.NVarChar ).Value = txtProducer.Text.Trim();
						cmd.Parameters.Add( "@type", SqlDbType.NVarChar ).Value = txtMovieType.Text.Trim();
						cmd.Parameters.Add( "@duration", SqlDbType.Int ).Value = Convert.ToInt32( txtDuration.Text.Trim() );
						cmd.Parameters.Add( "@expectedAudience", SqlDbType.NVarChar ).Value = ddlExpectedAudience.SelectedItem;
						cmd.Parameters.Add( "@rating", SqlDbType.NVarChar ).Value = ddlBBFCRating.SelectedItem;
						cmd.Parameters.Add( "@archived", SqlDbType.Bit ).Value = cbArchived.Checked;
						cmd.Parameters.Add( "@description", SqlDbType.NVarChar ).Value = txtDescription.Text.Trim();
						cmd.Parameters.Add( "@year", SqlDbType.Int ).Value = DateTime.Now.Year;
						cmd.ExecuteNonQuery();

						MessageBox.Show( "Movie successfully updated.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information );

						//Fills the dropdown again.
						FillMovieDropDown();
					}
				}
			}
			else // Add movie
			{
				using ( SqlConnection cn = new SqlConnection( connectionString ) )
				{
					if ( cn.State == ConnectionState.Closed ) cn.Open();

					using ( SqlCommand cmd = new SqlCommand( "CreateMovie", cn ) )
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Add( "@name", SqlDbType.NVarChar ).Value = txtMovieName.Text.Trim();
						cmd.Parameters.Add( "@directors", SqlDbType.NVarChar ).Value = txtDirector.Text.Trim();
						cmd.Parameters.Add( "@producers", SqlDbType.NVarChar ).Value = txtProducer.Text.Trim();
						cmd.Parameters.Add( "@type", SqlDbType.NVarChar ).Value = txtMovieType.Text.Trim();
						cmd.Parameters.Add( "@duration", SqlDbType.Int ).Value = Convert.ToInt32( txtDuration.Text.Trim() );
						cmd.Parameters.Add( "@expectedAudience", SqlDbType.NVarChar ).Value = ddlExpectedAudience.SelectedItem;
						cmd.Parameters.Add( "@rating", SqlDbType.NVarChar ).Value = ddlBBFCRating.SelectedItem;
						cmd.Parameters.Add( "@archived", SqlDbType.Bit ).Value = cbArchived.Checked;
						cmd.Parameters.Add( "@description", SqlDbType.NVarChar ).Value = txtDescription.Text.Trim();
						cmd.Parameters.Add( "@year", SqlDbType.Int ).Value = DateTime.Now.Year;
						cmd.ExecuteNonQuery();

						MessageBox.Show( "Movie successfully created.", "Create Success", MessageBoxButtons.OK, MessageBoxIcon.Information );

						//Re-enables buttons.
						btnCancelMovieCreate.Enabled = false;
						btnAddMovie.Enabled = true;
						cbSelectEditMovie.Enabled = true;

						//Fills the dropdown again.
						FillMovieDropDown();
					}
				}
			}
		}
		#endregion Movie Setting Events

		#region Distributor Setting Events
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cbSelectDistributor_SelectedIndexChanged( object sender, EventArgs e )
		{
			DistributorDetails selectedDistributor = _distributors[ cbSelectDistributor.SelectedIndex ];

			txtDistrName.Text = selectedDistributor.Name;
			txtDistrContactNumber.Text = selectedDistributor.ContactNumber;
			txtDistrEmail.Text = selectedDistributor.Email;
			txtDistrPhysicalAddress.Text = selectedDistributor.PhysicalAddress;
			chkActive.Checked = selectedDistributor.IsActive;
			ckbMontly.Checked = selectedDistributor.ReceiveMontly;
			ckbQuaterly.Checked = selectedDistributor.ReceiveQuaterly;
			ckbHalfYearly.Checked = selectedDistributor.ReceiveHalfYearly;
			ckbYearly.Checked = selectedDistributor.ReceiveYearly;

			btnDistrSave.Text = "Update Distributor";
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddDistributor_Click( object sender, EventArgs e )
		{
			// Enable/disable controls.
			cbSelectDistributor.Enabled = false;
			btnAddDistributor.Enabled = false;
			btnDistrCancel.Enabled = true;
			cbUnlinkedMovies.Enabled = false;
			btnLinkMovie.Enabled = false;

			// Set default values.
			grpDistributor.Text = "New Distributor";
			txtDistrName.Text = String.Empty;
			txtDistrEmail.Text = String.Empty;
			txtDistrContactNumber.Text = String.Empty;
			txtDistrPhysicalAddress.Text = String.Empty;
			ckbDistrActive.Checked = true;
			ckbMontly.Checked = false;
			ckbQuaterly.Checked = false;
			ckbHalfYearly.Checked = false;
			ckbYearly.Checked = false;
			btnDistrSave.Text = "Add Distributor";
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDistrSave_Click( object sender, EventArgs e )
		{
			try
			{
				//Check that required details have been provided.
				if ( String.IsNullOrEmpty( txtDistrName.Text.Trim() ) ) throw new Exception( "Please enter a name for the distributor" );
				if ( String.IsNullOrEmpty( txtDistrContactNumber.Text.Trim() ) ) throw new Exception( "Please enter a contact number for the distributor" );
				if ( String.IsNullOrEmpty( txtDistrEmail.Text.Trim() ) ) throw new Exception( "Please enter an email for the distributor" );

				String action = btnDistrSave.Text.Equals( "Update Distributor" ) ? "updated" : "created";

				using ( SqlConnection cn = new SqlConnection( connectionString ) )
				{
					if ( cn.State == ConnectionState.Closed ) cn.Open();

					using ( SqlCommand cmd = new SqlCommand( "CreateUpdateDistributor", cn ) )
					{
						Guid newId = Guid.NewGuid();
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Add( "@id", SqlDbType.UniqueIdentifier ).Value = cbSelectDistributor.Enabled && _distributors.Count > 0 ? _distributors[ cbSelectDistributor.SelectedIndex ].Id : newId;
						cmd.Parameters.Add( "@name", SqlDbType.NVarChar ).Value = txtDistrName.Text.Trim();
						cmd.Parameters.Add( "@active", SqlDbType.Bit ).Value = ckbDistrActive.Checked;
						cmd.Parameters.Add( "@email", SqlDbType.NVarChar ).Value = txtDistrEmail.Text.Trim();
						cmd.Parameters.Add( "@contactNumber", SqlDbType.NVarChar ).Value = txtDistrContactNumber.Text.Trim();
						cmd.Parameters.Add( "@address", SqlDbType.NVarChar ).Value = txtDistrPhysicalAddress.Text.Trim();
						cmd.Parameters.Add( "@monthly", SqlDbType.Bit ).Value = ckbMontly.Checked;
						cmd.Parameters.Add( "@quaterly", SqlDbType.Bit ).Value = ckbQuaterly.Checked;
						cmd.Parameters.Add( "@halfYearly", SqlDbType.Bit ).Value = ckbHalfYearly.Checked;
						cmd.Parameters.Add( "@yearly", SqlDbType.Bit ).Value = ckbYearly.Checked;
						cmd.ExecuteNonQuery();

						MessageBox.Show( "Distributor successfully " + action + ".", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information );
						Int32 position = cbSelectDistributor.Items.Count - 1;

						if ( action.Equals( "created" ) )
						{
							//Re-enables buttons.
							btnDistrCancel.Enabled = false;
							btnAddDistributor.Enabled = true;
							cbSelectDistributor.Enabled = true;
							cbSelectDistributor.Items.Add( txtDistrName.Text.Trim() );
							position = position + 1;
						}
						else
						{
							position = cbSelectDistributor.SelectedIndex;
						}

						//Fills the dropdown again.
						FillDistributorDropDown();
					}
				}
			}
			catch ( Exception ex )
			{
				MessageBox.Show( "Error:  " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDistrCancel_Click( object sender, EventArgs e )
		{
			// Enable/disable controls.
			cbSelectDistributor.Enabled = true;
			btnAddDistributor.Enabled = true;
			btnDistrCancel.Enabled = false;

			//Refills the users list.
			FillDistributorDropDown();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnLinkMovie_Click( object sender, EventArgs e )
		{
			try
			{
				//Link the distributor to the movie.
				using ( SqlConnection cn = new SqlConnection( connectionString ) )
				{
					if ( cn.State == ConnectionState.Closed ) cn.Open();

					using ( SqlCommand cmd = new SqlCommand( "LinkMovieDistributor", cn ) )
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Add( "@movieName", SqlDbType.NVarChar ).Value = cbUnlinkedMovies.SelectedItem;
						cmd.Parameters.Add( "@distributorGuid", SqlDbType.UniqueIdentifier ).Value = _distributors[ cbSelectDistributor.SelectedIndex ].Id;
						cmd.ExecuteNonQuery();

						//Remove the selecteditem from the drop down list.
						cbUnlinkedMovies.Items.RemoveAt( cbUnlinkedMovies.SelectedIndex );

						MessageBox.Show( "Movie successfully linked to distributor", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information );
					}
				}
			}
			catch ( Exception ex )
			{
				MessageBox.Show( "Error:  " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}
		#endregion Distributor Setting Events

		#region Methods
		/// <summary>
		/// 
		/// </summary>
		private void FillUserDropDown()
		{
			try
			{
				int selectedIndex = 0;

				if ( cbSelectUsers.Items.Count > 0 )
				{
					selectedIndex = cbSelectUsers.SelectedIndex;
				}

				//Clears the current list.
				cbSelectUsers.Items.Clear();
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
								String contactNumber = dr[ "ContactNumber" ].ToString();

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
								cbSelectUsers.Items.Add( user.ToString() );
							}
							//Always select the 1st item in the dropdown as default.
							if ( cbSelectUsers.Items.Count > 0 )
							{
								cbSelectUsers.SelectedIndex = selectedIndex;
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
		/// 
		/// </summary>
		private void FillMovieDropDown()
		{
			try
			{
				int selectedIndex = 0;

				if ( cbSelectEditMovie.Items.Count > 0 )
				{
					selectedIndex = cbSelectEditMovie.SelectedIndex;
				}

				//Clears the current list.
				cbSelectEditMovie.Items.Clear();
				//Clear the List object declared at the top.
				_movies.Clear();

				//Clears the current list.
				cbSelectMovie.Items.Clear();
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
								Guid movieGuid = dr.GetGuid( dr.GetOrdinal( "MovieId" ) );

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
								movie.MovieGuid = movieGuid;

								//Adds non-archived movies to the list created above.
								if ( !archived )
								{
									_nonArchivedMovies.Add( movie );
									cbSelectMovie.Items.Add( movie.MovieName );
								}
								//Adds the user to the list declared at the top.
								_movies.Add( movie );
								//Adds the user to the dropdown list.
								cbSelectEditMovie.Items.Add( movie.MovieName );
							}

							//Always select the 1st item in the dropdown as default.
							if ( cbSelectEditMovie.Items.Count > 0 )
							{
								cbSelectEditMovie.SelectedIndex = selectedIndex;
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
		/// 
		/// </summary>
		private void FillDistributorDropDown()
		{
			try
			{
				int selectedIndex = 0;

				if ( cbSelectDistributor.Items.Count > 0 )
				{
					selectedIndex = cbSelectDistributor.SelectedIndex;
				}

				//Clears the current list.
				cbSelectDistributor.Items.Clear();
				//Clear the List object declared at the top.
				_distributors.Clear();

				//Create new connection.
				using ( SqlConnection cn = new SqlConnection( connectionString ) )
				{
					//If the connection is closed, open it.
					if ( cn.State == ConnectionState.Closed ) cn.Open();

					//Gets all the users that the logged in user is allowed to view and adds it to the dropdown list.
					using ( SqlCommand cmd = new SqlCommand( "GetDistributorList", cn ) )
					{
						cmd.CommandType = CommandType.StoredProcedure;

						using ( SqlDataReader dr = cmd.ExecuteReader() )
						{
							while ( dr.Read() )
							{
								Guid id = dr.GetGuid( dr.GetOrdinal( "DistributorGuid" ) );
								String name = dr.GetString( dr.GetOrdinal( "Name" ) );
								Boolean active = dr.GetBoolean( dr.GetOrdinal( "IsActive" ) );
								String email = dr.GetString( dr.GetOrdinal( "Email" ) );
								String contactNumber = dr.GetString( dr.GetOrdinal( "ContactNumber" ) );
								String physicalAddress = dr.GetString( dr.GetOrdinal( "PhysicalAddress" ) );
								Boolean receiveMonthly = dr.GetBoolean( dr.GetOrdinal( "ReceiveMontly" ) );
								Boolean receiveQuaterly = dr.GetBoolean( dr.GetOrdinal( "ReceiveQuaterly" ) );
								Boolean receiveHalfYearly = dr.GetBoolean( dr.GetOrdinal( "ReceiveHalfYearly" ) );
								Boolean receiveYearly = dr.GetBoolean( dr.GetOrdinal( "ReceiveYearly" ) );

								//Create new distributor instance and sets its details.
								DistributorDetails distributor = new DistributorDetails()
								{
									Id = id,
									Name = name,
									IsActive = active,
									Email = email,
									ContactNumber = contactNumber,
									PhysicalAddress = physicalAddress,
									ReceiveMontly = receiveMonthly,
									ReceiveQuaterly = receiveQuaterly,
									ReceiveHalfYearly = receiveHalfYearly,
									ReceiveYearly = receiveYearly
								};
								//Adds the user to the list declared at the top.
								_distributors.Add( distributor );
								//Adds the user to the dropdown list.
								cbSelectDistributor.Items.Add( distributor.Name );
							}

							//Always select the 1st item in the dropdown as default.
							if ( cbSelectDistributor.Items.Count > 0 )
							{
								cbSelectDistributor.SelectedIndex = selectedIndex;
							}
						}
					}
				}
				//Gets a list of all the unlinked movies.
				GetUnlinkedMovies();
				if ( cbSelectDistributor.Items.Count < 1 || !btnAddDistributor.Enabled || cbUnlinkedMovies.Items.Count < 1 )
				{
					cbUnlinkedMovies.Enabled = false;
					btnLinkMovie.Enabled = false;
				}
				else
				{
					cbUnlinkedMovies.Enabled = true;
					btnLinkMovie.Enabled = true;
				}
			}
			catch ( Exception ex )
			{
				MessageBox.Show( "Error:  " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}


		/// <summary>
		/// Gets all the active movies that have not been linked to a distributor.
		/// </summary>
		public void GetUnlinkedMovies()
		{
			try
			{
				//Clear the items.
				cbUnlinkedMovies.Items.Clear();
				using ( SqlConnection cn = new SqlConnection( connectionString ) )
				{
					if ( cn.State == ConnectionState.Closed ) cn.Open();

					using ( SqlCommand cmd = new SqlCommand( "GetMovieList", cn ) )
					{
						using ( SqlDataReader dr = cmd.ExecuteReader() )
						{
							while ( dr.Read() )
							{
								String movieName = dr.GetString( dr.GetOrdinal( "Name" ) );
								String movieDistributor = dr.GetString( dr.GetOrdinal( "DistributorId" ) );
								Boolean isArchived = dr.GetBoolean( dr.GetOrdinal( "IsArchived" ) );

								//If there is no distributor linked to the active movie then add it.
								if ( String.IsNullOrEmpty( movieDistributor ) && !isArchived )
								{
									cbUnlinkedMovies.Items.Add( movieName );
								}
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
		/// Resets all the movie detail fieds.
		/// </summary>
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
		#endregion Methods
	}
}
