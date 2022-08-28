//Postfix to [RimWorld.RimWorld.CompAssignableToPawn_Bed.IdeoligionForbids]
using HarmonyLib;
using RimWorld;
using Verse;


namespace CoSpousalRelations {
public static class Harmony_IdeoligionForbids {
    // Modifies the "IdeoligionForbids" method to permit sharing a bed so long as any valid partner is found, so that co-spouses can share a bed
    public static void IdeoligionForbids_Postfix(Pawn pawn, ref bool __result, ref CompAssignableToPawn_Bed __instance) {
        // No need to check if the result was already false
        if (!__result) {return;}
        foreach (Pawn assignedPawn in __instance.AssignedPawnsForReading) {
            if (pawn != assignedPawn && BedUtility.WillingToShareBed(pawn, assignedPawn)) {
                // If a valid bed partner is found, it is valid, so long as every other pawn is a valid bed partner of that partner
                __result = false;
                return;
            }
        }
        return;
    }
}
}