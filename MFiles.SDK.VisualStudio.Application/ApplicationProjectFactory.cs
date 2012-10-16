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
	[Guid(GuidList.guidApplicationProjectFactoryString)]
	class ApplicationProjectFactory : ProjectFactory
	{
		public ApplicationProjectFactory( ApplicationProjectPackage package )
			: base( package )
		{
			Debug.Assert( object.ReferenceEquals( package, this.Package ) );
		}

		public new ApplicationProjectPackage Package { get { return ( ( ApplicationProjectPackage )base.Package ); } }

		protected override ProjectNode CreateProject()
		{
			var project = new ApplicationProjectNode( Package );

			var serviceProvider = ( IServiceProvider )this.Package;
			var oleService = ( IOleServiceProvider )serviceProvider.GetService( typeof( IOleServiceProvider ) );
			project.SetSite( oleService );

			return project;
		}
	}
}
