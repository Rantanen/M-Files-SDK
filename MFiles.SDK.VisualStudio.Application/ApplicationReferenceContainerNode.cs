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
	/// <summary>
	/// The "References" node in the project hierarchy.
	/// </summary>
	class ApplicationReferenceContainerNode : ReferenceContainerNode
	{
        private static string[] supportedReferenceTypes = new string[] {
            ProjectFileConstants.ProjectReference,
            ProjectFileConstants.Reference,
        };

		public ApplicationReferenceContainerNode( ApplicationProjectNode project )
			: base( project )
		{
		}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        protected override string[] SupportedReferenceTypes
        {
            get { return supportedReferenceTypes; }
        }

		/// <summary>
		/// Creates a reference node from the configuration data.
		/// </summary>
		/// <param name="referenceType"></param>
		/// <param name="element"></param>
		/// <returns></returns>
        protected override ReferenceNode CreateReferenceNode(string referenceType, ProjectElement element)
        {
            ReferenceNode node = null;

			// We got custom reference node for the file references.
            if(referenceType == ProjectFileConstants.Reference)
                return this.CreateApplicationReferenceNode(element);

			// Project references are created by the base class.
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

			// Create our own reference node.
			return new ApplicationReferenceNode( this.ProjectMgr, selectorData.bstrFile );
        }


		/// <summary>
		/// Creates a file reference from the project configuration data.
		/// </summary>
		/// <param name="element">Project configuration element</param>
		/// <returns>Application reference node</returns>
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
