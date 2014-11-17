articles.controller("basketDialogController", ["$scope", "$location", "$modalInstance", "basket",
	function ($scope, $location, $modalInstance, basket) {
		$scope.basket = basket;
		$scope.basketDetails = $scope.basket.getBasketDetails();

		$scope.checkout = function () {
			$modalInstance.close();
		};

		$scope.continueShoping = function () {
			$modalInstance.dismiss('cancel');
		};
	}
]);