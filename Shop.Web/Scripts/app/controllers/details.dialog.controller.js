articles.controller("detailsDialogController", ["$scope", "$location", "$modalInstance", "articleDetails",
	function ($scope, $location, $modalInstance, articleDetails) {
		$scope.ROOT_URL = ("{0}://{1}:{2}/").format($location.protocol(),
			$location.host(), $location.port());

		$scope.quantity = 0;
		$scope.articleDetails = articleDetails;

		$scope.addToBasket = function () {
			$modalInstance.close(parseInt($scope.quantity));
		};

		$scope.cancel = function () {
			$modalInstance.dismiss('cancel');
		};
	}
]);