using System;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace CoSpousalRelations {
    [StaticConstructorOnStartup]
    public static class ApplyHarmonyPatches {
		// Reference to this class for patches
        static ApplyHarmonyPatches() {
			// Instantiate Harmony
			var harmony = new Harmony("sic.CoSpousalRelations.thisisanid");
			Type patchType;
			MethodInfo original;
			string modified;

            //Prefix to [RimWorld.CompAssignableToPawn_Bed.SortAssignedPawns]
			patchType = typeof(Harmony_SortAssignedPawns);
            original = AccessTools.Method(typeof(CompAssignableToPawn), name: "SortAssignedPawns");
            modified = nameof(Harmony_SortAssignedPawns.SortAssignedPawns_Prefix);
            harmony.Patch(original, prefix: new HarmonyMethod(patchType, modified));

			//Postfix to [RimWorld.LovePartnerRelationUtility.GetMostDislikedNonPartnerBedOwner]
            patchType = typeof(Harmony_GetMostDislikedNonPartnerBedOwner);
            original = AccessTools.Method(typeof(LovePartnerRelationUtility), name: "GetMostDislikedNonPartnerBedOwner");
            modified = nameof(Harmony_GetMostDislikedNonPartnerBedOwner.GetMostDislikedNonPartnerBedOwner_Postfix);
            harmony.Patch(original, postfix: new HarmonyMethod(patchType, modified));
			//Postfix to [RimWorld.RimWorld.CompAssignableToPawn_Bed.IdeoligionForbids]
			patchType = typeof(Harmony_IdeoligionForbids);
            original = AccessTools.Method(typeof(CompAssignableToPawn_Bed), name: "IdeoligionForbids");
            modified = nameof(Harmony_IdeoligionForbids.IdeoligionForbids_Postfix);
            harmony.Patch(original, postfix: new HarmonyMethod(patchType, modified));
			//Postfix to [RimWorld.Pawn_RelationsTracker.SecondaryRomanceChanceFactor]
			patchType = typeof(Harmony_SecondaryRomanceChanceFactor);
            original = AccessTools.Method(typeof(Pawn_RelationsTracker), name: "SecondaryRomanceChanceFactor");
            modified = nameof(Harmony_SecondaryRomanceChanceFactor.SecondaryRomanceChanceFactor_Postfix);
            harmony.Patch(original, postfix: new HarmonyMethod(patchType, modified));
			//Postfix to [RimWorld.CompAssignableToPawn_Bed.TryUnassignPawn]
			patchType = typeof(Harmony_TryUnassignPawn);
            original = AccessTools.Method(typeof(CompAssignableToPawn_Bed), name: "TryUnassignPawn");
            modified = nameof(Harmony_TryUnassignPawn.TryUnassignPawn_Postfix);
            harmony.Patch(original, postfix: new HarmonyMethod(patchType, modified));
        }
    }
}