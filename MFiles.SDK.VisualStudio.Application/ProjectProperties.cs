using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Project;

namespace MFiles.SDK.VisualStudio.Application
{
	public class ProjectProperties
	{
		private ProjectNode project;
		private ProjectConfig[] projectConfigs;
		bool resetCache = true;

		public ProjectProperties( ProjectNode project, ProjectConfig[] configs )
		{
			this.project = project;
			this.projectConfigs = configs;
		}

		public string GetConfigProperty(string propertyName)
		{
			if(this.project != null)
			{
				string unifiedResult = null;
				bool cacheNeedReset = true;

				for(int i = 0; i < this.projectConfigs.Length; i++)
				{
					ProjectConfig config = projectConfigs[i];
					string property = config.GetConfigurationProperty(propertyName, cacheNeedReset);
					cacheNeedReset = false;

					if(property != null)
					{
						string text = property.Trim();

						if(i == 0)
							unifiedResult = text;
						else if(unifiedResult != text)
							return ""; // tristate value is blank then
					}
				}

				return unifiedResult;
			}

			return String.Empty;
		}

		public string GetProperty( string property )
		{
			return project.GetProjectProperty( property, true );
		}

		public void SetConfigProperty( string property, string value )
		{
			CCITracing.TraceCall();
			if( value == null )
			{
				value = String.Empty;
			}

			if( this.project != null )
			{
				for( int i = 0, n = this.projectConfigs.Length; i < n; i++ )
				{
					ProjectConfig config = projectConfigs[ i ];

					config.SetConfigurationProperty( property, value );
				}

				this.project.SetProjectFileDirty( true );
			}
		}

		public void SetProperty( string property, string value )
		{
			project.SetProjectProperty( property, value );
			project.SetProjectFileDirty( true );
		}

	}
}
