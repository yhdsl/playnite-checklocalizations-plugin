using CheckLocalizations.Models;
using CheckLocalizations.Services;
using Playnite.SDK;
using Playnite.SDK.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckLocalizations
{
    public class CheckLocalizationsSettings : ObservableObject
    {
        #region Settings variables
        public bool MenuInExtensions { get; set; } = true;
        public DateTime LastAutoLibUpdateAssetsDownload { get; set; } = DateTime.Now;

        public bool AutoImport { get; set; } = true;

        public bool EnableTag { get; set; } = false;
        public bool EnableTagSingle { get; set; } = false;
        public bool EnableTagAudio { get; set; } = false;
        public List<GameLanguage> GameLanguages { get; set; } = new List<GameLanguage>();


        public bool UiStyleSteam { get; set; } = false;
        public bool UiStylePcGamingWiki { get; set; } = true;


        private bool _EnableIntegrationViewItem = true;
        public bool EnableIntegrationViewItem { get => _EnableIntegrationViewItem; set => SetValue(ref _EnableIntegrationViewItem, value); }

        private bool _EnableIntegrationButton = true;
        public bool EnableIntegrationButton { get => _EnableIntegrationButton; set => SetValue(ref _EnableIntegrationButton, value); }

        private bool _EnableIntegrationButtonDetails = true;
        public bool EnableIntegrationButtonDetails { get => _EnableIntegrationButtonDetails; set => SetValue(ref _EnableIntegrationButtonDetails, value); }

        public bool EnableIntegrationButtonContextMenu { get; set; } = false;


        private bool _EnableIntegrationListLanguages = true;
        public bool EnableIntegrationListLanguages { get => _EnableIntegrationListLanguages; set => SetValue(ref _EnableIntegrationListLanguages, value); }


        private bool _EnableIntegrationFlags = true;
        public bool EnableIntegrationFlags { get => _EnableIntegrationFlags; set => SetValue(ref _EnableIntegrationFlags, value); }

        public bool OnlyDisplaySelectedFlags { get; set; } = false;
        public bool OnlyDisplayExistingFlags { get; set; } = false;


        public double ListLanguagesHeight { get; set; } = 120;
        public bool ListLanguagesWithColNote { get; set; } = false;
        public bool ListLanguagesVisibleEmpty { get; set; } = false;
        #endregion

        // Playnite serializes settings object to a JSON object and saves it as text file.
        // If you want to exclude some property from being saved then use `JsonDontSerialize` ignore attribute.
        #region Variables exposed
        private bool _HasData = false;
        [DontSerialize]
        public bool HasData { get => _HasData; set => SetValue(ref _HasData, value); }

        private bool _HasNativeSupport = false;
        [DontSerialize]
        public bool HasNativeSupport { get => _HasNativeSupport; set => SetValue(ref _HasNativeSupport, value); }

        private List<Models.Localization> _ListNativeSupport = new List<Models.Localization>();
        [DontSerialize]
        public List<Models.Localization> ListNativeSupport { get => _ListNativeSupport; set => SetValue(ref _ListNativeSupport, value); }
        #endregion  
    }


    public class CheckLocalizationsSettingsViewModel : ObservableObject, ISettings
    {
        private readonly CheckLocalizations Plugin;
        private CheckLocalizationsSettings EditingClone { get; set; }

        private CheckLocalizationsSettings _Settings;
        public CheckLocalizationsSettings Settings { get => _Settings; set => SetValue(ref _Settings, value); }


        public CheckLocalizationsSettingsViewModel(CheckLocalizations plugin)
        {
            // Injecting your plugin instance is required for Save/Load method because Playnite saves data to a location based on what plugin requested the operation.
            Plugin = plugin;

            // Load saved settings.
            CheckLocalizationsSettings savedSettings = plugin.LoadPluginSettings<CheckLocalizationsSettings>();

            // LoadPluginSettings returns null if not saved data is available.
            if (savedSettings != null)
            {
                Settings = savedSettings;
            }
            else
            {
                Settings = new CheckLocalizationsSettings();                
                Settings.GameLanguages = new List<GameLanguage>()
                {
                    new GameLanguage { DisplayName = "英语", Name = "English", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "法语", Name = "French", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "德语", Name = "German", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "意大利语", Name = "Italian", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "日语", Name = "Japanese", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "西班牙语 - 西班牙", Name = "Spanish", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "简体中文", Name = "Simplified Chinese", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "俄语", Name = "Russian", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "繁体中文", Name = "Traditional Chinese", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "韩语", Name = "Korean", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "波兰语", Name = "Polish", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "葡萄牙语 - 巴西", Name = "Brazilian Portuguese", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "阿拉伯语", Name = "Arabic", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "捷克语", Name = "Czech", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "匈牙利语", Name = "Hungarian", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "土耳其语", Name = "Turkish", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "阿拉伯语", Name = "Arabic", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "加泰罗尼亚语", Name = "Catalan", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "丹麦语", Name = "Danish", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "希腊语", Name = "Greek", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "爱沙尼亚语", Name = "Estonian", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "波斯语", Name = "Persian", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "芬兰语", Name = "Finnish", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "克罗地亚语", Name = "Croatian", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "印度尼西亚语", Name = "Indonesian", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "立陶宛语", Name = "Lithuanian", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "荷兰语", Name = "Dutch", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "挪威语", Name = "Norwegian", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "葡萄牙语", Name = "Portuguese", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "罗马尼亚语", Name = "Romanian", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "斯洛文尼亚语 ", Name = "Slovenian", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "塞尔维亚语", Name = "Serbian", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "瑞典语", Name = "Swedish", IsTag = false, IsNative = false },
                    new GameLanguage { DisplayName = "乌克兰语", Name = "Ukrainian", IsTag = false, IsNative = false }
                };
            }
        }

        // Code executed when settings view is opened and user starts editing values.
        public void BeginEdit()
        {
            EditingClone = Serialization.GetClone(Settings);
        }

        // Code executed when user decides to cancel any changes made since BeginEdit was called.
        // This method should revert any changes made to Option1 and Option2.
        public void CancelEdit()
        {
            Settings = EditingClone;
        }

        // Code executed when user decides to confirm changes made since BeginEdit was called.
        // This method should save settings made to Option1 and Option2.
        public void EndEdit()
        {
            Settings.EnableTag = Settings.EnableTagAudio || Settings.EnableTagSingle;

            Plugin.SavePluginSettings(Settings);
            CheckLocalizations.PluginDatabase.PluginSettings = this;
            this.OnPropertyChanged();
        }

        // Code execute when user decides to confirm changes made since BeginEdit was called.
        // Executed before EndEdit is called and EndEdit is not called if false is returned.
        // List of errors is presented to user if verification fails.
        public bool VerifySettings(out List<string> errors)
        {
            errors = new List<string>();
            return true;
        }
    }
}
