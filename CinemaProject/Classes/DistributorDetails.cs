using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaProject.Classes
{
	class DistributorDetails
	{
		public Guid Id { get; set; }
		public String Name { get; set; }
		public Boolean IsActive { get; set; }
		public String Email { get; set; }
		public String ContactNumber { get; set; }
		public String PhysicalAddress { get; set; }
		public Boolean ReceiveMontly { get; set; }
		public Boolean ReceiveQuaterly { get; set; }
		public Boolean ReceiveHalfYearly { get; set; }
		public Boolean ReceiveYearly { get; set; }
	}
}
