/**
 * 
 * 
 * IndexView
 * 
 * 
 * jango
 * 
 * 
 * 
*/
define(
    ['underscore',
        'backbone',
        'collections/BlogCollection'
    ], function (_, Backbone, PostList) {
        return Backbone.View.extend({
            el: $('#main-viewport'),
            template: _.template($('#index-template').html()),
            itemTmpt: _.template($('#item-template').html()),


            switchTab: function (e) {
                var selectedTab = e.target;
                this.$el.find('li').removeClass('tabcrt');
                $(selectedTab).addClass('tabcrt');
            },
            events: {
                'click #sort': function (e) {
                    var el = $(e.target);
                    var type = el.attr('attr');
                    this.list.setComparator(type);
                    this.list.sort();
                },
                'click #cnblogsType': function (e) {
                    var scope = this;
                    this.switchTab(e);
                    var el = $(e.target);
                    var type = el.attr('attr');
                    if (type === "sitehome") {
                        this.list.url = 'Handler2.ashx?type=xml&url=http://wcf.open.cnblogs.com/blog/sitehome/paged/' + this.list.pageIndex + '/' + this.list.PageSize;
                    }
                        //else if (type === "recommend") {
                        //    this.list.url = 'Handler2.ashx?type=xml&url=http://wcf.open.cnblogs.com/blog/bloggers/recommend/' + this.list.pageIndex + '/' + this.list.PageSize;
                        //}
                    else if (type === "48HoursTopViewPosts") {
                        this.list.url = 'Handler2.ashx?type=xml&url=http://wcf.open.cnblogs.com/blog/48HoursTopViewPosts/' + this.list.PageSize;
                    }
                    console.log(this.list.url);
                    this.loadList();
                },
                'click .orderItem': function (e) {
                    var el = $(e.currentTarget);
                    var index = el.attr('data-index');
                    var id = el.attr('data-id');
                    var model = this.list.models[index];
                    console.log(model);
                    console.log(this.app);
                    this.app.model = model;
                    this.app.id = id;


                    this.app.forward('detail');
                    //          var scope = this;
                    //          var param = { url: 'http://wcf.open.cnblogs.com/blog/post/body/' + id }
                    //          $.get('Handler.ashx', param, function (data) {
                    //            (typeof data === 'string') && (data = $.parseJSON(data));
                    //            if (data && data.string) {
                    //              //此处将content内容写入model
                    //              model.set('value', data.string.value);
                    //              var d = new Detail();
                    //              d.model = model;
                    //              d.render();
                    //            }
                    //          });
                    //          var s = '';
                }
            },
            initialize: function (app) {
                this.app = app;

                console.log(app);
                //先生成框架html
                this.$el.html(this.template());
                this.post = this.$('#post');

                var scope = this;
                //var curpage = 1;
                //var pageSize = 30;
                this.list = new PostList();
                //this.list.url = 'Handler2.ashx?url=http://wcf.open.cnblogs.com/blog/sitehome/paged/' + curpage + '/' + pageSize;
                this.loadList();
                this.wrapper = $('#lstbox');

                //this.listenTo(this.list, 'all', this.render);
                this.render();
            },
            loadList: function () {
                var scope = this;
                this.list.fetch({
                    success: function (ctx, resp) {
                        console.log(ctx);
                        console.log(resp);
                        scope.render();
                    }
                });
            },
            render: function () {
                var models = this.list.models;
                var html = '';
                for (var i = 0, len = models.length; i < len; i++) {
                    models[i].index = i;
                    html += this.itemTmpt(_.extend(models[i].toJSON(), { index: i }));
                }
                this.wrapper.html(html);
                this.$el.find('.icon_bar').hide();
                this.$el.find('.tab_search ').show();
                var s = '';
            }
        });
    })