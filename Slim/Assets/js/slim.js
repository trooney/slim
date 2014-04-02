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




$('.slim-links').each(function(i, el) {
	var $container = $(el)
	var $trigger = $container.find('.copy-trigger')
	var url = $container.find('a').attr('href')

	$trigger.zclip({
		path: SWF_PATH,
		copy: url,
		afterCopy: function() {
			Alert.notify('Slim URL ' + url + ' was copied to your clipboard', "success")
		}
	})
})
