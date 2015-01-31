using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MyLibrary.Valid.ValidationModel
{
    public static class ValidationModel
    {
        public static ValidStatusResult Validate<T>(T model)
        {
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            IList<ValidationResult> validationResults = new List<ValidationResult>();

            var result = Validator.TryValidateObject(model, context, validationResults, validateAllProperties: true);

            return new ValidStatusResult() { IsSuccess = result, ErrorMessage = validationResults };
        }
    }


    public class ValidStatusResult
    {
        public bool IsSuccess { set; get; }
        public IList<ValidationResult> ErrorMessage { set; get; }
    }
}
