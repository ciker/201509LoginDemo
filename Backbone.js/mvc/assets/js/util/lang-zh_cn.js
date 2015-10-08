/**
 * 国际化- 中文
 */

+(function(){
	var　lang = {
		base: {
			
		},
		validate: {
			notnull: '不能为空',
			number: '必须为数字',
			integer: '必须为整数',
			time: '必须为日期格式',
			mobile: '手机号格式不正确',
			url: '网址格式不正确',
			postcode: '邮编格式不正确',
			email: '邮箱格式不正确',
			min: function(len){
				return '字符长度不能小于' + len
			},
			max: function(len){
				return '字符长度不能超过' + len
			}
		}
	};
	
	if(typeof define === 'function' && define.amd){
		define(lang);
		return false;
	}
	window.lang = lang;
})();