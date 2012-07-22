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
            this.tbUserSettings = new System.Windows.Forms.TabPage();
            this.grpUserDetails = new System.Windows.Forms.GroupBox();
            this.btnCancelUserDetailsChange = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.dateDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.txtName = new System.Windows.Forms.TextBox();
            this.chkAdmin = new System.Windows.Forms.CheckBox();
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
            this.tbUserSettings.SuspendLayout();
            this.grpUserDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcUserRoles
            // 
            this.tbcUserRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbcUserRoles.Controls.Add(this.tbMovieSettings);
            this.tbcUserRoles.Controls.Add(this.tbUserSettings);
            this.tbcUserRoles.Location = new System.Drawing.Point(12, 12);
            this.tbcUserRoles.Name = "tbcUserRoles";
            this.tbcUserRoles.SelectedIndex = 0;
            this.tbcUserRoles.Size = new System.Drawing.Size(320, 238);
            this.tbcUserRoles.TabIndex = 0;
            // 
            // tbMovieSettings
            // 
            this.tbMovieSettings.BackColor = System.Drawing.SystemColors.Control;
            this.tbMovieSettings.Location = new System.Drawing.Point(4, 22);
            this.tbMovieSettings.Name = "tbMovieSettings";
            this.tbMovieSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tbMovieSettings.Size = new System.Drawing.Size(312, 212);
            this.tbMovieSettings.TabIndex = 0;
            this.tbMovieSettings.Text = "Movie Settings";
            // 
            // tbUserSettings
            // 
            this.tbUserSettings.BackColor = System.Drawing.SystemColors.Control;
            this.tbUserSettings.Controls.Add(this.grpUserDetails);
            this.tbUserSettings.Controls.Add(this.lblSelectUser);
            this.tbUserSettings.Controls.Add(this.btnAddUser);
            this.tbUserSettings.Controls.Add(this.dropdownSelectUsers);
            this.tbUserSettings.Location = new System.Drawing.Point(4, 22);
            this.tbUserSettings.Name = "tbUserSettings";
            this.tbUserSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tbUserSettings.Size = new System.Drawing.Size(312, 212);
            this.tbUserSettings.TabIndex = 1;
            this.tbUserSettings.Text = "User Settings";
            // 
            // grpUserDetails
            // 
            this.grpUserDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpUserDetails.Controls.Add(this.btnCancelUserDetailsChange);
            this.grpUserDetails.Controls.Add(this.btnSave);
            this.grpUserDetails.Controls.Add(this.lblName);
            this.grpUserDetails.Controls.Add(this.dateDateOfBirth);
            this.grpUserDetails.Controls.Add(this.txtName);
            this.grpUserDetails.Controls.Add(this.chkAdmin);
            this.grpUserDetails.Controls.Add(this.txtSurname);
            this.grpUserDetails.Controls.Add(this.chkActive);
            this.grpUserDetails.Controls.Add(this.lblSurname);
            this.grpUserDetails.Controls.Add(this.lblActive);
            this.grpUserDetails.Controls.Add(this.lblDateOfBirth);
            this.grpUserDetails.Controls.Add(this.lblAdmin);
            this.grpUserDetails.Location = new System.Drawing.Point(9, 33);
            this.grpUserDetails.Name = "grpUserDetails";
            this.grpUserDetails.Size = new System.Drawing.Size(297, 173);
            this.grpUserDetails.TabIndex = 17;
            this.grpUserDetails.TabStop = false;
            this.grpUserDetails.Text = "User Details";
            // 
            // btnCancelUserDetailsChange
            // 
            this.btnCancelUserDetailsChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelUserDetailsChange.Enabled = false;
            this.btnCancelUserDetailsChange.Location = new System.Drawing.Point(87, 144);
            this.btnCancelUserDetailsChange.Name = "btnCancelUserDetailsChange";
            this.btnCancelUserDetailsChange.Size = new System.Drawing.Size(75, 23);
            this.btnCancelUserDetailsChange.TabIndex = 18;
            this.btnCancelUserDetailsChange.Text = "Cancel";
            this.btnCancelUserDetailsChange.UseVisualStyleBackColor = true;
            this.btnCancelUserDetailsChange.Click += new System.EventHandler(this.btnCancelUserDetailsChange_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(6, 144);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Name";
            // 
            // dateDateOfBirth
            // 
            this.dateDateOfBirth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateDateOfBirth.Enabled = false;
            this.dateDateOfBirth.Location = new System.Drawing.Point(78, 62);
            this.dateDateOfBirth.Name = "dateDateOfBirth";
            this.dateDateOfBirth.Size = new System.Drawing.Size(213, 20);
            this.dateDateOfBirth.TabIndex = 16;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(78, 13);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(213, 20);
            this.txtName.TabIndex = 4;
            // 
            // chkAdmin
            // 
            this.chkAdmin.AutoSize = true;
            this.chkAdmin.Enabled = false;
            this.chkAdmin.Location = new System.Drawing.Point(78, 94);
            this.chkAdmin.Name = "chkAdmin";
            this.chkAdmin.Size = new System.Drawing.Size(15, 14);
            this.chkAdmin.TabIndex = 15;
            this.chkAdmin.UseVisualStyleBackColor = true;
            // 
            // txtSurname
            // 
            this.txtSurname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSurname.Enabled = false;
            this.txtSurname.Location = new System.Drawing.Point(78, 39);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(213, 20);
            this.txtSurname.TabIndex = 6;
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Enabled = false;
            this.chkActive.Location = new System.Drawing.Point(78, 120);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(15, 14);
            this.chkActive.TabIndex = 14;
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // lblSurname
            // 
            this.lblSurname.AutoSize = true;
            this.lblSurname.Location = new System.Drawing.Point(6, 42);
            this.lblSurname.Name = "lblSurname";
            this.lblSurname.Size = new System.Drawing.Size(49, 13);
            this.lblSurname.TabIndex = 7;
            this.lblSurname.Text = "Surname";
            // 
            // lblActive
            // 
            this.lblActive.AutoSize = true;
            this.lblActive.Location = new System.Drawing.Point(6, 120);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new System.Drawing.Size(37, 13);
            this.lblActive.TabIndex = 13;
            this.lblActive.Text = "Active";
            // 
            // lblDateOfBirth
            // 
            this.lblDateOfBirth.AutoSize = true;
            this.lblDateOfBirth.Location = new System.Drawing.Point(6, 68);
            this.lblDateOfBirth.Name = "lblDateOfBirth";
            this.lblDateOfBirth.Size = new System.Drawing.Size(66, 13);
            this.lblDateOfBirth.TabIndex = 9;
            this.lblDateOfBirth.Text = "Date of Birth";
            // 
            // lblAdmin
            // 
            this.lblAdmin.AutoSize = true;
            this.lblAdmin.Location = new System.Drawing.Point(6, 94);
            this.lblAdmin.Name = "lblAdmin";
            this.lblAdmin.Size = new System.Drawing.Size(36, 13);
            this.lblAdmin.TabIndex = 11;
            this.lblAdmin.Text = "Admin";
            // 
            // lblSelectUser
            // 
            this.lblSelectUser.AutoSize = true;
            this.lblSelectUser.Location = new System.Drawing.Point(6, 9);
            this.lblSelectUser.Name = "lblSelectUser";
            this.lblSelectUser.Size = new System.Drawing.Size(62, 13);
            this.lblSelectUser.TabIndex = 3;
            this.lblSelectUser.Text = "Select User";
            // 
            // btnAddUser
            // 
            this.btnAddUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddUser.Location = new System.Drawing.Point(231, 4);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(75, 23);
            this.btnAddUser.TabIndex = 2;
            this.btnAddUser.Text = "Add User";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // dropdownSelectUsers
            // 
            this.dropdownSelectUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dropdownSelectUsers.FormattingEnabled = true;
            this.dropdownSelectUsers.Location = new System.Drawing.Point(74, 6);
            this.dropdownSelectUsers.Name = "dropdownSelectUsers";
            this.dropdownSelectUsers.Size = new System.Drawing.Size(151, 21);
            this.dropdownSelectUsers.TabIndex = 1;
            this.dropdownSelectUsers.SelectedIndexChanged += new System.EventHandler(this.dropdownSelectUsers_SelectedIndexChanged);
            // 
            // MainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 262);
            this.Controls.Add(this.tbcUserRoles);
            this.MinimumSize = new System.Drawing.Size(360, 300);
            this.Name = "MainGUI";
            this.Text = "Cinema Management System";
            this.Load += new System.EventHandler(this.MainGUI_Load);
            this.tbcUserRoles.ResumeLayout(false);
            this.tbUserSettings.ResumeLayout(false);
            this.tbUserSettings.PerformLayout();
            this.grpUserDetails.ResumeLayout(false);
            this.grpUserDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcUserRoles;
        private System.Windows.Forms.TabPage tbMovieSettings;
        private System.Windows.Forms.TabPage tbUserSettings;
        private System.Windows.Forms.Label lblSelectUser;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.ComboBox dropdownSelectUsers;
        private System.Windows.Forms.DateTimePicker dateDateOfBirth;
        private System.Windows.Forms.CheckBox chkAdmin;
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
    }
}

