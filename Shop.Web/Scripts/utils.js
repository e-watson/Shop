String.prototype.format = function () {
	var replaced = this;
	for (var i = 0, len = arguments.length; i < len; i++) {
		var regex = new RegExp('\\{' + i + '\\}');
		replaced = replaced.replace(regex, arguments[i]);
	}
	return replaced;
};