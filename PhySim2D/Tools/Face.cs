namespace PhySim2D.Tools
{
    internal struct Face
    {
        public KVector2 WStart;
        public KVector2 WEnd;
        public KVector2 WNormal;

        internal static KVector2 Direction(Face face)
        {
            return face.WEnd - face.WStart;
        }

        internal static double DistanceFaceToPoint(Face face, KVector2 p)
        {
            return face.WNormal * (p - face.WStart);
        }

        internal static double DistanceFaceToFace(Face f1, Face f2)
        {
            return f1.WNormal * (f2.WStart - f1.WStart);
        }
    }
}
