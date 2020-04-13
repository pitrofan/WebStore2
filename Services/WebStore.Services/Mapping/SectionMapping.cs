﻿using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;

namespace WebStore.Infrastructure.Mapping
{
    public static class SectionMapping 
    {
        public static SectionDTO ToDTO(this Section section) => section is null ? null : new SectionDTO
        {
            Id = section.Id,
            Name = section.Name
        };

        public static Section FromDTO(this SectionDTO section) => section is null ? null : new Section
        {
            Id = section.Id,
            Name = section.Name
        };
    }
}
