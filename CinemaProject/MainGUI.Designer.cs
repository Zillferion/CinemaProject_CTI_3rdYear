namespace CinemaProject
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
			this.tbMovieSettings = new System.Windows.Forms.TabPage();
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
			this.tbcUserRoles.SuspendLayout();
			this.tbMovieSettings.SuspendLayout();
			this.tbUserSettings.SuspendLayout();
			this.grpUserDetails.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbcUserRoles
			// 
			this.tbcUserRoles.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.tbcUserRoles.Controls.Add( this.tbMovieSettings );
			this.tbcUserRoles.Controls.Add( this.tbUserSettings );
			this.tbcUserRoles.Location = new System.Drawing.Point( 12, 12 );
			this.tbcUserRoles.Name = "tbcUserRoles";
			this.tbcUserRoles.SelectedIndex = 0;
			this.tbcUserRoles.Size = new System.Drawing.Size( 608, 410 );
			this.tbcUserRoles.TabIndex = 0;
			// 
			// tbMovieSettings
			// 
			this.tbMovieSettings.BackColor = System.Drawing.SystemColors.Control;
			this.tbMovieSettings.Controls.Add( this.btnBook );
			this.tbMovieSettings.Controls.Add( this.btnMovieDetails );
			this.tbMovieSettings.Controls.Add( this.txtNumOfTickets );
			this.tbMovieSettings.Controls.Add( this.lblNumberOfTickets );
			this.tbMovieSettings.Controls.Add( this.cbbSession );
			this.tbMovieSettings.Controls.Add( this.lbl );
			this.tbMovieSettings.Controls.Add( this.cbbMovieName );
			this.tbMovieSettings.Controls.Add( this.lblMovie );
			this.tbMovieSettings.Location = new System.Drawing.Point( 4, 22 );
			this.tbMovieSettings.Name = "tbMovieSettings";
			this.tbMovieSettings.Padding = new System.Windows.Forms.Padding( 3 );
			this.tbMovieSettings.Size = new System.Drawing.Size( 600, 384 );
			this.tbMovieSettings.TabIndex = 0;
			this.tbMovieSettings.Text = "Movie Settings";
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
			this.txtConfirmPassword.TabIndex = 27;
			// 
			// txtPassword
			// 
			this.txtPassword.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.txtPassword.Location = new System.Drawing.Point( 105, 170 );
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size( 474, 20 );
			this.txtPassword.TabIndex = 26;
			// 
			// txtLoginUserName
			// 
			this.txtLoginUserName.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.txtLoginUserName.Location = new System.Drawing.Point( 105, 144 );
			this.txtLoginUserName.Name = "txtLoginUserName";
			this.txtLoginUserName.Size = new System.Drawing.Size( 474, 20 );
			this.txtLoginUserName.TabIndex = 25;
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
			this.txtContactNumber.TabIndex = 21;
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
			this.cbAdminLevel.FormattingEnabled = true;
			this.cbAdminLevel.Items.AddRange( new object[] {
            "Employee",
            "Manager",
            "Administrator"} );
			this.cbAdminLevel.Location = new System.Drawing.Point( 105, 117 );
			this.cbAdminLevel.Name = "cbAdminLevel";
			this.cbAdminLevel.Size = new System.Drawing.Size( 474, 21 );
			this.cbAdminLevel.TabIndex = 19;
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
			this.btnSave.TabIndex = 17;
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
			this.dateDateOfBirth.TabIndex = 16;
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
			this.chkActive.TabIndex = 14;
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
			this.dropdownSelectUsers.FormattingEnabled = true;
			this.dropdownSelectUsers.Location = new System.Drawing.Point( 74, 6 );
			this.dropdownSelectUsers.Name = "dropdownSelectUsers";
			this.dropdownSelectUsers.Size = new System.Drawing.Size( 439, 21 );
			this.dropdownSelectUsers.TabIndex = 1;
			this.dropdownSelectUsers.SelectedIndexChanged += new System.EventHandler( this.dropdownSelectUsers_SelectedIndexChanged );
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
			this.Load += new System.EventHandler( this.MainGUI_Load );
			this.tbcUserRoles.ResumeLayout( false );
			this.tbMovieSettings.ResumeLayout( false );
			this.tbMovieSettings.PerformLayout();
			this.tbUserSettings.ResumeLayout( false );
			this.tbUserSettings.PerformLayout();
			this.grpUserDetails.ResumeLayout( false );
			this.grpUserDetails.PerformLayout();
			this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.TabControl tbcUserRoles;
        private System.Windows.Forms.TabPage tbMovieSettings;
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
    }
}

