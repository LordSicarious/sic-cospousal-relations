// CoSpousalRelations.PawnRelationWorker_CoSpouse
using RimWorld;
using Verse;
using HarmonyLib;

namespace CoSpousalRelations {
public class PawnRelationWorker_CoSpouse:PawnRelationWorker {
	public override bool InRelation(Pawn me, Pawn other) {
		if (me == other)
		{
			return false;
		}
		if (other.GetSpouseCount(includeDead: true) == 0)
		{
			return false;
		}
		PawnRelationWorker worker = PawnRelationDefOf.Spouse.Worker;
		// Not considered Co-Spouses if you are direct Spouses
		if (worker.InRelation(me, other))
		{
			return false;
		}
		// Considered Co-Spouses if you share a living Spouse
		foreach (Pawn spouse in other.GetSpouses(includeDead: false))
		{
			if (worker.InRelation(me, spouse))
			{
				return true;
			}
		}
		return false;
	}
}
}