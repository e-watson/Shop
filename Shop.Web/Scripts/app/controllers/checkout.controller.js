var checkout = angular.module("checkout", ["ui.bootstrap", "LocalStorageModule", "services.sdBasketService"]);

checkout.controller("checkoutController", ["$location", "$scope", "$http", "localStorageService", "sdBasketService",
	function ($location, $scope, $http, localStorageService, sdBasketService) {

		//	Constants & view-model variables

		$scope.ROOT_URL = ("{0}://{1}:{2}/").format($location.protocol(),
			$location.host(), $location.port());
		$scope.ARTICLE_SERVICE_URL = ("{0}article").format($scope.ROOT_URL);

		$scope.basket = new Basket();
		$scope.basketInfo = new BasketInfo(0, 0);

		$scope.showProgress = false;

		//	Basket

		$scope.restoreBacket = function () {
			$scope.bItems = sdBasketService.deserialize(localStorageService.get("basket"));
			$("#encodedOrderLines").val(localStorageService.get("basket"));

			if ($scope.bItems.idData.length == 0) {
				$scope.showProgress = false;
				return;
			} else {
				$http.post($scope.ARTICLE_SERVICE_URL, $scope.bItems.idData).success(function (basketData) {
					for (var i = 0, iLen = basketData.length; i < iLen; i++) {
						for (var j = 0, jLen = $scope.bItems.idData.length; j < jLen; j++) {
							if (basketData[i].Id == $scope.bItems.idData[j]) {
								$scope.basket.addItem(basketData[i].Id, basketData[i].Name, basketData[i].Price,
									$scope.bItems.pairs[j].quantity);
							}
						}
					}

					$scope.basketInfo = $scope.basket.getBasketInfo();
					$scope.basketDetails = $scope.basket.getBasketDetails();

					$scope.showProgress = false;
				}).error(function () {
					$scope.showProgress = false;
					alert("Restore basket error!");
				});
			}
		}();
	}
]);