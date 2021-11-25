using System;
using MarblesTD.Core.Towers;
using UnityEngine;

namespace MarblesTD.Core.Upgrades
 {
     public abstract class Upgrade
     {
         public string Name;
         public string Description;
         public int Cost;
         public Sprite Icon;

         public virtual void UpdateSettings(SettingsBase settingsBase)
         {
             Name = settingsBase.Name;
             Description = settingsBase.Description;
             Cost = settingsBase.Cost;
             Icon = settingsBase.Icon;
         }

         public abstract void Apply(Tower tower);
         
         [Serializable]
         public class SettingsBase
         {
             public string Name;
             public string Description;
             public int Cost;
             public Sprite Icon;
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
             ExplicitUpdateSettings(newSettings);
             base.UpdateSettings(newSettings);
         }
     }
 }