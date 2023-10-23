namespace XMLContent.Data
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