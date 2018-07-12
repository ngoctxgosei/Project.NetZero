using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WS.Editions;

namespace WS.Sessions.Dto
{
    [AutoMapFrom(typeof(SubscribableEdition))]
    public class EditionInfoDto : EntityDto
    {
        public string DisplayName { get; set; }

        public int? TrialDayCount { get; set; }

        public decimal? MonthlyPrice { get; set; }

        public decimal? AnnualPrice { get; set; }

        public bool IsHighestEdition { get; set; }

        public bool IsFree { get; set; }


        public void SetEditionIsHighest(SubscribableEdition topEdition)
        {
            if (topEdition == null)
            {
                return;
            }

            IsHighestEdition = Id == topEdition.Id;
        }
    }
}