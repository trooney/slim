var SWF_PATH = "//cdnjs.cloudflare.com/ajax/libs/zclip/1.1.2/ZeroClipboard.swf";

var Alert = (function() {
	var $container = $('.slim-alerts')

	function create(message, type) {
		var classes = ['alert', type || "primary"].join(' ')
		return $('<div />').addClass(classes).text(message)
	}

	return {
		notify: function(message, type) {
			var alert = create(message, type)
			$('.slim-alerts').empty().append(alert)
		}
	}
})()




$('.short-item').each(function(i, el) {
	console.log(el)
	var $container = $(el)
	var $trigger = $container.find('.copy-btn')
	var url = $trigger.attr('href')

	$trigger.zclip({
		path: SWF_PATH,
		copy: url,
		setCSSEffects: false,
		afterCopy: function() {
			document.body.focus()
			Alert.notify('Copied ' + url + ' to your clipboard', "success")
		}
	})
})

var countryUsage = [
	{"Hash":"w7szu","Count":14,"CountryCode":"BM","CountryName":"Bermuda"},
	{"Hash":"w7szu","Count":7,"CountryCode":"RD","CountryName":"Reserved"}
]