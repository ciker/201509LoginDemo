/**
 * 
 * 
 * main (require config)
 * 
 * 
 * jango
 * 
 * 
 * 
 */


require.config({
    baseUrl: 'cnblogsApp/',
    urlArgs: 'v=' + (new Date().getTime()),
    waitSeconds: 30,
    paths: {
        'jquery': 'lib/jquery-1.7.1',
        'underscore': 'lib/underscore',
        'backbone': 'lib/backbone'
    },
    shim: {
        underscore: {
            exports: '_'
        },
        backbone: {
            deps: ['jquery', 'underscore'],
            exports: 'Backbone'
        }
    }
});
require.onError = function (err) {
    console.log(err.requireType);

    if (err.requireType === 'timeout') {
        console.log('modules:' + err.requireModules);
    }
    throw err;
}

//Backbone会把自己加到全局变量中
require(['backbone', 'Router/AppRouter'], function (Backbone, App) {
    //console.log(app);
    ////Backbone.history.start();  //开始监控url变化
    new App();
    Backbone.history.start();
    console.log(Backbone.History.started);
    //var indexView = new Index();
});

