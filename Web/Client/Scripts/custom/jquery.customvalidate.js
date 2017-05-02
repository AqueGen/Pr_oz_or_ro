
function setValidationValues(options, ruleName, value) {
    options.rules[ruleName] = value;
    if (options.message) {
        options.messages[ruleName] = options.message;
    }
}

$.validator.unobtrusive.adapters.add("equals", ["value", "invert"], function (options) {
    setValidationValues(options, "equals", {
        value: options.params.value,
        invert: options.params.invert === "true"
    });
});

$.validator.addMethod("equals", function (value, element, params) {
    var isequal;
    if (params.value === "true") {
        if (value) {
            isequal = value.toString().toLowerCase() === params.value;
        }
        else {
            isequal = false;
        }
    }
    else {
        isequal = value == params.value
    }
    return isequal !== params.invert;
});

$.validator.unobtrusive.adapters.add("checkif", ["mainrules", "other", "otherrules", "invertother"], function (options) {
    var fieldName = options.element.name,
	prefix = fieldName.substr(0, fieldName.lastIndexOf(".") + 1),
	other = options.params.other;
    if (other.indexOf("*.") === 0) {
        other = other.replace("*.", prefix);
    }
    var other = other.replace(/([!"#$%&'()*+,./:;<=>?@\[\\\]^`{|}~])/g, "\\$1"),
	otherElement = $(options.form).find(":input").filter("[name='" + other + "']")[0];

    setValidationValues(options, "checkif", {
        mainrules: $.parseJSON(options.params.mainrules),
        other: otherElement,
        otherrules: $.parseJSON(options.params.otherrules),
        invertother: options.params.invertother === "true"
    });
});

$.validator.addMethod("checkif", function (value, element, params) {
    // bind to the blur event of the target in order to revalidate whenever the target field is updated
    // TODO find a way to bind the event just once, avoiding the unbind-rebind overhead
    if (this.settings.onfocusout) {
        var other = params.other,
			type = other.type,
			sufix = ".validate-checkif-" + element.name;
        if (type === "radio" || type === "checkbox") {
            this.findByName(other.name).off(sufix).on("click" + sufix, function () {
                $(element).valid();
            });
        }
        else {
            $(other).off(sufix).on("blur" + sufix, function () {
                $(element).valid();
            });
        }
    }
    var form = element.form;
    var rules = {};
    var messages = {};
    $.each(params.mainrules, function () {
        var name = this.name,
			paramValues = this.params,
			message = this.message;
        $.each($.validator.unobtrusive.adapters, function () {
            if (this.name === name) {
                this.adapt({
                    element: element,
                    form: form,
                    message: message,
                    params: paramValues,
                    rules: rules,
                    messages: messages
                });
            }
        });
    });
    var mainPassed = true;
    for (var rule in rules) {
        if (!$.validator.methods[rule].call(this, value, element, rules[rule])) {
            mainPassed = false;
            var message = messages[rule];
            if (message) {
                this.settings.messages[element.name]['checkif'] = message;
            }
            break;
        }
    }
    if (mainPassed) {
        return true;
    }

    var targetElement = this.validationTargetFor(params.other);
    rules = {};
    messages = {};
    $.each(params.otherrules, function () {
        var name = this.name;
        var paramValues = this.params;
        $.each($.validator.unobtrusive.adapters, function () {
            if (this.name === name) {
                this.adapt({
                    element: targetElement,
                    form: form,
                    message: "",
                    params: paramValues,
                    rules: rules,
                    messages: messages
                });
            }
        });
    });
    var targetValue = this.elementValue(targetElement);
    var otherPassed = true;
    for (var rule in rules) {
        if (!$.validator.methods[rule].call(this, targetValue, targetElement, rules[rule])) {
            otherPassed = false;
            break;
        }
    }
    return otherPassed === params.invertother;
});