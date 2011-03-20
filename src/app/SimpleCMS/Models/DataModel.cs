using System;
using System.Web.Mvc;

namespace SimpleCMS.Models
{
    public class DataModel
    {
        [HiddenInput(DisplayValue = false)]
        public virtual int Id { get; set; }
        [HiddenInput(DisplayValue = false)]
        public virtual DateTime CreatedDate { get; set; }
        [HiddenInput(DisplayValue = false)]
        public virtual DateTime ModifiedDate { get; set; }

        public virtual void UpdateForCreate()
        {
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        public virtual void UpdateForEdit()
        {
            ModifiedDate = DateTime.Now;
        }

        public virtual void UpdateForSave()
        {
            if (Id == 0)
            {
                UpdateForCreate();
            }
            else
            {
                UpdateForEdit();
            }
        }

        public virtual bool Equals(DataModel other)
        {
            return (base.Equals(other) || Id == other.Id);
        }
    }
}