using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MFiles.SDK.Tasks.PackageDefinition
{
	[XmlRoot(ElementName="file")]
	public class ApplicationFile
	{
		[XmlText()]
		public string Name { get; set; }
	}
}
