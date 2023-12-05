using System;

namespace EntityLayer.Entities
{
    public class Service
    {
        public int ServiceID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string MainClass { get; set; }
        public string SubClass { get; set; }
    }
}