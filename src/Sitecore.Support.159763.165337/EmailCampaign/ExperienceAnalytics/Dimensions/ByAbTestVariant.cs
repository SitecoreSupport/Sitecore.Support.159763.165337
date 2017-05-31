using System;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.Support.EmailCampaign.ExperienceAnalytics.Properties;
using Sitecore.EmailCampaign.ExperienceAnalytics;

namespace Sitecore.Support.EmailCampaign.ExperienceAnalytics.Dimensions
{
  internal class ByAbTestVariant : ExmDimensionBase
  {
    public ByAbTestVariant(Guid dimensionId) : base(dimensionId, true, true)
    {
    }

    internal ByAbTestVariant(ILogger logger, Guid dimensionId) : base(logger, dimensionId, true, true)
    {
    }

    internal override string GenerateCustomKey(VisitAggregationState visitState)
    {
      if (visitState.CustomValues == null)
      {
        this.Logger.LogDebug(string.Format(Settings.Default.VisitAggregationStateParameterIsNullOrEmptyMessagePattern, "CustomValues", base.GetType().Name));
        return null;
      }
      if (!visitState.CustomValues.TestValueIndex.HasValue)
      {
        this.Logger.LogDebug(string.Format(Settings.Default.VisitAggregationStateParameterIsNullOrEmptyMessagePattern, "CustomValues.TestValueIndex", base.GetType().Name));
        return null;
      }
      return new KeyBuilder().Add(((int)visitState.PageEvent).ToString()).Add(visitState.CustomValues.TestValueIndex.Value.ToString()).ToString();
    }
  }
}
