﻿using System;
using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.ViewControllers;
using CustomSabersLite.Configuration;
using CustomSabersLite.Data;
using UnityEngine;
using TMPro;
using Zenject;
using System.ComponentModel;
using CustomSabersLite.Utilities.UI;
using CustomSabersLite.UI.Managers;

namespace CustomSabersLite.UI.Views
{
    [HotReload(RelativePathToLayout = "../BSML/saberSettings.bsml")]
    [ViewDefinition("CustomSabersLite.UI.BSML.saberSettings.bsml")]
    internal class SaberSettingsViewController : BSMLAutomaticViewController, INotifyPropertyChanged
    {
        private CSLConfig config;
        private SaberPreviewManager previewManager;

        [Inject]
        public void Construct(CSLConfig config, SaberPreviewManager previewManager)
        {
            this.config = config;
            this.previewManager = previewManager;
        }

        private bool parsed;
        
        [UIComponent("trail-duration")]
        private GenericInteractableSetting trailDurationInteractable;

        [UIComponent("trail-duration")]
        private TextMeshProUGUI trailDurationText;

        [UIComponent("trail-width")]
        private GenericInteractableSetting trailWidthInteractable;

        [UIComponent("trail-width")]
        private TextMeshProUGUI trailWidthText;

        [UIComponent("forcefully-foolish")]
        private Transform foolishSetting;

        [UIValue("enabled")]
        public bool Enabled
        {
            get => config.Enabled; 
            set => config.Enabled = value;
        }

        [UIValue("disable-white-trail")]
        public bool DisableWhiteTrail
        {
            get => config.DisableWhiteTrail;
            set => config.DisableWhiteTrail = value;
        }

        [UIValue("override-trail-duration")]
        public bool OverrideTrailDuration
        {
            get => config.OverrideTrailDuration;
            set
            {
                config.OverrideTrailDuration = value;
                if (parsed) BSMLHelpers.SetSliderInteractable(trailDurationInteractable, value, trailDurationText);
            }
        }

        [UIValue("override-trail-width")]
        public bool OverrideTrailWidth
        {
            get => config.OverrideTrailWidth;
            set
            {
                config.OverrideTrailWidth = value;
                if (parsed) BSMLHelpers.SetSliderInteractable(trailWidthInteractable, value, trailWidthText);
            }
        }

        [UIValue("trail-duration")]
        public int TrailDuration
        {
            get => config.TrailDuration;
            set => config.TrailDuration = value;
        }

        [UIValue("trail-width")]
        public int TrailWidth
        {
            get => config.TrailWidth;
            set => config.TrailWidth = value;
        }

        [UIValue("trail-type")]
        public string TrailType
        {
            get => config.TrailType.ToString();
            set => config.TrailType = Enum.TryParse(value, out TrailType trailType) ? trailType : config.TrailType;
        }

        [UIValue("trail-type-list")]
        public List<object> trailTypeList = Enum.GetNames(typeof(TrailType)).ToList<object>();

        [UIValue("enable-custom-events")]
        public bool EnableCustomEvents
        {
            get => config.EnableCustomEvents;
            set => config.EnableCustomEvents = value;
        }

        [UIValue("enable-custom-color-scheme")]
        public bool EnableCustomColorScheme
        {
            get => config.EnableCustomColorScheme;
            set
            {
                config.EnableCustomColorScheme = value;
                previewManager.SetColor();
            }
        }

        [UIValue("left-saber-color")]
        public Color LeftSaberColor
        {
            get => config.LeftSaberColor;
            set
            {
                config.LeftSaberColor = value;
                previewManager.SetColor();
            }
        }

        [UIValue("right-saber-color")]
        public Color RightSaberColor
        {
            get => config.RightSaberColor;
            set
            {
                config.RightSaberColor = value;
                previewManager.SetColor();
            }
        }

        [UIValue("forcefully-foolish")]
        public bool ForcefullyFoolish
        {
            get => config.ForcefullyFoolish;
            set => config.ForcefullyFoolish = value;
        }

        [UIAction("#post-parse")]
        private void PostParse()
        {
            parsed = true;
            BSMLHelpers.SetSliderInteractable(trailDurationInteractable, OverrideTrailDuration, trailDurationText);
            BSMLHelpers.SetSliderInteractable(trailWidthInteractable, OverrideTrailWidth, trailWidthText);
        }

        public void Activated()
        {
            foreach (string name in SharedSaberSettings.PropertyNames)
            {
                NotifyPropertyChanged(name);
            }

            foolishSetting.gameObject.SetActive(config.Fooled);
        }
    }
}
