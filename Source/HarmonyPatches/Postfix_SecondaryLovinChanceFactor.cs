//Postfix for [RimWorld.Pawn_RelationsTracker.SecondaryLovinChanceFactor]
using HarmonyLib;
using RimWorld;
using Verse;

namespace CoSpousalRelations {
public static class Postfix_SecondaryLovinChanceFactor {
    // Modifies the "SecondaryLovinChanceFactor" method to permit (rare) spontaneous bisexuality, as existed in earlier versions of the game
    public static void Patch(Pawn otherPawn, ref float __result, ref Pawn ___pawn) {
        if (___pawn.def == otherPawn.def && ___pawn != otherPawn) {
            if (___pawn.ageTracker.AgeBiologicalYearsFloat > 16f && otherPawn.ageTracker.AgeBiologicalYearsFloat > 16f) {
                __result = 0.15f;
            }
        }
    }
}
}