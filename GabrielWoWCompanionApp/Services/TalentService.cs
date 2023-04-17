using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabrielWoWCompanionApp.Services;

public class TalentService
{
    public static ObservableCollection<Talent> GetHolyTalents()
    {
        var talents = new ObservableCollection<Talent>
        {
            new() { Id = 0, Name = "Healing Focus", MaxRank = 2, DescriptionTemplate = "Reduces the pushback suffered from damaging attacks while casting any healing spell by {0}%.", TalentModifier = 35, IconPath = "healing_focus_icon.jpg"},
            
            new() { Id = 1, Name = "Improved Renew", MaxRank = 3, DescriptionTemplate = "Increases the amount healed by your Renew spell by {0}%.", TalentModifier = 5, IconPath = "renew_icon.jpg" },

            new() { Id = 2, Name = "Holy Specialization", MaxRank = 5, DescriptionTemplate = "Increases the critical effect chance of your Holy spells by {0}%.", TalentModifier = 1, IconPath = "holy_specialization_icon.jpg" },

            new() { Id = 3, Name = "Spell Warding", MaxRank = 5, DescriptionTemplate = "Reduces all spell damage taken by {0}%.", TalentModifier = 2, TotalRequiredTalents = 5, IconPath = "spellwarding_icon.jpg" },

            new() { Id = 4, Name = "Divine Fury", MaxRank = 5, DescriptionTemplate = "Reduces the casting time of your Smite, Holy Fire, Heal and Greater Heal spells by {0} sec.", TalentModifier = 0.1, TotalRequiredTalents = 5, IconPath = "divine_fury_icon.jpg" },

            new() { Id = 5, Name = "Desperate Prayer", MaxRank = 1, DescriptionTemplate = "Instantly Heals the caster for 263 to 325.", TotalRequiredTalents = 10, IconPath = "desperate_prayer_icon.jpg" },

            new() { Id = 6, Name = "Blessed Recovery", MaxRank = 3, DescriptionTemplate = "After being struck by a melee or ranged critical hit, Blessed Recovery heals you for {0}% of the damage taken over 6 sec.  Additional critical hits taken during the effect increase the healing received.", TalentModifier = 5, TotalRequiredTalents = 10, IconPath = "blessed_recovery_icon.jpg" },

            new() { Id = 7, Name = "Inspiration", MaxRank = 3, DescriptionTemplate = "Reduces your target's physical damage taken by {0}% for 15 sec after getting a critical effect from your Flash Heal, Heal, Greater Heal, Binding Heal, Penance, Prayer of Mending, Prayer of Healing, or Circle of Healing spell.", TalentModifier = 3.33, TotalRequiredTalents = 10, IconPath = "inspiration_icon.jpg" },

            new() { Id = 8, Name = "Holy Reach", MaxRank = 2, DescriptionTemplate = "Increases the range of your Smite and Holy Fire spells and the radius of your Prayer of Healing, Holy Nova, Divine Hymn and Circle of Healing spells by {0}%.", TalentModifier = 10, TotalRequiredTalents = 15, IconPath = "holy_reach_icon.jpg" },

            new() { Id = 9, Name = "Improved Healing", MaxRank = 3, DescriptionTemplate = "Reduces the mana cost of your Lesser Heal, Heal, Greater Heal, Divine Hymn and Penance spells by {0}%.", TalentModifier = 5, TotalRequiredTalents = 15, IconPath = "improved_healing_icon.jpg" },

            new() { Id = 10, Name = "Searing Light", MaxRank = 2, DescriptionTemplate = "Increases the damage of your Smite, Holy Fire, Holy Nova and Penance spells by {0}%.", TalentModifier = 5, TotalRequiredTalents = 15, IconPath = "searing_light_icon.jpg"  },

            new() { Id = 11, Name = "Healing Prayers", MaxRank = 2, DescriptionTemplate = "Reduces the mana cost of your Prayer of Healing and Prayer of Mending spell by {0}%.", TalentModifier = 10, TotalRequiredTalents = 20, IconPath = "healing_prayers_icon.jpg" },

            new() { Id = 12, Name = "Spirit of Redemption", MaxRank = 1, DescriptionTemplate = "Increases total Spirit by {0}% and upon death, the priest becomes the Spirit of Redemption for 15 sec.  The Spirit of Redemption cannot move, attack, be attacked or targeted by any spells or effects.  While in this form the priest can cast any healing spell free of cost.  When the effect ends, the priest dies.", TalentModifier = 5, TotalRequiredTalents = 20, IconPath = "redemption_icon.jpg" },

            new() { Id = 13, Name = "Spiritual Guidance", MaxRank = 5, DescriptionTemplate = "Increases spell power by {0}% of your total Spirit.", TalentModifier = 5, TotalRequiredTalents = 20, IconPath = "spiritual_guidance_icon.jpg" },

            new() { Id = 14, Name = "Surge of Light", MaxRank = 2, DescriptionTemplate = "Your spell criticals have a {0}% chance to cause your next Smite or Flash Heal spell to be instant cast, cost no mana but be incapable of a critical hit.  This effect lasts 10 sec.", TalentModifier = 25, TotalRequiredTalents = 25, IconPath = "sol_icon.jpg" },

            new() { Id = 15, Name = "Spiritual Healing", MaxRank = 5, DescriptionTemplate = "Increases the amount healed by your healing spells by {0}%.", TalentModifier = 2, TotalRequiredTalents = 25, IconPath = "spiritual_healing_icon.jpg" },

            new() { Id = 16, Name = "Holy Concentration", MaxRank = 3, DescriptionTemplate = "Your mana regeneration from spirit is increased by {0}% for 8 sec after you critically heal with Flash Heal, Greater Heal, Binding Heal or Empowered Renew.", TalentModifier = 16.33, TotalRequiredTalents = 30, IconPath = "holy_concentration_icon.jpg" },

            new() { Id = 17, Name = "Lightwell", MaxRank = 1, DescriptionTemplate = "Creates a Holy Lightwell.  Friendly players can click the Lightwell to restore 819 health over 6 sec.  Attacks done to you equal to 30% of your total health will cancel the effect. Lightwell lasts for 3 min or 10 charges.", TotalRequiredTalents = 30, IconPath = "lightwell_icon.jpg" },

            new() { Id = 18, Name = "Blessed Resilience", MaxRank = 3, TalentModifier = 1, TalentModifier2 = 20, DescriptionTemplate = "Increases the effectiveness of your healing spells by {0}%, and critical hits made against you have a {1}% chance to prevent you from being critically hit again for 6 sec.", TotalRequiredTalents = 30, IconPath = "blessed_resilience_icon.jpg"},

            new() { Id = 19, Name = "Body and Soul", MaxRank = 2, TalentModifier = 30, TalentModifier2 = 50, DescriptionTemplate = "When you cast Power Word: Shield, you increase the target's movement speed by {0}% for 4 sec, and you have a {1}% chance when you cast Abolish Disease on yourself to also cleanse 1 poison effect in addition to diseases.", TotalRequiredTalents = 35, IconPath = "body_and_soul_icon.jpg"},

            new() { Id = 20, Name = "Empowered Healing", MaxRank = 5, TalentModifier = 8, TalentModifier2 = 4, DescriptionTemplate = "Your Greater Heal spell gains an additional {0}% and your Flash Heal and Binding Heal gain an additional {1}% of your bonus healing effects.", TotalRequiredTalents = 35, IconPath = "empowered_healing_icon.jpg" },

            new() { Id = 21, Name = "Serendipity", MaxRank = 3 , DescriptionTemplate = "When you heal with Binding Heal or Flash Heal, the cast time of your next Greater Heal or Prayer of Healing spell is reduced by {0}%. Stacks up to 3 times. Lasts 20 sec.", TalentModifier = 4, TotalRequiredTalents = 35, IconPath = "serendipity_icon.jpg" },

            new() { Id = 22, Name = "Empowered Renew", MaxRank = 3, DescriptionTemplate = "Your Renew spell gains an additional {0}% of your bonus healing effects, and your Renew will instantly heal the target for {0} of the total periodic effect.", TalentModifier = 5, TotalRequiredTalents = 40, IconPath = "empowered_renew_icon.jpg"},

            new() { Id = 23, Name = "Circle of Healing", MaxRank = 1, DescriptionTemplate = "Heals up to 5 friendly party or raid members within 15 yards of the target for 958 to 1058.", TotalRequiredTalents = 40, IconPath = "coh_icon.jpg" },

            new() { Id = 24, Name = "Test of Faith", MaxRank = 3, DescriptionTemplate = "Increases healing by {0}% on friendly targets at or below 50% health.", TalentModifier = 4, TotalRequiredTalents = 40, IconPath = "test_of_faith_icon.jpg" },

            new() { Id = 25, Name = "Divine Providence", MaxRank = 5, TalentModifier = 2, TalentModifier2 = 6, DescriptionTemplate = "Increases the amount healed by Circle of Healing, Binding Heal, Holy Nova, Prayer of Healing, Divine Hymn and Prayer of Mending by {0}%, and reduces the cooldown of your Prayer of Mending by {1}%.", TotalRequiredTalents = 45, IconPath = "divine_providence_icon.jpg"},

            new() { Id = 26, Name = "Guardian Spirit", MaxRank = 1, DescriptionTemplate = "Calls upon a guardian spirit to watch over the friendly target. The spirit increases the healing received by the target by 40%, and also prevents the target from dying by sacrificing itself.  This sacrifice terminates the effect but heals the target of 50% of their maximum health. Lasts 10 sec.", TotalRequiredTalents = 50, IconPath = "guardian_spirit_icon.jpg" }
        };

        talents[10].PreRequisitieTalent = talents[4];
        talents[17].PreRequisitieTalent = talents[12];
        talents[4].DependantTalent = talents[10];
        talents[12].DependantTalent = talents[17];
        return talents;
    }
}
