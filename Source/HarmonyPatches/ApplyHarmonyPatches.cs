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
			patchType = typeof(Prefix_SortAssignedPawns);
            original = AccessTools.Method(typeof(CompAssignableToPawn), name: "SortAssignedPawns");
            modified = nameof(Prefix_SortAssignedPawns.Patch);
            harmony.Patch(original, prefix: new HarmonyMethod(patchType, modified));

			//Postfix to [RimWorld.LovePartnerRelationUtility.GetMostDislikedNonPartnerBedOwner]
            patchType = typeof(Postfix_GetMostDislikedNonPartnerBedOwner);
            original = AccessTools.Method(typeof(LovePartnerRelationUtility), name: "GetMostDislikedNonPartnerBedOwner");
            modified = nameof(Postfix_GetMostDislikedNonPartnerBedOwner.Patch);
            harmony.Patch(original, postfix: new HarmonyMethod(patchType, modified));
			//Postfix to [RimWorld.RimWorld.CompAssignableToPawn_Bed.IdeoligionForbids]
			patchType = typeof(Postfix_IdeoligionForbids);
            original = AccessTools.Method(typeof(CompAssignableToPawn_Bed), name: "IdeoligionForbids");
            modified = nameof(Postfix_IdeoligionForbids.Patch);
            harmony.Patch(original, postfix: new HarmonyMethod(patchType, modified));
			//Postfix to [RimWorld.Pawn_RelationsTracker.SecondaryLovinChanceFactor]
			patchType = typeof(Postfix_SecondaryLovinChanceFactor);
            original = AccessTools.Method(typeof(Pawn_RelationsTracker), name: "SecondaryLovinChanceFactor");
            modified = nameof(Postfix_SecondaryLovinChanceFactor.Patch);
            harmony.Patch(original, postfix: new HarmonyMethod(patchType, modified));
			//Postfix to [RimWorld.Pawn_RelationsTracker.SecondaryRomanceChanceFactor]
			patchType = typeof(Postfix_SecondaryRomanceChanceFactor);
            original = AccessTools.Method(typeof(Pawn_RelationsTracker), name: "SecondaryRomanceChanceFactor");
            modified = nameof(Postfix_SecondaryRomanceChanceFactor.Patch);
            harmony.Patch(original, postfix: new HarmonyMethod(patchType, modified));
			//Postfix to [RimWorld.CompAssignableToPawn_Bed.TryUnassignPawn]
			patchType = typeof(Postfix_TryUnassignPawn);
            original = AccessTools.Method(typeof(CompAssignableToPawn_Bed), name: "TryUnassignPawn");
            modified = nameof(Postfix_TryUnassignPawn.Patch);
            harmony.Patch(original, postfix: new HarmonyMethod(patchType, modified));
        }
    }
}