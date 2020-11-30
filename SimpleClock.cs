using System;
using Microsoft.Extensions.Logging;
using Cysharp.Threading.Tasks;
using OpenMod.Unturned.Plugins;
using OpenMod.API.Plugins;
using SDG.Unturned;
using OpenMod.Core.Helpers;
using UnityEngine;

[assembly: PluginMetadata("SS.SimpleClock", DisplayName = "SimpleClock")]
namespace SimpleClock
{
    public class SimpleClock : OpenModUnturnedPlugin
    {
        private readonly ILogger<SimpleClock> m_Logger;

        internal bool Active = false;
        internal byte lev = 0;

        public SimpleClock(
            ILogger<SimpleClock> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            m_Logger = logger;
        }

        protected override async UniTask OnLoadAsync()
        {
            Active = true;
            m_Logger.LogInformation("Plugin loaded correctly!");
            Level.onLevelLoaded += OnLevelLoaded;
        }

        private void OnLevelLoaded(int level) //When the plugin is loaded this event is triggered (I don't know why) but for avoid problems i added a simple check with a byte.
        {
            lev++;
            if (lev == 2)
            {
                AsyncHelper.Schedule("Log Time", () => LogTime().AsTask());
            }
        }

        public async UniTask LogTime()
        {
            await UniTask.SwitchToMainThread();
            while (Active)
            {
                await UniTask.DelayFrame(48, PlayerLoopTiming.Update);
                float num = LightingManager.day / LevelLighting.bias;

                var dhour = Math.Floor(num * 12f);
                string digitalHour = (dhour + 6).ToString("00");
                var dminute = Math.Floor(((num * 12f) % 1f) * 181f);
                var dminh = 181f / 60f;
                string digitalMinute = Math.Floor(dminute / dminh).ToString("00");

                if ((dhour + 6) > 12)
                {
                    digitalHour = (dhour - 6).ToString("00");
                }

                EffectManager.sendUIEffect(32521, 521, true, $"{digitalHour}:{digitalMinute}");
            }
        }

        protected override async UniTask OnUnloadAsync()
        {
            Level.onLevelLoaded -= OnLevelLoaded;
            lev = 0;
            Active = false;
            m_Logger.LogInformation("Plugin unloaded correctly!");
        }
    }
}
