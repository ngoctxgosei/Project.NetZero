using Abp.Localization;
using Abp.UI.Inputs;
using AutoMapper;
using WS.Authorization.Users;
using WS.Authorization.Users.Dto;
using WS.Editions.Dto;
using WS.Employees;
using WS.Employees.Dto;
using WS.Localization.Dto;
using WS.Tasks;
using WS.Tasks.Dto;
using static WS.Tasks.TaskAppService;

namespace WS
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());

            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();

            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();

            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));


            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */
            configuration.CreateMap<TaskEditDto, Task>();
            configuration.CreateMap<Employee,EmployeeListDto>();
            configuration.CreateMap<Employee, EmployeeEditDto>();
            configuration.CreateMap<EmployeeEditDto, Employee>();
        }
    }
} 