var thankYou = angular.module("thankYou", ["LocalStorageModule"]);

thankYou.controller("thankYouController", ["$location", "$scope", "localStorageService",
	function ($location, $scope, localStorageService) {

		$scope.SUCCESS = 1;

		$scope.shopingResult = window.shopingResult;

		$scope.success = $scope.shopingResult == $scope.SUCCESS;
		$scope.error = !$scope.success;

		if ($scope.success) {
			localStorageService.remove("basket");
		}
	}
]);