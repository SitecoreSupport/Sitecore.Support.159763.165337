using System;
using Sitecore.EmailCampaign.ExperienceAnalytics;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.Support.EmailCampaign.ExperienceAnalytics.Properties;

namespace Sitecore.Support.EmailCampaign.ExperienceAnalytics.Dimensions
{
  internal class ByLandingPage : ExmDimensionBase
  {
    public ByLandingPage(Guid dimensionId) : base(dimensionId, true, false)
    {
    }

    internal ByLandingPage(ILogger logger, Guid dimensionId) : base(logger, dimensionId, true, true)
    {
    }

    internal override string GenerateCustomKey(VisitAggregationState visitState)
    {
      if (visitState.LandingPage == null)
      {
        this.Logger.LogDebug(string.Format(Settings.Default.VisitAggregationStateParameterIsNullOrEmptyMessagePattern, "LandingPage", base.GetType().Name));
        return null;
      }
      if (visitState.LandingPage.Url.Path != null && visitState.LandingPage.Item.Id != Guid.Empty && visitState.PageEvent != ExmPageEventType.Unsubscribe)
      {
        return new KeyBuilder().Add(visitState.LandingPage.Url.Path).Add(visitState.LandingPage.Item.Id).ToString();
      }
      return null;
    }
  }
}
