using System;
using Sitecore.EmailCampaign.ExperienceAnalytics;
using Sitecore.EmailCampaign.ExperienceAnalytics.Dimensions;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.Support.EmailCampaign.ExperienceAnalytics.Properties;

namespace Sitecore.Support.EmailCampaign.ExperienceAnalytics.Dimensions
{
  internal class ByTimeOfDay : ExmDimensionBase
  {
    public ByTimeOfDay(Guid dimensionId) : base(dimensionId, true, true)
    {
    }

    internal ByTimeOfDay(ILogger logger, Guid dimensionId) : base(logger, dimensionId, true, true)
    {
    }

    internal override string GenerateCustomKey(VisitAggregationState visitState)
    {
      if (visitState.VisitContext == null)
      {
        this.Logger.LogDebug(string.Format(Settings.Default.VisitAggregationStateParameterIsNullOrEmptyMessagePattern, "VisitContext", base.GetType().Name));
        return null;
      }
      if (visitState.VisitContext.Visit == null)
      {
        this.Logger.LogDebug(string.Format(Settings.Default.VisitAggregationStateParameterIsNullOrEmptyMessagePattern, "VisitContext.Visit", base.GetType().Name));
        return null;
      }
      int dayOfWeek = (int)visitState.VisitContext.Visit.StartDateTime.DayOfWeek;
      int hour = visitState.VisitContext.Visit.StartDateTime.Hour;
      return new KeyBuilder().Add((int)visitState.PageEvent).Add(dayOfWeek).Add(hour).ToString();
    }
  }
}
