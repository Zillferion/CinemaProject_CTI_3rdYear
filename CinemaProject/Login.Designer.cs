namespace CinemaProject
{
	partial class Login
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnLogin = new System.Windows.Forms.Button();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.lblPassword = new System.Windows.Forms.Label();
			this.lblUsername = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnLogin
			// 
			this.btnLogin.Location = new System.Drawing.Point( 167, 91 );
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size( 179, 32 );
			this.btnLogin.TabIndex = 9;
			this.btnLogin.Text = "Login";
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler( this.btnLogin_Click );
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point( 143, 49 );
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size( 239, 20 );
			this.txtPassword.TabIndex = 8;
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point( 143, 9 );
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size( 239, 20 );
			this.txtUsername.TabIndex = 7;
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new System.Drawing.Point( 12, 52 );
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size( 56, 13 );
			this.lblPassword.TabIndex = 6;
			this.lblPassword.Text = "Password:";
			// 
			// lblUsername
			// 
			this.lblUsername.AutoSize = true;
			this.lblUsername.Location = new System.Drawing.Point( 12, 9 );
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size( 58, 13 );
			this.lblUsername.TabIndex = 5;
			this.lblUsername.Text = "Username:";
			// 
			// Login
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 430, 172 );
			this.Controls.Add( this.btnLogin );
			this.Controls.Add( this.txtPassword );
			this.Controls.Add( this.txtUsername );
			this.Controls.Add( this.lblPassword );
			this.Controls.Add( this.lblUsername );
			this.Name = "Login";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Login";
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnLogin;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.Label lblUsername;
	}
}