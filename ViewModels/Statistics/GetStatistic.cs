using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCosts.ViewModels.Statistics
{
    public class GetStatistic : IValidatableObject
    {
        [Required(ErrorMessage = "Не выбрано начало периода")]
        [Display(Name = "Начало временного периода")]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "Не выбран конец периода")]
        [Display(Name = "Конец временного периода")]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        [BindNever]
        public decimal SumCosts { get; set; }

        [BindNever]
        public bool IsCompleteStatistic { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (End < Start)
            {
                yield return new ValidationResult("Дата начала периода не может быть больше, чем дата окончания");
            }
            if ((End - Start).TotalDays < 30)
            {
                yield return new ValidationResult("Между границами периода должно быть хотя бы 30 дней");
            }
        }
    }
}
