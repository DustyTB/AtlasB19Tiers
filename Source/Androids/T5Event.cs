using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld.Planet;
using Verse;
using RimWorld;

namespace MOARANDROIDS
{
    // Token: 0x02000330 RID: 816
    public class IncidentWorker_DownedT5Android : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            return base.CanFireNowSub(parms) && this.TryFindTile(out int num) && SiteMakerHelper.TryFindRandomFactionFor(SiteCoreDefOf.DownedT5Android, null, out Faction faction, true, null);
        }

        private bool TryFindTile(out int tile)
        {
            return TileFinder.TryFindNewSiteTile(out tile, 7, 15, true, false, -1);
        }

        // Token: 0x06000DA9 RID: 3497 RVA: 0x000648C0 File Offset: 0x00062CC0
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            if (!this.TryFindFactions(out Faction faction, out Faction faction2))
            {
                return false;
            }
            if (!TileFinder.TryFindNewSiteTile(out int tile, 8, 30, false, true, -1))
            {
                return false;
            }
            Site site = SiteMaker.MakeSite(SiteCoreDefOf.DownedT5Android, SitePartDefOf.Turrets, tile, faction2);
            site.Tile = tile;
            int randomInRange = IncidentWorker_DownedT5Android.TimeoutDaysRange.RandomInRange;
            site.GetComponent<TimeoutComp>().StartTimeout(randomInRange * 60000);
            Find.WorldObjects.Add(site);
            return true;
        }

        // Token: 0x06000DAA RID: 3498 RVA: 0x0006497C File Offset: 0x00062D7C

        // Token: 0x06000DAB RID: 3499 RVA: 0x000649B8 File Offset: 0x00062DB8
        private bool TryFindFactions(out Faction alliedFaction, out Faction enemyFaction)
        {
            if ((from x in Find.FactionManager.AllFactions
                 where !x.def.hidden && !x.defeated && !x.IsPlayer && !x.HostileTo(Faction.OfPlayer) && this.CommonHumanlikeEnemyFactionExists(Faction.OfPlayer, x) && !this.AnyQuestExistsFrom(x)
                 select x).TryRandomElement(out alliedFaction))
            {
                enemyFaction = this.CommonHumanlikeEnemyFaction(Faction.OfPlayer, alliedFaction);
                return true;
            }
            alliedFaction = null;
            enemyFaction = null;
            return false;
        }

        // Token: 0x06000DAC RID: 3500 RVA: 0x000649F8 File Offset: 0x00062DF8
        private bool AnyQuestExistsFrom(Faction faction)
        {
            List<Site> sites = Find.WorldObjects.Sites;
            for (int i = 0; i < sites.Count; i++)
            {
                DefeatAllEnemiesQuestComp component = sites[i].GetComponent<DefeatAllEnemiesQuestComp>();
                if (component != null && component.Active && component.requestingFaction == faction)
                {
                    return true;
                }
            }
            return false;
        }

        // Token: 0x06000DAD RID: 3501 RVA: 0x00064A54 File Offset: 0x00062E54
        private bool CommonHumanlikeEnemyFactionExists(Faction f1, Faction f2)
        {
            return this.CommonHumanlikeEnemyFaction(f1, f2) != null;
        }

        // Token: 0x06000DAE RID: 3502 RVA: 0x00064A64 File Offset: 0x00062E64
        private Faction CommonHumanlikeEnemyFaction(Faction f1, Faction f2)
        {
            if ((from x in Find.FactionManager.AllFactions
                 where x != f1 && x != f2 && !x.def.hidden && x.def.humanlikeFaction && !x.defeated && x.HostileTo(f1) && x.HostileTo(f2)
                 select x).TryRandomElement(out Faction result))
            {
                return result;
            }
            return null;
        }

        // Token: 0x040008A5 RID: 2213
        private const float RelationsImprovement = 0f;

        private static readonly IntRange TimeoutDaysRange = new IntRange(12, 20);
    }
}