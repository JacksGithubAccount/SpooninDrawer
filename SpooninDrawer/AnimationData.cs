using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer
{
    public class AnimationData
    {
        public int AnimationSpeed;
        public bool IsLooping;
        public List<AnimationFrameData> Frames;
    }
    public class AnimationFrameData
    {
        public int X;
        public int Y;
        public int CellWidth;
        public int CellHeight;
    }
}
