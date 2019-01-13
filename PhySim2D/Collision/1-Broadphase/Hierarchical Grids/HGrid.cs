using PhySim2D.Collision.Broadphase.Hierarchical_Grids;
using PhySim2D.Collision.Colliders;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PhySim2D.Collision.Hierarchical_Grids
{
    internal class HGrid
    {
        public const int MAX_LEVEL = 32;

        Int32 OccupiedLevelMask { get; set; }
        Dictionary<int,LinkedList<HCell>> Cells { get; set; }
        int CurrentTick { get; set; }

        public HGrid()
        {
            for(int i = 0; i < MAX_LEVEL; i++)
            {
                Cells.Add(i, new LinkedList<HCell>());
            }
        }

        public void Detect()
        {

        }

        public void Update()
        {
            Dictionary<int, LinkedList<HCell>> copy = new Dictionary<int, LinkedList<HCell>>(Cells);
            Cells.Clear();

            foreach(KeyValuePair<int,LinkedList<HCell>> entry in copy)
                foreach(HCell cell in entry.Value)
                    this.Add(cell);

        }

        public void Add(HCell cell)
        {
            int level;
            AABB aabb = cell.Collider.ComputeAABB();
            float size = 10, width =(float) (aabb.Max.X - aabb.Min.X);

            for (level = 0; size * 1 < width; level++)
                size *= 2;

            Debug.Assert(level >= MAX_LEVEL,"The collider is too big. It cannot be contain in this hgrid");

            if (Cells.TryGetValue(level, out LinkedList<HCell> value))
            {
                value.AddLast(cell);
                Cells.Add(level, value);
                OccupiedLevelMask |= (1 << level);
            }

        }

        public void Remove(HCell cell)
        {
            foreach (KeyValuePair<int, LinkedList<HCell>> entry in Cells)
                if (entry.Value.Remove(cell))
                    break;
        }
    }
}
