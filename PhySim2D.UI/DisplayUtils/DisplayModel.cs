using System.Drawing.Drawing2D;

namespace PhySim2D.UI.DisplayUtils
{
    class DisplayModel
    {
        public Matrix MatMD { get; private set; }

        public static Matrix CenterAndResizeBasedOnWidth(float dimRealUnit, float heightPix, float widthPix)
        {
            float heightUnitReal = dimRealUnit * heightPix / widthPix;

            float xPixByUni = widthPix / dimRealUnit;
            float yPixByUni = heightPix / heightUnitReal;

            Matrix matMD = new Matrix();
            matMD.Scale(xPixByUni, -yPixByUni);
            matMD.Translate(dimRealUnit/2f, -heightUnitReal/2f);
            return matMD;
        }
    }
}
