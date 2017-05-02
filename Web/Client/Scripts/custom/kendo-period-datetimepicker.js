//DateTime period

//targetElement- will change (min/max) date for this element. Date will be take from sourceElements.

//$('target').data("kendoDateTimePicker")
//           .bind('change',
//               function () {
//                   minDate(targetElement, [sourceElements]);
//                   maxDate(targetElement, [sourceElements]);
//               });

function minDate(target, sources) {
    var maximalMinDateValue;
    for (var i = 0; i < sources.length; i++) {
        var sourceTarget = $(sources[i]);
        var sourceValue = sourceTarget.data("kendoDateTimePicker").value();
        if (sourceValue) {
            if (maximalMinDateValue) {
                if (maximalMinDateValue < sourceValue) {
                    maximalMinDateValue = sourceValue;
                }
            } else {
                maximalMinDateValue = sourceValue;
            }
        }
    }
    if (maximalMinDateValue) {
        var minValue = getDateValue(maximalMinDateValue);
        var datetimepicker = $(target).data("kendoDateTimePicker");
        datetimepicker.min(minValue);
    }
}

function maxDate(target, sources) {
    var minimalMaxDateValue;
    for (var i = 0; i < sources.length; i++) {
        var sourceTarget = $(sources[i]);
        var sourceValue = sourceTarget.data("kendoDateTimePicker").value();
        if (sourceValue) {
            if (minimalMaxDateValue) {
                if (minimalMaxDateValue > sourceValue) {
                    minimalMaxDateValue = sourceValue;
                }
            } else {
                minimalMaxDateValue = sourceValue;
            }
        }
    }
    if (minimalMaxDateValue) {
        var maxValue = getDateValue(minimalMaxDateValue);
        var datetimepicker = $(target).data("kendoDateTimePicker");
        datetimepicker.max(maxValue);
    }
}

function getDateValue(element) {
    if (element) {
        var value = new Date(element);
        value.setDate(value.getDate());
        value.setTime(value.getTime());
        return value;
    }
    return null;
}


function initDateTimePickersPeriod(targetStart, targetEnd) {
    var start = $(targetStart).data("kendoDateTimePicker");
    var end = $(targetEnd).data("kendoDateTimePicker");

    //start.bind('change',
    //    function() {
    //        var startDate = start.value();
    //        var endDate = end.value();

    //        if (startDate) {
    //            startDate = new Date(startDate);
    //            startDate.setDate(startDate.getDate());
    //            end.min(startDate);
    //        } else if (endDate) {
    //            start.max(new Date(endDate));
    //        } else {
    //            endDate = new Date();
    //            start.max(endDate);
    //            end.min(endDate);
    //        }
    //    });
    //end.bind('change',
    //    function() {
    //        var endDate = end.value();
    //        var startDate = start.value();

    //        if (endDate) {
    //            endDate = new Date(endDate);
    //            endDate.setDate(endDate.getDate());
    //            start.max(endDate);
    //        } else if (startDate) {
    //            end.min(new Date(startDate));
    //        } else {
    //            endDate = new Date();
    //            start.max(endDate);
    //            end.min(endDate);
    //        }
    //    });
}