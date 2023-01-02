using System;
using MarblesTD.Core.Common.Enums;

namespace MarblesTD.UnityCore.Common.Extensions
{
    public static class QuestHelper
    {
        public static string GetQuestDescription(this QuestID id)
        {
            return id switch
            {
                QuestID.Wave20 => "Powstrzymaj 20 falę",
                QuestID.Wave40 => "Powstrzymaj 40 falę",
                QuestID.WildOnly => "Tylko Dzicy sojusznicy",
                QuestID.NobleOnly => "Tylko Zacni sojusznicy",
                QuestID.NightOnly => "Tylko Nocni sojusznicy",
                QuestID.NoDamage => "Bez otrzymania obrażeń",
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }
    }
}