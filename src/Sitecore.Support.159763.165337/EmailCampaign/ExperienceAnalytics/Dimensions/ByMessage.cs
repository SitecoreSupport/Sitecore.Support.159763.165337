using Sitecore.Analytics.Model;
using Sitecore.EmailCampaign.ExperienceAnalytics;
using Sitecore.ExM.Framework.Diagnostics;
using System;
using Sitecore.Support.EmailCampaign.ExperienceAnalytics.Properties;

namespace Sitecore.Support.EmailCampaign.ExperienceAnalytics.Dimensions
{
  internal class ByMessage : ExmDimensionBase
  {
    public ByMessage(Guid dimensionId) : base(dimensionId, true, true)
    {
    }

    internal ByMessage(ILogger logger, Guid dimensionId) : base(logger, dimensionId, true, true)
    {
    }

    internal override string GenerateCustomKey(VisitAggregationState visitState)
    {
      if (visitState.CustomValues == null)
      {
        this.Logger.LogDebug(string.Format(Settings.Default.VisitAggregationStateParameterIsNullOrEmptyMessagePattern, "CustomValues", base.GetType().Name));
        return null;
      }
      if (visitState.CustomValues.MessageLanguage == null)
      {
        this.Logger.LogDebug(string.Format(Settings.Default.VisitAggregationStateParameterIsNullOrEmptyMessagePattern, "CustomValues.MessageLanguage", base.GetType().Name));
        return null;
      }
      return new KeyBuilder().Add(((int)visitState.PageEvent).ToString()).Add(visitState.CustomValues.MessageLanguage).Add(visitState.IsProductive).Add(visitState.IsBrowsed).ToString();
    }
  }
}
