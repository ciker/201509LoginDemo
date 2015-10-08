define([
    'jquery',
    'backbone',
    'util/router',
    'mvc/view/MainView',
    'app/menunav'
], function($, Backbone, router, MainView, nav){
	var NavView = MainView.extend({
		el: $('body'),
		slideSpeed: 200,
	    events: {
	    	'click .menu > li.has-sub-menu > span': 'foldMenu',
	    	'click .menu .sub-menu > li': 'routeMenu'
	    },
	    initialize: function() {
	    	nav.init();
	    	
	    	Backbone.history.start();
	    },
		
		foldMenu: function(e){
			var $T = $(e.target);
			var SlideSpeed = this.slideSpeed;
			var $P = $T.parent();
			if($P.hasClass('active')){
				$('.dy-nav').hide();
				$P.removeClass('active');
				
				$P.find('.sub-menu').slideUp(SlideSpeed);
			} else {
				$('.menu > li.has-sub-menu').removeClass('active');
				$('.menu > li.has-sub-menu').find('.sub-menu').slideUp(SlideSpeed)
				
				if($P.find('.sub-menu > li.active').length > 0){
					$('.dy-nav').show();
				}
				setTimeout(function(){
					nav.reset();
				}, SlideSpeed+5);
				
				$P.addClass('active');
				$P.find('.sub-menu').slideDown(SlideSpeed);
			}
		},
		
		routeMenu: function(e){
			var $T = $(e.currentTarget);
			var url = $T.data('url');
			if(url){
				$('.menu .sub-menu > li').removeClass('active');
				$T.addClass('active');
				
				var top = $T.offset().top;
				var _html = '<div class="loading-view"></div>';
				if($('.loading-view').length === 0){
					$('body').append(_html);
				}
				$('.loading-view').css({
					display: 'block',
					top: top + ($T.height()-$('.loading-view').height())/2
				});
				
				router.navigate(url, {trigger: true});
			}
		}
	});
	
	return new NavView;
});