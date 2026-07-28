using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Photographer : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public bool IsActive { get; private set; }

        protected Photographer() { }

        public Photographer(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя обязательно к заполнению");
            Name = name;
            IsActive = true;
        }

        public void UpdateInfo(string? name)
        {
            if (!string.IsNullOrWhiteSpace(name))
                Name = name;
        }

        public void ChangeActivity()
        {
            IsActive = !IsActive;
        }
    }
}
