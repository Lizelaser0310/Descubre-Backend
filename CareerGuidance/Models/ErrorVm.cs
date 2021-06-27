using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CareerGuidance.Models
{
    public class ErrorMessageVm
    {
        public string Property { get; set; }
        public List<string> Errors { get; set; }
    }
    
    public class ErrorVm
    {
        public static ErrorVm Create(params string[] errors)
        {
            return new ()
            {
                Errors = new List<string>(errors)
            };
        }
        public void AddMessage<T>(Expression<Func<T>> property, string error )
        {
            Messages ??= new List<ErrorMessageVm>();
            var propertyName = GetPropertyName(property);

            var idx = Messages.FindIndex(x => x.Property == propertyName);
            if (idx>=0)
            {
                Messages[idx].Errors.Add(error);
            }
            else
            {
                Messages.Add(new ErrorMessageVm
                {
                    Property = GetPropertyName(property),
                    Errors = new List<string>{error}
                });            
            }
        }

        public void AddError(string error)
        {
            Errors ??= new List<string>();
            Errors.Add(error);
        }

        public bool IsEmpty()
        {
            return Errors == null && Messages == null;
        }

        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return ((MemberExpression) propertyExpression.Body).Member.Name;
        }
        
        public List<string> Errors { get; set; }
        public List<ErrorMessageVm> Messages { get; set; }
    }
}