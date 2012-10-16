using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell.Interop;

namespace MFiles.SDK.VisualStudio.Application
{
	class ApplicationReferenceContainerNode : ReferenceContainerNode
	{
		public ApplicationReferenceContainerNode( ApplicationProjectNode project )
			: base( project )
		{
		}

        private static string[] supportedReferenceTypes = new string[] {
            ProjectFileConstants.ProjectReference,
            ProjectFileConstants.Reference,
        };
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        protected override string[] SupportedReferenceTypes
        {
            get { return supportedReferenceTypes; }
        }

        protected override ReferenceNode CreateReferenceNode(string referenceType, ProjectElement element)
        {
            ReferenceNode node = null;
            if(referenceType == ProjectFileConstants.Reference)
                return this.CreateApplicationReferenceNode(element);

			return base.CreateReferenceNode( referenceType, element );
        }

        /// <summary>
        /// Creates a file reference from the selector data.
        /// </summary>
        protected override ReferenceNode CreateFileComponent(VSCOMPONENTSELECTORDATA selectorData, string wrapperTool = null)
        {
            if(null == selectorData.bstrFile)
                throw new ArgumentNullException("selectorData");

			if( !File.Exists( selectorData.bstrFile ) )
				throw new FileNotFoundException( selectorData.bstrFile );

			return new ApplicationReferenceNode( this.ProjectMgr, selectorData.bstrFile );
        }


        protected ApplicationReferenceNode CreateApplicationReferenceNode(ProjectElement element)
        {
            try
            {
				return new ApplicationReferenceNode( this.ProjectMgr, element );
            }
            catch(ArgumentNullException e)
            {
                Trace.WriteLine("Exception : " + e.Message);
            }
            catch(FileNotFoundException e)
            {
                Trace.WriteLine("Exception : " + e.Message);
            }
            catch(BadImageFormatException e)
            {
                Trace.WriteLine("Exception : " + e.Message);
            }
            catch(FileLoadException e)
            {
                Trace.WriteLine("Exception : " + e.Message);
            }
            catch(System.Security.SecurityException e)
            {
                Trace.WriteLine("Exception : " + e.Message);
            }

			return null;
        }
	}
}
