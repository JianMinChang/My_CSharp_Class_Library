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
    /// <summary>
    /// 自定驗正數字的屬性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class CustomNumberRangeValidationAttribute : ValidationAttribute
    {
        private readonly bool isRequired;

        public int intMinNumber { get; set; }
        public int intMaxNumber { get; set; }



        public string RequiredErrorString { set; get; }
        public string RegexErrorString { set; get; }

        public string NumberMaxErrorString { set; get; }

        public string NumberMinErrorString { set; get; }

        public string NumberNotInMaxAndMinErrorString { set; get; }

        public CustomNumberRangeValidationAttribute(string Description, bool isRequired)
        {
            this.isRequired = isRequired;
            this.Description = Description;
        }

        private readonly string Description;

        public override string FormatErrorMessage(string name)
        {
            return string.Format(name, this.Description, this.intMaxNumber, this.intMinNumber);
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
                    result = ValidInt(value.ToString()) == true && int.Parse(value.ToString()) != int.MinValue ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(RegexErrorString));

                    if (result == null || string.IsNullOrWhiteSpace(result.ErrorMessage))
                    {//繼續驗證Range
                        result = VangeRagne(value);
                    }
                }

            }
            else
            {
                if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                {
                    result = ValidInt(value.ToString()) == true && int.Parse(value.ToString()) != int.MinValue ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(RegexErrorString));

                    if (result == null || string.IsNullOrWhiteSpace(result.ErrorMessage))
                    {//繼續驗證Range
                        result = VangeRagne(value);
                    }
                }
                else
                {
                    result = ValidationResult.Success;
                }

            }

            return result;
        }

        private ValidationResult VangeRagne(object value)
        {

            if (isRequired == false && value == null)
                return ValidationResult.Success;

            int tmp = int.Parse(value.ToString());
            string Err = string.Empty;
            if ((tmp <= intMaxNumber && tmp >= intMinNumber))
            {
                return ValidationResult.Success;
            }
            else
            {
                if (intMaxNumber > 0 && tmp > intMaxNumber)
                {
                    return new ValidationResult(FormatErrorMessage(NumberMaxErrorString));
                }
                if (intMinNumber > 0 && tmp < intMinNumber)
                {
                    return new ValidationResult(FormatErrorMessage(NumberMinErrorString));
                }

                return new ValidationResult(FormatErrorMessage(NumberNotInMaxAndMinErrorString));
            }
        }



        private bool ValidInt(string strint)
        {
            int tmp = 0;
            if (int.TryParse(strint, out tmp))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public static int? ConvertStrToInt(string Str)
        {
            int tmp = 0;
            if (!string.IsNullOrWhiteSpace(Str))
            {
                if (int.TryParse(Str, out tmp))
                {
                    return tmp;
                }
                else
                {
                    return int.MinValue;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
