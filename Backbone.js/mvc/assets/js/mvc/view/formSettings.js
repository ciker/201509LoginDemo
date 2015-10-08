define([
    'jquery',
    'mvc/view/MainView',
    'mvc/model/formSettings',
    'utils',
    'datatable'
], function($, MainView, FormModel, utils, DataTable){
	var FormView = MainView.extend({
		url: '/assets/json/formview.html',
		model: FormModel,
		dt: null,
	    events: {
	    	'click #newFormBtn': 'addNewForm',
	    	'click .datatable td a.link-form': 'linkForm',
	    	'click .datatable td a.edit-form': 'editForm',
	    	'click .datatable td a.del-form': 'deleteForm'
	    },
	    outerEvents: {
	    	'click #saveNewFormBtn': 'saveForm'
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
	    	this.dt = new DataTable($('#FormManageTable'), {
	    		url: '/assets/json/formlist.json',
				check: false,
				ajaxOptions: { type: 'get' },
				pullComplete: function(resp, dto){
					
				}
	    	});
	    },
	    
	    addNewForm: function(){
	    	utils.dialog($("#newFormDialog"));
	    },
	    
	    saveForm: function(e){
	    	if($("#newForm").valid()){
	    		this.model.save('#newForm', $(e.target));
	    	}
	    },
	    
	    linkForm: function(){
	    	utils.innerDialog($('#linkFormDialog'), {
	    		url: '/assets/json/linkform.html',
	    		container: this.$el,
	    		data: {},
	    		complete: function(){
	    			
	    		}
	    	});
	    },
	    
	    editForm: function(){
	    	utils.innerDialog($('#editFormDialog'), {
	    		url: '/assets/json/editform.html',
	    		container: this.$el,
	    		data: {},
	    		complete: function(){
	    			
	    		}
	    	});
	    },
	    
	    deleteForm: function(e){
	    	var _this = this,
	    		$T = $(e.target);
	    	var id = $T.data('id') || 'noid';
	    	utils.popover({
	    		title: '确定删除吗？',
	    		context: $T,
	    		btns: [{
	    			text: '确定',
	    			clazz: 'btn-success',
	    			click: function(){
	    				var $this = $(this);
	    				_this.model._delete({id: id}, $this);
	    			}
	    		}]
	    	});
	    }
	});
	
	return FormView;
});