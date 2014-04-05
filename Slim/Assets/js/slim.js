var SWF_PATH = "//cdnjs.cloudflare.com/ajax/libs/zclip/1.1.2/ZeroClipboard.swf"


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

var Usage = (function() {
	var USAGE_PATH = "/api/{hash}/usage"

	function getUsagePromise(hash) {
		var path = USAGE_PATH.replace('{hash}', hash)
		return  $.get(path);
	}

	function createLoader() {
		return $('<div class="loader"><i class="fa fa-spinner fa-spin fa-lg"></i>&nbsp; Loading data... </div>')
	}

	function createItem(d) {
		var $item = $('<div class="usage-item" />')

		$('<span class="count primary badge" />').text(d.Count).appendTo($item)
		$('<span class="city" />').text(d.City).appendTo($item)
		$('<span class="country" />').text(d.CountryName).appendTo($item)

		return $item;
	}

	function appendUsage($el, data) {
		var $header = $('<div class="usage-heading"><h4>Usage</h4></div>')
		var $list = $('<div class="usage-list" />')

		$.each(data, function(i, record) {
			var $item = createItem(record)
			$list.append($item)
		})

		$el.append($header).append($list)
	}

	return {
		bind: function($shortItem) {
			var $container = $shortItem.find('.usage-container')
			var $trigger = $shortItem.find('.usage-trigger')
			var hash = $shortItem.data('hash')

			$trigger.click(function(e) {
				e.preventDefault()

				var $loader = createLoader()
				$container.empty().append($loader)

				var p = getUsagePromise(hash)

				p.done(function(data) {
					$container.empty()
					appendUsage($container, data)
				})

				p.fail(function() {
					$container.empty().text("Oops! Failed to load data")
				})
			})

		}
	}
})()



$('.short-item').each(function(i, el) {
	var $container = $(el)

	// ZClip
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

	// Usage
	Usage.bind($container)
})

var countryUsage = [
	{"Hash":"w7szu","Count":12,"CountryCode":"CA","CountryName":"Canada","City":"Toronto"},
	{"Hash":"w7szu","Count":5,"CountryCode":"US","CountryName":"United States","City":"New York"},
	{"Hash":"w7szu","Count":1,"CountryCode":"BM","CountryName":"Bermuda","City":"Hamilton"},
]

