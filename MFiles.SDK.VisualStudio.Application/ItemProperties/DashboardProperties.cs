using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Project;

namespace MFiles.SDK.VisualStudio.Application.ItemProperties
{
	[ComVisible(true), CLSCompliant(false)]
	[Guid("93DE2DF2-B3FF-49E1-BF9B-517BB3C4F9EC")]
	public class DashboardProperties : FileNodeProperties
	{
		public DashboardProperties( ApplicationFileNode node )
			: base( node )
		{
		}

		[Browsable( true )]
		[DisplayName( "Is Dashboard" )]
		[Description( "Specifies whether this HTML file is a dashboard." )]
		[DefaultValueAttribute( true )]
		public bool IsDashboard
		{
			get
			{
				bool isDashboard;
				if( bool.TryParse( Node.ItemNode.GetMetadata( "IsDashboard" ), out isDashboard ) )
					return isDashboard;
				return true;
			}
			set
			{
				Node.ItemNode.SetMetadata( "IsDashboard", value.ToString() );
			}
		}

		[Browsable( true )]
		[DisplayName( "Dashboard ID" )]
		[Description( "Specifies the ID for this dashboard." )]
		[AmbientValue( "" )]
		public string DashboardID
		{
			get
			{
				string value = Node.ItemNode.GetMetadata( "DashboardID" );
				if( string.IsNullOrEmpty( value ) )
					return Path.GetFileNameWithoutExtension( this.Node.Url );
				return value;
			}
			set
			{
				if( value == "" ) value = null;
				Node.ItemNode.SetMetadata( "DashboardID", value );
			}
		}

		public bool ShouldSerializeDashboardID()
		{
			return !string.IsNullOrEmpty( Node.ItemNode.GetMetadata( "DashboardID" ) );
		}

		[Browsable( false )]
		public string IsDashboardValue
		{
			get { return Node.ItemNode.GetMetadata( "IsDashboard" ); }
			set { Node.ItemNode.SetMetadata( "IsDashboard", value ); }
		}
	}
}
