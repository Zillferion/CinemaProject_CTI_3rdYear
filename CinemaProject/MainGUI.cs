﻿using System;
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
        #region Enums

        private enum UserType
        {
            Employee = 0,
            Managrer = 1,
            Admin = 2
        }

        private enum ExpectedAudience
        {
            Mainstream = 0,
            Average = 1,
            Below_Average = 2,
            Specialist = 3,
            Non_Mainstream = 4
        }

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

        private static List<UserDetails> _users = new List<UserDetails>();
        private static List<MovieDetails> _movies = new List<MovieDetails>();
        private static List<MovieDetails> _nonArchivedMovies = new List<MovieDetails>();
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
        private void MainGUI_Load(object sender, EventArgs e)
        {
            //Hide tabs based on Admin Level.
            switch (UserInformation.AdminLevel)
            {
                //User is an Employee.
                case 0:
                    tbcUserRoles.TabPages.Remove(tbUserSettings);
                    tbcUserRoles.TabPages.Remove(tbMovieSettings);
                    tbcUserRoles.TabPages.Remove(tbDistributors);
                    FillMovieDropDown();
                    break;
                //User is a Manager.
                case 1:
                    tbcUserRoles.TabPages.Remove(tbUserSettings);
                    tbcUserRoles.TabPages.Remove(tbDistributors);
                    FillMovieDropDown();
                    break;
                //User is an Admin.
                case 2:
                    FillUserDropDown();
                    FillMovieDropDown();
                    //TODO:  FillDistributorDropDown();
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
        private void MainGUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Exit the application.
            Application.Exit();
        }

        #endregion General Events

        #region User Setting Events

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            // Enable/disable controls.
            dropdownSelectUsers.Enabled = false;
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

        private void btnCancelUserDetailsChange_Click(object sender, EventArgs e)
        {
            // Enable/disable controls.
            dropdownSelectUsers.Enabled = true;
            btnAddUser.Enabled = true;
            btnCancelUserDetailsChange.Enabled = false;

            //Refills the users list.
            FillUserDropDown();
        }

        private void dropdownSelectUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDetails selectedUser = _users[dropdownSelectUsers.SelectedIndex];

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                MessageBox.Show("Passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }
            if (btnSave.Text.Equals("Update User")) // Update existing user
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }

                    using (SqlCommand cmd = new SqlCommand("UpdateUser", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@userGuid", SqlDbType.UniqueIdentifier).Value = _users[dropdownSelectUsers.SelectedIndex].userGuid;
                        cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = txtName.Text.Trim();
                        cmd.Parameters.Add("@surname", SqlDbType.NVarChar).Value = txtSurname.Text.Trim();
                        cmd.Parameters.Add("@adminLevel", SqlDbType.Int).Value = cbAdminLevel.SelectedIndex;
                        cmd.Parameters.Add("@contactNumber", SqlDbType.NVarChar).Value = txtContactNumber.Text.Trim();
                        cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = txtPassword.Text.Trim();
                        cmd.Parameters.Add("@loginName", SqlDbType.NVarChar).Value = txtLoginUserName.Text.Trim();
                        cmd.Parameters.Add("@dateOfBirth", SqlDbType.DateTime).Value = dateDateOfBirth.Text;
                        cmd.Parameters.Add("@active", SqlDbType.Bit).Value = chkActive.Checked;
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("User successfully updated.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Fills the dropdown again.
                        FillUserDropDown();
                    }
                }
            }
            else // Add new user
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }

                    using (SqlCommand cmd = new SqlCommand("CreateUser", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = txtName.Text.Trim();
                        cmd.Parameters.Add("@surname", SqlDbType.NVarChar).Value = txtSurname.Text.Trim();
                        cmd.Parameters.Add("@adminLevel", SqlDbType.Int).Value = cbAdminLevel.SelectedIndex;
                        cmd.Parameters.Add("@contactNumber", SqlDbType.NVarChar).Value = txtContactNumber.Text.Trim();
                        cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = txtPassword.Text.Trim();
                        cmd.Parameters.Add("@loginName", SqlDbType.NVarChar).Value = txtLoginUserName.Text.Trim();
                        cmd.Parameters.Add("@dateOfBirth", SqlDbType.DateTime).Value = dateDateOfBirth.Text;
                        cmd.Parameters.Add("@active", SqlDbType.Bit).Value = chkActive.Checked;
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("User successfully created.", "Create Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        #endregion User Setting Events

        #region Attendance Events

        private void cbbSession_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbMovieName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbMovieName.SelectedItem.ToString() != "")
            {
                //Load Movie Session Info from tInkScreenMovie table
                MovieDetails selectedMovie = null;

                selectedMovie = _nonArchivedMovies[cbbMovieName.SelectedIndex];

                lblMovieTitle.Text = selectedMovie.MovieName;
                lblMovieDuration.Text = selectedMovie.Duration.ToString();
                lblMovieType.Text = selectedMovie.Type;
                lblMovieDirector.Text = selectedMovie.Director;
                lblMovieProducer.Text = selectedMovie.Producer;
                lblMovieBBFCRating.Text = selectedMovie.BbfcRate;
                lblMovieDesc.Text = selectedMovie.Description;

                //Load items into the session list
                List<string> sessionList = new List<string>();

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

        private void btnBook_Click(object sender, EventArgs e)
        {
            int NumOfTickets;
            if (cbbMovieName.SelectedText == "")
            {
                MessageBox.Show("No movie selected for booking!");
            }
            else if (cbbSession.SelectedText == "")
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

        #endregion Attendance Events

        #region Movie Setting Events

        private void btnAddMovie_Click(object sender, EventArgs e)
        {
            // Enable/disable controls.
            ddlMovies.Enabled = false;
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


        private void btnCancelMovieCreate_Click(object sender, EventArgs e)
        {
            // Enable/disable controls.
            ddlMovies.Enabled = true;
            btnAddMovie.Enabled = true;
            btnCancelMovieCreate.Enabled = false;

            //Refills the users list.
            FillMovieDropDown();
        }


        private void ddlMovies_SelectedIndexChanged(object sender, EventArgs e)
        {
            MovieDetails selectedMovie = _movies[ddlMovies.SelectedIndex];

            txtMovieName.Text = selectedMovie.MovieName;
            txtDirector.Text = selectedMovie.Director;
            txtProducer.Text = selectedMovie.Producer;
            txtMovieType.Text = selectedMovie.Type;
            txtDuration.Text = selectedMovie.Duration.ToString();
            ddlExpectedAudience.SelectedIndex = ddlExpectedAudience.Items.IndexOf(selectedMovie.ExpectedAudience);
            ddlBBFCRating.SelectedIndex = ddlBBFCRating.Items.IndexOf(selectedMovie.BbfcRate);
            cbArchived.Checked = selectedMovie.IsArchived;
            txtDescription.Text = selectedMovie.Description;
            btnSaveMovieChanges.Text = "Update Movie";
            spnrMovieYear.Value = selectedMovie.YearReleased;
        }


        private void btnSaveMovieChanges_Click(object sender, EventArgs e)
        {
            if (btnSaveMovieChanges.Text.Equals("Update Movie")) // Update movie
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();

                    using (SqlCommand cmd = new SqlCommand("UpdateMovie", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@movieGuid", SqlDbType.UniqueIdentifier).Value = _movies[ddlMovies.SelectedIndex].MovieGuid;
                        cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = txtMovieName.Text.Trim();
                        cmd.Parameters.Add("@directors", SqlDbType.NVarChar).Value = txtDirector.Text.Trim();
                        cmd.Parameters.Add("@producers", SqlDbType.NVarChar).Value = txtProducer.Text.Trim();
                        cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = txtMovieType.Text.Trim();
                        cmd.Parameters.Add("@duration", SqlDbType.Int).Value = Convert.ToInt32(txtDuration.Text.Trim());
                        cmd.Parameters.Add("@expectedAudience", SqlDbType.NVarChar).Value = ddlExpectedAudience.SelectedItem;
                        cmd.Parameters.Add("@rating", SqlDbType.NVarChar).Value = ddlBBFCRating.SelectedItem;
                        cmd.Parameters.Add("@archived", SqlDbType.Bit).Value = cbArchived.Checked;
                        cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = txtDescription.Text.Trim();
                        cmd.Parameters.Add("@year", SqlDbType.Int).Value = DateTime.Now.Year;
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Movie successfully updated.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Fills the dropdown again.
                        FillMovieDropDown();
                    }
                }
            }
            else // Add movie
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();

                    using (SqlCommand cmd = new SqlCommand("CreateMovie", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = txtMovieName.Text.Trim();
                        cmd.Parameters.Add("@directors", SqlDbType.NVarChar).Value = txtDirector.Text.Trim();
                        cmd.Parameters.Add("@producers", SqlDbType.NVarChar).Value = txtProducer.Text.Trim();
                        cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = txtMovieType.Text.Trim();
                        cmd.Parameters.Add("@duration", SqlDbType.Int).Value = Convert.ToInt32(txtDuration.Text.Trim());
                        cmd.Parameters.Add("@expectedAudience", SqlDbType.NVarChar).Value = ddlExpectedAudience.SelectedItem;
                        cmd.Parameters.Add("@rating", SqlDbType.NVarChar).Value = ddlBBFCRating.SelectedItem;
                        cmd.Parameters.Add("@archived", SqlDbType.Bit).Value = cbArchived.Checked;
                        cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = txtDescription.Text.Trim();
                        cmd.Parameters.Add("@year", SqlDbType.Int).Value = DateTime.Now.Year;
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Movie successfully created.", "Create Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Re-enables buttons.
                        btnCancelMovieCreate.Enabled = false;
                        btnAddMovie.Enabled = true;
                        ddlMovies.Enabled = true;

                        //Fills the dropdown again.
                        FillMovieDropDown();
                    }
                }
            }
        }

        #endregion Movie Setting Events

        #region Methods

        /// <summary>
        /// Clears the current User list, refills it and then goes to the 1st item.
        /// </summary>
        private void FillUserDropDown()
        {
            try
            {
                int selectedIndex = 0;

                if (dropdownSelectUsers.Items.Count > 0)
                {
                    selectedIndex = dropdownSelectUsers.SelectedIndex;
                }

                //Clears the current list.
                dropdownSelectUsers.Items.Clear();
                //Clear the List object declared at the top.
                _users.Clear();

                //Create new connection.
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    //If the connection is closed, open it.
                    if (cn.State == ConnectionState.Closed) cn.Open();

                    //Gets all the users that the logged in user is allowed to view and adds it to the dropdown list.
                    using (SqlCommand cmd = new SqlCommand("GetUserList", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@adminLevel", SqlDbType.Int).Value = UserInformation.AdminLevel;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                String name = dr.GetString(dr.GetOrdinal("UserName"));
                                String surname = dr.GetString(dr.GetOrdinal("UserSurname"));
                                DateTime doB = dr.GetDateTime(dr.GetOrdinal("UserDoB"));
                                Int32 adminLvl = dr.GetInt32(dr.GetOrdinal("AdminLevel"));
                                Boolean active = dr.GetBoolean(dr.GetOrdinal("bActive"));
                                DateTime registered = dr.GetDateTime(dr.GetOrdinal("Created"));
                                String username = dr.GetString(dr.GetOrdinal("LoginUsername"));
                                String password = dr.GetString(dr.GetOrdinal("LoginPassword"));
                                Guid userGuid = dr.GetGuid(dr.GetOrdinal("UserGuid"));
                                String contactNumber = dr["ContactNumber"].ToString();

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
                                _users.Add(user);
                                //Adds the user to the dropdown list.
                                dropdownSelectUsers.Items.Add(user.ToString());
                            }
                            //Always select the 1st item in the dropdown as default.
                            if (dropdownSelectUsers.Items.Count > 0)
                            {
                                dropdownSelectUsers.SelectedIndex = selectedIndex;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Clears the current Movie list, refills it and then goes to the 1st item.
        /// </summary>
        private void FillMovieDropDown()
        {
            try
            {
                int selectedIndex = 0;

                if (ddlMovies.Items.Count > 0)
                {
                    selectedIndex = ddlMovies.SelectedIndex;
                }

                //Clears the current list.
                ddlMovies.Items.Clear();
                //Clear the List object declared at the top.
                _movies.Clear();

                //Clears the current list.
                cbbMovieName.Items.Clear();
                //Clear the List object declared at the top.
                _nonArchivedMovies.Clear();

                //Create new connection.
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    //If the connection is closed, open it.
                    if (cn.State == ConnectionState.Closed) cn.Open();

                    //Gets all the users that the logged in user is allowed to view and adds it to the dropdown list.
                    using (SqlCommand cmd = new SqlCommand("GetMovieList", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                String name = dr.GetString(dr.GetOrdinal("Name"));
                                Int32 duration = dr.GetInt32(dr.GetOrdinal("Duration"));
                                Int32 releaseYear = dr.GetInt32(dr.GetOrdinal("YearProduced"));
                                String director = dr.GetString(dr.GetOrdinal("Director"));
                                String producer = dr.GetString(dr.GetOrdinal("Producer"));
                                String type = dr.GetString(dr.GetOrdinal("Type"));
                                String expectedAudience = dr.GetString(dr.GetOrdinal("ExpectedAudience"));
                                String rating = dr.GetString(dr.GetOrdinal("BBFC_Rate"));
                                Boolean archived = dr.GetBoolean(dr.GetOrdinal("IsArchived"));
                                String description = dr.GetString(dr.GetOrdinal("Description"));
                                Guid movieGuid = dr.GetGuid(dr.GetOrdinal("MovieId"));

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
                                if (!archived)
                                {
                                    _nonArchivedMovies.Add(movie);
                                    cbbMovieName.Items.Add(movie.MovieName);
                                }
                                //Adds the user to the list declared at the top.
                                _movies.Add(movie);
                                //Adds the user to the dropdown list.
                                ddlMovies.Items.Add(movie.MovieName);
                            }

                            //Always select the 1st item in the dropdown as default.
                            if (ddlMovies.Items.Count > 0)
                            {
                                ddlMovies.SelectedIndex = selectedIndex;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #endregion Methods

    }
}
