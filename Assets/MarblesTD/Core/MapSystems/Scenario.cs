using System;
using System.Collections.Generic;
using System.Linq;
using MarblesTD.Core.Common.Enums;

namespace MarblesTD.Core.MapSystems
{
    public class Scenario
    {
        public ScenarioID ID { get; }
        public bool Completed => _questsCompletion.Values.Any(x => x);
        
        readonly Dictionary<QuestID, bool> _questsCompletion;

        public Scenario(ScenarioID id, bool questA, bool questB, bool questC)
        {
            ID = id;
            _questsCompletion = new Dictionary<QuestID, bool>
            {
                {QuestID.Wave20, questA},
                {QuestID.Wave40, questB},
                {GetLastQuestFor(id), questC},
            };
        }

        public bool TryCompleteQuest(QuestID questID)
        {
            if (!_questsCompletion.TryGetValue(questID, out bool completed) || completed) return false;
            _questsCompletion[questID] = true;
            return true;
        }

        public bool GetQuestCompletion(int index)
        {
            return index switch
            {
                0 => _questsCompletion[QuestID.Wave20],
                1 => _questsCompletion[QuestID.Wave40],
                2 => _questsCompletion[GetLastQuestFor(ID)],
                _ => throw new ArgumentException()
            };
        }

        static QuestID GetLastQuestFor(ScenarioID id)
        {
            return id switch
            {
                ScenarioID.Ambush => QuestID.NoDamage,
                ScenarioID.BranchingOut => QuestID.WildOnly,
                ScenarioID.Garden => QuestID.NobleOnly,
                ScenarioID.HelloWorld => QuestID.NoDamage,
                ScenarioID.Infinity => QuestID.WildOnly,
                ScenarioID.Intersection => QuestID.NightOnly,
                ScenarioID.Labyrinth => QuestID.NoDamage,
                ScenarioID.Lake => QuestID.NoDamage,
                ScenarioID.LastStand => QuestID.NobleOnly,
                ScenarioID.Loops => QuestID.WildOnly,
                ScenarioID.LostGlasses => QuestID.NobleOnly,
                ScenarioID.MagicAntlers => QuestID.NightOnly,
                ScenarioID.PayUp => QuestID.WildOnly,
                ScenarioID.Sandwich => QuestID.WildOnly,
                ScenarioID.Scribbles => QuestID.NightOnly,
                ScenarioID.Snail => QuestID.NightOnly,
                ScenarioID.Sneak => QuestID.NobleOnly,
                ScenarioID.SpiderLair => QuestID.NoDamage,
                ScenarioID.Spider => QuestID.NightOnly,
                ScenarioID.TwinTowers => QuestID.NobleOnly,
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }
    }
}