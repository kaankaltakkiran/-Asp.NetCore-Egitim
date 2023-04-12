using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MyAspNetCoreApp.Web.TagHelpers
{
	//custom tag helper
	[HtmlTargetElement("thumbnail")]
	public class ImageThumbnailTagHelper:TagHelper
	{
		//dosya yolu 
        public string ImageSrc { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "img";
			//dosya ismini ayırma
			string filename= ImageSrc.Split('.')[0];
			//dosa uzantısı
			string fileExtensions=Path.GetExtension(ImageSrc);
			output.Attributes.SetAttribute("src", $"{filename}-100x100{fileExtensions}");
		}
	}
}
