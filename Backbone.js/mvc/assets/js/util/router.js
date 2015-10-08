define([
    'jquery',
    'backbone',
    'app/menunav'
], function($, Backbone, nav){
	var Router = Backbone.Router.extend({
		currentView: null,
		routes: {
			'': 'init',
			'form(/)': 'init',
			'user(/)': 'toUser',
			'*error': 'error'
		},
		
		switchView: function(view){
			var curRouter = location.hash,
				curLi;
			if(curRouter === ''){
				curLi = $('.menu').find('[data-url="#/form"]');
			} else {
				curLi = $('.menu').find('[data-url="'+curRouter+'"]');
			}
			curLi.parents('.has-sub-menu').addClass('active');
			curLi.addClass('active');
			nav.active(curLi);
			
			if(this.currentView){
				var _curView = this.currentView;
				_curView.unbindOuterEvent();
				_curView.undelegateEvents();
				_curView.remove();
			}
			this.currentView = view;
		},
  
		init: function(){
			var _this = this;
			require(['mvc/view/formSettings'], function(View){
				var view = new View;
				_this.switchView(view);
			});
		},
		
		toUser: function(){
			var _this = this;
			require(['mvc/view/userManage'], function(View){
				var view = new View;
				_this.switchView(view);
			});
		},
		
		error: function(){
			alert('页面未找到');
		}
	});
	
	return new Router();
});