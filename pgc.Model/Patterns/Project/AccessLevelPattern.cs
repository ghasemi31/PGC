using System;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class AccessLevelPattern:BasePattern
    {
        public string Title { get; set; }
        public Enums.Role Role { get; set; }
    }
}