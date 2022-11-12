//Postfix for [RimWorld.Pawn_RelationsTracker.SecondaryLovinChanceFactor]
using HarmonyLib;
using RimWorld;
using Verse;

namespace CoSpousalRelations {
public static class Postfix_SecondaryLovinChanceFactor {
    // Modifies the "SecondaryLovinChanceFactor" method to permit (rare) spontaneous bisexuality, as existed in earlier versions of the game
    public static void Patch(Pawn otherPawn, ref float __result, ref Pawn ___pawn, Pawn_RelationsTracker __instance) {
        if (__result > 0.15f) { return; }
        if (___pawn.def == otherPawn.def && ___pawn != otherPawn) {
            if (___pawn.ageTracker.AgeBiologicalYearsFloat > 16f && otherPawn.ageTracker.AgeBiologicalYearsFloat > 16f) {
                __result = __instance.LovinAgeFactor(otherPawn) * __instance.PrettinessFactor(otherPawn) * 0.15f;
            }
        }
    }
}
}