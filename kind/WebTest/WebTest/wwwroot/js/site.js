// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function (window, $) {

    $.fn.showLinkLocation = function () {
        var link = $(this);
        alert(link);
        //alert(window);
        this.filter("a").each(function () {
            var link = $(this);
            link.append(" (" + link.attr("href") + ")");
        });

        return this;

    };

    $.fn.greenify = function (options) {
 
        // This is the easiest way to have default options.
        var settings = $.extend({
            // These are the defaults.
            color: "#556b2f",
            backgroundColor: "green"
        }, options);

        alert(this);
        alert($.this);

        // Greenify the collection based on the settings variable.
        return this.css({
            color: settings.color,
            backgroundColor: settings.backgroundColor
        });

    };

    $.fn.yazboona = function (options) {

        // This is the easiest way to have default options.
        var settings = $.extend({
            // These are the defaults.
            color: "#54442f",
            backgroundColor: "red"
        }, options);

        // Greenify the collection based on the settings variable.
        return this.css({
            color: settings.color,
            backgroundColor: settings.backgroundColor
        });

    };
}(window, jQuery));