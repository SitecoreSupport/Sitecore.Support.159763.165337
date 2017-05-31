using Sitecore.Analytics.Aggregation.Data.Model;
using Sitecore.Analytics.Model;
using Sitecore.EmailCampaign.Analytics.Model;
using Sitecore.EmailCampaign.ExperienceAnalytics;

namespace Sitecore.Support.EmailCampaign.ExperienceAnalytics.Dimensions
{
  public class VisitAggregationState
  {
    internal IVisitAggregationContext VisitContext
    {
      get;
      set;
    }

    internal ExmCustomValues CustomValues
    {
      get;
      set;
    }

    internal ExmPageEventType PageEvent
    {
      get;
      set;
    }

    internal PageData LandingPage
    {
      get;
      set;
    }

    internal bool IsProductive
    {
      get;
      set;
    }

    internal bool IsBrowsed
    {
      get;
      set;
    }
  }
}
