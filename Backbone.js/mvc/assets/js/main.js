
require.config({
	baseUrl: 'assets/js',
	urlArgs: 'v=' + (new Date().getTime()),
	waitSeconds: 30,
	paths: {
		'jquery': 'lib/jquery-2.1.4.min',
		'underscore': 'lib/underscore-min',
		'backbone': 'lib/backbone-min',
		'utils': 'util/utils',
		'datatable': 'plugin/datatable/js/datatable',
		'lang': 'util/lang-zh_cn',
		'validate': 'plugin/jquery-validate-form/js/jquery.validate.form',
		'ztree': 'plugin/zTree_v3/js/jquery.ztree.core-3.5.min'
	},
	shim: {
		'validate': {
			deps: ['jquery', 'lang']
		},
		'datatable': {
			deps: ['jquery']
		},
		'ztree': {
			deps: ['jquery'],
			exports: '$.fn.zTree'
		}
	}
});
require.onError = function(err){
    console.log(err.requireType);
    if (err.requireType === 'timeout') {
        console.log('modules: ' + err.requireModules);
    }

    throw err;
};

require(['mvc/view/nav']);
