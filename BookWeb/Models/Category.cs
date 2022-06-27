using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Имя")]
        public string? Name { get; set; }
        [DisplayName("Количество целей")]
        [Range(1,100,ErrorMessage ="Порядок отображения должен находится в диапозоне от 1 до 100")]
        public int CountObject { get; set; }
        [DisplayName("Текущая цель")]
        public int DisplayOrder { get; set; }
        [DisplayName("Прогресс")]
        public string? Progress { get; set; }
        [DisplayName("Дата создания")]
        public DateTime CreatedDataTime { get; set; } = DateTime.Now; 
    }
}
