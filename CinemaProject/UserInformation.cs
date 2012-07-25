using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaProject
{
	class UserInformation
	{
		public static Guid UserID { get; set; }
		public static String Name { get; set; }
		public static String Surname { get; set; }
		public static DateTime DoB { get; set; }
		public static String ContactNumber { get; set; }
		public static Boolean bActive { get; set; }
		public static Int32 AdminLevel { get; set; }
	}
}
