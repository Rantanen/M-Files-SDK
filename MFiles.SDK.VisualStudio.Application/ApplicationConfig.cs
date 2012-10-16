using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell;

namespace MFiles.SDK.VisualStudio.Application
{
	class ApplicationConfig : ProjectConfig
	{
		ApplicationProjectNode project;

		public ApplicationConfig( ApplicationProjectNode project, string config )
			: base( project, config )
		{
			this.project = project;
		}

		public override int DebugLaunch( uint grfLaunch )
		{
			project.Build( "Run" );
			return VSConstants.S_OK;
		}
	}
}
