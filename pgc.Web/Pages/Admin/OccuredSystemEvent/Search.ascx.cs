using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model;

public partial class Pages_Admin_OccuredSystemEvent_Search : BaseSearchControl<OccuredSystemEventPattern>
{
    public override OccuredSystemEventPattern Pattern
    {
        get
        {
            return new OccuredSystemEventPattern()
            {
                PersianDate=pdrOccuredDate.DateRange,
                ActionType=lkpActionType.GetSelectedValue<EventActionType>(),
                //SystemEvent_ID=lkpEvent.GetSelectedValue<long>(),
            };
        }
        set
        {
            pdrOccuredDate.DateRange = value.PersianDate;
            lkpActionType.SetSelectedValue(value.ActionType);
            
            //if (value.SystemEvent_ID > 0)
            //    lkpEvent.SetSelectedValue(value.SystemEvent_ID, new SystemEventBusiness().Retrieve(value.SystemEvent_ID).Title);
            //else
            //    lkpEvent.SetSelectedValue(-1, "");
        }
    }

    public override OccuredSystemEventPattern DefaultPattern
    {
        get
        {
            OccuredSystemEventPattern pattern = new OccuredSystemEventPattern();

            var page = (this.Page as BaseManagementPage<OccuredSystemEventBusiness, OccuredSystemEvent, OccuredSystemEventPattern, pgcEntities>);

            if (page.HasValidQueryString<long>(QueryStringKeys.occuredid))
            {
                pattern.ID = page.GetQueryStringValue<long>(QueryStringKeys.occuredid);

               // lkpEvent.SetSelectedValue(pattern.ID, new OccuredSystemEventBusiness().Retrieve(pattern.ID).SystemEvent.Title);
            }

            return pattern;
        }
    }
}