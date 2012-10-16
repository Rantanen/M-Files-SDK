using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.Project;

namespace MFiles.SDK.VisualStudio.Application
{
	public interface IApplicationPropertyPage
	{
		Control Control { get; }
		bool IsDirty { get; }
		void WriteProperties( ProjectNode project );
		void ReadProperties( ProjectNode project );
	}
}
