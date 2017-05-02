function ToPercentage(totalValueTarget, minimalStepValueTarget, minimalStepPercentTarget) {
    var value = commaToPoint($(totalValueTarget).val());
    var minimalStepValue = commaToPoint($(minimalStepValueTarget).val());
    var minimalStepPercent = minimalStepValue / (value / 100);
    $(minimalStepPercentTarget).data('kendoNumericTextBox').value(minimalStepPercent);
};

function ToValue(totalValueTarget, minimalStepValueTarget, minimalStepPercentTarget) {
    var value = commaToPoint($(totalValueTarget).val());
    var minimalStepPercent = commaToPoint($(minimalStepPercentTarget).val());
    var minimalStepValue = (value / 100) * minimalStepPercent;
    $(minimalStepValueTarget).data('kendoNumericTextBox').value(minimalStepValue);
};

function commaToPoint(data) {
    return data.replace(',', '.');
}