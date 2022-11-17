using System;
using UnityEngine;

namespace MarblesTD.Core.Towers.Upgrades
 {
     public abstract class Upgrade
     {
         public string Name;
         public int Cost;
         
         public bool IsActive { get; protected set; }

         public virtual void UpdateSettings(SettingsBase settingsBase)
         {
             Name = settingsBase.Name;
             Cost = settingsBase.Cost;
         }

         public abstract void Apply(Tower tower);
         
         [Serializable]
         public class SettingsBase
         {
             [Header("Display Data")]
             public string Name;
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
             IsActive = true;
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