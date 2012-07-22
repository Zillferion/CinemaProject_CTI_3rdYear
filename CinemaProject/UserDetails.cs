using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaProject
{
    public class UserDetails
    {
        public string Name;
        public string Surname;
        public DateTime DateOfBirth;
        public bool IsAdmin;
        public bool IsActive;

        public UserDetails()
        {
            Name = String.Empty;
            Surname = String.Empty;
            DateOfBirth = DateTime.Today;
            IsAdmin = false;
            IsActive = true;
        }

        public UserDetails(string name, string surname, DateTime dateOfBirth, bool isAdmin, bool isActive)
        {
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            IsAdmin = isAdmin;
            IsActive = isActive;
        }

        public string ToString()
        {
            return String.Format("{0} {1}", Name, Surname);
        }
    }
}
