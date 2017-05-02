using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Kapitalist.Web.Resources;
using Kapitalist.Web.Resources.Localizations.Validation;

namespace Kapitalist.Web.Framework.Attributes
{
	public class LocalizationProvider : DataAnnotationsModelMetadataProvider
	{
		protected override ModelMetadata CreateMetadata(
							 IEnumerable<Attribute> attributes,
							 Type containerType,
							 Func<object> modelAccessor,
							 Type modelType,
							 string propertyName)
		{
            return base.CreateMetadata(CorrectAttributes(attributes, propertyName),
				containerType, modelAccessor, modelType, propertyName);
		}

		static IEnumerable<Attribute> CorrectAttributes(IEnumerable<Attribute> attributes, string property)
		{
			DisplayAttribute displayAttribute = null;
			foreach (var attribute in attributes)
			{
				if (attribute != null)
				{
					if (attribute.GetType().Name.Equals("DisplayAttribute"))
					{
						CorrectDisplayAttribute(displayAttribute = (DisplayAttribute) attribute, property);
					}
					else if (attribute is ValidationAttribute)
					{
						CorrectValidationAttribute((ValidationAttribute) attribute);
					}
				}
				yield return attribute;
			}
			if (displayAttribute == null)
			{
				CorrectDisplayAttribute(displayAttribute = new DisplayAttribute(), property);
				yield return displayAttribute;
			}
		}

		static void CorrectDisplayAttribute(DisplayAttribute attribute, string property)
		{
            if (attribute.Name == null)
            {
                if (property == null)
                    return;
                else
                    attribute.Name = property;
            } 

			string localizedText = GlobalRes.ResourceManager.GetString(attribute.Name);
			if (localizedText != null)
				attribute.Name = localizedText;
		}

		static void CorrectValidationAttribute(ValidationAttribute attribute)
		{
			if (attribute.ErrorMessageResourceType == null)
			{
				string localizedText;
				if ((attribute.ErrorMessage != null &&
						(localizedText = GlobalRes.ResourceManager.GetString(attribute.ErrorMessage)) != null)
				|| (localizedText = GlobalRes.ResourceManager.GetString(attribute.GetType().Name)) != null)
					attribute.ErrorMessage = localizedText;
			}
		}

		public static void Register()
		{
			ModelMetadataProviders.Current = new LocalizationProvider();
			var validationMessages = typeof(Messages).FullName;
			DefaultModelBinder.ResourceClassKey = validationMessages;
			ClientDataTypeModelValidatorProvider.ResourceClassKey = validationMessages;
			ValidationExtensions.ResourceClassKey = validationMessages;

			// Переопреділення (локалізація) імпліцитного [Required] для типів значень, які не можуть бути null
			ModelValidatorProvider validatorProvider = ModelValidatorProviders.Providers
				.FirstOrDefault(p => p.GetType() == typeof(DataAnnotationsModelValidatorProvider));
			if (validatorProvider != null)
			{
				ModelValidatorProviders.Providers.Remove(validatorProvider);
				ModelValidatorProviders.Providers.Add(new LocalizedValidatorProvider());
			}
		}

		public class LocalizedValidatorProvider : DataAnnotationsModelValidatorProvider
		{
			protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
			{
				// Add an implied [Required] attribute for any non-nullable value type,
				// unless they've configured us not to do that.
				if (AddImplicitRequiredAttributeForValueTypes &&
					metadata.IsRequired &&
					!attributes.Any(a => a is RequiredAttribute))
				{
					RequiredAttribute requiredAttribute = new RequiredAttribute();
					CorrectValidationAttribute(requiredAttribute);
					attributes = attributes.Concat(new[] { requiredAttribute });
				}
				return base.GetValidators(metadata, context, attributes);
			}
		}
	}
}
