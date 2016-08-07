using System;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class GalleryPicPattern:BasePattern
    {
        public string Description { get; set; }
        public long Gallery_ID { get; set; }
    }
}