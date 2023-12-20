using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace onlineCourses.Models.Attributes
{
    public class UniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DBContext context = new DBContext();
            
            var entity = context.Model.FindEntityType(validationContext.ObjectType).GetTableName();
            var property = validationContext.MemberName;
            var propertyValue = value.ToString();

            var result = context.Database.SqlQueryRaw<int>(
                    @"select count(*) as Value from " + entity + " where " + property + " = {0}", propertyValue
                ).First();

            if (result == 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"{property} Already Exists.");
            }
        }
    }
}
