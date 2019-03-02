using System;
using System.Runtime.Serialization;

namespace PhySim2D.Tools
{
    [DataContract]
    internal class KTransform : ICloneable, IEquatable<KTransform>
    {
        //Local Matrix to World
        [DataMember]
        public KMatrix3x3Opti MatLW { get; set; }

        //World Matrix to Local
        [DataMember]
        public KMatrix3x3Opti MatWL { get; set; }

        //TODO: Bug: Il faut que l objet tourne autour de son centre de masse au lieu de la position

        #region Getters and setters
        [DataMember]
        public KVector2 Position { get; set; }
        [DataMember]
        public double Rotation { get; set; }
        [DataMember]
        public KVector2 Scale { get; set; }
        #endregion

        public KTransform() : this(KVector2.Zero,0, KVector2.One) { }

        public KTransform(KVector2 pos, double rot, KVector2 scale)
        {
            Position = pos;
            Rotation = rot;
            Scale = scale;
            SyncMatrix();
        }

        public KVector2 TransformPointLW(KVector2 lPoint)
        {
            return KVector2.TransformPoint(lPoint, MatLW);
        }

        public KVector2 TransformDirLW(KVector2 lDir)
        {
            return KVector2.TransformDir(lDir, MatLW);
        }

        public KVector2 TransformNormalLW(KVector2 lDir)
        {
            return KVector2.TransformNormal(lDir, MatLW);
        }

        public KVector2 TransformVectorLW(KVector2 lDir)
        {
            KVector2 result = new KVector2(MatLW.A11 * lDir.X * 1/Scale.X+ MatLW.A12 * lDir.Y, MatLW.A21 * lDir.X + MatLW.A22 * lDir.Y * 1/Scale.Y);
            return result;
        }

        public KVector2 TransformVectorWL(KVector2 lDir)
        {
            KVector2 result = new KVector2(MatWL.A11 * lDir.X * Scale.X + MatWL.A12 * lDir.Y * -Scale.Y, MatWL.A21 * lDir.X * Scale.X+ MatWL.A22 * lDir.Y * Scale.Y);
            return result;
        }

        public KVector2 TransformPointWL(KVector2 wPoint)
        {
            return KVector2.TransformPoint(wPoint, MatWL );
        }

        public KVector2 TransformDirWL(KVector2 wDir)
        {
            return KVector2.TransformDir(wDir, MatWL);
        }

        public KVector2 TransformNormalWL(KVector2 lDir)
        {
            return KVector2.TransformNormal(lDir, MatWL);
        }

        public void SyncMatrix()
        {
            ComputeWorldToLocal(this, out KMatrix3x3Opti MatWL);
            ComputeLocalToWorld(this, out KMatrix3x3Opti MatLW);
            this.MatLW = MatLW;
            this.MatWL = MatWL;
        }

        #region Interfaces Methods

        public bool Equals(KTransform other)
        {
            return (Position.Equals(other.Position) && Rotation.Equals(other.Rotation) && Scale.Equals(other.Scale));
        }

        public object Clone()
        {
            return new KTransform(Position, Rotation, Scale);
        }
        #endregion 

        #region Object Methods
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            KTransform other = (KTransform)obj;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode() + Rotation.GetHashCode() + Scale.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("< Pos : {0} ; Rot: {1} ; Sca : {2} >",Position,Rotation,Scale);
        }
        #endregion

        #region Static Methods

        public static void ComputeLocalToWorld(KTransform t, out KMatrix3x3Opti mat)
        {
            ComputeTrRotSc(t.Position, -t.Rotation, t.Scale, out mat);
        }

        public static void ComputeWorldToLocal(KTransform t, out KMatrix3x3Opti mat)
        {
            ComputeScRotTr(new KVector2(1 / t.Scale.X, 1 / t.Scale.Y), t.Rotation, -t.Position, out mat);
        }

        public static void ComputeTrRotSc(KVector2 translation, double rotation, KVector2 scale, out KMatrix3x3Opti mat)
        {
            mat = KMatrix3x3Opti.CreateTranslation(translation)
                * KMatrix3x3Opti.CreateRotation(rotation)
                * KMatrix3x3Opti.CreateScale(scale);
        }

        public static void ComputeScRotTr(KVector2 scale, double rotation, KVector2 translation, out KMatrix3x3Opti mat)
        {
            mat = KMatrix3x3Opti.CreateScale(scale)
                * KMatrix3x3Opti.CreateRotation(rotation)
                * KMatrix3x3Opti.CreateTranslation(translation);
        }

        #endregion
    }
}
