using System;

namespace AppServiceDemo.Data.Entities.Base
{
    public class TimeStampedEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
