using System;

namespace CinemaProject.Classes
{
	public class UserDetails
	{
		/*
		 * Contains details of selected users.
		 */
		public string Name;
		public string Surname;
		public DateTime DateOfBirth;
		public Int32 adminLevel;
		public bool IsActive;
		public DateTime dateRegistered;
		public String contactNumber;
		public String userName;
		public String userPassword;
		public Guid userGuid;

		public UserDetails()
		{
			Name = String.Empty;
			Surname = String.Empty;
			DateOfBirth = DateTime.Today;
			adminLevel = 0;
			IsActive = true;
			dateRegistered = DateTime.Now;
			contactNumber = String.Empty;
			userName = String.Empty;
			userPassword = String.Empty;
			userGuid = new Guid();
		}

		public UserDetails( string name, string surname, DateTime dateOfBirth, Int32 adminLvl, bool isActive, DateTime registered, String uName, String uPassword, Guid uGuid, String contactNum )
		{
			Name = name;
			Surname = surname;
			DateOfBirth = dateOfBirth;
			adminLevel = adminLvl;
			IsActive = isActive;
			dateRegistered = registered;
			contactNumber = contactNum;
			userName = uName;
			userPassword = uPassword;
			userGuid = uGuid;
		}

		public string ToString()
		{
			return String.Format( "{0} {1}", Name, Surname );
		}
	}
}
