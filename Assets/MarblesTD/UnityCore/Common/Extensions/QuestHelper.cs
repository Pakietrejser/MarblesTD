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
                QuestID.WildOnly => "Tylko dzicy sojusznicy od 1 do 40 fali",
                QuestID.NobleOnly => "Tylko zacni sojusznicy od 1 do 40 fali",
                QuestID.NightOnly => "Tylko nocni sojusznicy od 1 do 40 fali",
                QuestID.NoDamage => "Bez otrzymania obrażeń od 1 do 40 fali",
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }
    }
}