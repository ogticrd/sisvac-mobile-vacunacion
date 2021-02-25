using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SisVac.Helpers
{
    [ContentProperty(nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }

            // Do your translation lookup here, using whatever method you require
            var assembly = typeof(ImageResourceExtension).GetTypeInfo().Assembly;
            var imageSource = ImageSource.FromResource("SisVac.Resources.Images." + Source, assembly);

#if DEBUG
            foreach (var res in assembly.GetManifestResourceNames())
            {
                System.Diagnostics.Debug.WriteLine("found resource: " + res);
            }
#endif

            return imageSource;
        }
    }
}
