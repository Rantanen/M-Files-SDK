using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Project;

using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace MFiles.SDK.VisualStudio.Application
{
	/// <summary>
	/// Project factory for creating the Application project.
	/// </summary>
	[Guid(GuidList.guidApplicationProjectFactoryString)]
	class ApplicationProjectFactory : ProjectFactory
	{
		public ApplicationProjectFactory( ApplicationProjectPackage package )
			: base( package )
		{
		}

		public new ApplicationProjectPackage Package { get { return ( ( ApplicationProjectPackage )base.Package ); } }

		/// <summary>
		/// Create the application project
		/// </summary>
		/// <returns>Application project node</returns>
		protected override ProjectNode CreateProject()
		{
			var project = new ApplicationProjectNode( Package );

			// TODO: Maybe explain what the IOleServiceProvider does here.
			// Now we're just using it because the samples said so. :p
			var serviceProvider = ( IServiceProvider )this.Package;
			var oleService = ( IOleServiceProvider )serviceProvider.GetService( typeof( IOleServiceProvider ) );
			project.SetSite( oleService );

			return project;
		}
	}
}
