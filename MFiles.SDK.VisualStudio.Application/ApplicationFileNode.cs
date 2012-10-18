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
	public class ApplicationFileNode : FileNode
	{
		public ApplicationFileNode( ApplicationProjectNode project, ProjectElement item )
			: base( project, item )
		{
		}

		protected override NodeProperties CreatePropertiesObject()
		{
			var extension = Path.GetExtension( this.Url ) ?? "";
			extension = extension.ToLowerInvariant();

			if( extension == ".js" )
				return new ScriptProperties( this );
			if( extension == ".html" )
				return new DashboardProperties( this );

			return new FileNodeProperties( this );
		}
	}
}
