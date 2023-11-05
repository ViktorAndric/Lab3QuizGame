using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuizGame
{
    public class CategoryItem
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; } = false;

        public CategoryItem(string name, bool isSelected)
        {
            Name = name;
            IsSelected = isSelected;
        }
    }
}
