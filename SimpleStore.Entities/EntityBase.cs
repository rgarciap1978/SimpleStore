﻿namespace SimpleStore.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public bool Status { get; set; }

        public EntityBase()
        {
            Status = true;
        }
    }
}
