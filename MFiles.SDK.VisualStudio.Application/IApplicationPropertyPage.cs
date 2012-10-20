using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.Project;

namespace MFiles.SDK.VisualStudio.Application
{
	/// <summary>
	/// Interface for application property page UI controls.
	/// </summary>
	public interface IApplicationPropertyPage
	{
		Control Control { get; }
		bool IsDirty { get; }
		void WriteProperties( ProjectProperties properties );
		void ReadProperties( ProjectProperties properties );
	}
}
