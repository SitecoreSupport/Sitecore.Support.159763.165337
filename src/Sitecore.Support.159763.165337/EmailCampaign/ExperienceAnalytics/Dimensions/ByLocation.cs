using Sitecore.Analytics.Model;
using Sitecore.EmailCampaign.ExperienceAnalytics;
using Sitecore.ExM.Framework.Diagnostics;
using System;

namespace Sitecore.Support.EmailCampaign.ExperienceAnalytics.Dimensions
{
  internal class ByLocation : ExmDimensionBase
  {
    public ByLocation(Guid dimensionId) : base(dimensionId, true, true)
    {
    }

    internal ByLocation(ILogger logger, Guid dimensionId) : base(logger, dimensionId, true, true)
    {
    }

    internal override string GenerateCustomKey(VisitAggregationState visitState)
    {
      string groupResolverValue = this.GetGroupResolverValue("Country", visitState.VisitContext);
      string groupResolverValue2 = this.GetGroupResolverValue("Region", visitState.VisitContext);
      string groupResolverValue3 = this.GetGroupResolverValue("City", visitState.VisitContext);
      if (groupResolverValue == null || groupResolverValue2 == null || groupResolverValue3 == null)
      {
        return null;
      }
      return new KeyBuilder().Add(((int)visitState.PageEvent).ToString()).Add(groupResolverValue).Add(groupResolverValue2).Add(groupResolverValue3).ToString();
    }

    internal override bool ValidateVisitForResolver(VisitData visit)
    {
      return visit.GeoData != null;
    }
  }
}
