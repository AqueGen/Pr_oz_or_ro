using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Globalization;
using Newtonsoft.Json;

namespace Kapitalist.Web.Framework.Attributes
{
	/// <summary>
	/// Attribute for source property value validation if other property is valid
	/// </summary>
	/// 
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class CheckIfAttribute : ValidationAttribute, IClientValidatable
	{
		public ValidationAttribute MainRule { get; protected set; }
		public string OtherProperty { get; }
		public string OtherPropertyDisplayName { get; protected set; }
		public ValidationAttribute OtherRule { get; protected set; }
		public bool InvertOther { get; set; }

		public CheckIfAttribute(string otherProperty)
		{
			OtherProperty = otherProperty;
		}

		public CheckIfAttribute(string otherProperty, object value)
			: this(otherProperty)
		{
			OtherRule = new EqualsAttribute(value);
		}

		protected CheckIfAttribute(string otherProperty, ValidationAttribute otherRule)
			: this(null, otherProperty, otherRule)
		{
		}

		protected CheckIfAttribute(ValidationAttribute mainRule, string otherProperty, ValidationAttribute otherRule = null)
			: this(otherProperty)
		{
			MainRule = mainRule;
			OtherRule = otherRule;
		}

		void EnsureRules(Type modelType, string propertyName)
		{
			if (MainRule == null)
			{
				PropertyInfo propertyInfo = modelType.GetProperty(propertyName);
				var rules = propertyInfo.GetCustomAttributes<ValidationRuleAttribute>(true)
					.Select(a => a.ValidationRule).ToArray();
				switch (rules.Length)
				{
					case 0:
						MainRule = GetDefaultValidationAttribute(propertyInfo.PropertyType);
						break;
					case 1:
						MainRule = rules[0];
						break;
					default:
						MainRule = new MultipleValidation(rules);
						break;
				}
			}
			if (OtherRule == null)
			{
				OtherRule = GetDefaultValidationAttribute(modelType.GetProperty(OtherProperty)?.PropertyType);
			}
		}

		ValidationAttribute GetDefaultValidationAttribute(Type type)
		{
			return type == typeof(bool)
				? (ValidationAttribute) new EqualsAttribute(true)
				: new RequiredAttribute();
		}

		public override bool RequiresValidationContext {
			get {
				return true;
			}
		}

		public override string FormatErrorMessage(string name)
		{
			if (ErrorMessage == null && ErrorMessageResourceName == null && MainRule != null)
			{
				return MainRule.FormatErrorMessage(name);
			}
			return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, OtherPropertyDisplayName ?? OtherProperty);
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			EnsureRules(validationContext.ObjectType, validationContext.MemberName);
			var mainResult = MainRule.GetValidationResult(value, validationContext);
			if (mainResult == ValidationResult.Success)
			{
				return ValidationResult.Success;
			}
			PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
			if (otherPropertyInfo == null)
			{
				return new ValidationResult(String.Format(CultureInfo.CurrentCulture, "Could not find a property named {0}.", OtherProperty));
			}
			object otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
			validationContext.MemberName = OtherProperty;
			if ((OtherRule.GetValidationResult(otherPropertyValue, validationContext) == ValidationResult.Success) == InvertOther)
			{
				return ValidationResult.Success;
			}
			if (ErrorMessage == null && ErrorMessageResourceName == null)
			{
				return mainResult;
			}
			if (OtherPropertyDisplayName == null)
			{
				OtherPropertyDisplayName = ModelMetadataProviders.Current.GetMetadataForProperty(() => validationContext.ObjectInstance, validationContext.ObjectType, OtherProperty).GetDisplayName();
			}
			return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
		}

		public static string FormatPropertyForClientValidation(string property)
		{
			if (property == null)
			{
				throw new ArgumentException("Value cannot be null or empty.", "property");
			}
			return "*." + property;
		}

		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{
			EnsureRules(metadata.ContainerType, metadata.PropertyName);
			string messge = null;
			if (ErrorMessage != null || ErrorMessageResourceName != null)
			{
				if (metadata.ContainerType != null)
				{
					if (OtherPropertyDisplayName == null)
					{
						OtherPropertyDisplayName = ModelMetadataProviders.Current.GetMetadataForProperty(() => metadata.Model, metadata.ContainerType, OtherProperty).GetDisplayName();
					}
				}
				messge = FormatErrorMessage(metadata.GetDisplayName());
			}
			return new ValidatorProvider(metadata, context).GetClientValidationRules(
				MainRule,
				FormatPropertyForClientValidation(OtherProperty),
				OtherRule,
				InvertOther,
				messge);
		}

		public class MultipleValidation : ValidationAttribute, IClientValidatable
		{
			private IEnumerable<ValidationAttribute> Items { get; }

			public MultipleValidation(params ValidationAttribute[] items)
			{
				Items = items;
			}

			public override bool RequiresValidationContext {
				get {
					return true;
				}
			}

			public override string FormatErrorMessage(string name)
			{
				return null;
			}

			protected override ValidationResult IsValid(object value, ValidationContext validationContext)
			{
				foreach (var rule in Items)
				{
					ValidationResult result = rule.GetValidationResult(value, validationContext);
					if (result != ValidationResult.Success)
					{
						return result;
					}
				}
				return ValidationResult.Success;
			}

			public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
			{
				return new ValidatorProvider(metadata, context).GetClientValidationRules(Items);
			}
		}

		private class ValidatorProvider : DataAnnotationsModelValidatorProvider
		{
			private ControllerContext _context;
			private ModelMetadata _metadata;
			private static JsonSerializerSettings _serializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

			public ValidatorProvider(ModelMetadata metadata, ControllerContext context)
			{
				_metadata = metadata;
				_context = context;
			}

			public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ValidationAttribute mainRule, string otherProperty, ValidationAttribute otherRule, bool invertOther, string errorMessage)
			{
				var rule = new ModelClientValidationRule();
				rule.ValidationType = "checkif";
				IEnumerable<Rule> mainClientRules;
				var rules = GetClientValidationRules(new ValidationAttribute[] { mainRule }).ToArray();
				if (errorMessage == null)
				{
					mainClientRules = rules.Select(r => new Rule(r.ValidationType, r.ValidationParameters, r.ErrorMessage));
				}
				else
				{
					rule.ErrorMessage = errorMessage;
					mainClientRules = rules.Select(r => new Rule(r.ValidationType, r.ValidationParameters));
				}
				rule.ValidationParameters.Add("mainrules", JsonConvert.SerializeObject(mainClientRules, Formatting.None, _serializerSettings));
				rule.ValidationParameters.Add("other", otherProperty);
				var otherClientRules = GetClientValidationRules(new ValidationAttribute[] { otherRule })
					.Select(r => new Rule(r.ValidationType, r.ValidationParameters));
				rule.ValidationParameters.Add("otherrules", JsonConvert.SerializeObject(otherClientRules, Formatting.None, _serializerSettings));
				if (invertOther)
				{
					rule.ValidationParameters.Add("invertother", "true");
				}
				yield return rule;
			}

			public IEnumerable<ModelClientValidationRule> GetClientValidationRules(IEnumerable<ValidationAttribute> attributes)
			{
				return base.GetValidators(_metadata, _context, attributes).SelectMany(v => v.GetClientValidationRules());
			}

			private class Rule
			{
				public Rule()
				{
				}
				public Rule(string name, IDictionary<string, object> parameters, string message = null)
				{
					Name = name;
					if (parameters != null && parameters.Count > 0)
					{
						Params = parameters;
					}
					Message = message;
				}
				[JsonProperty("name")]
				public string Name { get; set; }
				[JsonProperty("params")]
				public IDictionary<string, object> Params { get; set; }
				[JsonProperty("message")]
				public string Message { get; set; }
			}
		}
	}

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
	public class ValidationRuleAttribute : Attribute
	{
		public ValidationAttribute ValidationRule { get; }
		public ValidationRuleAttribute(Type type, object param)
			: this(type, new object[] { param })
		{
		}
		public ValidationRuleAttribute(Type type, object[] @params = null)
		{
			ValidationRule = (ValidationAttribute) Activator.CreateInstance(type, @params);
		}
		public string ErrorMessage {
			get {
				return ValidationRule.ErrorMessage;
			}
			set {
				ValidationRule.ErrorMessage = value;
			}
		}
		public string ErrorMessageResourceName {
			get {
				return ValidationRule.ErrorMessageResourceName;
			}
			set {
				ValidationRule.ErrorMessageResourceName = value;
			}
		}
		public Type ErrorMessageResourceType {
			get {
				return ValidationRule.ErrorMessageResourceType;
			}
			set {
				ValidationRule.ErrorMessageResourceType = value;
			}
		}
		object[] _otherProperties;
		public object[] OtherProperties {
			get {
				return _otherProperties;
			}
			set {
				_otherProperties = value;
				Type type = ValidationRule.GetType();
				for (int i = 0 ; i < value.Length ; ++i)
				{
					type.GetProperty((string) value[i]).SetValue(ValidationRule, value[++i]);
				}
			}
		}
	}
}