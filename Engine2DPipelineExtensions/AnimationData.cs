using System.Collections.Generic;

namespace Engine2D.PipelineExtensions
{
    class AnimationData
    {
        public int AnimationSpeed;
        public bool IsLooping;
        public List<AnimationFrameData> Frames;
    }

    class AnimationFrameData
    {
        public int X;
        public int Y;
        public int CellWidth;
        public int CellHeight;
    }
}
