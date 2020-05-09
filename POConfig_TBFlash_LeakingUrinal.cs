using System.Collections.Generic;

namespace TBFlash.LeakingUrinal
{
    public class POConfig_TBFlash_LeakingUrinal : POConfig_NoData_SO<TBFlash_LeakingUrinal>
    {
        public bool isUrinal;
        public List<string> frontSprites;
        public List<string> leftSprites;
        public List<string> backSprites;
    }
}
