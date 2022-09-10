//Prefix to [RimWorld.CompAssignableToPawn.SortAssignedPawns]
using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace CoSpousalRelations {
public static class Harmony_SortAssignedPawns {
    // Modifies the "SortAssignedPawns" method for beds in order to minimise distance between pawns that actually want to sleep together
    public static bool SortAssignedPawns_Prefix(ref List<Pawn> ___assignedPawns, ref CompAssignableToPawn __instance) {
        // If this isn't being called by a bed, just do the normal sort
        if (__instance.GetType() != typeof(CompAssignableToPawn_Bed)) {
            return true;
        }
        // Make a list for eventually rearranging the pawns
        List<Pawn> pawnList = ___assignedPawns.ToList();
        int pawnCount = pawnList.Count();
        // Make a dictionary to keep track of the total distance from a pawn to its partners
        Dictionary<Pawn,int> sumPartnerDistance = new Dictionary<Pawn,int>();
        for (int i = 0; i < pawnCount; i++) {
            sumPartnerDistance.Add(pawnList[i],0);
        }
        List<Pawn> distSorted;
        int score = ScoreArrangement(pawnList);
        bool isSorted = false;
        while (!isSorted) {
            isSorted = true;
            for (int i = 0; i < pawnCount; i++) {
                sumPartnerDistance[pawnList[i]] = 0;
                for (int j = 0; j < i; j++) {
                    // Need to check Love Partner Relation Utility as well because Free Love Ideologies are willing to sleep with anyone
                    if(BedUtility.WillingToShareBed(pawnList[i], pawnList[j]) && LovePartnerRelationUtility.LovePartnerRelationExists(pawnList[i], pawnList[j])) {
                        sumPartnerDistance[pawnList[i]] += i - j;
                        sumPartnerDistance[pawnList[j]] += i - j;
                    }
                }
            }
            distSorted = pawnList.ToList();
            distSorted.SortBy((Pawn p) => sumPartnerDistance[p]);
            foreach (Pawn pawn in distSorted) {
                for (int i = 0; i < pawnCount; i++) {
                    pawnList.Remove(pawn);
                    pawnList.Insert(i, pawn);
                    if (ScoreArrangement(pawnList) < score) {
                        score = ScoreArrangement(pawnList);
                        ___assignedPawns = pawnList.ToList();
                        isSorted = false;
                    }
                }
                pawnList = ___assignedPawns.ToList();
            }
        }
        return false;
    }

    // Scores an arrangement of pawns based on the total distance between pawns willing to sleep with each other
    private static int ScoreArrangement(List<Pawn> pawnList) {
        // Make a quick lookup array 
        int score = 0;
        int pawnCount = pawnList.Count();
        for (int i = 0; i < pawnCount; i++) {
            for (int j = 0; j < i; j++) {
                if(BedUtility.WillingToShareBed(pawnList[i], pawnList[j]) && LovePartnerRelationUtility.LovePartnerRelationExists(pawnList[i], pawnList[j])) {
                    score += i - j;
                }
            }
        }
        return score;
    }
}
}