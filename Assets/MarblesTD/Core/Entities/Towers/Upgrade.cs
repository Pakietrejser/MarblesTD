using System;

namespace MarblesTD.Core.Entities.Towers
 {
     public abstract class Upgrade
     {
         public abstract int Cost { get; }
         public abstract string Description { get; }
         public bool Applied { get; protected set; }
         public abstract void Apply(Tower tower);
     }
     
     public abstract class Upgrade<TTower> : Upgrade where TTower : Tower
     {
         protected abstract void ExplicitApply(TTower tower);
         public sealed override void Apply(Tower tower)
         {
             if (!(tower is TTower newTower)) throw new ArgumentException();
             Applied = true;
             ExplicitApply(newTower);
         }
     }
 }