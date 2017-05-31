using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Analytics.Aggregation.Data.Model;
using Sitecore.Analytics.Model;
using Sitecore.Diagnostics;
using Sitecore.EmailCampaign.Analytics.Model;
using Sitecore.EmailCampaign.ExperienceAnalytics;
using Sitecore.EmailCampaign.ExperienceAnalytics.Dimensions;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.ExperienceAnalytics.Aggregation;
using Sitecore.ExperienceAnalytics.Aggregation.Data.Model;
using Sitecore.ExperienceAnalytics.Aggregation.Data.Schema;
using Sitecore.ExperienceAnalytics.Aggregation.Dimensions;
using Sitecore.ExperienceAnalytics.Core;
using Sitecore.ExperienceAnalytics.Core.Grouping;
using Sitecore.Support.EmailCampaign.ExperienceAnalytics.Properties;

namespace Sitecore.Support.EmailCampaign.ExperienceAnalytics.Dimensions
{
  public abstract class ExmDimensionBase : DimensionBase
  {
    private readonly ExmExpert _exmExpert;

    private readonly bool _processClickEvents;

    private readonly bool _processOpenEvents;

    internal readonly ILogger Logger;

    protected ExmDimensionBase(Guid dimensionId, bool processClickEvents, bool processOpenEvents) : this(Sitecore.ExM.Framework.Diagnostics.Logger.Instance, dimensionId, processClickEvents, processOpenEvents)
    {
    }

    protected ExmDimensionBase(Sitecore.ExM.Framework.Diagnostics.ILogger logger, Guid dimensionId, bool processClickEvents, bool processOpenEvents) : base(dimensionId)
    {
      Assert.ArgumentNotNull(logger, "logger");
      this.Logger = logger;
      this._processClickEvents = processClickEvents;
      this._processOpenEvents = processOpenEvents;
      this._exmExpert = new ExmExpert();
    }

    public override IEnumerable<DimensionData> GetData(IVisitAggregationContext context)
    {
      Assert.ArgumentNotNull(context, "context");
      VisitAggregationState visitState = new VisitAggregationState
      {
        VisitContext = context
      };
      List<DimensionData> list = new List<DimensionData>();
      VisitData visit = context.Visit;
      if (visit == null)
      {
        return list;
      }
      if (this._processClickEvents && visit.CustomValues != null && visit.CustomValues.Any<KeyValuePair<string, object>>())
      {
        list = this.GetClickEventData(visit, visitState).ToList<DimensionData>();
      }
      if (this._processOpenEvents)
      {
        DimensionData openEventData = this.GetOpenEventData(visit, visitState);
        if (openEventData != null)
        {
          list.Add(openEventData);
        }
      }
      return list;
    }

    protected internal virtual DimensionData GetOpenEventData(VisitData visit, VisitAggregationState visitState)
    {
      Assert.ArgumentNotNull(visit, "visit");
      Assert.ArgumentNotNull(visitState, "visitState");
      if (visit.Pages == null || !visit.Pages.Any<PageData>())
      {
        return null;
      }
      visitState.CustomValues = DimensionUtils.GetPageCustomValues(visit);
      if (this.GenerateBaseKey(visitState) == null)
      {
        return null;
      }
      DimensionData dimensionData = new DimensionData
      {
        MetricsValue = new SegmentMetricsValue
        {
          Visits = 1,
          Pageviews = 1
        }
      };
      visitState.LandingPage = visit.Pages.First<PageData>();
      foreach (PageEventData current in visitState.LandingPage.PageEvents)
      {
        ExmPageEventType exmPageEventType = DimensionUtils.ParsePageEvent(current.PageEventDefinitionId);
        if (exmPageEventType == ExmPageEventType.Bounce || exmPageEventType == ExmPageEventType.Sent || exmPageEventType == ExmPageEventType.Spam)
        {
          visitState.PageEvent = exmPageEventType;
          break;
        }
        if (exmPageEventType == ExmPageEventType.Open)
        {
          visitState.PageEvent = exmPageEventType;
        }
        else if (exmPageEventType == ExmPageEventType.FirstOpen)
        {
          visitState.PageEvent = ExmPageEventType.Open;
          dimensionData.MetricsValue.Count = 1;
          break;
        }
      }
      if (visitState.PageEvent == ExmPageEventType.Unspecified)
      {
        return null;
      }
      string text = this.GenerateCustomKey(visitState);
      if (text == null)
      {
        return null;
      }
      dimensionData.DimensionKey = string.Format("{0}_{1}", this.GenerateBaseKey(visitState), text);
      return dimensionData;
    }

    protected internal virtual IEnumerable<DimensionData> GetClickEventData(VisitData visit, VisitAggregationState visitState)
    {
      Assert.ArgumentNotNull(visit, "visit");
      Assert.ArgumentNotNull(visitState, "visitState");
      ExmCustomValuesHolder emailFingerPrints = this._exmExpert.GetEmailFingerPrints(visit);
      Dictionary<ExmPageEventType, string> dictionary = new Dictionary<ExmPageEventType, string>();
      if (emailFingerPrints != null && emailFingerPrints.ExmCustomValues != null)
      {
        int[] array = emailFingerPrints.ExmCustomValues.Keys.ToArray<int>();
        for (int i = 0; i < array.Length; i++)
        {
          int num = array[i] - 1;
          int lastPageIndex = (i == array.Length - 1) ? (visit.Pages.Count - 1) : (array[i + 1] - 2);
          visitState.CustomValues = emailFingerPrints.ExmCustomValues[array[i]];
          string text = this.GenerateBaseKey(visitState);
          if (text != null)
          {
            // Changes for bug #165337
            if (num > visit.Pages.Count - 1 || num < 0)
            {
              Log.Debug
              (
                string.Format
                (
                  "Sitecore.Support.159763.165337: Corrupted interaction detected.\r\n" +
                  "ExmCusomValues page index:{0}\r\n" +
                  "Number of pages in the interaction:{1}\r\n" +
                  "Interaction Id: {2}",
                  num,
                  visit.Pages.Count,
                  visit.InteractionId.ToString()
                ),
                this
              );
            }
            else
            {
              visitState.LandingPage = visit.Pages[num];
              DimensionData dimensionData = this.GenerateDimension(visit, visitState, num, lastPageIndex, text);
              if (dimensionData != null &&
                !dictionary.Any((KeyValuePair<ExmPageEventType, string> d) =>
                    d.Key == ExmPageEventType.Unsubscribe &&
                    d.Value == dimensionData.DimensionKey))
              {
                dictionary[visitState.PageEvent] = dimensionData.DimensionKey;
                yield return dimensionData;
              }
            }
            // Changes end
          }
        }
      }
      yield break;
    }

    private DimensionData GenerateDimension(VisitData visit, VisitAggregationState visitState, int firstPageIndex, int lastPageIndex, string baseKey)
    {
      DimensionData dimensionData = null;
      DimensionData dimensionData2 = new DimensionData
      {
        MetricsValue = new SegmentMetricsValue
        {
          Visits = 1,
          Pageviews = lastPageIndex - firstPageIndex + 1
        }
      };
      for (int i = firstPageIndex; i <= lastPageIndex; i++)
      {
        foreach (PageEventData current in visit.Pages[i].PageEvents)
        {
          ExmPageEventType exmPageEventType = DimensionUtils.ParsePageEvent(current.PageEventDefinitionId);
          if (exmPageEventType != ExmPageEventType.Unspecified)
          {
            if (exmPageEventType == ExmPageEventType.Unsubscribe)
            {
              dimensionData = new DimensionData
              {
                MetricsValue = new SegmentMetricsValue
                {
                  Visits = 1,
                  Pageviews = 1
                }
              };
            }
            if (exmPageEventType == ExmPageEventType.FirstClick)
            {
              dimensionData2.MetricsValue.Count = 1;
            }
          }
          if (current.IsGoal)
          {
            dimensionData2.MetricsValue.Conversions++;
          }
          dimensionData2.MetricsValue.Value += current.Value;
        }
        dimensionData2.MetricsValue.TimeOnSite += visit.Pages[i].Duration;
      }
      DimensionData dimensionData3;
      if (dimensionData == null)
      {
        dimensionData3 = dimensionData2;
        visitState.PageEvent = ExmPageEventType.Click;
        visitState.IsProductive = (dimensionData2.MetricsValue.Value > 0);
        visitState.IsBrowsed = (dimensionData2.MetricsValue.Pageviews > 1);
      }
      else
      {
        dimensionData3 = dimensionData;
        visitState.PageEvent = ExmPageEventType.Unsubscribe;
      }
      dimensionData2.MetricsValue.Bounces = (visitState.IsBrowsed ? 0 : 1);
      string text = this.GenerateCustomKey(visitState);
      if (text == null)
      {
        return null;
      }
      dimensionData3.DimensionKey = string.Format("{0}_{1}", baseKey, text);
      return dimensionData3;
    }

    internal virtual string GetGroupResolverValue(string groupName, IVisitAggregationContext context)
    {
      Assert.ArgumentNotNull(groupName, "groupName");
      Assert.ArgumentNotNull(context, "context");
      string result;
      try
      {
        if (!this.ValidateVisitForResolver(context.Visit))
        {
          result = null;
        }
        else
        {
          IVisitGroupResolver groupResolver = AggregationContainer.GetGroupResolver(groupName);
          result = groupResolver.GetGroupIds(context).First<string>();
        }
      }
      catch (Exception arg)
      {
        this.Logger.LogDebug(string.Format("{0}, {1}", Settings.Default.GetGroupResolverValueExceptionMessage, arg));
        result = null;
      }
      return result;
    }

    internal string GenerateBaseKey(VisitAggregationState visitState)
    {
      if (visitState.CustomValues == null)
      {
        this.Logger.LogDebug(string.Format(Settings.Default.VisitAggregationStateParameterIsNullOrEmptyMessagePattern, "CustomValues", base.GetType().Name));
        return null;
      }
      if (visitState.CustomValues.ManagerRootId == default(Guid))
      {
        this.Logger.LogDebug(string.Format(Settings.Default.VisitAggregationStateParameterIsNullOrEmptyMessagePattern, "CustomValues.ManagerRootId", base.GetType().Name));
        return null;
      }
      if (visitState.CustomValues.MessageId == default(Guid))
      {
        this.Logger.LogDebug(string.Format(Settings.Default.VisitAggregationStateParameterIsNullOrEmptyMessagePattern, "CustomValues.MessageId", base.GetType().Name));
        return null;
      }
      return new HierarchicalKeyBuilder().Add(visitState.CustomValues.ManagerRootId).Add(visitState.CustomValues.MessageId).ToString();
    }

    internal abstract string GenerateCustomKey(VisitAggregationState visitState);

    internal virtual bool ValidateVisitForResolver(VisitData visit)
    {
      return true;
    }
  }
}
