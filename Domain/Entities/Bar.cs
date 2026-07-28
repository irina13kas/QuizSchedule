using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Bar : BaseEntity
    {
        public string Name { get; private set; }

        public string Address { get; private set; }

        public int? Capacity { get; private set; }
        public bool IsActive { get; private set; } = true;

        protected Bar() { }

        public Bar(string name, string address, int? capacity = default)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя обязательно к заполнению");

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Адрес обязательно к заполнению");

            if (capacity <= 0)
                throw new ArgumentException("Вместимость не может быть меньше нуля");
            Name = name;
            Address = address;
            Capacity = capacity;
        }

        public void UpdateInfo(string? name, string? address, int? capacity)
        {
            if (!string.IsNullOrWhiteSpace(name))
                Name = name;

            if (!string.IsNullOrWhiteSpace(address))
                Address = address;

            if(capacity.HasValue)
                Capacity = capacity.Value;
        }

        public void ChangeActivity()
        {
            IsActive = !IsActive;
        }
    }
}
