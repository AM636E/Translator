/// <reference path="jquery.unobtrusive-ajax.min.js" />
(function ($) {
    $(document).ready(function () {
        $('textarea.elastic').elastic();
        $("select").chosen({ disable_search_threshold: 10 });       
    });
})(jQuery)