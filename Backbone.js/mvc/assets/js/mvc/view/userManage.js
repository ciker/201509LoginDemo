define([
    'jquery',
    'mvc/view/MainView',
    'utils',
    'datatable'
], function($, MainView, utils, DataTable){
	var UserView = MainView.extend({
		url: '/assets/json/userview.html',
		dt: null,
	    events: {
	    	
	    },
	    outerEvents: {
	    	
	    },

	    initialize: function() {
	    	this.render({
	    		complete: function(){
	    			this.initTable();
	    		}
	    	});
	    	this.bindOuterEvent();
	    },
	    
	    initTable: function(){
	    	this.dt = new DataTable($('#UserManageTable'), {
	    		url: '/assets/json/userlist.json',
				check: false,
				ajaxOptions: { type: 'get' },
				pullComplete: function(resp, dto){
					
				}
	    	});
	    }
	});
	
	return UserView;
});