define([
    'jquery',
    'underscore',
    'backbone',
    'util/router',
    'utils',
    'datatable',
    'validate'
], function($, _, Backbone, Router, utils){
	return Backbone.View.extend({
		el: $('#MainCenter'),
		
		render: function(opts){
	    	if(!this.el){
	    		console.log('缺少主容器ID');
	    		return false;
	    	}
	    	utils.get(this.url, null, 'html', {
	    		context: this,
	    		before: function(){
	    			opts.before && opts.before.call(this);
	    		},
	    		success: function(resp){
    	    		this.$el.html(resp);
	    		},
	    		complete: function(){
	    			this.$el.find('form').validate();
	    			$('.loading-view').hide();
	    			
	    			opts.complete && opts.complete.call(this);
	    		}
	    	});
	    },
	    
	    bindOuterEvent: function(){
	    	var events = _.result(this, 'outerEvents');
	    	if(!events) return this;
	    	
	    	this.unbindOuterEvent();
	        for(var key in events){
	        	var method = events[key];
	        	if (!_.isFunction(method)) method = this[events[key]];
	        	if (!method) continue;
	        	var match = key.match(/^(\S+)\s*(.*)$/);
	        	$('body').on(match[1] + '.delegateEvents' + this.cid, match[2], _.bind(method, this));
	        }
	        return this;
	    },
	    
	    unbindOuterEvent: function(){
	    	$('body').off('.delegateEvents' + this.cid);
	    	return this;
	    }
	});
});