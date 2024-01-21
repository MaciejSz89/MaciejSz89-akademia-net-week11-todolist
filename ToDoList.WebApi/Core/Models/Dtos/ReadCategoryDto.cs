using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ToDoList.WebApi.Core.Models.Dtos
{
    public partial class ReadCategoryDto
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;

    }
}
