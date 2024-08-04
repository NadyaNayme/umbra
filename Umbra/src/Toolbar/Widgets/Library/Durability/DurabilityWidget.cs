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

using System.Collections.Generic;
using Dalamud.Interface;
using Dalamud.Plugin.Services;
using Dalamud.Utility;
using Lumina.Excel.GeneratedSheets;
using System.Linq;
using Umbra.Common;
using Umbra.Game;

namespace Umbra.Widgets;

[ToolbarWidget("Durability", "Widget.Durability.Name", "Widget.Durability.Description")]
internal partial class DurabilityWidget(
    WidgetInfo                  info,
    string?                     guid         = null,
    Dictionary<string, object>? configValues = null
) : DefaultToolbarWidget(info, guid, configValues)
{
    /// <inheritdoc/>
    public override MenuPopup Popup { get; } = new();

    private IPlayer      Player      { get; } = Framework.Service<IPlayer>();
    private IDataManager DataManager { get; } = Framework.Service<IDataManager>();

    private static List<uint> _darkMatterItemIds = [5594, 5595, 5596, 5597, 5598, 10386, 17837, 33916];

    /// <inheritdoc/>
    protected override void Initialize()
    {
        Node.OnClick += _ => Framework.Service<IChatSender>().Send("/action Repair");
        Node.OnRightClick += _ => Framework.Service<IChatSender>().Send("/action \"Materia Extraction\"");
    }

    /// <inheritdoc/>
    protected override void OnUpdate()
    {
        SetGhost(!GetConfigValue<bool>("Decorate"));

        if (GetConfigValue<bool>("HideWhenOkay")
            && Player.Equipment.LowestDurability > GetConfigValue<int>("WarningThreshold")
            && Player.Equipment.HighestSpiritbond < 100) {
            Node.Style.IsVisible = false;
            return;
        }

        Node.Style.IsVisible    = true;
        Popup.UseGrayscaleIcons = false;

        switch (GetConfigValue<string>("IconLocation")) {
            case "Left":
                SetLeftIcon(GetIconId());
                SetRightIcon(null);
                break;
            case "Right":
                SetLeftIcon(null);
                SetRightIcon(GetIconId());
                break;
            case "Hidden":
                SetLeftIcon(null);
                SetRightIcon(null);
                break;
        }

        switch (GetConfigValue<string>("TextAlign")) {
            case "Left":
                SetTextAlignLeft();
                break;
            case "Center":
                SetTextAlignCenter();
                break;
            case "Right":
                SetTextAlignRight();
                break;
        }

        switch (GetConfigValue<string>("DisplayMode")) {
            case "Full":
                SetTwoLabels(
                    $"{I18N.Translate("Widget.Durability.Durability")}: {Player.Equipment.LowestDurability}%",
                    $"{I18N.Translate("Widget.Durability.Spiritbond")}: {Player.Equipment.HighestSpiritbond}%"
                );

                break;
            case "Short":
                SetLabel($"{Player.Equipment.LowestDurability}% / {Player.Equipment.HighestSpiritbond}%");
                break;
            case "DurabilityOnly":
                SetLabel($"{Player.Equipment.LowestDurability}%");
                break;
            case "SpiritbondOnly":
                SetLabel($"{Player.Equipment.HighestSpiritbond}%");
                break;
            case "IconOnly":
                SetLabel(null);
                break;
        }

        LeftIconNode.Style.ImageGrayscale  = GetConfigValue<bool>("DesaturateIcon");
        RightIconNode.Style.ImageGrayscale = GetConfigValue<bool>("DesaturateIcon");
        LabelNode.Style.TextOffset         = new(0, GetConfigValue<int>("TextYOffset"));
        TopLabelNode.Style.TextOffset      = new(0, GetConfigValue<int>("TextYOffsetTop"));
        BottomLabelNode.Style.TextOffset   = new(0, GetConfigValue<int>("TextYOffsetBottom"));

        bool hasText = GetConfigValue<string>("DisplayMode") != "IconOnly";
        LeftIconNode.Style.Margin  = new(0, 0, 0, hasText ? -2 : 0);
        RightIconNode.Style.Margin = new(0, hasText ? -2 : 0, 0, 0);
        Node.Style.Padding         = new(0, hasText ? 6 : 3);

        List<EquipmentSlot> slots = [..Player.Equipment.Slots];
        slots.Sort((a, b) => b.Spiritbond.CompareTo(a.Spiritbond));

        for (var i = 0; i < 13; i++) {
            EquipmentSlot eq = slots[i];

            var slotId1 = $"Slot_{i}";
            Popup.SetButtonVisibility(slotId1, !eq.IsEmpty);
            Popup.SetButtonLabel(slotId1, eq.ItemName);
            Popup.SetButtonIcon(slotId1, eq.IconId);
            Popup.SetButtonAltLabel(slotId1, $"{eq.Durability}% / {eq.Spiritbond}%");
        }

        if (Popup.IsOpen) {
            Popup.SetButtonDisabled("Repair",  !UpdateRepairButtonState());
            Popup.SetButtonDisabled("Extract", !Player.IsGeneralActionUnlocked(14));
            Popup.SetButtonDisabled("Melding", !Player.IsGeneralActionUnlocked(13));
        }
    }

    private uint GetIconId()
    {
        if (Player.Equipment.LowestDurability <= GetConfigValue<int>("CriticalThreshold")) {
            return 60074; // Critical
        }

        if (Player.Equipment.LowestDurability <= GetConfigValue<int>("WarningThreshold")) {
            return 60073; // Warning
        }

        return 61512;
    }

    private bool UpdateRepairButtonState()
    {
        if (!Player.IsGeneralActionUnlocked(6)) {
            Popup.SetButtonAltLabel("Repair", null);
            return false;
        }

        uint highestDarkMatterType  = 0;
        int  highestDarkMatterCount = 0;

        foreach (uint itemId in _darkMatterItemIds) {
            var count = Player.GetItemCount(itemId);

            if (count > 0 && itemId > highestDarkMatterType) {
                highestDarkMatterCount = count;
                highestDarkMatterType  = itemId;
            }
        }

        if (0 == highestDarkMatterType) {
            Popup.SetButtonAltLabel("Repair", null);
            return false;
        }

        Item item = DataManager.GetExcelSheet<Item>()!.GetRow(highestDarkMatterType)!;
        Popup.SetButtonAltLabel("Repair", $"{item.Name.ToDalamudString().TextValue} x {highestDarkMatterCount}");

        return true;
    }
}
