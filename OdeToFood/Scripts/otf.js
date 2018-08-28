$(function () {
    var ajaxFormSubmit = function () {
        var $form = $(this).parent();

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        }

        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-otf-target"));
            $target.replaceWith(data);
            $("#withReviews").on("click", "a", getPage);
        });

        return false;
    };

    var submitAutoCompleteForm = function (event, ui) {
        var $input = $(this);
        $input.val(ui.item.label);

        $("form[data-otf-ajax='true'] :nth-child(2)").trigger("click");

        return false;
    };

    var createAutoComplete = function () {
        var $input = $(this);

        var options = {
            source: $input.attr("data-otf-autocomplete"),
            select: submitAutoCompleteForm,
        };

        $input.autocomplete(options);

        return false;
    };

    var getPage = function () {
        var $a = $(this);

        if ($a.attr("href") != null) {
            var options = {
                url: $a.attr("href"),
                type: "GET"
            }

            $.ajax(options).done(function (data) {

                var target = $("#withReviews .pagedList:nth-child(1)").attr("data-otf-target");

                $(target).replaceWith(data);
                $("#withReviews").on("click", "a", getPage);
            });
        }

        return false;
    }

    $("form[data-otf-ajax='true']").on("click", "button" , ajaxFormSubmit);

    $("input[data-otf-autocomplete]").each(createAutoComplete);

    $("#withReviews").on("click", "a", getPage);
}) 