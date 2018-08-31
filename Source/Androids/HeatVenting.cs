using System.Collections.Generic;
using Verse;

namespace MOARANDROIDS
{
    // Token: 0x0200044C RID: 1100
    public class PawnCapacityWorker_HeatVenting : PawnCapacityWorker
    {
        public override float CalculateCapacityLevel(HediffSet diffSet, List<PawnCapacityUtility.CapacityImpactor> impactors = null)
        {
            BodyPartTagDef EVKidney = BodyPartTagDefOf.HVSource;
            return PawnCapacityUtility.CalculateTagEfficiency(diffSet, EVKidney, float.MaxValue, default(FloatRange), impactors, -1f);
        }

        public override bool CanHaveCapacity(BodyDef body)
        {
            return body.HasPartWithTag(BodyPartTagDefOf.HVSource);
        }
    }
}