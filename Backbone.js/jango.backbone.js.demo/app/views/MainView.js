/**
 * 
 * 
 * View template
 * 
 * 
 * jango
 * 
 * 
 * 2015年10月9日12:04:34
 */

define([
    'jquery',
    'underscore',
    'backbone',
    'utils'
],
    function ($, _, Backbone, utils) {
        return Backbone.View.extend({
            el: $('.mainContent'),
            render: function (opts) {
                if (!this.el) {
                    console.log('no container');
                    return false;
                }
                if (opts.type === "load") {
                    utils.load(this.url, null, function (res) {
                        console.log(res);
                        //{
                        //    success: function (resp) {
                        //        console.log(resp);
                        this.$el.html(resp);
                        //    }
                    });
                } else {
                    utils.ajax(this.url, null, null, 'html', {
                        context: this,
                        before: function () {
                            opts.before && opts.before.call(this);
                        },
                        success: function (resp) {
                            this.$el.html(resp);
                        },
                        complete: function () {
                            opts.complete && opts.complete.call(this);
                        }
                    });
                }
            }

        });
    })