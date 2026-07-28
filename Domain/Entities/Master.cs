using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Master : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public bool IsActive { get; private set; } = true;

        protected Master() { }

        public Master(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя обязательно к заполнению");
            Name = name;
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
