using Sitecore.Analytics.Model;
using Sitecore.EmailCampaign.Analytics.Model;

namespace Sitecore.Support.EmailCampaign.ExperienceAnalytics.Dimensions
{
  internal class ExmExpert
  {
    public ExmCustomValuesHolder GetEmailFingerPrints(VisitData visit)
    {
      ExmCustomValuesHolder valueHolder = ExmCustomValuesHolder.GetValueHolder(visit.CustomValues);
      if (valueHolder == null)
      {
        return null;
      }
      if (valueHolder.ExmCustomValues.Count > 0)
      {
        return valueHolder;
      }
      return null;
    }
  }
}
