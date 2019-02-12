using PhySim2D.Sim;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using PhySim2D.UI.DisplayUtils;
using PhySim2D.Collision.Colliders;
using PhySim2D.UI.Views;
using System.Collections.Generic;
using PhySim2D.Collision;
using PhySim2D.Tools;
using PhySim2D.Dynamics;

namespace PhySim2D.UI.Components
{
    internal struct ColorState
    {
        public readonly static Color INACTIVE = Color.FromArgb(100, 72, 1, 188);
        public readonly static Color SLEEP = Color.FromArgb(100, 1, 138, 188);
        public readonly static Color AWAKE = Color.FromArgb(100, 10, 188, 1);
        public readonly static Color AABB = Color.FromArgb(100, 206, 61, 255);
        public readonly static Color CENTER_OF_MASS = Color.FromArgb(200, 91, 1, 12);
        public readonly static Color FORCE = Color.FromArgb(200, 91, 1, 12);
    }

    public class TimeStepEventArgs : EventArgs
    {
        public float Step { get; set; }

        public TimeStepEventArgs(float step)
        {
            this.Step = step;
        }
    }

    public partial class PhysDebugViz : Control
    {
        private Scene scene = new Scene();
        private float step = 0.01f;
        private float time = 0;
        private bool isRunning = false;

        internal DebugViewFlags Flags { get; set; }

        private List<Contact> list;

        public event EventHandler<TimeStepEventArgs> TimeStep;

        public PhysDebugViz()
        {
            InitializeComponent();
            scene.ContactsInCollision += Scene_ContactsInCollision;

        }

        private void Scene_ContactsInCollision(object sender, ContactInCollisionEventArgs e)
        {
            list = e.Contacts;
        }

        public void Run()
        {
            while (isRunning)
            {

                scene.DoStep(step);
                time += step;
                this.Invalidate();

                try
                {
                    Thread.Sleep(20);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        public void Start()
        {
            if (!isRunning)
            {
                isRunning = true;
                Thread t = new Thread(new ThreadStart(Run));
                t.Start();
            }
        }

        public void Step()
        {
            if (isRunning)
            {
                isRunning = false;
                Stop();
            }
            else
            {
                scene.DoStep(step);
                time += step;
                this.Invalidate();
            }
        }

        public void Stop()
        {
            isRunning = false;
        }

        protected void OnTimeStep(object sender, TimeStepEventArgs e)
        {
            TimeStep?.Invoke(sender, e);
        }

        #region Display
        protected override void OnPaint(PaintEventArgs pe)
        {
            OnTimeStep(this, new TimeStepEventArgs(time));
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            g.Transform = DisplayModel.CenterAndResizeBasedOnWidth(50, this.Height, this.Width);

            DrawDebug(g);
        }

        public void DrawDebug(Graphics g)
        {

            Segment axisX = new Segment(new KVector2(-10, 0), new KVector2(10, 0));
            Segment axisY = new Segment(new KVector2(0, -10), new KVector2(0, 10));
            axisX.Transform = new KTransform();
            axisY.Transform = new KTransform();

            DrawSegment(g, axisX, Color.Beige, 0.1f);
            DrawSegment(g, axisY, Color.Beige, 0.1f);

            foreach (Rigidbody b in scene.Bodies)
            {

                foreach (Collider s in b.Colliders)
                {
                    if ((Flags & DebugViewFlags.SHAPE) == DebugViewFlags.SHAPE)
                    {
                            DrawShape(g, s, ColorState.SLEEP);
                    }

                    if ((Flags & DebugViewFlags.AABB) == DebugViewFlags.AABB)
                    {
                        DrawAABB(g, s, ColorState.AABB);
                    }

                    if ((Flags & DebugViewFlags.CENTER_OF_MASS) == DebugViewFlags.CENTER_OF_MASS)
                    {
                        KVector2 txPosition = b.State.Transform.TransformPointLW(b.MassData.CenterOfMass) - new KVector2(0.5f, 0.5f);
                        g.FillEllipse(new SolidBrush(ColorState.CENTER_OF_MASS), new RectangleF((float) txPosition.X, (float) txPosition.Y, 1, 1));
                    }

                    if ((Flags & DebugViewFlags.POSITION) == DebugViewFlags.POSITION)
                    {
                        KVector2 txPosition = b.State.Transform.Position - new KVector2(0.5f, 0.5f);
                        g.FillEllipse(new SolidBrush(ColorState.CENTER_OF_MASS), new RectangleF((float)txPosition.X, (float)txPosition.Y, 1, 1));
                    }
                }
            }

            if ((Flags & DebugViewFlags.FORCES) == DebugViewFlags.FORCES)
            {
              
            }

            if ((Flags & DebugViewFlags.CONTACTS_POINT) == DebugViewFlags.CONTACTS_POINT)
            {
                if (list != null)
                    foreach (Contact c in list)
                    {


                        Collider colA = c.FixtureA.Collider;
                        Collider colB = c.FixtureB.Collider;

                        DrawShape(g, colB, Color.Yellow);
                        DrawShape(g, colA, Color.AntiqueWhite);
                        

                        for (int i = 0; i < c.Manifold.Count; i++)
                        {
                            DrawPoint(g, c.Manifold.ContactPoints[i].WPosition, Color.Red);
                        }
                    }
            }

        }

        private void DrawPoint(Graphics g, KVector2 position, Color c)
        {
            KVector2 offset = new KVector2(0.2f, 0.2f);
            g.FillEllipse(new SolidBrush(c), (float) (position - offset).X, (float) (position - offset).Y, (float)(2 * offset).X, (float)  (2 * offset).Y);
        }

        private void DrawVector(Graphics g, KVector2 vector, KVector2 position, Color c)
        {
            g.DrawLine(new Pen(c, 0.1f),(float) position.X, (float) position.Y, (float)(position + vector).X, (float)(position + vector).Y);

        }

        private void DrawShape(Graphics g, Collider s, Color c)
        {
            switch (s.Type)
            {
                case ColliderType.CIRCLE:
                    DrawCircle(g, (Circle)s, c);
                    break;
                case ColliderType.POLYGON:
                    DrawPolygon(g, (Polygon)s, c);
                    break;
                case ColliderType.SEGMENT:
                    DrawSegment(g, (Segment)s, c, 0.2f);
                    break;
                default:
                    break;

            }
        }

        private void DrawAABB(Graphics g, Collider s, Color c)
        {
            AABB aabb = s.ComputeAABB();
            g.DrawRectangle(new Pen(c, 0.1f),(float) aabb.Min.X, (float) aabb.Min.Y, (float) (aabb.Max.X - aabb.Min.X), (float) (aabb.Max.Y - aabb.Min.Y));
        }

        private void DrawSegment(Graphics g, Segment seg, Color c, float thickness)
        {
            KVector2 start = seg.Transform.TransformPointLW(seg.LStart);
            KVector2 end = seg.Transform.TransformPointLW(seg.RetrievePoint(1));
            g.DrawLine(new Pen(c, thickness), (float) start.X,(float) start.Y,(float) end.X,(float) end.Y);
        }

        private void DrawPolygon(Graphics g, Polygon pol, Color c)
        {
            PointF[] points = new PointF[pol.Vertices.Count];
            for (int i = 0; i < pol.Vertices.Count; i++)
            {
                KVector2 tempV = pol.Transform.TransformPointLW(pol.Vertices[i]);
                points[i] = new PointF((float) tempV.X,(float) tempV.Y);
            }
            g.FillPolygon(new SolidBrush(c), points);
        }

        private void DrawCircle(Graphics g, Circle c, Color color)
        {
            g.FillEllipse(new SolidBrush(color), (RectangleF)c.ComputeAABB());
        }
        #endregion
    }
}