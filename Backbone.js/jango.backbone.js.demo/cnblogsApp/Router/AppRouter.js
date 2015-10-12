/**
 * 
 * 
 * AppRouter
 * 
 * 
 * jango
 * 
 * 
 * 
**/

define(
    [
        'jquery',
        'backbone',
        'views/IndexView',
        'views/DetailView'],
    function ($,
        Backbone,
        indexView,
        Detail) {
        var App = Backbone.Router.extend({
            routes: {
                "": "index",    // #index
                "index": "index",    // #index
                "detail": "detail"    // #detail
                ,
                'recommand': 'recommand'
            },
            index: function () {
                var index = new indexView(this.interface);

            },
            detail: function () {
                var detail = new Detail(this.interface);

            },
            recommand: function () {

            },
            initialize: function () {
                var router = this;
            },
            interface: {
                forward: function (url) {
                    window.location.href = ('#' + url).replace(/^#+/, '#');
                }

            }


        });
        return App;
    })