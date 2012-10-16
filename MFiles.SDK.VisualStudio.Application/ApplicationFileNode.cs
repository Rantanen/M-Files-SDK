using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			return new ApplicationFileNodeProperties( this );
		}
	}
}
