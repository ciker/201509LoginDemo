/**
 * 
 * UserList Model Collection
 * 
 * 
 * jango
 * 
 * 
 * 2015年10月9日12:25:54
*/

define(
    [
    'jquery',
    'underscore',
    'backbone',
    '/models/UserInfo'
    ], function (userInfo) {
        return Backbone.Collection.extend({
            model: userInfo
        });
    })
