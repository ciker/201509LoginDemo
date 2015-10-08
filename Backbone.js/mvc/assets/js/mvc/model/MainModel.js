define([
    'backbone',
    'utils'
], function(Backbone, utils){
	return Backbone.Model.extend({
		surl: null,
		durl: null,
		
		save: function(formSelector, $el, calls){
			utils.submitForm(formSelector, this.surl, $el, calls);
		},
		
		_delete: function(keys, $el, calls){
			utils.post(this.durl, keys, $el, calls);
		}
	});
});