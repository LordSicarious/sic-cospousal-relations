//Postfix to [RimWorld.RimWorld.CompAssignableToPawn_Bed.TryUnassignPawn]
using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;


namespace CoSpousalRelations {
public static class Harmony_TryUnassignPawn {
    // Modifies the "IdeoligionForbids" method to permit sharing a bed so long as any valid partner is found, so that co-spouses can share a bed
    public static void TryUnassignPawn_Postfix(Pawn pawn, bool sort, ref CompAssignableToPawn_Bed __instance) {
        Building_Bed ownedBed = pawn.ownership.OwnedBed;
        IEnumerable<Pawn> pawnListTmp = Enumerable.Empty<Pawn>(), pawnListMax = Enumerable.Empty<Pawn>();
        foreach (Pawn occupant in __instance.AssignedPawnsForReading.ToList()) {
            if (pawnListMax.Contains(occupant)) {
                continue;
            }
            pawnListTmp = RecursivelyAssignPawnsFromList(__instance.AssignedPawnsForReading.ToList(), occupant);
            if (pawnListTmp.Count() > pawnListMax.Count()) {
                pawnListMax = pawnListTmp.ToList();
            }
        }
        foreach (Pawn pawnToLeave in __instance.AssignedPawnsForReading.ToList().Except(pawnListMax)) {
            Messages.Message("PawnNoLongerAssignableToBed".Translate(pawnToLeave, pawn), pawnToLeave, MessageTypeDefOf.NegativeEvent, historical: true);
            __instance.TryUnassignPawn(pawnToLeave);
        }
            /*if (__instance.IdeoligionForbids(occupant)) {
                Messages.Message("PawnNoLongerAssignableToBed".Translate(occupant, pawn), occupant, MessageTypeDefOf.NegativeEvent, historical: true);
                __instance.TryUnassignPawn(occupant);
            }*/
    }

    // Returns the largest unbroken network of love relations within pawnList that contains pawn
    private static IEnumerable<Pawn> RecursivelyAssignPawnsFromList(IEnumerable<Pawn> pawnList, Pawn pawn) {
        IEnumerable<Pawn> pawnListTmp = Enumerable.Empty<Pawn>(), pawnListMax = Enumerable.Empty<Pawn>();
        foreach (Pawn partner in pawnList.ToList().Except(pawn)) {
            if (pawnListMax.Contains(partner)) {
                continue;
            }
            if (BedUtility.WillingToShareBed(pawn, partner)) {
                pawnListTmp = RecursivelyAssignPawnsFromList(pawnList.ToList().Except(pawn), partner);
                if (pawnListTmp.Count() > pawnListMax.Count()) {
                    pawnListMax = pawnListTmp.ToList();
                }
            }
        }
        return pawnListMax.Prepend(pawn);
    }
}
}