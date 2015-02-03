using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyLibrary.Attribute.Web.WebForm
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class CustomDateTimeValidationAttribute : ValidationAttribute
    {
        private readonly bool isRequired;

        public string RequiredErrorString { set; get; }
        public string RegexErrorString { set; get; }


        public CustomDateTimeValidationAttribute(string Description, bool isRequired)
        {
            this.isRequired = isRequired;
            this.Description = Description;
        }

        private readonly string Description;

        public override string FormatErrorMessage(string name)
        {
            return string.Format(name, this.Description);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = new ValidationResult(string.Empty);

            if (isRequired)
            {
                if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                {
                    result = new ValidationResult(FormatErrorMessage(RequiredErrorString));
                }
                else
                {
                    result = ValidDate(value.ToString()) == true && DateTime.Parse(value.ToString()) != DateTime.MinValue ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(RegexErrorString));
                }

            }
            else
            {
                if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                {
                    result = ValidDate(value.ToString()) == true && DateTime.Parse(value.ToString()) != DateTime.MinValue ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(RegexErrorString));
                }
                else
                {
                    result = ValidationResult.Success;
                }

            }

            return result;
        }


        private bool ValidDate(string date)
        {
            DateTime tmp = new DateTime();
            if (DateTime.TryParse(date, out tmp))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public static DateTime? ConvertStrToDateTime(string Str)
        {
            DateTime tmp = new DateTime();
            if (!string.IsNullOrWhiteSpace(Str))
            {
                if (DateTime.TryParse(Str, out tmp))
                {
                    return tmp;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
            else
            {
                return null;
            }
        }
    }

}
