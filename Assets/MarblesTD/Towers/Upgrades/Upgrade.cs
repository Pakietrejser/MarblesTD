using System;

namespace MarblesTD.Towers.Upgrades
 {
     public interface IUpgrade
     {
         string Name { get; }
         string Description { get; }
         int Cost { get; }
     
         void Apply(Tower tower);
     }
     
     public abstract class Upgrade<TTower, TSettings> : IUpgrade where TTower : Tower where TSettings : Upgrade<TTower,TSettings>.Settings
     {
         protected abstract TSettings Setting { get; }
         
         public virtual string Name => Setting.Name;
         public virtual string Description => Setting.Description;
         public virtual int Cost => Setting.Cost;

         public void Apply(Tower tower) { ExplicitApply((TTower) tower); }
         protected abstract void ExplicitApply(TTower tower);
         
         [Serializable]
         public class Settings
         {
             public string Name;
             public string Description;
             public int Cost;
         }
     }

     public class RuleOfThree : Upgrade<QuickFox, RuleOfThree.Settings>
     {
         public int Damage => Setting.Damage;
         public int Range => Setting.Range;
         
         protected override Settings Setting { get; }
         public RuleOfThree(Settings setting) { Setting = setting; }

         protected override void ExplicitApply(QuickFox tower)
         {
             tower.Damage += Damage * Range;
         }
         
         [Serializable]
         public new class Settings : Upgrade<QuickFox, Settings>.Settings
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