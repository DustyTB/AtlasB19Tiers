using System.Collections.Generic;
using Verse;

namespace MOARANDROIDS
{
    // Token: 0x0200044A RID: 1098
    public class PawnCapacityWorker_ElectricalEfficiency : PawnCapacityWorker
    {
        public override float CalculateCapacityLevel(HediffSet diffSet, List<PawnCapacityUtility.CapacityImpactor> impactors = null)
        {
            BodyPartTagDef EVKidney = BodyPartTagDefOf.EVKidney;
            return PawnCapacityUtility.CalculateTagEfficiency(diffSet, EVKidney, float.MaxValue, default(FloatRange), impactors, -1f);
        }

        public override bool CanHaveCapacity(BodyDef body)
        {
            return body.HasPartWithTag(BodyPartTagDefOf.EVKidney);
        }
    }
}