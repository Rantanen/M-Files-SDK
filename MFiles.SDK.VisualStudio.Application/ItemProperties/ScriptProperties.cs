using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Project;

namespace MFiles.SDK.VisualStudio.Application.ItemProperties
{
	[ComVisible(true), CLSCompliant(false)]
	[Guid("919C0ED9-CCE1-4770-ADAE-9D9DA7423BEB")]	
	public class ScriptProperties : FileNodeProperties
	{
		public ScriptProperties( ApplicationFileNode node )
			: base( node )
		{
		}

		[Browsable( true )]
		[DisplayName("Environment")]
		[Description("Specifies the environments in which this script is available")]
		public ScriptEnvironment Environment
		{
			get
			{
				ScriptEnvironment env;
				if( Enum.TryParse<ScriptEnvironment>( Node.ItemNode.GetMetadata( "Environment" ), out env ) )
					return env;
				return ScriptEnvironment.All;
			}
			set
			{
				Node.ItemNode.SetMetadata( "Environment", value.ToString() );
			}
		}

		[Browsable( false )]
		public string EnvironmentValue
		{
			get { return Node.ItemNode.GetMetadata( "Environment" ); }
			set { Node.ItemNode.SetMetadata( "Environment", value ); }
		}
	}

	public enum ScriptEnvironment
	{
		None,
		ShellUI,
		VaultUI,
		VaultCore,
		All
	}
}
