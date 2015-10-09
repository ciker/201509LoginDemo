require.config({
    baseUrl: 'app/',
    urlArgs: 'v=' + (new Date().getTime()),
    waitSeconds: 30,
    paths: {
        'jquery': 'lib/jquery-1.7.1',
        'underscore': 'lib/underscore',
        'backbone': 'lib/backbone',
        'utils': 'util/utils'
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

