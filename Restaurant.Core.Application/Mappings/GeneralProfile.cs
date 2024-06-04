using AutoMapper;
using Restaurant.Core.Application.Enums;
using Restaurant.Core.Application.ViewModels.Dish;
using Restaurant.Core.Application.ViewModels.DishOrder;
using Restaurant.Core.Application.ViewModels.Ingredient;
using Restaurant.Core.Application.ViewModels.Order;
using Restaurant.Core.Application.ViewModels.Table;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region "User Profile"
            #endregion

            #region "Ingredient Profile"
            CreateMap<Ingredient, IngredientViewModel>()
                    .ReverseMap()
                    .ForMember(x => x.Created, opt => opt.Ignore())
                    .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Ingredient, SaveIngredientViewModel>()
                    .ReverseMap()
                    .ForMember(x => x.Dishes, opt => opt.Ignore())
                    .ForMember(x => x.Created, opt => opt.Ignore())
                    .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region "Dish Profile"
            CreateMap<Dish, DishViewModel>()
                .ReverseMap()
                    .ForMember(x => x.Orders, opt => opt.Ignore())
                    .ForMember(x => x.DishIngredients, opt => opt.Ignore())
                    .ForMember(x => x.Created, opt => opt.Ignore())
                    .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Dish, SaveDishViewModel>()
                .ForMember(x => x.Category, opt => opt.MapFrom(src => (int)Enum.Parse(typeof(DishCategory), src.Category)))
                .ForMember(x => x.IngredientsIds, opt => opt.Ignore())  
                .ReverseMap()
                    .ForMember(x => x.Category, opt => opt.MapFrom(src => src.Category.ToString()))
                    //.ForMember(x => x.Ingredients, opt => opt.Ignore())
                    .ForMember(x => x.DishIngredients, opt => opt.Ignore())
                    .ForMember(x => x.Orders, opt => opt.Ignore())
                    .ForMember(x => x.Created, opt => opt.Ignore())
                    .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region "Order Profile"
            CreateMap<Order, OrderViewModel>()
                .ReverseMap()
                    .ForMember(x => x.Created, opt => opt.Ignore())
                    .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Order, SaveOrderViewModel>()
                .ForMember(x => x.Status, opt => opt.MapFrom(src => (int)Enum.Parse(typeof(OrderStatus), src.Status)))
                .ReverseMap()
                    .ForMember(x => x.Status, opt => opt.MapFrom(src =>  src.Status.ToString()))
                    .ForMember(x => x.Created, opt => opt.Ignore())
                    .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region "Table Profile"
            CreateMap<Table, TableViewModel>()
                .ReverseMap()
                    .ForMember(x => x.Created, opt => opt.Ignore())
                    .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Table, SaveTableViewModel>()
                .ForMember(x => x.Status, opt => opt.MapFrom(src => (int)Enum.Parse(typeof(TableStatus), src.Status)))
                .ReverseMap()
                    .ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                    .ForMember(x => x.Orders, opt => opt.Ignore())
                    .ForMember(x => x.Created, opt => opt.Ignore())
                    .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Table, UpdateTableViewModel>()
                .ReverseMap()
                    .ForMember(x => x.Status, opt => opt.Ignore())
                    .ForMember(x => x.Orders, opt => opt.Ignore())
                    .ForMember(x => x.Created, opt => opt.Ignore())
                    .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region "DishOrder Profile"
            CreateMap<DishOrder, DishOrderViewModel>()
                .ReverseMap()
                    .ForMember(x => x.Dish, opt => opt.Ignore())
                    .ForMember(x => x.Order, opt => opt.Ignore())
                    .ForMember(x => x.Created, opt => opt.Ignore())
                    .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion
        }

    }
}
