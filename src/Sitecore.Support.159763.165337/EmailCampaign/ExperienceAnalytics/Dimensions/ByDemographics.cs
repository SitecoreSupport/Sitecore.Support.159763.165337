using System;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Analytics.Model.Framework;
using Sitecore.EmailCampaign.ExperienceAnalytics;
using Sitecore.EmailCampaign.ExperienceAnalytics.Dimensions;
using Sitecore.EmailCampaign.ExperienceAnalytics.Properties;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.Support.EmailCampaign.ExperienceAnalytics.Properties;

namespace Sitecore.Support.EmailCampaign.ExperienceAnalytics.Dimensions
{
  internal class ByDemographics : ExmDimensionBase
  {
    public ByDemographics(Guid dimensionId) : base(dimensionId, true, true)
    {
    }

    internal ByDemographics(ILogger logger, Guid dimensionId) : base(logger, dimensionId, true, true)
    {
    }

    internal override string GenerateCustomKey(VisitAggregationState visitState)
    {
      if (visitState.VisitContext == null)
      {
        this.Logger.LogDebug(string.Format(Settings.Default.VisitAggregationStateParameterIsNullOrEmptyMessagePattern, "VisitContext", base.GetType().Name));
        return null;
      }
      string result;
      try
      {
        IContactPersonalInfo facet = visitState.VisitContext.Contact.GetFacet<IContactPersonalInfo>("Personal");
        string text = facet.Gender ?? Settings.Default.UnspecifiedValue;
        int num = facet.BirthDate.HasValue ? DimensionUtils.CalculateAge(facet.BirthDate.Value, DateTime.UtcNow) : 0;
        if (text == Settings.Default.UnspecifiedValue && num == 0)
        {
          result = null;
        }
        else
        {
          result = new KeyBuilder().Add((int)visitState.PageEvent).Add(text).Add(num).ToString();
        }
      }
      catch (FacetNotAvailableException)
      {
        this.Logger.LogDebug(string.Format(Settings.Default.FacetIsNotAvailableMessagePattern, "Personal", base.GetType().Name));
        result = null;
      }
      return result;
    }
  }
}
