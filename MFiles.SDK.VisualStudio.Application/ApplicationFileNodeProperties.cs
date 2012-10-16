using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Project;

namespace MFiles.SDK.VisualStudio.Application
{
	[ComVisible(true), CLSCompliant(false)]
	[Guid("919C0ED9-CCE1-4770-ADAE-9D9DA7423BEB")]	
	public class ApplicationFileNodeProperties : FileNodeProperties
	{
		[TypeConverter( typeof( ExpandableObjectConverter ) )]
		public class EnvironmentProperties : INotifyPropertyChanged
		{
			[DefaultValue( false )]
			public bool ShellUi
			{
				get { return shellUi; }
				set { shellUi = value; RaisePropertyChanged( "ShellUi" ); }
			}
			private bool shellUi;

			[DefaultValue( false )]
			public bool VaultUi
			{
				get { return vaultUi; }
				set { vaultUi = value; RaisePropertyChanged( "VaultUi" ); }
			}
			private bool vaultUi;

			[DefaultValue( false )]
			public bool VaultCore
			{
				get { return vaultCore; }
				set { vaultCore = value; RaisePropertyChanged( "VaultCore" ); }
			}
			private bool vaultCore;

			public void RaisePropertyChanged( string property )
			{
				var pc = PropertyChanged;
				if( pc != null ) pc( this, new PropertyChangedEventArgs( property ) );
			}

			public override string ToString()
			{
				var envs = new List<string>();
				if( ShellUi ) envs.Add( "Shell UI" );
				if( VaultUi ) envs.Add( "Vault UI" );
				if( VaultCore ) envs.Add( "Vault Core" );

				if( envs.Count == 0 ) return "Default";
				else return string.Join( "; ", envs.ToArray() );
			}

			public string Serialize()
			{
				var envs = new List<string>();
				if( ShellUi ) envs.Add( "shellui" );
				if( VaultUi ) envs.Add( "vaultui" );
				if( VaultCore ) envs.Add( "vaultcore" );
				return string.Join( ";", envs.ToArray() );
			}

			public void Deserialize( string envs )
			{
				var envParts = envs.ToLower().Split( ';' );
				if( envParts.Contains( "shellui" ) ) shellUi = true;
				if( envParts.Contains( "vaultui" ) ) vaultUi = true;
				if( envParts.Contains( "vaultcore" ) ) vaultCore = true;
			}

			public event PropertyChangedEventHandler PropertyChanged;
		}

		private EnvironmentProperties environments = new EnvironmentProperties();

		public ApplicationFileNodeProperties( ApplicationFileNode node )
			: base( node )
		{
			var envs = node.ItemNode.GetMetadata( "Environment" ) ?? "";
			environments.Deserialize( envs );
			environments.PropertyChanged += ( s, e ) => node.ItemNode.SetMetadata( "Environment", environments.Serialize() );
		}

		[Browsable( true )]
		[DisplayName("Environments")]
		[Description("Specifies the environments in which this script is available")]
		public EnvironmentProperties Environments
		{
			get { return environments; }
			protected set { environments = value; }
		}
	}
}
