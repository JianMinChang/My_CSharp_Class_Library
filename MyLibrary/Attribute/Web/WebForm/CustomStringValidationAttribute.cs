﻿using System;
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
    public class CustomStringValidationAttribute : ValidationAttribute
    {
        private readonly bool isRequired;

        public string Regex { get; set; }

        public int StringMaxLength { get; set; }

        public int StringMinLength { get; set; }

        public string RequiredErrorString { set; get; }
        public string RegexErrorString { set; get; }

        public string StringLengthMaxErrorString { set; get; }

        public string StringLengthMinErrorString { set; get; }

        public string StringLengthNotInMaxAndMinErrorString { set; get; }

        public CustomStringValidationAttribute(string Description, bool isRequired)
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
            var composedAttributes = ConstructAttributes().ToArray();
            if (composedAttributes.Length == 0) return ValidationResult.Success;

            var errorMsgBuilder = new StringBuilder();

            foreach (var attribute in composedAttributes)
            {
                if (attribute.ToString().IndexOf("StringLengthAttribute", 0) > 0)
                {
                    var valRes = attribute.GetValidationResult(value, validationContext);
                    if (valRes != null && !string.IsNullOrWhiteSpace(valRes.ErrorMessage) && errorMsgBuilder.Length == 0)
                    {
                        if (value.ToString().Length > StringMaxLength && value.ToString().Length < StringMinLength)
                        {
                            errorMsgBuilder.AppendLine(FormatErrorMessage(StringLengthNotInMaxAndMinErrorString));
                        }
                        else
                        {
                            if (value.ToString().Length > StringMaxLength)
                            {
                                errorMsgBuilder.AppendLine(FormatErrorMessage(StringLengthMaxErrorString));
                            }
                            if (value.ToString().Length < StringMinLength)
                            {
                                errorMsgBuilder.AppendLine(FormatErrorMessage(StringLengthMinErrorString));
                            }
                        }
                    }
                }
                else
                {
                    var valRes = attribute.GetValidationResult(value, validationContext);
                    if (valRes != null && !string.IsNullOrWhiteSpace(valRes.ErrorMessage) && errorMsgBuilder.Length == 0)
                    {
                        errorMsgBuilder.AppendLine(FormatErrorMessage(attribute.ErrorMessage));
                    }
                }

            }
            var msg = errorMsgBuilder.ToString();
            if (string.IsNullOrWhiteSpace(msg))
                return ValidationResult.Success;
            return new ValidationResult(msg);
        }

        private IEnumerable<ValidationAttribute> ConstructAttributes()
        {
            if (isRequired)
                yield return new RequiredAttribute() { AllowEmptyStrings = isRequired, ErrorMessage = RequiredErrorString };
            if (Regex != null)
                yield return new RegularExpressionAttribute(Regex) { ErrorMessage = RegexErrorString };
            if (!(StringMaxLength <= 0 && StringMinLength <= 0))
                yield return new StringLengthAttribute(StringMaxLength) { MinimumLength = StringMinLength };

        }
    }
}