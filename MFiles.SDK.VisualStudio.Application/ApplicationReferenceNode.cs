
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using MSBuild = Microsoft.Build.Evaluation;
using MSBuildExecution = Microsoft.Build.Execution;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Framework;

namespace Microsoft.VisualStudio.Project
{
	[CLSCompliant(false)]
	[ComVisible(true)]
	public class ApplicationReferenceNode : ReferenceNode
	{
		private string path;
		private string filename;

		/// <summary>
		/// A flag for specifying if the object was disposed.
		/// </summary>
		private bool isDisposed;

		public override string Url { get { return path; } }

		public override string Caption { get { return filename; } }

		/// <summary>
		/// Constructor for the ReferenceNode
		/// </summary>
		public ApplicationReferenceNode(ProjectNode root, ProjectElement element)
			: base(root, element)
		{
			this.path = element.GetFullPathForElement();
			this.filename = Path.GetFileName(Url);
		}

		/// <summary>
		/// Constructor for the AssemblyReferenceNode
		/// </summary>
		public ApplicationReferenceNode(ProjectNode root, string assemblyPath)
			: base(root)
		{
			// Validate the input parameters.
			if(null == root)
			{
				throw new ArgumentNullException("root");
			}
			if(string.IsNullOrEmpty(assemblyPath))
			{
				throw new ArgumentNullException("assemblyPath");
			}

			this.path = assemblyPath;
			this.filename = Path.GetFileName( Url );

		}

		/// <summary>
		/// Closes the node.
		/// </summary>
		/// <returns></returns>
		public override int Close()
		{
			try { this.Dispose(true); }
			finally { base.Close(); }

			return VSConstants.S_OK;
		}

		/// <summary>
		/// Links a reference node to the project and hierarchy.
		/// </summary>
		protected override void BindReferenceData()
		{
			// Create the ProjectElement if it doesn't exist.
			if(this.ItemNode == null || this.ItemNode.Item == null)
			{
				this.ItemNode = new ProjectElement(this.ProjectMgr, this.Url, ProjectFileConstants.Reference);
			}

			// Set the basic information.
			this.ItemNode.SetMetadata(ProjectFileConstants.Name, this.Caption);
		}

		/// <summary>
		/// Disposes the node
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose(bool disposing)
		{
			if(this.isDisposed) { return; }

			base.Dispose(disposing);
			this.isDisposed = true;
		}

		/// <summary>
		/// Checks if an assembly is already added. The method parses all references and compares the full assemblynames, or the location of the assemblies to decide whether two assemblies are the same.
		/// </summary>
		/// <returns>true if the assembly has already been added.</returns>
		protected internal override bool IsAlreadyAdded(out ReferenceNode existingReference)
		{
			ReferenceContainerNode referencesFolder = this.ProjectMgr.FindChild(ReferenceContainerNode.ReferencesNodeVirtualName) as ReferenceContainerNode;
			Debug.Assert(referencesFolder != null, "Could not find the References node");

			for(HierarchyNode n = referencesFolder.FirstChild; n != null; n = n.NextSibling)
			{
				ApplicationReferenceNode referenceNode = n as ApplicationReferenceNode;
				if(null != referenceNode)
				{
					// We will check if the full assemblynames are the same or if the Url of the assemblies is the same.
					if(String.Compare(referenceNode.Url, this.Url, StringComparison.OrdinalIgnoreCase) == 0)
					{
						existingReference = referenceNode;
						return true;
					}
				}
			}

			existingReference = null;
			return false;
		}

		/// <summary>
		/// Determines if this is node a valid node for painting the default reference icon.
		/// </summary>
		/// <returns></returns>
		protected override bool CanShowDefaultIcon()
		{
			if(String.IsNullOrEmpty(this.path) || !File.Exists(this.path))
			{
				return false;
			}

			return true;
		}
	}
}
