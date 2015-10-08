/**
 * 公共JS
 */

+(function(){
	var utils = {
		logout: function (gotoUrl) {
		    var url = '';
		    if (gotoUrl)
		        url = gotoUrl;
		    location.href = 'http://sso.31huiyi.com/login/Logout?cookieValue=31huiyi&returnURL=' + encodeURIComponent(url);
	    },
	    logOutThenRefresh: function () {
	        this.logout(location.href);
	    },
	    popover: function(opts){
	    	var _opts = $.extend({
	    		title: '',
	    		useDefCancel: true,
	    		context: null,
	    		btns: []
	    	}, opts);
	    	$('.popover').remove();
	    	
	    	var title = _opts.title,
	    		context = _opts.context,
	    		btns = _opts.btns,
	    		_html = '<div class="popover align-bottom"><h4>'+title+'</h4><div class="pcontent"></div></div>';
	    	
	    	var $P = $(_html);
	    	var pos = getPos(context.get(0));
	    	$('body').append($P);
	    	
	    	if(_opts.useDefCancel){
	    		btns.push({
	    			text: '取消',
	    			clazz: 'btn-default',
	    			click: function(){
	    				$P.css('opacity', 0);
	    				setTimeout(function(){
	    					$P.remove();
	    				}, 200);
	    			}
	    		});
	    	}
	    	$.each(btns, function(){
	    		var $B = $('<button type="button" class="btn btn-xs"></button');
	    		$B.addClass(this.clazz).text(this.text);
	    		$B.off('click').on('click', this.click);
	    		$P.find('.pcontent').append($B);
	    	});
	    	
	    	var w1 = context.width(),
	    		w2 = $P.get(0).clientWidth,
	    		height = $P.get(0).clientHeight,
	    		left = pos.left;
			var _left = (left+w1/2)- w2/2 - 2;
			
			$P.css({
				left: _left,
				top: pos.bottom - height + 5
			});
			
			setTimeout(function(){
				$P.css({
					top: pos.top - height - 10,
					opacity: 1
				});
			}, 10);
	    	
	    	function getPos(el){
	    		return el.getBoundingClientRect();
	    	}
	    },
	    resetForm: function($F){
	    	$F.find('.has-error,.has-success').removeClass('has-error has-success');
	    	$F.find('.err-msg').remove();
	    	if($F.length > 0) $F.get(0).reset();
	    },
	    dialog: function($D){
	    	$D.show();
	    	this.resetForm($D.find('form'));
	    	setTimeout(function(){
	    		$D.css('opacity', 1);
	    		$D.find('.dialog-content').css('margin-top', '8%');
	    	}, 10);
	    	$D.find('[data-dismiss=true]').off('click').on('click', function(){
	    		$D.css('opacity', 0);
	    		$D.find('.dialog-content').css('margin-top', 0);
	    		setTimeout(function(){
	    			$D.hide();
	    		}, 200);
	    	});
	    },
	    innerDialog: function($D, opts){
	    	var _utils = this,
	    		$el = opts.container;
	    	$el.css('overflow', 'hidden');
	    	
	    	var	width = $el.get(0).clientWidth,
	    		height = $el.get(0).clientHeight;
	    	$D.css({
	    		display: 'block',
	    		width: width,
	    		height: height,
    			left: '100%',
    			top: 0
	    	});
	    	
	    	$D.on('click', '[data-dismiss=true]', function(){
	    		$D.css({
	    			left: '100%',
	    			opacity: 0
	    		});
	    		setTimeout(function(){
	    			$D.hide();
	    			$el.css('overflow', 'auto');
	    		}, 200);
	    	});
	    	
	    	setTimeout(function(){
	    		$D.css({
	    			left: 0,
	    			opacity: 1
	    		});
	    		
	    		_utils.get(opts.url, opts.data || {}, 'html', {
	    			context: $D.find('.dialog-content'),
	    			before: function(){
	    				this.addClass('processing').css({
	    					backgroundPosition: 'center 45%',
	    					backgroundSize: 'initial'
	    				});
	    			},
	    			success: function(resp){
	    				this.html(resp);
	    				$D.find('.dialog-body').css({
	    		    		width: width,
	    		    		height: height-40-55
	    		    	});
	    			},
	    			complete: function(){
	    				this.removeClass('processing');
	    				
	    				var _form = this.find('form');
	    				_utils.resetForm(_form);
	    				_form.validate();
	    				
	    				opts.complete && opts.complete.call(this);
	    			}
	    		});
	    	}, 10);
	    },
	    msg: function(opts){
	    	var _opts = $.extend(true, {
	    		text: '',
	    		type: 'success',
	    		timeout: '3000',
	    		event: false
	    	}, opts);
	    	
	    	var text = _opts.text,
	    		type = _opts.type,
	    		event = _opts.event,
	    		clazz = type + '-msg',
	    		faclazz = 'fa-check';
	    	
	    	if(type === 'warning'){
	    		faclazz = 'fa-warning';
	    	} else if(type === 'error'){
	    		faclazz = 'fa-remove';
	    	}
	    	var _html = '<div class="alertmsg"><div class="'+clazz+'"><div class="msgicon"><i class="fa '+faclazz+'"></i></div><span>'+text+'</span></div></div>';
	    	$('.alertmsg').remove();
	    	$('body').append(_html);
	    	setTimeout(function(){
	    		$('.alertmsg').css({
	    			top: '10px',
	    			opacity: 1
	    		});
	    	},10);
	    	if(event){
	    		$('.alertmsg').off(event).on(event, function(){
	    			$(this).css({
	    				top: '-40px',
	    				opacity: 0
	    			});
	    		});
	    	} else {
	    		setTimeout(function(){
	    			$('.alertmsg').css({
	    				top: '-40px',
	    				opacity: 0
	    			});
	    		}, _opts.timeout-10);
	    	}
	    },
	    get: function(url, data, dataType, calls){
	    	var _this = this;
	    	var _context = calls.context || null;
	    	$.ajax({
	    		url: url,
	    		type: 'get',
	    		dataType: dataType || 'html',
	    		data: data || {},
	    		beforeSend: function(){
	    			calls.before && calls.before.call(_context);
	    		},
	    		success: function(resp){
	    			calls.success && calls.success.call(_context, resp);
	    		},
	    		error: function(xhr, errorMsg, errObj){
	    			console.log(errObj);
	    			_this.msg({
	    				text: errObj,
	    				type: 'error'
	    			});
	    			calls.error && calls.error.call(_context, errObj);
	    		},
	    		complete: function(xhr){
	    			calls.complete && calls.complete.call(_context, xhr);
	    		}
	    	});
	    },
	    ajax: function (url, data, $Btn, calls) {
	    	var _this = this,
	    		Timeout = 3000;
	    	$.ajax({
	    		url: url,
	    		type: 'get',
	    		dataType: 'json',
	    		data: data || {},
	    		context: $Btn,
	    		beforeSend: function(){
	    			if(this && this.length > 0){
	    				var text = $.trim(this.text());
	    				this.data('text', text);
	    				this.height(this.height());
	    				this.width(this.width());
	    				this.addClass('disabled processing').html('');
	    			}
	    			calls.before && calls.before();
	    		},
	    		success: function(resp){
	    			if(formatJSON(resp)){
	    				setTimeout(function(){
	    					calls.success && calls.success(resp);
	    				}, Timeout);
	    			}
	    		},
	    		error: function(xhr, errorMsg, errObj){
	    			if(errorMsg === 'timeout'){
						alert('请求超时！');
					} else if(errorMsg === 'parsererror'){
						alert('解析异常！\n' + errObj.message);
					} else {
						console.log(errObj);
						alert('服务器异常！');
					}
	    			this.data('error', true);
	    			
	    			calls.error && calls.error();
	    		},
	    		complete: function(xhr, status){
	    			var _this = this;
	    			if(_this && _this.length > 0){
	    				var text = _this.data('text');
	    				if(_this.data('error')){
    						_this.removeClass('disabled processing').html(text);
	    				} else {
	    					setTimeout(function(){
	    						_this.removeClass('disabled processing').html(text);
	    					}, Timeout+10);
	    				}
	    				
	    				/* popover */
	    				var $P = _this.parents('.popover');
	    				if($P.length > 0){
	    					$P.css('opacity', 0);
	    					setTimeout(function(){
	    						$P.remove();
	    					}, 200);
	    				}
	    				
	    				/* dialog */
	    				var $D = _this.parents('.dialog').not('.dialog-insert');
	    				if($D.length > 0){
	    					$D.css('opacity', 0);
	    					$D.find('.dialog-content').css('margin-top', 0);
	    					setTimeout(function(){
	    						$D.hide();
	    					}, 200);
	    				}
	    			}
	    			calls.complete && calls.complete();
	    		}
	    	});
	    },
	    post: function(url, data, $Btn, calls){
	    	this.ajax(url, data, $Btn, calls || {});
	    },
	    serializFormData: function($F){
	    	var _utils = this;
	    	if ($F.length === 0){
	    		return false;
	    	}
	    	
    		var array = $F.serializeArray();
    		$(array).each(function(){
    			if(Object.prototype.toString.call(this.value) !== '[object String]'){
    				this.value = this.value.toString();
    			}
    			this.value = _utils.encode(this.value);
    		});
	    	
	        return array;
	    },
	    submitForm: function(selector, url, $Btn, calls){
	    	var _utils = this;
	    	var data = _utils.serializFormData($(selector));
	    	
	    	this.ajax(url, data, $Btn, calls || {});
	    },
	    encode: function(htmlStr){
	        var s = '';
			if(!htmlStr) return s;
			
	    	if(htmlStr.length == 0) return '';
	    	s = htmlStr.replace(/</g, '&lt;');
	    	s = s.replace(/>/g, '&gt;');
	    	s = s.replace(/ /g, '&nbsp;');
	    	s = s.replace(/\'/g, '&#39;');
	    	s = s.replace(/\"/g, '&quot;');
	    	return s;
	    },
	    decode: function(str){
	    	var s = '';
			if(!str) return s;
			
	    	if(str.length == 0) return '';
	    	s = str.replace(/&lt;/g, '<');
	    	s = s.replace(/&gt;/g,'>');
	    	s = s.replace(/&nbsp;/g,' ');
	    	s = s.replace(/&#39;/g,"\'");
	    	s = s.replace(/&quot;/g,"\"");
	    	return s;
	    }
	};

	function formatJSON(resp, timeout){
		var msg = resp.msg,
        	data = resp.data,
        	flag = true;
        if(!msg){
        	return true;
        }

        switch(resp.action){
        case 'alertok':
        	utils.msg({
        		text: msg,
        		timeout: timeout
        	});
            break;
        case 'alerterror':
        	utils.msg({
        		type: 'error',
        		text: msg,
        		timeout: timeout
            });
            flag = false;
            break;
        case 'notdisppearalert':
        	utils.msg({
        		type: 'error',
             	text: msg,
             	event: 'click'
            });
            flag = false;
            break;
        case 'alerterrors':
            var s = '';
            for (var o in data) {
            	s += data[o];
            }
            utils.msg({
            	type: 'error',
            	text: msg,
            	timeout: timeout
            });
            flag = false;
            break;
        }
        return flag;
	}
	
	if(typeof define === 'function' && define.amd){
		define(['jquery'], function(){
			return utils;
		});
		return false;
	}
	window.utils = utils;
})();
