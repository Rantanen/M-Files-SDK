using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace MFiles.SDK.VisualStudio.Application
{
	[ComVisible( true )]
	[Guid("CA16E6EF-8014-4EAD-BDB8-6A9A59DD6830")]
	[ClassInterface(ClassInterfaceType.AutoDual)]
	public class ApplicationDebugPropertyPage :
		ApplicationPropertyPageBase
	{
		public override string Title { get { return "Debug"; } }

		public override IApplicationPropertyPage CreatePropertyPage()
		{
			return new ApplicationDebugPropertyPageUI( this );
		}
	}
}
