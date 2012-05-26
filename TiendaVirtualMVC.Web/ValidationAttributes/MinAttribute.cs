namespace TiendaVirtualMVC.Web.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class MinAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly double minValue;

        public MinAttribute(double minValue) :
            base("El {0} debe ser mayor o igual a {1}")
        {
            this.minValue = minValue;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, minValue);
        }

        public override bool IsValid(object value)
        {
            var doubleValue = Convert.ToDouble(value);
            return doubleValue >= minValue;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = this.FormatErrorMessage(metadata.PropertyName);
            rule.ValidationType = "min";
            rule.ValidationParameters.Add("value",minValue);
            yield return rule;
        }
    }
}