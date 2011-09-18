using System;
using System.Web.Mvc;

namespace SimpleCMS.Core.Models
{
    public class DataModel
    {
        [HiddenInput(DisplayValue = false)]
        public virtual int Id { get; set; }
        [HiddenInput(DisplayValue = false)]
        public virtual DateTime CreatedDate { get; set; }
        [HiddenInput(DisplayValue = false)]
        public virtual DateTime ModifiedDate { get; set; }

        public virtual void UpdateForSave(DateTime time)
        {
            ModifiedDate = time;
            if (Id == 0)
            {
                CreatedDate = time;
            }
        }

        public virtual bool Equals(DataModel other)
        {
            return (base.Equals(other) || Id == other.Id);
        }
    }
}