// (function($){

if(typeof Object.create !== 'function') {
        Object.create = function(o) {
            function F() {
            }

            F.prototype = o;
            return new F();
        };
    }

var NotifyObj ={
	init:function(option){

		var settings = {
			type : 'default',
			text :  '',
			position: 'top',
	        template    : '<div class="notify_message"><span class="notify_text"></span>'+	
    					  '<div class="notify_close"></div></div>',
	        html:'',
	        animation:{
	        	open:'toggle',
	        	close:'toggle',
	        	speed:500,
	        	fadeSpeed:'fase'
	        },
	        timeout:false,
	        callback:{
	        	onshow:function(){

	        	},
	        	aftershow:function(){

	        	},
	        	onclose:function(){

	        	},
	        	afterclose:function(){

	        	}
	        }
		}

		this.options = $.extend({},settings,option);
		this.options.id = 'notify_'+(new Date().getTime()*Math.floor(Math.random()*1000000));
		this.options = $.extend({},this.options,option);
		this._build();
		return this;
	},

	_build:function(){
		var $bar = $('<div class="notify_bar notify_type_'+this.options.type+' nofity_position_'+this.options.position+'"></div>').attr('id',this.options.id);
		$bar.append(this.options.template).find(".notify_text").html(this.options.text);
		this.options.html = $bar[0].outerHTML;
	}
}


$.notifyRenderer ={};
$.notifyRenderer.init = function(options){
        var notification = Object.create(NotifyObj).init(options);
        $.notifyRenderer.show(notification)
};

$.notifyRenderer.show = function(notification){
	console.log(notification)
	$('body').append(notification.options.html);

	$(notification).show();
};

$.fn.notify = function(options) {
        options.custom = $(this);
        return $.notyRenderer.init(options);
    };



window.notify = function notify(options){
	 return $.notifyRenderer.init(options);
}
// })(jQuery)