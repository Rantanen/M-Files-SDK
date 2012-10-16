using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MFiles.SDK.Tasks.PackageDefinition
{
	[XmlRoot( ElementName = "application" )]
	public class ApplicationDefinition
	{
		public ApplicationDefinition()
		{
			Modules = new List<ApplicationModule>();
			Dashboards = new List<ApplicationDashboard>();
		}

		[XmlElement( ElementName = "guid" )]
		public string Guid { get; set; }

		[XmlElement( ElementName = "name" )]
		public string Name { get; set; }

		[XmlElement( ElementName = "description" )]
		public string Description { get; set; }

		[XmlArray( ElementName = "modules" )]
		[XmlArrayItem( ElementName = "module" )]
		public List<ApplicationModule> Modules { get; set; }

		[XmlArray( ElementName = "dashboards" )]
		[XmlArrayItem( ElementName = "dashboard" )]
		public List<ApplicationDashboard> Dashboards { get; set; }
	}
}
