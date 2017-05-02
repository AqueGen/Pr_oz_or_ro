using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace Kapitalist.Web.Framework.Attributes
{
	/// <summary>
	/// Attribute for value validation (equals to defined value or not)
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class EqualsAttribute : ValidationAttribute, IClientValidatable
	{
		public object TargetValue { get; }
		public bool Invert { get; }

		public EqualsAttribute(bool value)
		{
			TargetValue = true;
			Invert = !value;
		}

		public EqualsAttribute(object value)
		{
			TargetValue = value;
			Invert = false;
		}

		public EqualsAttribute(object value, bool invert)
		{
			TargetValue = value;
			Invert = invert;
		}

		public override string FormatErrorMessage(string name)
		{
			return string.Format(ErrorMessageString, name, TargetValue);
		}

		public override bool IsValid(object value)
		{
			return object.Equals(value, TargetValue) != Invert;
		}

		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{
			var rule = new ModelClientValidationRule {
				ErrorMessage = FormatErrorMessage(metadata.DisplayName),
				ValidationType = "equals"
			};
		    if (TargetValue == null)
		    {
                rule.ValidationParameters.Add("value", null);
            }
		    else
		    {
                string value = TargetValue.ToString();
                rule.ValidationParameters.Add("value", TargetValue is bool ? value.ToLower() : value);
            }
			
			if (Invert)
			{
				rule.ValidationParameters.Add("invert", "true");
			}
			yield return rule;
		}
	}
}