using System;
using System.Windows.Forms;
using PhySim2D.UI.Views;
using PhySim2D.UI.Components;

namespace PhySim2D.UI
{
    public partial class PhysicVisualization : Form
    {
        public PhysicVisualization()
        {
            InitializeComponent();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            DebugScene.Start();
        }

        private void BtnStep_Click(object sender, EventArgs e)
        {
            DebugScene.Step();
        }

        private void DebugScene_TimeStep(object sender, TimeStepEventArgs e)
        {
            lblTime.Text = e.Step.ToString() + " ms";
        }


        private void PhysicVisualization_Load(object sender, EventArgs e)
        {
            var items = ChckListBxDebugFlags.Items;
            foreach (DebugViewFlags flags in Enum.GetValues(typeof(DebugViewFlags)))
            {
                items.Add(flags, (DebugScene.Flags & flags) == flags);
            }
        }

        private void ChckListBxDebugFlags_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
                DebugScene.Flags |= (DebugViewFlags) ChckListBxDebugFlags.Items[e.Index];
            else
                DebugScene.Flags &= ~(DebugViewFlags) ChckListBxDebugFlags.Items[e.Index];

            DebugScene.Invalidate();
        }
    }
}
