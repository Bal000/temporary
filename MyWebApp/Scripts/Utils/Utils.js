(function () {
    config = {
        ajaxLoader: function () {
            return '<div id="ajax-spinner" style="height:100%;width:100%;"><img src="/Content/Images/GearLoader.gif" style="margin:auto;display:block;padding:10px;"></img></div>';
        }
    },
    loaders = {
        ajaxContentLoader: function () {
            return config.ajaxLoader();
        }
    },
    ajax =
     {
         post: function (url, data, $ele, callback, $eleToFocus, timeout, onError) {
             $.ajax({
                 method: "POST",
                 url: url,
                 data: data,
                 beforeSend: function () {
                     $ele.append(loaders.ajaxContentLoader());
                 },
                 success: function (data, textStatus, jqXHR) {
                     if (data.success) {
                         $ele.html(data.view);

                         if (callback)
                             callback();
                     }
                     else {
                         alert('Server Error'.toUpperCase() + '\r\nStatusCode: ' + data.statusCode + '\r\n Errors: ' + data.errors);
                     }
                 },
                 timeout: (typeof timeout !== 'undefined') ? timeout : (30 * 1000),
                 error: function (jqXHR, textStatus, errorThrown) {
                     if (onError) {
                         onError(jqXHR, textStatus, errorThrown)
                     }
                     else {
                         alert('Error: ' + errorThrown + '\r\n StatusCode: ' + jqXHR.status);
                         $('#product-content').html(jqXHR.responseText);
                     }
                 },
                 complete: function () {
                     $('#ajax-spinner').remove();
                     if ($eleToFocus) {
                         $($eleToFocus).focus();
                     }
                 }
             });
         },
         get: function (url, data, $ele, callback, $eleToFocus, timeout, onError) {
             $.ajax({
                 method: "GET",
                 url: url,
                 data: data,
                 beforeSend: function () {
                     $ele.append(loaders.ajaxContentLoader());
                 },
                 success: function (data, textStatus, jqXHR) {
                     if (data.success) {
                         $ele.html(data.view);

                         if (callback)
                             callback();
                     }
                     else {
                         alert('Server Error'.toUpperCase() +
                               '\r\nStatusCode: ' + data.statusCode +
                               '\r\n Errors: ' + data.errors);
                     }
                 },
                 timeout: (typeof timeout !== 'undefined') ? timeout : (30 * 1000),
                 error: function (jqXHR, textStatus, errorThrown) {
                     if (onError) {
                         onError(jqXHR, textStatus, errorThrown)
                     }
                     else {
                         alert('Error: ' + errorThrown + '\r\n StatusCode: ' + jqXHR.status);
                         $('#product-content').html(jqXHR.responseText);
                     }
                 },
                 complete: function () {
                     $('#ajax-spinner').remove();
                     if ($eleToFocus) {
                         $($eleToFocus).focus();
                     }
                 }
             });
         }
     };

    if (!window.utils) {
        window.utils = {};
    }
    window.utils = { ajax: ajax };
})();

