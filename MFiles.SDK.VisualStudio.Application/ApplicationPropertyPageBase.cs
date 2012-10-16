using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace MFiles.SDK.VisualStudio.Application
{
	[ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual)]
	[Guid("A7214157-9126-4516-AF64-C5E7F34DB825")]
	public abstract class ApplicationPropertyPageBase : IPropertyPage
	{
		IApplicationPropertyPage page = null;

		private bool active;
		private IPropertyPageSite site;
		private ProjectNode project;
		private ProjectConfig[] projectConfigs;
		private string name;
		private static volatile object Mutex = new object();
		private bool isDisposed;

		public abstract IApplicationPropertyPage CreatePropertyPage();
		public abstract string Title { get; }

		protected int ApplyChanges()
		{
			if( !IsDirty ) { return VSConstants.S_OK; }

			page.WriteProperties( new ProjectProperties( project, projectConfigs ) );

			return VSConstants.S_OK;
		}

		protected void BindProperties()
		{
			page.ReadProperties( new ProjectProperties( project, projectConfigs ) );
		}

		public bool IsDirty { get { return page != null && page.IsDirty; } }

		#region IPropertyPage methods.
		public virtual void Activate(IntPtr parent, RECT[] pRect, int bModal)
		{
			if(this.page == null)
			{
                if (pRect == null)
                {
                    throw new ArgumentNullException("pRect");
                }

				this.page = CreatePropertyPage();
				this.page.Control.Size = new Size(pRect[0].right - pRect[0].left, pRect[0].bottom - pRect[0].top);
				this.page.Control.Visible = false;
				this.page.Control.Size = new Size(550, 300);
				this.page.Control.CreateControl();
				NativeMethods.SetParent(this.page.Control.Handle, parent);
			}

			if(this.page != null)
			{
				this.active = true;
				UpdateObjects();
			}
		}

		protected void UpdateObjects()
		{
			if(this.projectConfigs != null && this.project != null)
			{
				this.BindProperties();
			}
		}
		public virtual int Apply()
		{
			if(IsDirty)
			{
				return this.ApplyChanges();
			}
			return VSConstants.S_OK;
		}

		public virtual void Deactivate()
		{
			if(null != this.page)
			{
				this.page.Control.Dispose();
				this.page = null;
			}
			this.active = false;
		}

		public virtual void GetPageInfo(PROPPAGEINFO[] arrInfo)
		{
            if (arrInfo == null)
            {
                throw new ArgumentNullException("arrInfo");
            }

			PROPPAGEINFO info = new PROPPAGEINFO();

			info.cb = (uint)Marshal.SizeOf(typeof(PROPPAGEINFO));
			info.dwHelpContext = 0;
			info.pszDocString = null;
			info.pszHelpFile = null;
			info.pszTitle = this.Title;
			info.SIZE.cx = 550;
			info.SIZE.cy = 300;
			arrInfo[0] = info;
		}

		public virtual void Help(string helpDir)
		{
		}

		public virtual int IsPageDirty()
		{
			// Note this returns an HRESULT not a Bool.
			return (IsDirty ? (int)VSConstants.S_OK : (int)VSConstants.S_FALSE);
		}

		public virtual void Move(RECT[] arrRect)
		{
            if (arrRect == null)
            {
                throw new ArgumentNullException("arrRect");
            }
			
            RECT r = arrRect[0];

			this.page.Control.Location = new Point(r.left, r.top);
			this.page.Control.Size = new Size(r.right - r.left, r.bottom - r.top);
		}

		public virtual void SetObjects(uint count, object[] punk)
		{
            if (punk == null)
                return;

			if(count > 0)
			{
				if(punk[0] is ProjectConfig)
				{
					ArrayList configs = new ArrayList();

					for(int i = 0; i < count; i++)
					{
						ProjectConfig config = (ProjectConfig)punk[i];

						if(this.project == null || (this.project != (punk[0] as ProjectConfig).ProjectMgr))
						{
							this.project = config.ProjectMgr;
						}

						configs.Add(config);
					}

					this.projectConfigs = (ProjectConfig[])configs.ToArray(typeof(ProjectConfig));
				}
				else if(punk[0] is NodeProperties)
				{
                    if (this.project == null || (this.project != (punk[0] as NodeProperties).Node.ProjectMgr))
						this.project = (punk[0] as NodeProperties).Node.ProjectMgr;

					System.Collections.Generic.Dictionary<string, ProjectConfig> configsMap = new System.Collections.Generic.Dictionary<string, ProjectConfig>();

					for(int i = 0; i < count; i++)
					{
						NodeProperties property = (NodeProperties)punk[i];
						IVsCfgProvider provider;
						ErrorHandler.ThrowOnFailure(property.Node.ProjectMgr.GetCfgProvider(out provider));
						uint[] expected = new uint[1];
						ErrorHandler.ThrowOnFailure(provider.GetCfgs(0, null, expected, null));
						if(expected[0] > 0)
						{
							ProjectConfig[] configs = new ProjectConfig[expected[0]];
							uint[] actual = new uint[1];
							ErrorHandler.ThrowOnFailure(provider.GetCfgs(expected[0], configs, actual, null));

							foreach(ProjectConfig config in configs)
							{
								if(!configsMap.ContainsKey(config.ConfigName))
								{
									configsMap.Add(config.ConfigName, config);
								}
							}
						}
					}

					if(configsMap.Count > 0)
					{
						if(this.projectConfigs == null)
						{
							this.projectConfigs = new ProjectConfig[configsMap.Keys.Count];
						}
						configsMap.Values.CopyTo(this.projectConfigs, 0);
					}
				}
			}
			else
			{
				this.project = null;
			}

			if(this.active && this.project != null)
			{
				UpdateObjects();
			}
		}


		public virtual void SetPageSite(IPropertyPageSite theSite)
		{
			this.site = theSite;
		}

		public virtual void Show(uint cmd)
		{
			this.page.Control.Visible = true; // TODO: pass SW_SHOW* flags through      
			this.page.Control.Show();
		}

		public virtual int TranslateAccelerator(MSG[] arrMsg)
		{
            if (arrMsg == null)
            {
                throw new ArgumentNullException("arrMsg");
            }

			MSG msg = arrMsg[0];

			if((msg.message < NativeMethods.WM_KEYFIRST || msg.message > NativeMethods.WM_KEYLAST) && (msg.message < NativeMethods.WM_MOUSEFIRST || msg.message > NativeMethods.WM_MOUSELAST))
				return 1;

			return (NativeMethods.IsDialogMessageA(this.page.Control.Handle, ref msg)) ? 0 : 1;
		}

		#endregion
	}
}
