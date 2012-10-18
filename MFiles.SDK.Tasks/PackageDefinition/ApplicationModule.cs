using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MFiles.SDK.Tasks.PackageDefinition
{
	[XmlRoot(ElementName="module")]
	public class ApplicationModule
	{
		public ApplicationModule()
		{
			Files = new List<ApplicationFile>();
		}

		[XmlAttribute(AttributeName="environment")]
		public string Environment { get; set; }

		[XmlElement(ElementName="file")]
		public List<ApplicationFile> Files { get; set; }
	}
}
