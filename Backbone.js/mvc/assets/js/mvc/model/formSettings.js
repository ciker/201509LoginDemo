define([
    'jquery',
    'mvc/model/MainModel',
    'utils'
], function($, MainModel, utils){
	var FormModel = MainModel.extend({
		surl: '/assets/json/saveform.json',
		durl: '/assets/json/delform.json',
		
		initialize: function(){
			
		}
	});
	
	return new FormModel;
});