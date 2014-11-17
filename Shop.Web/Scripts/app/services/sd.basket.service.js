angular.module("services.sdBasketService", []);

angular.module("services.sdBasketService").factory("sdBasketService", function () {
	return {
		deserialize: function (basket) {
			var result = {
				idData: [],
				pairs: []
			};

			if (basket) {
				var pair;
				var items = basket.split(",");
				
				for (var i = 0, len = items.length; i < len; i++) {
					pair = items[i].split("-");

					result.idData.push(parseInt(pair[0]));
					result.pairs.push({
						id: parseInt(pair[0]),
						quantity: parseInt(pair[1])
					});
				}
			}

			return result;
		}
	};
});