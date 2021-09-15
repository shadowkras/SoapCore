using System;
using System.ServiceModel.Channels;
using System.Xml;
using SoapCore.Extensibility;
using SoapCore.Meta;

namespace SoapCore
{
	public class SoapOptions
	{
		public Type ServiceType { get; set; }
		public string Path { get; set; }
		public SoapEncoderOptions[] EncoderOptions { get; set; }
		public SoapSerializer SoapSerializer { get; set; }
		public bool CaseInsensitivePath { get; set; }
		public ISoapModelBounder SoapModelBounder { get; set; }

		[Obsolete]
		public Binding Binding { get; set; }

		public bool UseBasicAuthentication { get; set; }

		[Obsolete]
		public int BufferThreshold { get; set; }
		[Obsolete]
		public long BufferLimit { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether publication of service metadata on HTTP GET request is activated
		/// <para>Defaults to true</para>
		/// </summary>
		public bool HttpGetEnabled { get; set; } = true;

		/// <summary>
		/// Gets or sets a value indicating whether publication of service metadata on HTTPS GET request is activated
		/// <para>Defaults to true</para>
		/// </summary>
		public bool HttpsGetEnabled { get; set; } = true;

		public bool OmitXmlDeclaration { get; set; } = true;

		public bool IndentXml { get; set; } = true;

		public XmlNamespaceManager XmlNamespacePrefixOverrides { get; set; }
		public WsdlFileOptions WsdlFileOptions { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the service messages will use 'ServiceName + Operation Name + Output/Input + Message'.
		/// <para>Or it will use the OperationName instead, to conform to older frameworks (such as Java).</para>
		/// </summary>
		public bool UseStandardInputOutputMessages { get; set; }

		public string PortName { get; set; }


		[Obsolete]
		public static SoapOptions FromSoapCoreOptions<T>(SoapCoreOptions opt)
		{
			return FromSoapCoreOptions(opt, typeof(T));
		}

		public static SoapOptions FromSoapCoreOptions(SoapCoreOptions opt, Type serviceType)
		{
			var options = new SoapOptions
			{
				ServiceType = serviceType,
				Path = opt.Path,
				EncoderOptions = opt.EncoderOptions,
				SoapSerializer = opt.SoapSerializer,
				CaseInsensitivePath = opt.CaseInsensitivePath,
				SoapModelBounder = opt.SoapModelBounder,
				UseBasicAuthentication = opt.UseBasicAuthentication,
				HttpsGetEnabled = opt.HttpsGetEnabled,
				HttpGetEnabled = opt.HttpGetEnabled,
				OmitXmlDeclaration = opt.OmitXmlDeclaration,
				IndentXml = opt.IndentXml,
				XmlNamespacePrefixOverrides = opt.XmlNamespacePrefixOverrides,
				WsdlFileOptions = opt.WsdlFileOptions,
				PortName = opt.PortName,
				UseStandardInputOutputMessages = opt.UseStandardInputOutputMessages
			};

#pragma warning disable CS0612 // Type or member is obsolete
			if (opt.Binding is object)
			{
				if (opt.Binding.HasBasicAuth())
				{
					options.UseBasicAuthentication = true;
				}

				if (options.EncoderOptions is null)
				{
					opt.EncoderOptions = opt.Binding.ToEncoderOptions();
				}
			}
#pragma warning restore CS0612 // Type or member is obsolete

			return options;
		}
	}
}
