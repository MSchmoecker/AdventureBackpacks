﻿using System.Collections.Generic;
using AdventureBackpacks.Assets.Effects;
using Vapok.Common.Managers.StatusEffects;

namespace AdventureBackpacks.Assets.Items.BackpackItems;

internal class LegacySilverBackpack : BackpackItem
{
    public LegacySilverBackpack(string prefabName, string itemName) : base(prefabName, itemName)
    {
        RegisterConfigSettings();
    }

    internal sealed override void RegisterConfigSettings()
    {
        RegisterBackpackBiome();
        RegisterBackpackSize();
        RegisterStatusEffectInfo();
        RegisterWeightMultiplier();
        RegisterCarryBonus(45);
        RegisterSpeedMod();
        RegisterEnableFreezing(true);
    }
    
    internal override Vector2i GetInventorySize(int quality)
    {
        return base.GetInventorySize(1);
    }

    internal override void UpdateStatusEffects(int quality, CustomSE statusEffects, List<HitData.DamageModPair> modifierList, ItemDrop.ItemData itemData)
    {
        itemData.m_shared.m_movementModifier = SpeedMod.Value/quality;
        if (EnableFreezing.Value)
            modifierList.Add(FrostResistance.EffectMod);

        ((SE_Stats)statusEffects.Effect).m_addMaxCarryWeight = CarryBonus.Value * quality;

    }
}