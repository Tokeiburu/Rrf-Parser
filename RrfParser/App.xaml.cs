using System;
using System.Reflection;
using System.Windows;
using ErrorManager;
using GRF.Image;
using GrfToWpfBridge.Application;
using RrfParser.Core;
using TokeiLibrary;

namespace RrfParser {
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {
		public App() {
			Configuration.ConfigAsker = RrfParserConfiguration.ConfigAsker;
			Configuration.ProgramDataPath = RrfParserConfiguration.ProgramDataPath;
			ErrorHandler.SetErrorHandler(new DefaultErrorHandler());
		}

		protected override void OnStartup(StartupEventArgs e) {
			ApplicationManager.CrashReportEnabled = true;
			ImageConverterManager.AddConverter(new DefaultImageConverter());

			Configuration.SetImageRendering(Resources);

			Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("pack://application:,,,/" + Assembly.GetEntryAssembly().GetName().Name.Replace(" ", "%20") + ";component/WPF/Styles/GRFEditorStyles.xaml", UriKind.RelativeOrAbsolute) });
			Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("pack://application:,,,/" + Assembly.GetEntryAssembly().GetName().Name.Replace(" ", "%20") + ";component/WPF/Styles/StyleLightBlue.xaml", UriKind.RelativeOrAbsolute) });

			base.OnStartup(e);
		}
	}
}
