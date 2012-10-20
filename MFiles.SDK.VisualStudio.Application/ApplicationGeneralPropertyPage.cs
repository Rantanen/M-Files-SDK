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
	/// <summary>
	/// M-Files Application Project general properties page.
	/// </summary>
	[ComVisible( true )]
	[Guid( "81139195-80C9-4077-8895-EFFCD5CC3793" )]
	[ClassInterface(ClassInterfaceType.AutoDual)]
	public class ApplicationGeneralPropertyPage : ApplicationPropertyPageBase
	{
		public override string Title { get { return "Application"; } }

		public override IApplicationPropertyPage CreatePropertyPage()
		{
			return new ApplicationGeneralPropertyPageUI( this );
		}
	}
}
