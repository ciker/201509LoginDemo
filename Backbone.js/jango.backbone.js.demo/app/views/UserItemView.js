/**
 * 
 * UserItem  View
 * 
 * 
 * jango
 * 
 * 
 * 2015Äê10ÔÂ9ÈÕ11:20:27
*/



//var UserItemView = Backbone.View.extend({
//    initialize: function () {

//    },
//    events: {
//        'click btn_Add': 'add_UserInfo',
//        'click btn_edit': 'edit_UserInfo'
//    },

//    add_UserInfo: function () {

//    }


//});

define(
    [
        'jquery',
        'views/MainView',
        'utils'
    ], function ($, MainView) {
        return MainView.extend({
            url: 'Handler.ashx?url=http://www.cnblogs.com',//'http://www.logindemo.com/',
            events: {

            },
            initialize: function () {
                this.render({
                    type: 'get'
                });
            }

        });
    })