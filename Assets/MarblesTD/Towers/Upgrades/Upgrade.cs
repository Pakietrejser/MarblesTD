// namespace MarblesTD.Towers.Upgrades
// {
//     public abstract class Upgrade
//     {
//         public abstract string Name { get; }
//         public abstract string Description { get; }
//         
//         public abstract int Priority { get; }
//         public abstract int Cost { get; }
//
//         // path, tier
//
//         public abstract void Apply(TTower tower);
//     }
//
//     public abstract class Upgrade<TTower> : Upgrade
//     {
//         public override Tower Tower => GetType();
//     }
//     
//     public enum Path
//     {
//         NULL,
//         Left,
//         Middle,
//         Right,
//     }
//
//     public class RuleOfThree : Upgrade<QuickFox>
//     {
//         public override string Name { get; }
//         public override string Description { get; }
//         
//         public override int Priority { get; }
//         public override int Cost { get; }
//         public override void Apply(QuickFox tower)
//         {
//             throw new System.NotImplementedException();
//         }
//     }
//
//
//     public static class TowerExtensions
//     {
//         public static int UpgradeTier(this Tower tower)
//         {
//             tower.Upgrades
//         }
//         
//         public static Path UpgradePath(this Tower tower)
//         {
//             
//         }
//     }
//     
//
//     public abstract class Tower
//     {
//         public abstract string Name { get; }
//         public abstract string Description { get; }
//         
//         public abstract int Cost { get; }
//         
//         
//         public readonly Upgrade[,] Upgrades;
//
//         public Tower(Upgrade[,] upgrades)
//         {
//             this.Upgrades = upgrades;
//         }
//     }
// }

//TODO: s
// internal interface ICommandHandler
// {
//     Type CommandType { get; }
//     Action<ICommand> Execute { get; }
//     bool CanUndo { get; }
//     Action<ICommand> Undo { get; }
// }
//
// public abstract class CommandHandler<TCommand> : ICommandHandler where TCommand : ICommand
// {
//     public Type CommandType => typeof(TCommand);
//     public virtual bool CanUndo => false;
//         
//     public Action<ICommand> Execute => command => ExplicitExecute((TCommand) command);
//     public Action<ICommand> Undo => command => ExplicitUndo((TCommand) command);
//
//     protected abstract void ExplicitExecute(TCommand command);
//     protected virtual void ExplicitUndo(TCommand command) => throw new InvalidOperationException();
// }