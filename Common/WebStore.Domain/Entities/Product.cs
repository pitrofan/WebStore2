﻿using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    /// <summary>
    /// Товар
    /// </summary>
    public class Product : NamedEntity, IOrderedEntity
    {
        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Идентификатор секции
        /// </summary>
        public int SectionId { get; set; }

        /// <summary>
        /// Секция
        /// </summary>
        [ForeignKey(nameof(SectionId))]
        public virtual Section Section { get; set; }

        /// <summary>
        /// Идентификатор бренда
        /// </summary>
        public int? BrandId { get; set; }

        /// <summary>
        /// Бренд
        /// </summary>
        [ForeignKey(nameof(BrandId))]
        public virtual Brand Brand { get; set; }

        /// <summary>
        /// Адрес файла с изображением
        /// </summary>
        public string ImageUrl{ get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        [Column(TypeName ="decimal(18,2)")]// 18 знаков, из них 2 после запятой
        public decimal Price{ get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        [NotMapped]
        public int NotMappedProperty { get; set; } // Свойство не сохраняемое в БД
    }
}
