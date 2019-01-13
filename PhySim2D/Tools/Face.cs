namespace PhySim2D.Tools
{
    internal struct Face
    {
        public KVector2 WPStart;
        public KVector2 WPEnd;
        public KVector2 WNormal;

        internal static KVector2 Direction(Face face)
        {
            return face.WPEnd - face.WPStart;
        }

        internal static double DistanceFaceToPoint(Face face, KVector2 p)
        {
            return face.WNormal * (p - face.WPStart);
        }

        internal static double DistanceFaceToFace(Face f1, Face f2)
        {
            return f1.WNormal * (f2.WPStart - f1.WPStart);
        }
    }
}
