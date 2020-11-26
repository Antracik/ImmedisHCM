$(document).on('change', '.js-get-cities', function () {
    var $selectList = $(this);
    var $cityList = $('.js-set-cities');
    var id = $selectList.val();
    console.log(id);
    $.ajax({
        type: 'GET',
        url: '../../../../Ajax/GetCitiesForCountry',
        data: { Id: id },
        success: function (result) {
            $cityList.empty();
            $.each(result, function (key, value) {
                $cityList.append($("<option></option>")
                    .attr("value", value.id).text(value.name));
            });
        },
        error: function () {
        }
    });
});

$(document).on('change', '.js-get-departments', function () {
    var $selectList = $(this);
    var $cityList = $('.js-set-departments');
    var id = $selectList.val();
    console.log(id);
    $.ajax({
        type: 'GET',
        url: '../../../../Ajax/GetDepartmentsForCompany',
        data: { Id: id },
        success: function (result) {
            $cityList.empty();
            $.each(result, function (key, value) {
                $cityList.append($("<option></option>")
                    .attr("value", value.id).text(value.name));
            });
        },
        error: function () {
        }
    });
});

