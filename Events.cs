using OpenMod.API.Eventing;
using OpenMod.Core.Commands.Events;
using System.Threading.Tasks;

namespace SimpleClock
{
    public class Events : IEventListener<CommandExecutingEvent>
    {
        public async Task HandleEventAsync(object sender, CommandExecutingEvent @event)
        {
            if (@event.CommandContext.CommandPrefix == "time")
            {
                @event.IsCancelled = true;
                await @event.Actor.PrintMessageAsync("This command is disabled by SimpleClock to avoid bugs!");
            }
        }
    }
}
