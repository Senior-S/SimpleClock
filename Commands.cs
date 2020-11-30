using Cysharp.Threading.Tasks;
using OpenMod.Core.Commands;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Command = OpenMod.Core.Commands.Command;

namespace SimpleClock
{
    public class Commands
    {
        [Command("test")]
        [CommandDescription("My awesome command")]
        public class CommandTest : Command
        {
            public CommandTest(IServiceProvider serviceProvider) : base(serviceProvider)
            {
            }

            protected override async Task OnExecuteAsync()
            {
                var actor = Context.Actor;
                await UniTask.SwitchToMainThread();
                await actor.PrintMessageAsync("Day: " + LightingManager.day + " Bias: " + LevelLighting.bias);
            }
        }
    }
}
