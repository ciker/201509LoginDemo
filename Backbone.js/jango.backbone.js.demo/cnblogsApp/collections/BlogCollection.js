/**
 * 
 * 
 * BlogCollection
 * 
 * 
 * jango
 * 
 * 
 * 
**/

define(['backbone', 'models/BlogModel'], function (Backbone, BlogModel) {
    return Backbone.Collection.extend({
        model: BlogModel,
        parse: function (data) {
            // 'data' contains the raw JSON object
            //return (data && data.entry) || {}
            return (data && data.feed && data.feed.entry) || {}
        },
        setComparator: function (type) {
            this.comparator = function (item) {
                return Math.max(item.attributes[type]);
            }
        },
        initialize: function () {
            this.pageIndex = 1;
            this.PageSize = 10;
            this.url = 'Handler2.ashx?type=xml&url=http://wcf.open.cnblogs.com/blog/sitehome/paged/' + this.pageIndex + '/' + this.PageSize;

        }
    });
})