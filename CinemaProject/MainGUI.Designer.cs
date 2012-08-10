﻿namespace CinemaProject
{
    partial class MainGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.tbcUserRoles = new System.Windows.Forms.TabControl();
			this.tbBookings = new System.Windows.Forms.TabPage();
			this.btnBook = new System.Windows.Forms.Button();
			this.btnMovieDetails = new System.Windows.Forms.Button();
			this.txtNumOfTickets = new System.Windows.Forms.TextBox();
			this.lblNumberOfTickets = new System.Windows.Forms.Label();
			this.cbbSession = new System.Windows.Forms.ComboBox();
			this.lbl = new System.Windows.Forms.Label();
			this.cbbMovieName = new System.Windows.Forms.ComboBox();
			this.lblMovie = new System.Windows.Forms.Label();
			this.tbUserSettings = new System.Windows.Forms.TabPage();
			this.grpUserDetails = new System.Windows.Forms.GroupBox();
			this.txtConfirmPassword = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtLoginUserName = new System.Windows.Forms.TextBox();
			this.lblConfirmPassword = new System.Windows.Forms.Label();
			this.lblPassword = new System.Windows.Forms.Label();
			this.lblUsername = new System.Windows.Forms.Label();
			this.txtContactNumber = new System.Windows.Forms.TextBox();
			this.lblContactNumber = new System.Windows.Forms.Label();
			this.cbAdminLevel = new System.Windows.Forms.ComboBox();
			this.btnCancelUserDetailsChange = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.lblName = new System.Windows.Forms.Label();
			this.dateDateOfBirth = new System.Windows.Forms.DateTimePicker();
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtSurname = new System.Windows.Forms.TextBox();
			this.chkActive = new System.Windows.Forms.CheckBox();
			this.lblSurname = new System.Windows.Forms.Label();
			this.lblActive = new System.Windows.Forms.Label();
			this.lblDateOfBirth = new System.Windows.Forms.Label();
			this.lblAdmin = new System.Windows.Forms.Label();
			this.lblSelectUser = new System.Windows.Forms.Label();
			this.btnAddUser = new System.Windows.Forms.Button();
			this.dropdownSelectUsers = new System.Windows.Forms.ComboBox();
			this.tbMovieSettings = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtProducer = new System.Windows.Forms.TextBox();
			this.lblProducer = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.lblMovieName = new System.Windows.Forms.Label();
			this.txtMovieName = new System.Windows.Forms.TextBox();
			this.txtDirector = new System.Windows.Forms.TextBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.lblDirector = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.lblDuration = new System.Windows.Forms.Label();
			this.lblSelectMovie = new System.Windows.Forms.Label();
			this.btnAddMovie = new System.Windows.Forms.Button();
			this.ddlMovies = new System.Windows.Forms.ComboBox();
			this.tbDistributors = new System.Windows.Forms.TabPage();
			this.lblInfo = new System.Windows.Forms.Label();
			this.txtDuration = new System.Windows.Forms.TextBox();
			this.txtMovieType = new System.Windows.Forms.TextBox();
			this.lblType = new System.Windows.Forms.Label();
			this.lblExpectedAudience = new System.Windows.Forms.Label();
			this.ddlExpectedAudience = new System.Windows.Forms.ComboBox();
			this.ddlBBFCRating = new System.Windows.Forms.ComboBox();
			this.lblBBFCRating = new System.Windows.Forms.Label();
			this.tbcUserRoles.SuspendLayout();
			this.tbBookings.SuspendLayout();
			this.tbUserSettings.SuspendLayout();
			this.grpUserDetails.SuspendLayout();
			this.tbMovieSettings.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbcUserRoles
			// 
			this.tbcUserRoles.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.tbcUserRoles.Controls.Add( this.tbBookings );
			this.tbcUserRoles.Controls.Add( this.tbUserSettings );
			this.tbcUserRoles.Controls.Add( this.tbMovieSettings );
			this.tbcUserRoles.Controls.Add( this.tbDistributors );
			this.tbcUserRoles.Location = new System.Drawing.Point( 12, 12 );
			this.tbcUserRoles.Name = "tbcUserRoles";
			this.tbcUserRoles.SelectedIndex = 0;
			this.tbcUserRoles.Size = new System.Drawing.Size( 608, 410 );
			this.tbcUserRoles.TabIndex = 0;
			// 
			// tbBookings
			// 
			this.tbBookings.BackColor = System.Drawing.SystemColors.Control;
			this.tbBookings.Controls.Add( this.btnBook );
			this.tbBookings.Controls.Add( this.btnMovieDetails );
			this.tbBookings.Controls.Add( this.txtNumOfTickets );
			this.tbBookings.Controls.Add( this.lblNumberOfTickets );
			this.tbBookings.Controls.Add( this.cbbSession );
			this.tbBookings.Controls.Add( this.lbl );
			this.tbBookings.Controls.Add( this.cbbMovieName );
			this.tbBookings.Controls.Add( this.lblMovie );
			this.tbBookings.Location = new System.Drawing.Point( 4, 22 );
			this.tbBookings.Name = "tbBookings";
			this.tbBookings.Padding = new System.Windows.Forms.Padding( 3 );
			this.tbBookings.Size = new System.Drawing.Size( 600, 384 );
			this.tbBookings.TabIndex = 0;
			this.tbBookings.Text = "Bookings";
			// 
			// btnBook
			// 
			this.btnBook.Location = new System.Drawing.Point( 212, 117 );
			this.btnBook.Name = "btnBook";
			this.btnBook.Size = new System.Drawing.Size( 97, 23 );
			this.btnBook.TabIndex = 7;
			this.btnBook.Text = "Book";
			this.btnBook.UseVisualStyleBackColor = true;
			this.btnBook.Click += new System.EventHandler( this.btnBook_Click );
			// 
			// btnMovieDetails
			// 
			this.btnMovieDetails.Location = new System.Drawing.Point( 209, 33 );
			this.btnMovieDetails.Name = "btnMovieDetails";
			this.btnMovieDetails.Size = new System.Drawing.Size( 97, 23 );
			this.btnMovieDetails.TabIndex = 6;
			this.btnMovieDetails.Text = "Show Details";
			this.btnMovieDetails.UseVisualStyleBackColor = true;
			this.btnMovieDetails.Click += new System.EventHandler( this.btnMovieDetails_Click );
			// 
			// txtNumOfTickets
			// 
			this.txtNumOfTickets.Location = new System.Drawing.Point( 106, 91 );
			this.txtNumOfTickets.Name = "txtNumOfTickets";
			this.txtNumOfTickets.Size = new System.Drawing.Size( 200, 20 );
			this.txtNumOfTickets.TabIndex = 5;
			// 
			// lblNumberOfTickets
			// 
			this.lblNumberOfTickets.AutoSize = true;
			this.lblNumberOfTickets.Location = new System.Drawing.Point( 6, 94 );
			this.lblNumberOfTickets.Name = "lblNumberOfTickets";
			this.lblNumberOfTickets.Size = new System.Drawing.Size( 94, 13 );
			this.lblNumberOfTickets.TabIndex = 4;
			this.lblNumberOfTickets.Text = "Number of Tickets";
			// 
			// cbbSession
			// 
			this.cbbSession.FormattingEnabled = true;
			this.cbbSession.Location = new System.Drawing.Point( 106, 62 );
			this.cbbSession.Name = "cbbSession";
			this.cbbSession.Size = new System.Drawing.Size( 200, 21 );
			this.cbbSession.TabIndex = 3;
			this.cbbSession.SelectedIndexChanged += new System.EventHandler( this.cbbSession_SelectedIndexChanged );
			// 
			// lbl
			// 
			this.lbl.AutoSize = true;
			this.lbl.Location = new System.Drawing.Point( 6, 65 );
			this.lbl.Name = "lbl";
			this.lbl.Size = new System.Drawing.Size( 44, 13 );
			this.lbl.TabIndex = 2;
			this.lbl.Text = "Session";
			// 
			// cbbMovieName
			// 
			this.cbbMovieName.FormattingEnabled = true;
			this.cbbMovieName.Location = new System.Drawing.Point( 106, 6 );
			this.cbbMovieName.Name = "cbbMovieName";
			this.cbbMovieName.Size = new System.Drawing.Size( 200, 21 );
			this.cbbMovieName.TabIndex = 1;
			this.cbbMovieName.SelectedIndexChanged += new System.EventHandler( this.cbbMovieName_SelectedIndexChanged );
			// 
			// lblMovie
			// 
			this.lblMovie.AutoSize = true;
			this.lblMovie.Location = new System.Drawing.Point( 6, 9 );
			this.lblMovie.Name = "lblMovie";
			this.lblMovie.Size = new System.Drawing.Size( 67, 13 );
			this.lblMovie.TabIndex = 0;
			this.lblMovie.Text = "Movie Name";
			// 
			// tbUserSettings
			// 
			this.tbUserSettings.BackColor = System.Drawing.SystemColors.Control;
			this.tbUserSettings.Controls.Add( this.grpUserDetails );
			this.tbUserSettings.Controls.Add( this.lblSelectUser );
			this.tbUserSettings.Controls.Add( this.btnAddUser );
			this.tbUserSettings.Controls.Add( this.dropdownSelectUsers );
			this.tbUserSettings.Location = new System.Drawing.Point( 4, 22 );
			this.tbUserSettings.Name = "tbUserSettings";
			this.tbUserSettings.Padding = new System.Windows.Forms.Padding( 3 );
			this.tbUserSettings.Size = new System.Drawing.Size( 600, 384 );
			this.tbUserSettings.TabIndex = 1;
			this.tbUserSettings.Text = "User Settings";
			// 
			// grpUserDetails
			// 
			this.grpUserDetails.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.grpUserDetails.Controls.Add( this.txtConfirmPassword );
			this.grpUserDetails.Controls.Add( this.txtPassword );
			this.grpUserDetails.Controls.Add( this.txtLoginUserName );
			this.grpUserDetails.Controls.Add( this.lblConfirmPassword );
			this.grpUserDetails.Controls.Add( this.lblPassword );
			this.grpUserDetails.Controls.Add( this.lblUsername );
			this.grpUserDetails.Controls.Add( this.txtContactNumber );
			this.grpUserDetails.Controls.Add( this.lblContactNumber );
			this.grpUserDetails.Controls.Add( this.cbAdminLevel );
			this.grpUserDetails.Controls.Add( this.btnCancelUserDetailsChange );
			this.grpUserDetails.Controls.Add( this.btnSave );
			this.grpUserDetails.Controls.Add( this.lblName );
			this.grpUserDetails.Controls.Add( this.dateDateOfBirth );
			this.grpUserDetails.Controls.Add( this.txtName );
			this.grpUserDetails.Controls.Add( this.txtSurname );
			this.grpUserDetails.Controls.Add( this.chkActive );
			this.grpUserDetails.Controls.Add( this.lblSurname );
			this.grpUserDetails.Controls.Add( this.lblActive );
			this.grpUserDetails.Controls.Add( this.lblDateOfBirth );
			this.grpUserDetails.Controls.Add( this.lblAdmin );
			this.grpUserDetails.Location = new System.Drawing.Point( 9, 33 );
			this.grpUserDetails.Name = "grpUserDetails";
			this.grpUserDetails.Size = new System.Drawing.Size( 585, 345 );
			this.grpUserDetails.TabIndex = 17;
			this.grpUserDetails.TabStop = false;
			this.grpUserDetails.Text = "User Details";
			// 
			// txtConfirmPassword
			// 
			this.txtConfirmPassword.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.txtConfirmPassword.Location = new System.Drawing.Point( 103, 196 );
			this.txtConfirmPassword.Name = "txtConfirmPassword";
			this.txtConfirmPassword.PasswordChar = '*';
			this.txtConfirmPassword.Size = new System.Drawing.Size( 474, 20 );
			this.txtConfirmPassword.TabIndex = 12;
			// 
			// txtPassword
			// 
			this.txtPassword.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.txtPassword.Location = new System.Drawing.Point( 105, 170 );
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size( 474, 20 );
			this.txtPassword.TabIndex = 11;
			// 
			// txtLoginUserName
			// 
			this.txtLoginUserName.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.txtLoginUserName.Location = new System.Drawing.Point( 105, 144 );
			this.txtLoginUserName.Name = "txtLoginUserName";
			this.txtLoginUserName.Size = new System.Drawing.Size( 474, 20 );
			this.txtLoginUserName.TabIndex = 10;
			// 
			// lblConfirmPassword
			// 
			this.lblConfirmPassword.AutoSize = true;
			this.lblConfirmPassword.Location = new System.Drawing.Point( 6, 199 );
			this.lblConfirmPassword.Name = "lblConfirmPassword";
			this.lblConfirmPassword.Size = new System.Drawing.Size( 91, 13 );
			this.lblConfirmPassword.TabIndex = 24;
			this.lblConfirmPassword.Text = "Confirm Password";
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new System.Drawing.Point( 6, 173 );
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size( 82, 13 );
			this.lblPassword.TabIndex = 23;
			this.lblPassword.Text = "Login Password";
			// 
			// lblUsername
			// 
			this.lblUsername.AutoSize = true;
			this.lblUsername.Location = new System.Drawing.Point( 6, 147 );
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size( 84, 13 );
			this.lblUsername.TabIndex = 22;
			this.lblUsername.Text = "Login Username";
			// 
			// txtContactNumber
			// 
			this.txtContactNumber.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.txtContactNumber.Location = new System.Drawing.Point( 105, 65 );
			this.txtContactNumber.Name = "txtContactNumber";
			this.txtContactNumber.Size = new System.Drawing.Size( 474, 20 );
			this.txtContactNumber.TabIndex = 7;
			// 
			// lblContactNumber
			// 
			this.lblContactNumber.AutoSize = true;
			this.lblContactNumber.Location = new System.Drawing.Point( 6, 65 );
			this.lblContactNumber.Name = "lblContactNumber";
			this.lblContactNumber.Size = new System.Drawing.Size( 84, 13 );
			this.lblContactNumber.TabIndex = 20;
			this.lblContactNumber.Text = "Contact Number";
			// 
			// cbAdminLevel
			// 
			this.cbAdminLevel.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.cbAdminLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbAdminLevel.FormattingEnabled = true;
			this.cbAdminLevel.Items.AddRange( new object[] {
            "Employee",
            "Manager",
            "Administrator",
            "Distributor"} );
			this.cbAdminLevel.Location = new System.Drawing.Point( 105, 117 );
			this.cbAdminLevel.Name = "cbAdminLevel";
			this.cbAdminLevel.Size = new System.Drawing.Size( 474, 21 );
			this.cbAdminLevel.TabIndex = 9;
			// 
			// btnCancelUserDetailsChange
			// 
			this.btnCancelUserDetailsChange.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.btnCancelUserDetailsChange.Enabled = false;
			this.btnCancelUserDetailsChange.Location = new System.Drawing.Point( 87, 316 );
			this.btnCancelUserDetailsChange.Name = "btnCancelUserDetailsChange";
			this.btnCancelUserDetailsChange.Size = new System.Drawing.Size( 75, 23 );
			this.btnCancelUserDetailsChange.TabIndex = 18;
			this.btnCancelUserDetailsChange.Text = "Cancel";
			this.btnCancelUserDetailsChange.UseVisualStyleBackColor = true;
			this.btnCancelUserDetailsChange.Click += new System.EventHandler( this.btnCancelUserDetailsChange_Click );
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.btnSave.Location = new System.Drawing.Point( 6, 316 );
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size( 75, 23 );
			this.btnSave.TabIndex = 14;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler( this.btnSave_Click );
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point( 6, 16 );
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size( 35, 13 );
			this.lblName.TabIndex = 5;
			this.lblName.Text = "Name";
			// 
			// dateDateOfBirth
			// 
			this.dateDateOfBirth.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.dateDateOfBirth.Location = new System.Drawing.Point( 105, 91 );
			this.dateDateOfBirth.Name = "dateDateOfBirth";
			this.dateDateOfBirth.Size = new System.Drawing.Size( 474, 20 );
			this.dateDateOfBirth.TabIndex = 8;
			// 
			// txtName
			// 
			this.txtName.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.txtName.Location = new System.Drawing.Point( 105, 13 );
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size( 474, 20 );
			this.txtName.TabIndex = 4;
			// 
			// txtSurname
			// 
			this.txtSurname.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.txtSurname.Location = new System.Drawing.Point( 105, 39 );
			this.txtSurname.Name = "txtSurname";
			this.txtSurname.Size = new System.Drawing.Size( 474, 20 );
			this.txtSurname.TabIndex = 6;
			// 
			// chkActive
			// 
			this.chkActive.AutoSize = true;
			this.chkActive.Location = new System.Drawing.Point( 105, 222 );
			this.chkActive.Name = "chkActive";
			this.chkActive.Size = new System.Drawing.Size( 15, 14 );
			this.chkActive.TabIndex = 13;
			this.chkActive.UseVisualStyleBackColor = true;
			// 
			// lblSurname
			// 
			this.lblSurname.AutoSize = true;
			this.lblSurname.Location = new System.Drawing.Point( 6, 42 );
			this.lblSurname.Name = "lblSurname";
			this.lblSurname.Size = new System.Drawing.Size( 49, 13 );
			this.lblSurname.TabIndex = 7;
			this.lblSurname.Text = "Surname";
			// 
			// lblActive
			// 
			this.lblActive.AutoSize = true;
			this.lblActive.Location = new System.Drawing.Point( 6, 222 );
			this.lblActive.Name = "lblActive";
			this.lblActive.Size = new System.Drawing.Size( 37, 13 );
			this.lblActive.TabIndex = 13;
			this.lblActive.Text = "Active";
			// 
			// lblDateOfBirth
			// 
			this.lblDateOfBirth.AutoSize = true;
			this.lblDateOfBirth.Location = new System.Drawing.Point( 6, 97 );
			this.lblDateOfBirth.Name = "lblDateOfBirth";
			this.lblDateOfBirth.Size = new System.Drawing.Size( 66, 13 );
			this.lblDateOfBirth.TabIndex = 9;
			this.lblDateOfBirth.Text = "Date of Birth";
			// 
			// lblAdmin
			// 
			this.lblAdmin.AutoSize = true;
			this.lblAdmin.Location = new System.Drawing.Point( 6, 123 );
			this.lblAdmin.Name = "lblAdmin";
			this.lblAdmin.Size = new System.Drawing.Size( 61, 13 );
			this.lblAdmin.TabIndex = 11;
			this.lblAdmin.Text = "Admin level";
			// 
			// lblSelectUser
			// 
			this.lblSelectUser.AutoSize = true;
			this.lblSelectUser.Location = new System.Drawing.Point( 6, 9 );
			this.lblSelectUser.Name = "lblSelectUser";
			this.lblSelectUser.Size = new System.Drawing.Size( 62, 13 );
			this.lblSelectUser.TabIndex = 3;
			this.lblSelectUser.Text = "Select User";
			// 
			// btnAddUser
			// 
			this.btnAddUser.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btnAddUser.Location = new System.Drawing.Point( 519, 4 );
			this.btnAddUser.Name = "btnAddUser";
			this.btnAddUser.Size = new System.Drawing.Size( 75, 23 );
			this.btnAddUser.TabIndex = 2;
			this.btnAddUser.Text = "Add User";
			this.btnAddUser.UseVisualStyleBackColor = true;
			this.btnAddUser.Click += new System.EventHandler( this.btnAddUser_Click );
			// 
			// dropdownSelectUsers
			// 
			this.dropdownSelectUsers.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.dropdownSelectUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.dropdownSelectUsers.FormattingEnabled = true;
			this.dropdownSelectUsers.Location = new System.Drawing.Point( 74, 6 );
			this.dropdownSelectUsers.Name = "dropdownSelectUsers";
			this.dropdownSelectUsers.Size = new System.Drawing.Size( 439, 21 );
			this.dropdownSelectUsers.TabIndex = 1;
			this.dropdownSelectUsers.SelectedIndexChanged += new System.EventHandler( this.dropdownSelectUsers_SelectedIndexChanged );
			// 
			// tbMovieSettings
			// 
			this.tbMovieSettings.BackColor = System.Drawing.SystemColors.Control;
			this.tbMovieSettings.Controls.Add( this.groupBox1 );
			this.tbMovieSettings.Controls.Add( this.lblSelectMovie );
			this.tbMovieSettings.Controls.Add( this.btnAddMovie );
			this.tbMovieSettings.Controls.Add( this.ddlMovies );
			this.tbMovieSettings.Location = new System.Drawing.Point( 4, 22 );
			this.tbMovieSettings.Name = "tbMovieSettings";
			this.tbMovieSettings.Padding = new System.Windows.Forms.Padding( 3 );
			this.tbMovieSettings.Size = new System.Drawing.Size( 600, 384 );
			this.tbMovieSettings.TabIndex = 3;
			this.tbMovieSettings.Text = "Movie Settings";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.groupBox1.Controls.Add( this.ddlBBFCRating );
			this.groupBox1.Controls.Add( this.lblBBFCRating );
			this.groupBox1.Controls.Add( this.ddlExpectedAudience );
			this.groupBox1.Controls.Add( this.lblExpectedAudience );
			this.groupBox1.Controls.Add( this.txtMovieType );
			this.groupBox1.Controls.Add( this.lblType );
			this.groupBox1.Controls.Add( this.txtDuration );
			this.groupBox1.Controls.Add( this.lblInfo );
			this.groupBox1.Controls.Add( this.txtProducer );
			this.groupBox1.Controls.Add( this.lblProducer );
			this.groupBox1.Controls.Add( this.button1 );
			this.groupBox1.Controls.Add( this.button2 );
			this.groupBox1.Controls.Add( this.lblMovieName );
			this.groupBox1.Controls.Add( this.txtMovieName );
			this.groupBox1.Controls.Add( this.txtDirector );
			this.groupBox1.Controls.Add( this.checkBox1 );
			this.groupBox1.Controls.Add( this.lblDirector );
			this.groupBox1.Controls.Add( this.label7 );
			this.groupBox1.Controls.Add( this.lblDuration );
			this.groupBox1.Location = new System.Drawing.Point( 9, 33 );
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size( 585, 345 );
			this.groupBox1.TabIndex = 17;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Movie Details";
			// 
			// txtProducer
			// 
			this.txtProducer.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.txtProducer.Location = new System.Drawing.Point( 105, 86 );
			this.txtProducer.Name = "txtProducer";
			this.txtProducer.Size = new System.Drawing.Size( 474, 20 );
			this.txtProducer.TabIndex = 7;
			// 
			// lblProducer
			// 
			this.lblProducer.AutoSize = true;
			this.lblProducer.Location = new System.Drawing.Point( 6, 86 );
			this.lblProducer.Name = "lblProducer";
			this.lblProducer.Size = new System.Drawing.Size( 61, 13 );
			this.lblProducer.TabIndex = 20;
			this.lblProducer.Text = "Producer(s)";
			// 
			// button1
			// 
			this.button1.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.button1.Enabled = false;
			this.button1.Location = new System.Drawing.Point( 87, 316 );
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size( 75, 23 );
			this.button1.TabIndex = 18;
			this.button1.Text = "Cancel";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.button2.Location = new System.Drawing.Point( 6, 316 );
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size( 75, 23 );
			this.button2.TabIndex = 14;
			this.button2.Text = "Save";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// lblMovieName
			// 
			this.lblMovieName.AutoSize = true;
			this.lblMovieName.Location = new System.Drawing.Point( 6, 16 );
			this.lblMovieName.Name = "lblMovieName";
			this.lblMovieName.Size = new System.Drawing.Size( 35, 13 );
			this.lblMovieName.TabIndex = 5;
			this.lblMovieName.Text = "Name";
			// 
			// txtMovieName
			// 
			this.txtMovieName.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.txtMovieName.Location = new System.Drawing.Point( 105, 13 );
			this.txtMovieName.Name = "txtMovieName";
			this.txtMovieName.Size = new System.Drawing.Size( 474, 20 );
			this.txtMovieName.TabIndex = 4;
			// 
			// txtDirector
			// 
			this.txtDirector.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.txtDirector.Location = new System.Drawing.Point( 105, 60 );
			this.txtDirector.Name = "txtDirector";
			this.txtDirector.Size = new System.Drawing.Size( 474, 20 );
			this.txtDirector.TabIndex = 6;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point( 105, 220 );
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size( 15, 14 );
			this.checkBox1.TabIndex = 13;
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// lblDirector
			// 
			this.lblDirector.AutoSize = true;
			this.lblDirector.Location = new System.Drawing.Point( 6, 63 );
			this.lblDirector.Name = "lblDirector";
			this.lblDirector.Size = new System.Drawing.Size( 55, 13 );
			this.lblDirector.TabIndex = 7;
			this.lblDirector.Text = "Director(s)";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point( -1, 221 );
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size( 37, 13 );
			this.label7.TabIndex = 13;
			this.label7.Text = "Active";
			// 
			// lblDuration
			// 
			this.lblDuration.AutoSize = true;
			this.lblDuration.Location = new System.Drawing.Point( 8, 143 );
			this.lblDuration.Name = "lblDuration";
			this.lblDuration.Size = new System.Drawing.Size( 92, 13 );
			this.lblDuration.TabIndex = 11;
			this.lblDuration.Text = "Duration (minutes)";
			// 
			// lblSelectMovie
			// 
			this.lblSelectMovie.AutoSize = true;
			this.lblSelectMovie.Location = new System.Drawing.Point( 6, 9 );
			this.lblSelectMovie.Name = "lblSelectMovie";
			this.lblSelectMovie.Size = new System.Drawing.Size( 69, 13 );
			this.lblSelectMovie.TabIndex = 3;
			this.lblSelectMovie.Text = "Select Movie";
			// 
			// btnAddMovie
			// 
			this.btnAddMovie.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btnAddMovie.Location = new System.Drawing.Point( 519, 4 );
			this.btnAddMovie.Name = "btnAddMovie";
			this.btnAddMovie.Size = new System.Drawing.Size( 75, 23 );
			this.btnAddMovie.TabIndex = 2;
			this.btnAddMovie.Text = "Add Movie";
			this.btnAddMovie.UseVisualStyleBackColor = true;
			// 
			// ddlMovies
			// 
			this.ddlMovies.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.ddlMovies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlMovies.FormattingEnabled = true;
			this.ddlMovies.Location = new System.Drawing.Point( 74, 6 );
			this.ddlMovies.Name = "ddlMovies";
			this.ddlMovies.Size = new System.Drawing.Size( 439, 21 );
			this.ddlMovies.TabIndex = 1;
			// 
			// tbDistributors
			// 
			this.tbDistributors.Location = new System.Drawing.Point( 4, 22 );
			this.tbDistributors.Name = "tbDistributors";
			this.tbDistributors.Padding = new System.Windows.Forms.Padding( 3 );
			this.tbDistributors.Size = new System.Drawing.Size( 600, 384 );
			this.tbDistributors.TabIndex = 4;
			this.tbDistributors.Text = "Distributor Section";
			this.tbDistributors.UseVisualStyleBackColor = true;
			// 
			// lblInfo
			// 
			this.lblInfo.AutoSize = true;
			this.lblInfo.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
			this.lblInfo.Location = new System.Drawing.Point( 8, 39 );
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size( 434, 13 );
			this.lblInfo.TabIndex = 25;
			this.lblInfo.Text = "*For multiple directors or producers please seperate them using a comma (,)";
			// 
			// txtDuration
			// 
			this.txtDuration.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.txtDuration.Location = new System.Drawing.Point( 105, 140 );
			this.txtDuration.Name = "txtDuration";
			this.txtDuration.Size = new System.Drawing.Size( 474, 20 );
			this.txtDuration.TabIndex = 26;
			// 
			// txtMovieType
			// 
			this.txtMovieType.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.txtMovieType.Location = new System.Drawing.Point( 105, 112 );
			this.txtMovieType.Name = "txtMovieType";
			this.txtMovieType.Size = new System.Drawing.Size( 474, 20 );
			this.txtMovieType.TabIndex = 27;
			// 
			// lblType
			// 
			this.lblType.AutoSize = true;
			this.lblType.Location = new System.Drawing.Point( 6, 112 );
			this.lblType.Name = "lblType";
			this.lblType.Size = new System.Drawing.Size( 31, 13 );
			this.lblType.TabIndex = 28;
			this.lblType.Text = "Type";
			// 
			// lblExpectedAudience
			// 
			this.lblExpectedAudience.AutoSize = true;
			this.lblExpectedAudience.Location = new System.Drawing.Point( 3, 169 );
			this.lblExpectedAudience.Name = "lblExpectedAudience";
			this.lblExpectedAudience.Size = new System.Drawing.Size( 100, 13 );
			this.lblExpectedAudience.TabIndex = 29;
			this.lblExpectedAudience.Text = "Expected Audience";
			// 
			// ddlExpectedAudience
			// 
			this.ddlExpectedAudience.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.ddlExpectedAudience.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlExpectedAudience.FormattingEnabled = true;
			this.ddlExpectedAudience.Items.AddRange( new object[] {
            "Mainstream",
            "Average",
            "Below-Average",
            "Specialist",
            "Non-Mainstream"} );
			this.ddlExpectedAudience.Location = new System.Drawing.Point( 105, 166 );
			this.ddlExpectedAudience.Name = "ddlExpectedAudience";
			this.ddlExpectedAudience.Size = new System.Drawing.Size( 465, 21 );
			this.ddlExpectedAudience.TabIndex = 18;
			// 
			// ddlBBFCRating
			// 
			this.ddlBBFCRating.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.ddlBBFCRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlBBFCRating.FormattingEnabled = true;
			this.ddlBBFCRating.Items.AddRange( new object[] {
            "U",
            "PG",
            "12",
            "12A",
            "15",
            "18"} );
			this.ddlBBFCRating.Location = new System.Drawing.Point( 105, 193 );
			this.ddlBBFCRating.Name = "ddlBBFCRating";
			this.ddlBBFCRating.Size = new System.Drawing.Size( 465, 21 );
			this.ddlBBFCRating.TabIndex = 30;
			// 
			// lblBBFCRating
			// 
			this.lblBBFCRating.AutoSize = true;
			this.lblBBFCRating.Location = new System.Drawing.Point( -1, 196 );
			this.lblBBFCRating.Name = "lblBBFCRating";
			this.lblBBFCRating.Size = new System.Drawing.Size( 68, 13 );
			this.lblBBFCRating.TabIndex = 31;
			this.lblBBFCRating.Text = "BBFC Rating";
			// 
			// MainGUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 632, 434 );
			this.Controls.Add( this.tbcUserRoles );
			this.MinimumSize = new System.Drawing.Size( 360, 300 );
			this.Name = "MainGUI";
			this.Text = "Cinema Management System";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler( this.MainGUI_FormClosed );
			this.Load += new System.EventHandler( this.MainGUI_Load );
			this.tbcUserRoles.ResumeLayout( false );
			this.tbBookings.ResumeLayout( false );
			this.tbBookings.PerformLayout();
			this.tbUserSettings.ResumeLayout( false );
			this.tbUserSettings.PerformLayout();
			this.grpUserDetails.ResumeLayout( false );
			this.grpUserDetails.PerformLayout();
			this.tbMovieSettings.ResumeLayout( false );
			this.tbMovieSettings.PerformLayout();
			this.groupBox1.ResumeLayout( false );
			this.groupBox1.PerformLayout();
			this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.TabControl tbcUserRoles;
        private System.Windows.Forms.TabPage tbBookings;
        private System.Windows.Forms.TabPage tbUserSettings;
        private System.Windows.Forms.Label lblSelectUser;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.ComboBox dropdownSelectUsers;
		private System.Windows.Forms.DateTimePicker dateDateOfBirth;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label lblActive;
        private System.Windows.Forms.Label lblAdmin;
        private System.Windows.Forms.Label lblDateOfBirth;
        private System.Windows.Forms.Label lblSurname;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox grpUserDetails;
        private System.Windows.Forms.Button btnCancelUserDetailsChange;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtNumOfTickets;
        private System.Windows.Forms.Label lblNumberOfTickets;
        private System.Windows.Forms.ComboBox cbbSession;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.ComboBox cbbMovieName;
        private System.Windows.Forms.Label lblMovie;
        private System.Windows.Forms.Button btnMovieDetails;
        private System.Windows.Forms.Button btnBook;
		private System.Windows.Forms.ComboBox cbAdminLevel;
		private System.Windows.Forms.Label lblContactNumber;
		private System.Windows.Forms.TextBox txtContactNumber;
		private System.Windows.Forms.Label lblUsername;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.Label lblConfirmPassword;
		private System.Windows.Forms.TextBox txtLoginUserName;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtConfirmPassword;
		private System.Windows.Forms.TabPage tbMovieSettings;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtProducer;
		private System.Windows.Forms.Label lblProducer;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label lblMovieName;
		private System.Windows.Forms.TextBox txtMovieName;
		private System.Windows.Forms.TextBox txtDirector;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Label lblDirector;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label lblDuration;
		private System.Windows.Forms.Label lblSelectMovie;
		private System.Windows.Forms.Button btnAddMovie;
		private System.Windows.Forms.ComboBox ddlMovies;
		private System.Windows.Forms.TabPage tbDistributors;
		private System.Windows.Forms.Label lblInfo;
		private System.Windows.Forms.TextBox txtDuration;
		private System.Windows.Forms.TextBox txtMovieType;
		private System.Windows.Forms.Label lblType;
		private System.Windows.Forms.ComboBox ddlExpectedAudience;
		private System.Windows.Forms.Label lblExpectedAudience;
		private System.Windows.Forms.ComboBox ddlBBFCRating;
		private System.Windows.Forms.Label lblBBFCRating;
    }
}

