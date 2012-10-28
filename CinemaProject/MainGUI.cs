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
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace CinemaProject
{
    /*
     * 1.  Add comments everywhere.
     * 2.  Optimize code to get rid of un-needed Member Variables.
     * 3.  Make new movie/user/distributor the selected user after creation.
     * 4.  Show list of movies a distributor is linked to so that items can be removed.
     * 5.  Add log-out button.
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
        /// Creates a worksheet to generate report
        /// </summary>
        private Microsoft.Office.Interop.Excel.Range range = null;
        object misValue = System.Reflection.Missing.Value;

        private Microsoft.Office.Interop.Excel.Application _app;
        public Microsoft.Office.Interop.Excel.Application App
        {
            get
            {
                if (_app == null)
                {
                    bool flag = false;
                    foreach (var item in System.Diagnostics.Process.GetProcesses())
                    {
                        if (item.ProcessName == "EXCEL")
                        {
                            flag = true;
                            break;
                        }
                    }

                    if (!flag)
                    {
                        _app = new Excel.Application();
                    }
                    else
                    {
                        object obj = System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");
                        _app = obj as Excel.Application;
                    }

                    _app.Visible = false;
                }

                return _app;
            }
            set { _app = value; }
        }

        private Microsoft.Office.Interop.Excel.Workbook _workbook;
        public Microsoft.Office.Interop.Excel.Workbook Workbook
        {
            get
            {
                if (_workbook == null)
                {
                    //_workbook = App.Workbooks.Add(2);
                    _workbook = App.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                                    }

                return _workbook;
            }
            set { _workbook = value; }
        }

        private Microsoft.Office.Interop.Excel.Worksheet _worksheet;
        public Microsoft.Office.Interop.Excel.Worksheet Worksheet
        {
            get
            {
                if (_worksheet == null)
                {
                    _worksheet = (Microsoft.Office.Interop.Excel.Worksheet)Workbook.Sheets[1];
                }

                return _worksheet;
            }
            set { _worksheet = value; }
        }

        private List<ScreenDetails> _screenList;
        public List<ScreenDetails> ScreenList
        {
            get
            {
                if (_screenList == null)
                {
                    _screenList = new List<ScreenDetails>();

                    //Create new connection.
                    using (SqlConnection cn = new SqlConnection(connectionString))
                    {
                        //If the connection is closed, open it.
                        if (cn.State == ConnectionState.Closed) cn.Open();

                        //Gets all the screens.
                        using (SqlCommand cmd = new SqlCommand("GetScreenList", cn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    Guid id = dr.GetGuid(dr.GetOrdinal("ScreenId"));
                                    int screenNum = dr.GetInt32(dr.GetOrdinal("ScreenNum"));
                                    int seats = dr.GetInt32(dr.GetOrdinal("Seats"));

                                    //Create new distributor instance and sets its details.
                                    ScreenDetails screen = new ScreenDetails()
                                    {
                                        ScreenID = id,
                                        ScreenNum = screenNum,
                                        Seats = seats
                                    };

                                    _screenList.Add(screen);
                                }
                            }
                        }
                    }
                }

                return _screenList;
            }
            set { _screenList = value; }
        }

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
                    tbcUserRoles.TabPages.Remove(tbScheduleScreenings);
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
        private void MainGUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Exit the application.
            System.Windows.Forms.Application.Exit();
        }
        #endregion General Events

        #region User Setting Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddUser_Click(object sender, EventArgs e)
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
        private void btnCancelUserDetailsChange_Click(object sender, EventArgs e)
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
        private void dropdownSelectUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDetails selectedUser = _users[cbSelectUsers.SelectedIndex];

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
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Check that required details have been provided.
                if (String.IsNullOrEmpty(txtName.Text.Trim())) throw new Exception("Please enter name.");
                if (String.IsNullOrEmpty(txtSurname.Text.Trim())) throw new Exception("Please enter surname.");
                if (String.IsNullOrEmpty(txtLoginUserName.Text.Trim())) throw new Exception("Please enter login username.");
                if (String.IsNullOrEmpty(dateDateOfBirth.Text.Trim())) throw new Exception("Please enter date of birth.");
                if (String.IsNullOrEmpty(txtContactNumber.Text.Trim())) throw new Exception("Please enter contact number.");
                if (String.IsNullOrEmpty(txtPassword.Text.Trim())) throw new Exception("Please enter password.");
                if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
                {
                    txtPassword.Focus();
                    throw new Exception("Passwords do not match.");
                }

                if (btnSave.Text.Equals("Update User")) // Update existing user
                {
                    using (SqlConnection cn = new SqlConnection(connectionString))
                    {
                        if (cn.State == ConnectionState.Closed) { cn.Open(); }

                        using (SqlCommand cmd = new SqlCommand("UpdateUser", cn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@userGuid", SqlDbType.UniqueIdentifier).Value = _users[cbSelectUsers.SelectedIndex].userGuid;
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
                            cbSelectUsers.Enabled = true;

                            //Fills the dropdown again.
                            FillUserDropDown();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion User Setting Events

        #region Attendance Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbSession_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbMovieName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(cbAttendanceSelectMovie.SelectedItem.ToString()))
            {
                EnableMovieButtonControl(false);
            }
            else
            {
                //Load Movie Session Info from tInkScreenMovie table
                MovieDetails selectedMovie = null;

                selectedMovie = _nonArchivedMovies[cbAttendanceSelectMovie.SelectedIndex];

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

                EnableMovieButtonControl(true);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBook_Click(object sender, EventArgs e)
        {
            int NumOfTickets;
            if (cbAttendanceSelectMovie.SelectedText == "")
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Mode"></param>
        public void EnableMovieButtonControl(bool enabled)
        {
            cbAttendanceSelectMovie.Enabled = enabled;
            cbbSession.Enabled = enabled;
            txtNumOfTickets.Enabled = enabled;
            btnBook.Enabled = enabled;

        }
        #endregion Attendance Events

        #region Movie Setting Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddMovie_Click(object sender, EventArgs e)
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
        private void btnCancelMovieCreate_Click(object sender, EventArgs e)
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
        private void ddlMovies_SelectedIndexChanged(object sender, EventArgs e)
        {
            MovieDetails selectedMovie = _movies[cbSelectEditMovie.SelectedIndex];

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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        cmd.Parameters.Add("@movieGuid", SqlDbType.UniqueIdentifier).Value = _movies[cbSelectEditMovie.SelectedIndex].MovieGuid;
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
        private void cbSelectDistributor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DistributorDetails selectedDistributor = _distributors[cbSelectDistributor.SelectedIndex];

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
        private void btnAddDistributor_Click(object sender, EventArgs e)
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
        private void btnDistrSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Check that required details have been provided.
                if (String.IsNullOrEmpty(txtDistrName.Text.Trim())) throw new Exception("Please enter a name for the distributor");
                if (String.IsNullOrEmpty(txtDistrContactNumber.Text.Trim())) throw new Exception("Please enter a contact number for the distributor");
                if (String.IsNullOrEmpty(txtDistrEmail.Text.Trim())) throw new Exception("Please enter an email for the distributor");

                String action = btnDistrSave.Text.Equals("Update Distributor") ? "updated" : "created";

                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();

                    using (SqlCommand cmd = new SqlCommand("CreateUpdateDistributor", cn))
                    {
                        Guid newId = Guid.NewGuid();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = cbSelectDistributor.Enabled && _distributors.Count > 0 ? _distributors[cbSelectDistributor.SelectedIndex].Id : newId;
                        cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = txtDistrName.Text.Trim();
                        cmd.Parameters.Add("@active", SqlDbType.Bit).Value = ckbDistrActive.Checked;
                        cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = txtDistrEmail.Text.Trim();
                        cmd.Parameters.Add("@contactNumber", SqlDbType.NVarChar).Value = txtDistrContactNumber.Text.Trim();
                        cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = txtDistrPhysicalAddress.Text.Trim();
                        cmd.Parameters.Add("@monthly", SqlDbType.Bit).Value = ckbMontly.Checked;
                        cmd.Parameters.Add("@quaterly", SqlDbType.Bit).Value = ckbQuaterly.Checked;
                        cmd.Parameters.Add("@halfYearly", SqlDbType.Bit).Value = ckbHalfYearly.Checked;
                        cmd.Parameters.Add("@yearly", SqlDbType.Bit).Value = ckbYearly.Checked;
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Distributor successfully " + action + ".", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (action.Equals("created"))
                        {
                            //Re-enables buttons.
                            btnDistrCancel.Enabled = false;
                            btnAddDistributor.Enabled = true;
                            cbSelectDistributor.Enabled = true;
                            cbSelectDistributor.Items.Add(txtDistrName.Text.Trim());
                        }

                        //Fills the dropdown again.
                        FillDistributorDropDown();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDistrCancel_Click(object sender, EventArgs e)
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
        private void btnLinkMovie_Click(object sender, EventArgs e)
        {
            try
            {
                //Link the distributor to the movie.
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();

                    using (SqlCommand cmd = new SqlCommand("LinkMovieDistributor", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@movieName", SqlDbType.NVarChar).Value = cbUnlinkedMovies.SelectedItem;
                        cmd.Parameters.Add("@distributorGuid", SqlDbType.UniqueIdentifier).Value = _distributors[cbSelectDistributor.SelectedIndex].Id;
                        cmd.ExecuteNonQuery();

                        //Remove the selecteditem from the drop down list.
                        cbUnlinkedMovies.Items.RemoveAt(cbUnlinkedMovies.SelectedIndex);

                        MessageBox.Show("Movie successfully linked to distributor", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion Distributor Setting Events

        #region Managment Tasks Events

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                string distributorName = cbSelectDistributor.SelectedItem.ToString();

                List<String> movNameList = new List<string>();
                List<Int32> seatsList = new List<int>();
                List<Boolean> archivedList = new List<bool>();

                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    //If the connection is closed, open it.
                    if (cn.State == ConnectionState.Closed) cn.Open();

                    //Gets all the users that the logged in user is allowed to view and adds it to the dropdown list.
                    using (SqlCommand cmd = new SqlCommand("GetDistributorReportData", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = distributorName;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                String movName = dr.GetString(dr.GetOrdinal("Movie Name"));
                                Int32 seats = dr.GetInt32(dr.GetOrdinal("Booked Seats"));
                                Boolean archived = (dr.GetString(dr.GetOrdinal("Is Archived")) == "Archived");

                                movNameList.Add(movName);
                                seatsList.Add(seats);
                                archivedList.Add(archived);

                            }
                        }
                    }
                }
                if (movNameList.Count != 0)
                {
                    WriteReport(distributorName, movNameList, seatsList, archivedList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnScheduleMovies_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dateScheduleFrom.Value.Date;
            DateTime toDate = dateScheduleTo.Value.Date;

            #region Validation

            if (fromDate < DateTime.Today)
            {
                MessageBox.Show("From Date cannot be in the past.");
                dateScheduleFrom.Focus();
                return;
            }

            if (toDate < DateTime.Today)
            {
                MessageBox.Show("To Date cannot be in the past.");
                dateScheduleTo.Focus();
                return;
            }

            if (toDate < fromDate)
            {
                MessageBox.Show("To Date cannot be before From Date.");
                dateScheduleFrom.Focus();
                return;
            }

            #endregion Validation

            ScheduleMovies(fromDate, toDate);
        }

        #endregion Managment Tasks Events

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        private void FillUserDropDown()
        {
            try
            {
                int selectedIndex = 0;

                // If the dropdown has items and one of them is selected, remember that.
                if ((cbSelectUsers.Items.Count > 0) &&
                    (cbSelectUsers.SelectedIndex != -1))
                {
                    selectedIndex = cbSelectUsers.SelectedIndex;
                }

                //Clears the current list.
                cbSelectUsers.Items.Clear();
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
                                cbSelectUsers.Items.Add(user.ToString());
                            }
                            //Always select the 1st item in the dropdown as default.
                            if (cbSelectUsers.Items.Count > 0)
                            {
                                cbSelectUsers.SelectedIndex = selectedIndex;
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
        /// 
        /// </summary>
        private void FillMovieDropDown()
        {
            try
            {
                int selectedIndex = 0;

                // If the dropdown has items and one of them is selected, remember that.
                if ((cbSelectEditMovie.Items.Count > 0) &&
                    (cbSelectEditMovie.SelectedIndex != -1))
                {
                    selectedIndex = cbSelectEditMovie.SelectedIndex;
                }

                //Clears the current list.
                cbSelectEditMovie.Items.Clear();
                //Clear the List object declared at the top.
                _movies.Clear();

                //Clears the current list.
                cbAttendanceSelectMovie.Items.Clear();
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
                                    cbAttendanceSelectMovie.Items.Add(movie.MovieName);
                                }
                                //Adds the user to the list declared at the top.
                                _movies.Add(movie);
                                //Adds the user to the dropdown list.
                                cbSelectEditMovie.Items.Add(movie.MovieName);
                            }

                            //Always select the 1st item in the dropdown as default.
                            if (cbSelectEditMovie.Items.Count > 0)
                            {
                                cbSelectEditMovie.SelectedIndex = selectedIndex;
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
        /// 
        /// </summary>
        private void FillDistributorDropDown()
        {
            try
            {
                int selectedIndex = 0;

                // If the dropdown has items and one of them is selected, remember that.
                if ((cbSelectDistributor.Items.Count > 0) &&
                    (cbSelectDistributor.SelectedIndex != -1))
                {
                    selectedIndex = cbSelectDistributor.SelectedIndex;
                }

                //Clears the current list.
                cbSelectDistributor.Items.Clear();
                //Clear the List object declared at the top.
                _distributors.Clear();

                //Create new connection.
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    //If the connection is closed, open it.
                    if (cn.State == ConnectionState.Closed) cn.Open();

                    //Gets all the users that the logged in user is allowed to view and adds it to the dropdown list.
                    using (SqlCommand cmd = new SqlCommand("GetDistributorList", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Guid id = dr.GetGuid(dr.GetOrdinal("DistributorGuid"));
                                String name = dr.GetString(dr.GetOrdinal("Name"));
                                Boolean active = dr.GetBoolean(dr.GetOrdinal("IsActive"));
                                String email = dr.GetString(dr.GetOrdinal("Email"));
                                String contactNumber = dr.GetString(dr.GetOrdinal("ContactNumber"));
                                String physicalAddress = dr.GetString(dr.GetOrdinal("PhysicalAddress"));
                                Boolean receiveMonthly = dr.GetBoolean(dr.GetOrdinal("ReceiveMontly"));
                                Boolean receiveQuaterly = dr.GetBoolean(dr.GetOrdinal("ReceiveQuaterly"));
                                Boolean receiveHalfYearly = dr.GetBoolean(dr.GetOrdinal("ReceiveHalfYearly"));
                                Boolean receiveYearly = dr.GetBoolean(dr.GetOrdinal("ReceiveYearly"));

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
                                _distributors.Add(distributor);
                                //Adds the user to the dropdown list.
                                cbSelectDistributor.Items.Add(distributor.Name);
                            }

                            //Always select the 1st item in the dropdown as default.
                            if (cbSelectDistributor.Items.Count > 0)
                            {
                                cbSelectDistributor.SelectedIndex = selectedIndex;
                            }
                        }
                    }
                }
                //Gets a list of all the unlinked movies.
                GetUnlinkedMovies();
                if (cbSelectDistributor.Items.Count < 1 || !btnAddDistributor.Enabled || cbUnlinkedMovies.Items.Count < 1)
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
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();

                    using (SqlCommand cmd = new SqlCommand("GetMovieList", cn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                String movieName = dr.GetString(dr.GetOrdinal("Name"));
                                String movieDistributor = dr.GetString(dr.GetOrdinal("DistributorId"));
                                Boolean isArchived = dr.GetBoolean(dr.GetOrdinal("IsArchived"));

                                //If there is no distributor linked to the active movie then add it.
                                if (String.IsNullOrEmpty(movieDistributor) && !isArchived)
                                {
                                    cbUnlinkedMovies.Items.Add(movieName);
                                }
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

        public void WriteReport(string distributor, List<String> movNameList, List<Int32> seatsList, List<Boolean> archivedList)
        {
            string path = Path.Combine(System.Windows.Forms.Application.StartupPath, distributor + ".xls");

            Workbook = App.Workbooks.Add(misValue);
            Worksheet = (Excel.Worksheet)Workbook.Worksheets.get_Item(1);

            range = Worksheet.get_Range("A1", "A1");
            range.Font.Bold = true;
            range = Worksheet.get_Range("A2", "C2");
            range.Font.Bold = true;
            range.ColumnWidth = 20;

            Worksheet.Cells[1, 1] = "Distributor Name:";
            Worksheet.Cells[1, 2] = distributor;
            
            Worksheet.Cells[2, 1] = "Movie Name";
            Worksheet.Cells[2, 2] = "Number of Seats";
            Worksheet.Cells[2, 3] = "Archived";

            for (int i = 0; i < movNameList.Count; i++)
            {
                Worksheet.Cells[i + 3, 1] = movNameList[i];
                Worksheet.Cells[i + 3, 2] = seatsList[i];
                Worksheet.Cells[i + 3, 3] = archivedList[i];
            }
            
            Workbook.SaveAs(path, 
                            Type.Missing, 
                            Type.Missing, 
                            Type.Missing, 
                            Type.Missing, 
                            Type.Missing, 
                            Excel.XlSaveAsAccessMode.xlNoChange, 
                            Type.Missing, 
                            Type.Missing, 
                            Type.Missing, 
                            Type.Missing, 
                            Type.Missing);
            Workbook.Close();
            MessageBox.Show("File saved under " + path);

            App.Quit();
            App = null;
            GC.Collect();
        }

        private void ScheduleMovies(DateTime fromDate, DateTime toDate)
        {
            foreach (ScreenDetails screen in ScreenList)
            {
                //string str = "ScreenID:  " + screen.ScreenID + Environment.NewLine +
                //             "ScreenNum: " + screen.ScreenNum + Environment.NewLine +
                //             "Seats:     " + screen.Seats;

                //MessageBox.Show(str);
            }

            try
            {
                // Mainstream     = 0
                // Average        = 1
                // Below_Average  = 2
                // Specialist     = 3
                // Non_Mainstream = 4

                List<MovieDetails> moviesMainstream = new List<MovieDetails>();
                List<MovieDetails> moviesAverage = new List<MovieDetails>();
                List<MovieDetails> moviesBelowAverage = new List<MovieDetails>();
                List<MovieDetails> moviesSpecialist = new List<MovieDetails>();
                List<MovieDetails> moviesNonMainstream = new List<MovieDetails>();

                List<MovieDetails> moviesSorted = new List<MovieDetails>();

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

                                //Sets the movies details.
                                MovieDetails movie = new MovieDetails()
                                {
                                    MovieName = name,
                                    Duration = duration,
                                    YearReleased = releaseYear,
                                    Director = director,
                                    Producer = producer,
                                    Type = type,
                                    ExpectedAudience = expectedAudience,
                                    BbfcRate = rating,
                                    IsArchived = archived,
                                    Description = description,
                                    MovieGuid = movieGuid
                                };

                                // Adds non-archived movies to the individual lists created above.
                                if (!archived)
                                {
                                    switch (expectedAudience)
                                    {
                                        case "Mainstream":
                                            moviesMainstream.Add(movie);
                                            break;
                                        case "Average":
                                            moviesAverage.Add(movie);
                                            break;
                                        case "Below_Average":
                                            moviesBelowAverage.Add(movie);
                                            break;
                                        case "Specialist":
                                            moviesSpecialist.Add(movie);
                                            break;
                                        case "Non_Mainstream":
                                            moviesNonMainstream.Add(movie);
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }

                // Combines individual Lists into one list.
                #region Combine Lists

                foreach (MovieDetails movie in moviesMainstream)
                {
                    moviesSorted.Add(movie);
                }

                foreach (MovieDetails movie in moviesAverage)
                {
                    moviesSorted.Add(movie);
                }

                foreach (MovieDetails movie in moviesBelowAverage)
                {
                    moviesSorted.Add(movie);
                }

                foreach (MovieDetails movie in moviesSpecialist)
                {
                    moviesSorted.Add(movie);
                }

                foreach (MovieDetails movie in moviesNonMainstream)
                {
                    moviesSorted.Add(movie);
                }

                #endregion Combine Lists

                //Create new connection.
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    //If the connection is closed, open it.
                    if (cn.State == ConnectionState.Closed) cn.Open();

                    List<MovieScreeningSchedule> schedule = new List<MovieScreeningSchedule>();

                    // Loop through the sorted list of movies and screens, scheduling the most popular movies for 
                    // the biggest theatres. If there are more movies than screens, the least popular movies stand
                    // over for the next schedule. If there are more screens than movies, the most popular movies
                    // are shown in multiple theatres.
                    for (int i = 0; i < ScreenList.Count; i++)
                    {
                        ScreenDetails screen = ScreenList[i];
                        MovieDetails movie = moviesSorted[i % moviesSorted.Count];

                        DateTime dateIterator = fromDate.Date;

                        do
                        {
                            int openingHour = 8;
                            int closingHour = 22;
                            int cleanupMinutes = 30;

                            DateTime timeIterator = dateIterator.AddHours(openingHour);
                            DateTime closeTime = dateIterator.AddHours(closingHour);

                            MovieScreeningSchedule daySchedule = new MovieScreeningSchedule()
                            {
                                MovieName = movie.MovieName,
                                Date = dateIterator.ToString("yyyy-MM-dd"),
                                ScreenNum = screen.ScreenNum,
                                ScreeningsList = new List<string>()
                            };

                            do
                            {
                                DateTime endTime = timeIterator.AddMinutes(movie.Duration);

                                // Adds the scheduling to the database.
                                using (SqlCommand cmd = new SqlCommand("ScheduleMovieScreening", cn))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Add("@MovieId", SqlDbType.UniqueIdentifier).Value = movie.MovieGuid;
                                    cmd.Parameters.Add("@ScreenId", SqlDbType.UniqueIdentifier).Value = screen.ScreenID;
                                    cmd.Parameters.Add("@StartScreen", SqlDbType.DateTime).Value = timeIterator;
                                    cmd.Parameters.Add("@EndScreen", SqlDbType.DateTime).Value = endTime;
                                    cmd.ExecuteNonQuery();
                                }

                                daySchedule.ScreeningsList.Add(String.Format("{0:HH:mm} - {1:HH:mm}", timeIterator, endTime));

                            } while ((timeIterator = timeIterator.AddMinutes(movie.Duration + cleanupMinutes)) <= closeTime.AddMinutes((movie.Duration + cleanupMinutes) * -1));

                            schedule.Add(daySchedule);

                        } while ((dateIterator = dateIterator.AddDays(1).Date) <= toDate);
                    }
                    
                    dataMovieSchedule.DataSource = schedule;
                    dataMovieSchedule.Visible = true;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Methods

    }
}
