using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaProject
{
	class MovieDetails
	{
		public String MovieName { get; set; }
		public Int32 Duration { get; set; }
		public Int32 YearReleased { get; set; }
		public String Director { get; set; }
		public String Producer { get; set; }
		public String Type { get; set; }
		public String ExpectedAudience { get; set; }
		public String BbfcRate { get; set; }
		public Boolean IsArchived { get; set; }
		public String Description { get; set; }
	}
}
