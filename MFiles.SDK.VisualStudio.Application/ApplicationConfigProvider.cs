using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Project;

namespace MFiles.SDK.VisualStudio.Application
{
	class ApplicationConfigProvider : ConfigProvider
	{
		ApplicationProjectNode project;

		public ApplicationConfigProvider( ApplicationProjectNode project )
			: base(project)
		{
			this.project = project;
		}

		protected override ProjectConfig CreateProjectConfiguration( string configName )
		{
			return new ApplicationConfig( this.project, configName );
		}
	}
}
