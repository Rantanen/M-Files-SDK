using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFiles.SDK.VisualStudio.Application.ItemProperties;
using Microsoft.VisualStudio.Project;

namespace MFiles.SDK.VisualStudio.Application
{
	/// <summary>
	/// Application project file node for creating custom properties objects.
	/// </summary>
	public class ApplicationFileNode : FileNode
	{
		public ApplicationFileNode( ApplicationProjectNode project, ProjectElement item )
			: base( project, item )
		{
		}

		protected override NodeProperties CreatePropertiesObject()
		{
			// Decide on the correct properties based on the extension.
			var extension = Path.GetExtension( this.Url ) ?? "";
			extension = extension.ToLowerInvariant();

			if( extension == ".js" )
				return new ScriptProperties( this );
			if( extension == ".html" )
				return new DashboardProperties( this );

			// In default case return the default file node properties.
			return new FileNodeProperties( this );
		}
	}
}
