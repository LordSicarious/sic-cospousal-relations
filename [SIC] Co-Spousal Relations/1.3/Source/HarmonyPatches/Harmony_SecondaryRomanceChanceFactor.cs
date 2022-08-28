//Postfix to [RimWorld.Pawn_RelationsTracker.SecondaryRomanceChanceFactor]
using HarmonyLib;
using RimWorld;
using Verse;

namespace CoSpousalRelations {
public static class Harmony_SecondaryRomanceChanceFactor {
    // Modifies the "IdeoligionForbids" method to permit sharing a bed so long as any valid partner is found, so that co-spouses can share a bed
    public static void SecondaryRomanceChanceFactor_Postfix(Pawn otherPawn, ref float __result, ref Pawn ___pawn) {
        if (CoSpousalRelationsDefOf.CoSpouse.Worker.InRelation(___pawn, otherPawn)) {
            __result += 0.15f;
        }
        return;
    }
}
}