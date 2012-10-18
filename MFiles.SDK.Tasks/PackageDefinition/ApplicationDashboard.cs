using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MFiles.SDK.Tasks.PackageDefinition
{
	[XmlRoot(ElementName="dashboard")]
	public class ApplicationDashboard
	{
		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }

		[XmlElement(ElementName = "content")]
		public string Content { get; set; }
	}
}
