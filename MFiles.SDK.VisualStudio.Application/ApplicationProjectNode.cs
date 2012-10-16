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
	public class ApplicationProjectNode : ProjectNode
	{
		private static Guid addComponentLastActiveTab = VSConstants.GUID_BrowseFilePage;
		private Guid GUID_MruPage = new Guid("{19B97F03-9594-4c1c-BE28-25FF030113B3}");


		private ApplicationProjectPackage package;
		private ApplicationReferenceContainerNode referenceContainer;

		public ApplicationProjectNode( ApplicationProjectPackage package )
		{
			this.package = package;

			this.CanProjectDeleteItems = true;
			this.SupportsProjectDesigner = true;
		}

		public override bool IsCodeFile( string fileName )
		{
			return true;
		}

		public override Guid ProjectGuid
		{
			get { return GuidList.guidApplicationProjectFactory; }
		}

		public override string ProjectType
		{
			get { return "MFilesApplication"; }
		}

		protected override ReferenceContainerNode CreateReferenceContainerNode()
		{
			this.referenceContainer = new ApplicationReferenceContainerNode(this);
			return this.referenceContainer;
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

        /// <summary>
		/// Override AddProjectReference to hide the .NET and COM tabs.
        /// </summary>
        /// <returns></returns>
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
                    string browseFilters = "M-Files Application Files (*.js;*.zip)\0*.js;*.zip\0";
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

		protected override Guid[] GetConfigurationIndependentPropertyPages ()
		{
			var guids = new List<Guid>();
			guids.Add( typeof( ApplicationGeneralPropertyPage ).GUID );
			guids.Add( typeof( ApplicationDebugPropertyPage ).GUID );
			return guids.ToArray();
		}

		protected override Guid[] GetPriorityProjectDesignerPages()
		{
			return GetConfigurationIndependentPropertyPages();
		}

		protected override Guid[] GetConfigurationDependentPropertyPages()
		{
			var guids = new List<Guid>();
			// guids.Add( typeof( ApplicationDebugPropertyPage ).GUID );
			return guids.ToArray();
		}

		public override void AddFileFromTemplate( string source, string target )
		{
			this.FileTemplateProcessor.UntokenFile( source, target );
			this.FileTemplateProcessor.Reset();
		}
	}
}
