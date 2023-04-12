using AutoMapper;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Mapping
{
    public class ViewModelMapping:Profile
    {
        public ViewModelMapping()
        {
            //Product dan ProductViewModel çevir
            CreateMap<Product,ProductViewModel>().ReverseMap();
			//Visitor gördğünde VisitorViewModel Çevir
			CreateMap<Visitor,VisitorViewModel>().ReverseMap();
        }
    }
}
