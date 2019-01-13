using PhySim2D.Tools;

namespace PhySim2D.Sim
{
    public static class Config
    {
        internal static KVector2 DefaultNormal { get; } = new KVector2(0, 1);

        internal const int MaxManifoldPoints = 2;

        public const float EpsilonsFloat = 1.192092896E-05f;

        internal const double EpsilonsDouble = 1.2204460492503131E-016;

        internal const float PositionalCorrPercent = 1f;

        internal const float ValidAreaPolygon = 1f;

    }
}
