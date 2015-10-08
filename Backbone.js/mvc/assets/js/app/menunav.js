define([
    'jquery'
], function($){
	return {
		init: function(){
			var _this = this;
			var nav = $('.dy-nav');
			
			$('.menu .sub-menu > li').hover(function(){
				$('.dy-nav').show();
				var _offsetTop = $(this).offset().top;
				$(nav).css('top', _offsetTop);
			});
			$('.menu .sub-menu').mouseout(function(){
				_this.reset();
			});
		},
		
		active: function($Active){
			if($Active.length === 0){
				return false;
			}
			var nav = $('.dy-nav');
			
			var width = $Active.width();
			var height = $Active.height();
			var offsetTop = $Active.offset().top;
			$(nav).css({
				width: width,
				height: height,
				top: offsetTop
			});
		},
		
		reset: function(){
			var nav = $('.dy-nav');
			var _$A = $('.menu .sub-menu').find('.active');
			if(_$A.length > 0){
				var _offsetTop = _$A.offset().top;
				$(nav).css('top', _offsetTop);
			}
		}
	};
});