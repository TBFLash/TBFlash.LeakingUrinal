using System;

namespace TBFlash.LeakingUrinal
{
    public class POConfig_TBFlash_LeakingUrinal : POConfig_ToiletUtility
    {
        public override Type RuntimeType()
        {
              return typeof(TBFlash_LeakingUrinal);
            //  return typeof(ToiletUtility);
        }
    }
}
