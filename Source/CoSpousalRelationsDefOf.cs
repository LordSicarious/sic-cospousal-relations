// CoSpousalRelations.CoSpousalRelationsDefOf
using RimWorld;
using Verse;

namespace CoSpousalRelations {
    [DefOf]
    public static class CoSpousalRelationsDefOf {
        public static PawnRelationDef CoSpouse;
        static CoSpousalRelationsDefOf() {
		    DefOfHelper.EnsureInitializedInCtor(typeof(CoSpousalRelationsDefOf));
	    }
    }
}