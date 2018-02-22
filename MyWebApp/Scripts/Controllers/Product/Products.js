/// <reference path="../../Utils/Utils.js" />

(function () {

    var utils = window.utils
    var config = {
        urls: {
            GetPagedProducts: "/Product/GetPagedProducts"
        },
        elements: {

        }
    },
    eventHooking = {
        paginationLinkClick: function () {
            $('.pagination-content div.numeric').unbind('click').click(function () {
                var page = $(this).text();
                events.getPagedProducts({ page: page, pageSize: 10 });
            });
        },
        paginatonPreviousClick: function () {
            $('.pagination-content div#paginaton-previous').unbind('click').click(function () {
                var currentPage = $('.pagination-content div.numeric.active').text();
                events.getPagedProducts({ page: (currentPage - 1), pageSize: 10 });
            });
        },
        paginatonNextClick: function () {
            $('.pagination-content div#paginaton-next').unbind('click').click(function () {
                var currentPage = $('.pagination-content div.numeric.active').text();
                events.getPagedProducts({ page: (currentPage + 1), pageSize: 10 });
            });
        }
    },
    events = {
        getPagedProducts: function (page) {
            utils.ajax.post(config.urls.GetPagedProducts, page, $('#product-content'), function () {
                eventHooking.paginationLinkClick();
                eventHooking.paginatonPreviousClick();
                eventHooking.paginatonNextClick();
            });
        }
    },
    init = function () {
        events.getPagedProducts({page:1, pageSize:10});
    }

    if (!window.product) {
        window.product = {};
    }

    window.product = { init: init }




})();