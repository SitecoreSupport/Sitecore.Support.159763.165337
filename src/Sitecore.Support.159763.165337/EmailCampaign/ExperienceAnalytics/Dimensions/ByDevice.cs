using System;
using System.Collections.Generic;
using Sitecore.Analytics.Aggregation.Data.Model;
using Sitecore.Analytics.Model;
using Sitecore.CES.DeviceDetection;
using Sitecore.Diagnostics;
using Sitecore.EmailCampaign.ExperienceAnalytics;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.ExperienceAnalytics.Aggregation.Data.Model;

namespace Sitecore.Support.EmailCampaign.ExperienceAnalytics.Dimensions
{
  internal class ByDevice : ExmDimensionBase
  {
    public ByDevice(Guid dimensionId) : base(dimensionId, true, true)
    {
    }

    internal ByDevice(ILogger logger, Guid dimensionId) : base(logger, dimensionId, true, true)
    {
    }

    public override IEnumerable<DimensionData> GetData(IVisitAggregationContext context)
    {
      // Changes for bug 159763
      if (!DeviceDetectionManager.IsEnabled)
      {
        return new List<DimensionData>();
      }
      //End Changes

      return base.GetData(context);
    }

    internal override string GenerateCustomKey(VisitAggregationState visitState)
    {
      // Changes for bug 159763
      if (!DeviceDetectionManager.IsEnabled)
      {
        return null;
      }
      //End Changes

      string groupResolverValue = this.GetGroupResolverValue("DeviceType", visitState.VisitContext);
      string groupResolverValue2 = this.GetGroupResolverValue("DeviceModel", visitState.VisitContext);
      string groupResolverValue3 = this.GetGroupResolverValue("BrowserModel", visitState.VisitContext);
      string groupResolverValue4 = this.GetGroupResolverValue("OperationSystem", visitState.VisitContext);
      if (groupResolverValue == null || groupResolverValue2 == null || groupResolverValue3 == null || groupResolverValue4 == null)
      {
        return null;
      }
      return new KeyBuilder().Add(((int)visitState.PageEvent).ToString()).Add(groupResolverValue).Add(groupResolverValue2).Add(groupResolverValue3).Add(groupResolverValue4).ToString();
    }
    internal override bool ValidateVisitForResolver(VisitData visit)
    {
      return visit.UserAgent != null;
    }
  }
}

