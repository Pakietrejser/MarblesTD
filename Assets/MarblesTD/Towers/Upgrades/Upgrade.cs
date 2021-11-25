using System;

namespace MarblesTD.Towers.Upgrades
 {
     public abstract class Upgrade
     {
         public string Name;
         public string Description;
         public int Cost;

         public virtual void UpdateSettings(SettingsBase settingsBase)
         {
             Name = settingsBase.Name;
             Description = settingsBase.Description;
             Cost = settingsBase.Cost;
         }

         public abstract void Apply(Tower tower);
         
         [Serializable]
         public class SettingsBase
         {
             public string Name;
             public string Description;
             public int Cost;
         }
     }
     
     public abstract class Upgrade<TTower, TSettings> : Upgrade where TTower : Tower where TSettings : Upgrade.SettingsBase
     {
         protected Upgrade(TSettings settings)
         {
             UpdateSettings(settings);
         }

         protected abstract void ExplicitApply(TTower tower);
         public sealed override void Apply(Tower tower)
         {
             if (!(tower is TTower newTower)) throw new ArgumentException();
             ExplicitApply(newTower);
         }

         protected abstract void ExplicitUpdateSettings(TSettings settings);
         public sealed override void UpdateSettings(SettingsBase settingsBase)
         {
             if (!(settingsBase is TSettings newSettings)) throw new ArgumentException();
             base.UpdateSettings(newSettings);
             ExplicitUpdateSettings(newSettings);
         }
     }

     public class RuleOfThree : Upgrade<QuickFox, RuleOfThree.Settings>
     {
         public int Damage;
         public int Range;
         
         public RuleOfThree(Settings settings) : base(settings) { }

         protected override void ExplicitApply(QuickFox tower)
         {
             tower.Damage += Damage * Range;
         }

         protected override void ExplicitUpdateSettings(Settings settings)
         {
             Damage = settings.Damage;
             Range = settings.Range;
         }

         [Serializable]
         public class Settings : SettingsBase
         {
             public int Damage;
             public int Range;
         }
     }
 }
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
public enum Path
{
    None,
    Left,
    Middle,
    Right,
}