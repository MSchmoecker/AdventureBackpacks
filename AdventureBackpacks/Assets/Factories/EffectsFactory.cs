﻿using System;
using System.Collections.Generic;
using AdventureBackpacks.Assets.Effects;
using BepInEx.Bootstrap;
using Vapok.Common.Abstractions;
using Vapok.Common.Managers.Configuration;

namespace AdventureBackpacks.Assets.Factories;

public enum BackpackEffect
{
    FeatherFall,
    ColdResistance,
    Demister,
    WaterResistance,
    FrostResistance,
    TrollArmor,
    NecromancyArmor
}
public class EffectsFactory : FactoryBase
{
    public static Dictionary<BackpackEffect,EffectsBase> EffectList => _effectList;

    private static Dictionary<BackpackEffect,EffectsBase> _effectList = new();
    
    public EffectsFactory(ILogIt logger, ConfigSyncBase configs) : base(logger, configs)
    {
    
    }

    public void RegisterEffects()
    {
        _effectList.Add(BackpackEffect.FeatherFall, new FeatherFall("Feather Fall", "When activated allows you to slow fall gracefully and without damage from high elevations."));
        _effectList.Add(BackpackEffect.ColdResistance, new ColdResistance("Cold Immunity", "When activated keeps you from feeling cold. Does not prevent freezing."));
        _effectList.Add(BackpackEffect.Demister, new Effects.Demister("Demister", "When activated provides you with the Wisplight Effect, which clears mist from a small area around you while in the Mistlands."));
        _effectList.Add(BackpackEffect.WaterResistance, new Waterproof("Water Resistance", "When activated allows you to stay dry from the rain. Will still get wet if swimming."));
        _effectList.Add(BackpackEffect.FrostResistance, new FrostResistance("Frost Resistance", "When activated allows you to stay warm in freezing conditions, negating the freezing debuff."));
        _effectList.Add(BackpackEffect.TrollArmor, new TrollArmor("Troll Armor Set", "When activated the backpack acts as the Shoulder Set piece of the Troll Armor Set allowing the set to complete for the Sneak Effect"));

        if (Chainloader.PluginInfos.ContainsKey("com.chebgonaz.ChebsNecromancy"))
        {
            _effectList.Add(BackpackEffect.NecromancyArmor, new NecromancyArmor("Necromancy Armor Effect", "When activated the backpack provides the Necromancy Armor effect from Cheb's Necromancy"));
        }

        foreach (BackpackEffect effect in Enum.GetValues(typeof(BackpackEffect)))
        {   
            if (EffectList.ContainsKey(effect))
                EffectList[effect].RegisterEffectConfiguration();
        }
    }
}