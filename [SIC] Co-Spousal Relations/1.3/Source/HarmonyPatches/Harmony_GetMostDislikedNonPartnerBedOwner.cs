//Postfix to [RimWorld.LovePartnerRelationUtility.GetMostDislikedNonPartnerBedOwner]
using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

using static RimWorld.SpouseRelationUtility;


namespace CoSpousalRelations {
public static class Harmony_GetMostDislikedNonPartnerBedOwner {
    // Modifies the Shared Bed thoughtworker's CurrentStateInternal method to ignore Co-Spouses
    public static void GetMostDislikedNonPartnerBedOwner_Postfix(Pawn p, ref Pawn __result) {
        // Result unchanged if there was no NonPartner Bed Owner
        if (__result == null) {return;}
        Building_Bed ownedBed = p.ownership.OwnedBed;
        if (ownedBed == null) {return;}
		__result = null;
		int num = 0;
        List<Pawn> pSpouses = p.GetSpouses(false).ToList(), occupantSpouses;
        bool sharing;
		foreach (Pawn occupant in ownedBed.OwnersForReading) {
            sharing = false;
			if (occupant != p && !LovePartnerRelationUtility.LovePartnerRelationExists(p, occupant)) {
                occupantSpouses = occupant.GetSpouses(false).ToList();
                foreach (Pawn mutualSpouse in pSpouses.Intersect(occupantSpouses)) {
                    if (ownedBed.OwnersForReading.Contains(mutualSpouse)) {
                        sharing = true;
                    }
                }
                if (!sharing) {
                    int num2 = p.relations.OpinionOf(occupant);
                    if (__result == null || num2 < num) {
                        __result = occupant;
                        num = num2;
                    }
                }
			}
		}
        return;
    }
}
}