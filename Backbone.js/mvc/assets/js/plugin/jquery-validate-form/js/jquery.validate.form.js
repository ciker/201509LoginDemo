/**
 * 基于jQuery的表单验证控件
 * 定制化验证插件，HTML结构依赖bootstrap表单结构
 * Loncy.Kao
 * 2015-06-30
 */

+(function($){
	var Validator = {
		isNull: function(value){
			return value == '' || !value;
		},
		isNumber: function(value){
			return value == '' || /^(-?\d+)(\.\d+)?$/g.test(value);
		},
		isInteger: function(value){
			return value == '' || /^-?\d+$/g.test(value);
		},
		isMobile: function(value){
			return value == '' || /(^1\d{10})$|(^00852\d{8}$)/g.test(value);
		},
		isURL: function(value){
			var strRegex = "^((https|http|ftp|rtsp|mms)?://)"
                + "?(([0-9a-zA-Z_!~*'().&=+$%-]+: )?[0-9a-zA-Z_!~*'().&=+$%-]+@)?"
                + "(([0-9]{1,3}\.){3}[0-9]{1,3}"
                + "|"
                + "([0-9a-zA-Z_!~*'()-]+\.)*"
                + "([0-9a-zA-Z][0-9a-zA-Z-]{0,61})?[0-9a-zA-Z]\."
                + "[a-zA-Z]{2,6})"
                + "(:[0-9]{1,4})?"
                + "((/?)|"
                + "(/[0-9a-zA-Z_!~*'().;?:@&=+$,%#-]+)+/?)$";
			var reg = new RegExp(strRegex, 'g');
			return value == '' || reg.test(value);
		},
		isPostCode: function(value){
			return value == '' || /^[1-9][0-9]{5}$/g.test(value);
		},
		isEmail: function(value){
			return value == '' || /^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$/g.test(value);
		}
	};
	
	if(typeof define === 'function' && define.amd){
		define(['lang'], function(lang){
			initValidate(lang);
		});
	} else {
		if(typeof lang === 'undefined'){
			alert('未引入国际化脚本JS');
			return false;
		}
		initValidate(lang);
	}
	
	function initValidate(lang){
		$.fn.extend({
			validate: function(){
				var ValidLang = lang.validate;
				$(this).find('[required]').off('blur').on('blur', function(e){
					var $this = $(this);
					var value = $this.val();
					var flag = true;
					
					if($this.data('notnull') && Validator.isNull(value)){
						flag = showErrMsg($this, ValidLang.notnull);
					} else if($this.data('number') && !Validator.isNumber(value)){
						flag = showErrMsg($this, ValidLang.number);
					} else if($this.data('integer') && !Validator.isInteger(value)){
						flag = showErrMsg($this, ValidLang.integer);
					} else if($this.data('minlength') && value.length < parseInt($this.data('minlength'))){
						flag = showErrMsg($this, ValidLang.min($this.data('minlength')));
					} else if($this.data('maxlength') && value.length > parseInt($this.data('maxlength'))){
						flag = showErrMsg($this, ValidLang.max($this.data('maxlength')));
					} else if($this.data('mobile') && !Validator.isMobile(value)){
						flag = showErrMsg($this, ValidLang.mobile);
					} else if($this.data('url') && !Validator.isURL(value)){
						flag = showErrMsg($this, ValidLang.url);
					} else if($this.data('postcode') && !Validator.isPostCode(value)){
						flag = showErrMsg($this, ValidLang.postcode);
					} else if($this.data('email') && !Validator.isEmail(value)){
						flag = showErrMsg($this, ValidLang.email);
					} else if($this.data('reg')){
						var reg_str = $this.data('reg');
						var reg_msg = $this.data('reg-msg');
						var reg = new RegExp(reg_str);
						if(value != '' && !reg.test(value)){
							flag = showErrMsg($this, reg_str);
						}
					}
					
					if(flag){
						$this.parents('.form-group').removeClass('has-error').addClass('has-success');
						$this.parent().find(".err-msg").remove();
					}
				});
				
				function showErrMsg($el, msg){
					$el.parents('.form-group').removeClass('has-success').addClass("has-error");
					$el.parent().find('.err-msg').remove();
					$el.parent().append('<div class="err-msg">' + msg + '</div>');
					return false;
				}
			},
			valid: function(){
				var $F = $(this);
				$F.find('[required]').blur();
				if($F.find('.has-error').length > 0){
					$F.find('.has-error').eq(0).find('[required]').focus();
					return false;
				}
				return true;
			}
		});
	}
})(jQuery);
