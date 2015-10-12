/**
 * 
 * 
 * DetailView
 * 
 * jango
 * 
*/

define(
    ['underscore',
        'backbone'], function (_, Backbone) {
            return Backbone.View.extend({
                el: $('#main-viewport'),
                template: _.template($('#index-template').html()),
                detail: _.template($('#detail-template').html()),
                initialize: function (app) {
                    this.app = app;
                    this.$el.html(this.template());
                    this.wrapper = $('#lstbox');
                    this.render();
                },
                render: function () {
                    var scope = this;
                    var id = this.app.id;
                    console.log(id);
                    var param = { url: 'http://wcf.open.cnblogs.com/blog/post/body/' + id }

                    var model = this.app.model;

                    $.get('Handler2.ashx', param, function (data) {
                        (typeof data === 'string') && (data = $.parseJSON(data));
                        if (data && data.string) {
                            console.log(data);
                            //此处将content内容写入model
                            model.set('value', data.string.value);
                            //scope.wrapper.html(scope.detail(model.toJSON()));
                            scope.wrapper.html(scope.detail(model.toJSON()));
                        }
                    });
                    this.$el.find('.icon_bar').show();
                    this.$el.find('.tab_search ').hide();

                },
                events: {
                    'click #js_return': function () {
                        this.app.forward('index');
                    },
                    'click #js_home': 'js_home'
                },
                js_home: function () {
                    this.app.forward('index');
                }
            });
        })