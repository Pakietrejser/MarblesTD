using System;
using System.Collections.Generic;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Common.Extensions;

namespace MarblesTD.Core.MapSystems
{
    public abstract class Scenario
    {
        public virtual string Name => GetType().GetName();
        
        public IEnumerable<Quest> Quests
        {
            get
            {
                yield return Quest.Wave20;
                yield return Quest.Wave30;
                yield return LastQuest switch
                {
                    Quest.Wave20 => throw new Exception("Invalid last quest"),
                    Quest.Wave30 => throw new Exception("Invalid last quest"),
                    _ => LastQuest
                };
            }
        } 
        
        protected abstract Quest LastQuest { get; }
    }

    public class ScenarioA : Scenario
    {
        protected override Quest LastQuest => Quest.NoDamage;
    }
}