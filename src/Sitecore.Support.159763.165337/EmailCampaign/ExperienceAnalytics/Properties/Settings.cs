using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Sitecore.Support.EmailCampaign.ExperienceAnalytics.Properties
{
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0"), CompilerGenerated]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

    public static Settings Default
    {
      get
      {
        return Settings.defaultInstance;
      }
    }

    [ApplicationScopedSetting, DefaultSettingValue("Parameter '{0}' of VisitAggregationState is null or empty and the '{1}' dimension will not be processed!"), SettingsDescription("A message pattern for logging a debug message that a dimension will not be processed because parameters are null or empty: {0} - parameter name; {1} - dimension name."), DebuggerNonUserCode]
    public string VisitAggregationStateParameterIsNullOrEmptyMessagePattern
    {
      get
      {
        return (string)this["VisitAggregationStateParameterIsNullOrEmptyMessagePattern"];
      }
    }

    [ApplicationScopedSetting, DefaultSettingValue("1ad8ebeb-24e3-46f1-9edd-2295c5219c5e"), DebuggerNonUserCode]
    public Guid OpenEventItemId
    {
      get
      {
        return (Guid)this["OpenEventItemId"];
      }
    }

    [ApplicationScopedSetting, DefaultSettingValue("e97e9557-0b84-4103-b545-988bf7336c7c"), DebuggerNonUserCode]
    public Guid FirstOpenEventItemId
    {
      get
      {
        return (Guid)this["FirstOpenEventItemId"];
      }
    }

    [ApplicationScopedSetting, DefaultSettingValue("87431b9b-fa39-4780-beb3-1047b9e61876"), DebuggerNonUserCode]
    public Guid ClickEventItemId
    {
      get
      {
        return (Guid)this["ClickEventItemId"];
      }
    }

    [ApplicationScopedSetting, DefaultSettingValue("bfc9eb31-1d02-486b-a3a0-5b36a138ccf7"), DebuggerNonUserCode]
    public Guid FirstClickEventItemId
    {
      get
      {
        return (Guid)this["FirstClickEventItemId"];
      }
    }

    [ApplicationScopedSetting, DefaultSettingValue("450adcbf-9429-48d1-b87f-b45691833d1f"), DebuggerNonUserCode]
    public Guid UnsubscribeEventItemId
    {
      get
      {
        return (Guid)this["UnsubscribeEventItemId"];
      }
    }

    [ApplicationScopedSetting, DefaultSettingValue("f7e054f5-6f73-4c09-82b0-9f36141be42f"), DebuggerNonUserCode]
    public Guid BounceEventItemId
    {
      get
      {
        return (Guid)this["BounceEventItemId"];
      }
    }

    [ApplicationScopedSetting, DefaultSettingValue("2a65acc5-9851-40dd-851b-23f7a6c53092"), DebuggerNonUserCode]
    public Guid SentEventItemId
    {
      get
      {
        return (Guid)this["SentEventItemId"];
      }
    }

    [ApplicationScopedSetting, DefaultSettingValue("d5ab8d8d-efc1-4eec-b7f1-80cdd05febd3"), DebuggerNonUserCode]
    public Guid SpamEventItemId
    {
      get
      {
        return (Guid)this["SpamEventItemId"];
      }
    }

    [ApplicationScopedSetting, DefaultSettingValue("0ec316ba-73e7-4c72-9c7d-43a711c11bc9"), DebuggerNonUserCode]
    public Guid ByAbTestVariantSegmentId
    {
      get
      {
        return (Guid)this["ByAbTestVariantSegmentId"];
      }
    }

    [ApplicationScopedSetting, DefaultSettingValue("ExmDimensionBase: An exception occurred when getting the Group Resolver value!"), DebuggerNonUserCode]
    public string GetGroupResolverValueExceptionMessage
    {
      get
      {
        return (string)this["GetGroupResolverValueExceptionMessage"];
      }
    }

    [ApplicationScopedSetting, DefaultSettingValue("~/icon/office/16x16/mail_open2.png"), DebuggerNonUserCode]
    public string OpenEventImagePath
    {
      get
      {
        return (string)this["OpenEventImagePath"];
      }
    }

    [ApplicationScopedSetting, DefaultSettingValue("~/icon/office/16x16/mouse_pointer.png"), DebuggerNonUserCode]
    public string ClickEventImagePath
    {
      get
      {
        return (string)this["ClickEventImagePath"];
      }
    }

    [ApplicationScopedSetting, DefaultSettingValue("~/icon/office/16x16/mail_exchange.png"), DebuggerNonUserCode]
    public string BounceEventImagePath
    {
      get
      {
        return (string)this["BounceEventImagePath"];
      }
    }

    [ApplicationScopedSetting, DefaultSettingValue("~/icon/office/16x16/mail_bug.png"), DebuggerNonUserCode]
    public string SpamEventImagePath
    {
      get
      {
        return (string)this["SpamEventImagePath"];
      }
    }

    [ApplicationScopedSetting, DefaultSettingValue("unspecified"), DebuggerNonUserCode]
    public string UnspecifiedValue
    {
      get
      {
        return (string)this["UnspecifiedValue"];
      }
    }

    [ApplicationScopedSetting, DefaultSettingValue("The '{0}' facet is not available and the '{1}' dimension will not be processed!"), SettingsDescription("A message pattern for logging a debug message that a dimension will not be processed because the facet is not available: {0} - facet name; {1} - dimension name."), DebuggerNonUserCode]
    public string FacetIsNotAvailableMessagePattern
    {
      get
      {
        return (string)this["FacetIsNotAvailableMessagePattern"];
      }
    }

    [ApplicationScopedSetting, DefaultSettingValue("c1745f34-f2b9-4ac3-a6de-faee8ce62ae1"), DebuggerNonUserCode]
    public Guid ByLandingPageSegmentId
    {
      get
      {
        return (Guid)this["ByLandingPageSegmentId"];
      }
    }

    [ApplicationScopedSetting, DefaultSettingValue("399d686d-16b6-46e3-89e9-44fb9535c2b2"), DebuggerNonUserCode]
    public Guid ByTimeOfDaySegmentId
    {
      get
      {
        return (Guid)this["ByTimeOfDaySegmentId"];
      }
    }
  }
}
