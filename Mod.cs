using SimAirport.Modding.Base;
using SimAirport.Modding.Settings;

namespace TBFlash.LeakingUrinal
{
    public class Mod : BaseMod
    {
        public override string Name => "Leaking Urinal";

        public override string InternalName => "TBFlash.LeakingUrinal";

        public override string Description => "When maintenance is required, now you know!";

        public override string Author => "TBFlash";

        public override SettingManager SettingManager { get; set; }

        public override void OnTick()
        {
        }

        public override void OnLoad(SimAirport.Modding.Data.GameState state)
        {
        }
    }
}
