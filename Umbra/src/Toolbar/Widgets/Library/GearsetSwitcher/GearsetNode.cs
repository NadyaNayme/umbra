﻿/* Umbra | (c) 2024 by Una              ____ ___        ___.
 * Licensed under the terms of AGPL-3  |    |   \ _____ \_ |__ _______ _____
 *                                     |    |   //     \ | __ \\_  __ \\__  \
 * https://github.com/una-xiv/umbra    |    |  /|  Y Y  \| \_\ \|  | \/ / __ \_
 *                                     |______//__|_|  /____  /|__|   (____  /
 *     Umbra is free software: you can redistribute  \/     \/             \/
 *     it and/or modify it under the terms of the GNU Affero General Public
 *     License as published by the Free Software Foundation, either version 3
 *     of the License, or (at your option) any later version.
 *
 *     Umbra UI is distributed in the hope that it will be useful,
 *     but WITHOUT ANY WARRANTY; without even the implied warranty of
 *     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *     GNU Affero General Public License for more details.
 */

using System;
using Dalamud.Interface;
using Umbra.Common;
using Umbra.Game;
using Una.Drawing;

namespace Umbra.Widgets;

internal partial class GearsetNode : Node
{
    public const int NodeWidth  = 200;
    public const int NodeHeight = 40;

    public int    ButtonIconYOffset { get; set; }
    public string ButtonIconType    { get; set; } = "Default";
    public string BackgroundType    { get; set; } = "GradientV";
    public bool   ShowExperienceBar { get; set; } = true;
    public bool   ShowExperiencePct { get; set; } = true;
    public bool   ShowItemLevel     { get; set; } = true;
    public bool   ShowWarningIcon   { get; set; } = true;

    public readonly Gearset Gearset;

    private readonly IGearsetRepository _repository;
    private readonly IPlayer            _player;

    public GearsetNode(IGearsetRepository repository, IPlayer player, Gearset gearset)
    {
        _repository = repository;
        _player     = player;

        Gearset    = gearset;
        Stylesheet = GearsetSwitcherItemStylesheet;
        ClassList  = ["gearset"];

        OnMouseUp += _ => {
            switch (gearset.JobName) {
                case "Paladin":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 20 individual ");
                    break;
                case "Warrior":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 21 individual ");
                    break;
                case "Dark Knight":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 22 individual ");
                    break;
                case "Gunbreaker":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 23 individual ");
                    break;
                case "Monk":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 24 individual ");
                    break;
                case "Dragoon":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 25 individual ");
                    break;
                case "Ninja":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 26 individual ");
                    break;
                case "Samurai":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 27 individual ");
                    break;
                case "Reaper":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 28 individual ");
                    break;
                case "Viper":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 29 individual ");
                    break;
                case "White Mage":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 30 individual ");
                    break;
                case "Scholar":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 31 individual ");
                    break;
                case "Astrologian":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 32 individual ");
                    break;
                case "Sage":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 33 individual ");
                    break;
                case "Bard":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 34 individual ");
                    break;
                case "Machinist":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 35 individual ");
                    break;
                case "Dancer":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 36 individual ");
                    break;
                case "Black Mage":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 40 individual ");
                    break;
                case "Summoner":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 41 individual ");
                    break;
                case "Red Mage":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 42 individual ");
                    break;
                case "Pictomancer":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 43 individual ");
                    break;
                case "Blue Mage":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 44 individual ");
                    break;
                case "Carpenter":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 50 individual ");
                    break;
                case "Blacksmith":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 51 individual ");
                    break;
                case "Armorer":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 52 individual ");
                    break;
                case "Goldsmith":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 53 individual ");
                    break;
                case "Leatherworker":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 60 individual ");
                    break;
                case "Weaver":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 61 individual ");
                    break;
                case "Alchemist":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 62 individual ");
                    break;
                case "Culinarian":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 63 individual ");
                    break;
                case "Miner":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 70 individual ");
                    break;
                case "Botanist":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 71 individual ");
                    break;
                case "Fisher":
                    Framework.Service<IChatSender>().Send("/macrocancel");
                    Framework.Service<IChatSender>().Send("/runmacro 72 individual ");
                    break;
            }
            if (_repository.CurrentGearset is not null) {
                var equippedSet = _repository.CurrentGearset.Id;
                _repository.EquipGearset(equippedSet);
            }
        };

        ChildNodes = [
            new() {
                Id        = "Icon",
                ClassList = ["gearset--icon"],
                Style     = new() { IconId = gearset.JobId + 62000u },
                ChildNodes = [
                    new() {
                        Id        = "ExclamationMark",
                        ClassList = ["gearset--icon--exclamation-mark"],
                        NodeValue = FontAwesomeIcon.ExclamationTriangle.ToIconString(),
                        Tooltip   = I18N.Translate("Widget.GearsetSwitcher.WarningTooltip.AppearanceDiffers"),
                        Style     = new() { IsVisible = false },
                    }
                ]
            },
            new() {
                ClassList = ["gearset--body"],
                ChildNodes = [
                    new() {
                        Id        = "Name",
                        ClassList = ["gearset--body--name"],
                        NodeValue = gearset.Name
                    },
                    new() {
                        Id        = "Info",
                        ClassList = ["gearset--body--info"],
                        NodeValue = $"Level {gearset.JobLevel} {gearset.JobName}"
                    }
                ]
            },
            new() {
                Id        = "ItemLevel",
                ClassList = ["gearset--ilvl"],
                NodeValue = gearset.Name
            },
            new() {
                Id        = "ExpBar",
                ClassList = ["gearset-exp-bar"],
                ChildNodes = [
                    new() {
                        Id        = "ExpBarFill",
                        ClassList = ["gearset-exp-bar--bar"],
                        Style     = new() { Size = new(25, NodeHeight - 14) },
                    },
                ]
            },
            new() {
                Id        = "ExpBarText",
                ClassList = ["gearset-exp-bar--text"],
                NodeValue = "50%"
            }
        ];
    }

    public void Update()
    {
        IconNode.Style.IconId      = _player.GetJobInfo(Gearset.JobId).GetIcon(ButtonIconType);
        IconNode.Style.ImageOffset = new(0, ButtonIconYOffset);

        NameNode.NodeValue       = Gearset.Name;
        InfoNode.NodeValue       = GetCurrentGearsetStatusText();
        IlvlNode.NodeValue       = Gearset.ItemLevel.ToString();
        IlvlNode.Style.IsVisible = ShowItemLevel;

        ExpBarNode.Style.IsVisible     = !Gearset.IsMaxLevel && ShowExperienceBar;
        ExpBarTextNode.Style.IsVisible = !Gearset.IsMaxLevel && ShowExperiencePct;
        ExpBarTextNode.NodeValue       = $"{Gearset.JobXp}%";
        ExpBarFillNode.Style.Size      = new((NodeWidth - 12) * Gearset.JobXp / 100, 1);
        WarnNode.Style.IsVisible       = ShowWarningIcon;

        if (!ShowExperiencePct) {
            IlvlNode.TagsList.Remove("with-exp-bar");
        } else {
            switch (Gearset.IsMaxLevel) {
                case false when !IlvlNode.TagsList.Contains("with-exp-bar"):
                    IlvlNode.TagsList.Add("with-exp-bar");
                    break;
                case true when IlvlNode.TagsList.Contains("with-exp-bar"):
                    IlvlNode.TagsList.Remove("with-exp-bar");
                    break;
            }
        }

        if (Gearset == _repository.CurrentGearset && !TagsList.Contains("current")) {
            TagsList.Add("current");
        } else if (Gearset != _repository.CurrentGearset && TagsList.Contains("current")) {
            TagsList.Remove("current");
        }

        SetBackgroundGradientFor(Gearset.Category);

        if (ShowWarningIcon && Gearset.IsMainHandMissing) {
            WarnNode.Style.Color     = new(0xE00000DA);
            WarnNode.Style.IsVisible = true;
            WarnNode.Tooltip         = I18N.Translate("Widget.GearsetSwitcher.WarningTooltip.MissingMainHand");
        } else if (ShowWarningIcon && Gearset.HasMissingItems) {
            WarnNode.Style.Color     = new(0xE000DADF);
            WarnNode.Style.IsVisible = true;
            WarnNode.Tooltip         = I18N.Translate("Widget.GearsetSwitcher.WarningTooltip.MissingItems");
        } else if (ShowWarningIcon && Gearset.AppearanceDiffers) {
            WarnNode.Style.Color     = new(0xC0A0A0A0);
            WarnNode.Style.IsVisible = true;
            WarnNode.Tooltip         = I18N.Translate("Widget.GearsetSwitcher.WarningTooltip.AppearanceDiffers");
        } else {
            WarnNode.Style.IsVisible = false;
        }
    }

    private Node IconNode       => QuerySelector("Icon")!;
    private Node NameNode       => QuerySelector("Name")!;
    private Node InfoNode       => QuerySelector("Info")!;
    private Node IlvlNode       => QuerySelector("ItemLevel")!;
    private Node WarnNode       => QuerySelector("ExclamationMark")!;
    private Node ExpBarNode     => QuerySelector("ExpBar")!;
    private Node ExpBarFillNode => QuerySelector("ExpBarFill")!;
    private Node ExpBarTextNode => QuerySelector("ExpBarText")!;

    private string GetCurrentGearsetStatusText()
    {
        string jobName = !Gearset.JobName.Equals(Gearset.Name, StringComparison.OrdinalIgnoreCase)
            ? Gearset.JobName
            : string.Empty;

        return $"{I18N.Translate("Widget.GearsetSwitcher.JobLevel", Gearset.JobLevel)} {jobName}".TrimEnd();
    }

    private void SetBackgroundGradientFor(GearsetCategory category)
    {
        switch (category) {
            case GearsetCategory.Tank:
                UpdateBackground(new("Role.Tank"));
                break;
            case GearsetCategory.Healer:
                UpdateBackground(new("Role.Healer"));
                break;
            case GearsetCategory.Melee:
                UpdateBackground(new("Role.MeleeDps"));
                break;
            case GearsetCategory.Ranged:
                UpdateBackground(new("Role.PhysicalRangedDps"));
                break;
            case GearsetCategory.Caster:
                UpdateBackground(new("Role.MagicalRangedDps"));
                break;
            case GearsetCategory.Crafter:
                UpdateBackground(new("Role.Crafter"));
                break;
            case GearsetCategory.Gatherer:
                UpdateBackground(new("Role.Gatherer"));
                break;
            default:
                UpdateBackground(new(0));
                break;
        }
    }

    private void UpdateBackground(Color color)
    {
        switch (BackgroundType) {
            case "GradientV":
                Style.BackgroundColor    = null;
                Style.BackgroundGradient = GradientColor.Vertical(color, new(0));
                return;
            case "GradientVI":
                Style.BackgroundColor    = null;
                Style.BackgroundGradient = GradientColor.Vertical(new(0), color);
                return;
            case "GradientH":
                Style.BackgroundColor    = null;
                Style.BackgroundGradient = GradientColor.Horizontal(color, new(0));
                return;
            case "GradientHI":
                Style.BackgroundColor    = null;
                Style.BackgroundGradient = GradientColor.Horizontal(new(0), color);
                return;
            case "Plain":
                Style.BackgroundColor    = color;
                Style.BackgroundGradient = null;
                return;
            default:
                Style.BackgroundColor    = null;
                Style.BackgroundGradient = null;
                break;
        }
    }
}
