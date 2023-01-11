using System;
using MarblesTD.Core.Entities.Towers;
using UnityEngine;

namespace MarblesTD.Core.Common.Requests.List
{
    public readonly struct CreateTowerRequest : IRequest<Tower>
    {
        public readonly Tower.IView View;
        public readonly Vector2 Position;
        public readonly Type Type;

        public CreateTowerRequest(Tower.IView view, Vector2 position, Type type)
        {
            View = view;
            Position = position;
            Type = type;
        }
    }
}