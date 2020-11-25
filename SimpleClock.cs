using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Cysharp.Threading.Tasks;
using OpenMod.Unturned.Plugins;
using OpenMod.API.Plugins;
using SDG.Unturned;
using System.Threading.Tasks;
using OpenMod.Core.Helpers;

[assembly: PluginMetadata("SS.SimpleClock", DisplayName = "SimpleClock")]
namespace SimpleClock
{
    public class SimpleClock : OpenModUnturnedPlugin
    {
        private readonly IConfiguration m_Configuration;
        private readonly IStringLocalizer m_StringLocalizer;
        private readonly ILogger<SimpleClock> m_Logger;

        internal bool Active = false;
        internal uint ActualHour = 00;
        internal uint ActualMinute = 00;

        internal int lev = 0;

        public SimpleClock(
            IConfiguration configuration, 
            IStringLocalizer stringLocalizer,
            ILogger<SimpleClock> logger, 
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            m_Configuration = configuration;
            m_StringLocalizer = stringLocalizer;
            m_Logger = logger;
        }

        protected override async UniTask OnLoadAsync()
        {
            // await UniTask.SwitchToMainThread();
            Active = true;
            m_Logger.LogInformation("Plugin loaded correctly!");
            Level.onLevelLoaded += OnLevelLoaded;
			// await UniTask.SwitchToThreadPool();
        }

        private void OnLevelLoaded(int level)
        {
            lev++;
            if (lev == 2)
            {
                AsyncHelper.Schedule("Clock Update", () => MinutesHelper().AsTask());
                AsyncHelper.Schedule("Clock Update", () => Clock().AsTask());
            }
        }

        public async UniTask Clock()
        {
            await UniTask.SwitchToMainThread();
            while (Active)
            {
                await UniTask.DelayFrame(48, PlayerLoopTiming.Update);
                var cycle = LightingManager.cycle;
                var actualtime = LightingManager.time;
                ActualHour = GetRealHour(actualtime, cycle);
                EffectManager.sendUIEffect(20540, 540, true, ActualHour.ToString(), ActualMinute.ToString());
            }
        }

        public async UniTask MinutesHelper()
        {
            await UniTask.SwitchToMainThread();
            LightingManager.time = 0;
            int i = 0;
            while (Active)
            {
                await UniTask.DelayFrame(48, PlayerLoopTiming.Update);
                i++;
                if (i <= (LightingManager.cycle / 24) - 1)
                {
                    i = 0;
                }
                
            }
        }

        //private uint GetActualMinute(uint ActualTime)
        //{

        //}

        private uint GetRealHour(uint ActualTime, uint Cycle)
        {
            
            if (ActualTime >= 0 && ActualTime < Cycle / 24)
            {
                return 05;
            }
            else if (ActualTime >= Cycle / 24 && ActualTime < Cycle / 23)
            {
                return 06;
            }
            else if (ActualTime >= Cycle / 23 && ActualTime < Cycle / 22)
            {
                return 07;
            }
            else if (ActualTime >= Cycle / 22 && ActualTime < Cycle / 21)
            {
                return 08;
            }
            else if (ActualTime >= Cycle / 21 && ActualTime < Cycle / 20)
            {
                return 09;
            }
            else if (ActualTime >= Cycle / 20 && ActualTime < Cycle / 19)
            {
                return 10;
            }
            else if (ActualTime >= Cycle / 19 && ActualTime < Cycle / 18)
            {
                return 11;
            }
            else if (ActualTime >= Cycle / 18 && ActualTime < Cycle / 17)
            {
                return 12;
            }
            else if (ActualTime >= Cycle / 17 && ActualTime < Cycle / 16)
            {
                return 13;
            }
            else if (ActualTime >= Cycle / 16 && ActualTime < Cycle / 15)
            {
                return 14;
            }
            else if (ActualTime >= Cycle / 15 && ActualTime < Cycle / 14)
            {
                return 15;
            }
            else if (ActualTime >= Cycle / 14 && ActualTime < Cycle / 13)
            {
                return 16;
            }
            else if (ActualTime >= Cycle / 13 && ActualTime < Cycle / 12)
            {
                return 17;
            }
            else if (ActualTime >= Cycle / 12 && ActualTime < Cycle / 11)
            {
                return 18;
            }
            else if (ActualTime >= Cycle / 11 && ActualTime < Cycle / 10)
            {
                return 19;
            }
            else if (ActualTime >= Cycle / 10 && ActualTime < Cycle / 9)
            {
                return 20;
            }
            else if (ActualTime >= Cycle / 9 && ActualTime < Cycle / 8)
            {
                return 21;
            }
            else if (ActualTime >= Cycle / 8 && ActualTime < Cycle / 7)
            {
                return 22;
            }
            else if (ActualTime >= Cycle / 7 && ActualTime < Cycle / 6)
            {
                return 23;
            }
            else if (ActualTime >= Cycle / 6 && ActualTime < Cycle / 5)
            {
                return 00;
            }
            else if (ActualTime >= Cycle / 5 && ActualTime < Cycle / 4)
            {
                return 01;
            }
            else if (ActualTime >= Cycle / 4 && ActualTime < Cycle / 3)
            {
                return 02;
            }
            else if (ActualTime >= Cycle / 3 && ActualTime < Cycle / 2)
            {
                return 03;
            }
            else if (ActualTime >= Cycle / 2 && ActualTime < Cycle / 1)
            {
                return 04;
            }
            return 00;
        }

        protected override async UniTask OnUnloadAsync()
        {
            // await UniTask.SwitchToMainThread();
            Active = false;
            m_Logger.LogInformation("Plugin unloaded correctly!");
        }
    }
}
