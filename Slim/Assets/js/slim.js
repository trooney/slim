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




$('.short-list').each(function(i, el) {
	var $container = $(el)
	var $trigger = $container.find('.copy-btn')
	var url = $trigger.find('span').attr('href')

	$trigger.zclip({
		path: SWF_PATH,
		copy: url,
		setCSSEffects: false,
		afterCopy: function() {
			document.body.focus()
			Alert.notify('Slim URL ' + url + ' was copied to your clipboard', "success")
		}
	})
})
