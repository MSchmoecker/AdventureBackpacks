﻿using System.Collections.Generic;
using AdventureBackpacks.Assets.Factories;
using ItemManager;
using Vapok.Common.Managers.StatusEffects;

namespace AdventureBackpacks.Assets.Items.BackpackItems;

internal class BackpackMeadows : BackpackItem
{
    public BackpackMeadows(string prefabName, string itemName) : base(prefabName , itemName)
    {
        RegisterConfigSettings();
        
        Item.Configurable = Configurability.Recipe | Configurability.Drop;
        
        AssignCraftingTable(CraftingTable.Workbench,2);
        
        Item.MaximumRequiredStationLevel = 3;
        
        AddRecipeIngredient("CapeDeerHide",1);
        AddRecipeIngredient("DeerHide",8);
        AddRecipeIngredient("BoneFragments",2);
        
        AddUpgradeIngredient("LeatherScraps", 5);
        AddUpgradeIngredient("DeerHide", 3);
        
        Item.DropsFrom.Add("Greyling", 0.002f, 1);
    }

    internal sealed override void RegisterConfigSettings()
    {
        RegisterBackpackBiome(BackpackBiomes.Meadows);
        RegisterBackpackSize(1,3,1);
        RegisterBackpackSize(2,4,1);
        RegisterBackpackSize(3,5,1);
        RegisterBackpackSize(4,6,1);
        RegisterWeightMultiplier();
        RegisterCarryBonus(5);
        RegisterSpeedMod();
        if ((BackpackBiome.Value & BackpackBiomes.Meadows) != 0) 
            EffectsFactory.EffectList[BackpackEffect.ColdResistance].RegisterEffectBiomeQuality(BackpackBiomes.Meadows, 3);
    }

    internal override void UpdateStatusEffects(int quality, CustomSE statusEffects, List<HitData.DamageModPair> modifierList, ItemDrop.ItemData itemData)
    {
        itemData.m_shared.m_movementModifier = SpeedMod.Value/quality;
        
        ((SE_Stats)statusEffects.Effect).m_addMaxCarryWeight = CarryBonus.Value * quality;
    }
}