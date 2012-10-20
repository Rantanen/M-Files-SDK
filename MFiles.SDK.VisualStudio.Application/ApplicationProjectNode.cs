using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell.Interop;

namespace MFiles.SDK.VisualStudio.Application
{
	/// <summary>
	/// The main Application project.
	/// 
	/// This class represents the project itself as well as the node
	/// in the solution explorer.
	/// </summary>
	public class ApplicationProjectNode : ProjectNode
	{
		// Some private values taken from the original ProjectNode.
		private static Guid addComponentLastActiveTab = VSConstants.GUID_BrowseFilePage;
		private Guid GUID_MruPage = new Guid("{19B97F03-9594-4c1c-BE28-25FF030113B3}");

		private ApplicationProjectPackage package;
		private ApplicationReferenceContainerNode referenceContainer;

		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors" )]
		public ApplicationProjectNode( ApplicationProjectPackage package )
		{
			this.package = package;

			// Enable 'delete' in the context menu.
			this.CanProjectDeleteItems = true;

			// Display the property pages in the designer area (.NET project style)
			// instead of seperate dialog (C++ project style).
			this.SupportsProjectDesigner = true;
		}

		public override bool IsCodeFile( string fileName )
		{
			return Path.GetExtension( fileName ).ToLowerInvariant() == ".js";
		}

		/// <summary>
		/// Gets the project type guid.
		/// </summary>
		public override Guid ProjectGuid { get { return GuidList.guidApplicationProjectFactory; } }

		/// <summary>
		/// Gets the project type this project serves.
		/// </summary>
		public override string ProjectType { get { return "MFilesApplication"; } }

        /// <summary>
		/// Override AddProjectReference to hide the .NET and COM tabs.
		/// 
		/// This is copied from the base project - just removed some of
		/// the elements in the tabInitList to remove the .NET and COM
		/// tabs from the dialog.
        /// </summary>
        /// <returns>Success message</returns>
        public override int AddProjectReference()
        {
            CCITracing.TraceCall();

            IVsComponentSelectorDlg4 componentDialog;
            string strBrowseLocations = Path.GetDirectoryName(this.BaseURI.Uri.LocalPath);
			var tabInitList = new List<VSCOMPONENTSELECTORTABINIT>()
			{
				new VSCOMPONENTSELECTORTABINIT {
		            // Tell the Add Reference dialog to call hierarchies GetProperty with the following
		            // propID to enablefiltering out ourself from the Project to Project reference
					varTabInitInfo = (int)__VSHPROPID.VSHPROPID_ShowProjInSolutionPage,
					guidTab = VSConstants.GUID_SolutionPage,
				},
	            // Add the Browse for file page            
				new VSCOMPONENTSELECTORTABINIT {
					varTabInitInfo = 0,
					guidTab = VSConstants.GUID_BrowseFilePage,
				},
			};
			tabInitList.ForEach(tab => tab.dwSize = (uint)Marshal.SizeOf(typeof(VSCOMPONENTSELECTORTABINIT)));

            componentDialog = this.GetService(typeof(IVsComponentSelectorDlg)) as IVsComponentSelectorDlg4;
            try
            {
                // call the container to open the add reference dialog.
                if (componentDialog != null)
                {
                    // Let the project know not to show itself in the Add Project Reference Dialog page
                    this.ShowProjectInSolutionPage = false;
                    // call the container to open the add reference dialog.
                    string browseFilters = "M-Files Application Packages (*.zip)\0*.zip\0";
                    ErrorHandler.ThrowOnFailure(componentDialog.ComponentSelectorDlg5(
                        (System.UInt32)(__VSCOMPSELFLAGS.VSCOMSEL_MultiSelectMode | __VSCOMPSELFLAGS.VSCOMSEL_IgnoreMachineName),
                        (IVsComponentUser)this,
                        0,
						null,
						SR.GetString(SR.AddReferenceDialogTitle, CultureInfo.CurrentUICulture),   // Title
                        "VS.AddReference",                          // Help topic
                        0, 0,
                        (uint)tabInitList.Count,
                        tabInitList.ToArray(),
                        ref addComponentLastActiveTab,
						browseFilters,
                        ref strBrowseLocations,
						this.TargetFrameworkMoniker.FullName));
                }
            }
            catch (COMException e)
            {
                Trace.WriteLine("Exception : " + e.Message);
                return e.ErrorCode;
            }
            finally
            {
                // Let the project know it can show itself in the Add Project Reference Dialog page
                this.ShowProjectInSolutionPage = true;
            }
            return VSConstants.S_OK;
        }


		// Configuration page methods.

		/// <summary>
		/// Get the configuration pages which do not have the Configuration drop downs enabled.
		/// </summary>
		/// <returns>Guids to the configuration pages.</returns>
		protected override Guid[] GetConfigurationIndependentPropertyPages ()
		{
			var guids = new List<Guid>();
			guids.Add( typeof( ApplicationGeneralPropertyPage ).GUID );
			return guids.ToArray();
		}

		/// <summary>
		/// Gets the configuration pages which depend on the current project configuration.
		/// </summary>
		/// <returns>Guids to the configuration pages.</returns>
		protected override Guid[] GetConfigurationDependentPropertyPages()
		{
			var guids = new List<Guid>();
			guids.Add( typeof( ApplicationDebugPropertyPage ).GUID );
			return guids.ToArray();
		}

		/// <summary>
		/// Returns.. priority designer pages.. (Documentation is as helpful as ever).
		/// </summary>
		/// <returns></returns>
		protected override Guid[] GetPriorityProjectDesignerPages()
		{
			// Returning the first independent property page is
			// as good a guess as any for now.
			Guid[] guid = new Guid[ 1 ];
			guid[ 0 ] = GetConfigurationIndependentPropertyPages()[ 0 ];
			return guid;
		}


		// Factory methods for returning our own implementations.

		protected override ReferenceContainerNode CreateReferenceContainerNode()
		{
			return new ApplicationReferenceContainerNode( this );
		}

		public override FileNode CreateFileNode( ProjectElement item )
		{
			if( item == null ) throw new ArgumentNullException( "item" );
			return new ApplicationFileNode( this, item );
		}

		protected override ConfigProvider CreateConfigProvider()
		{
			return new ApplicationConfigProvider( this );
		}

	}
}
