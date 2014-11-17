var articles = angular.module("articles", ["ui.bootstrap", "LocalStorageModule", "services.sdBasketService"]);

articles.controller("articlesController", ["$location", "$scope", "$http", "localStorageService", "$modal", "sdBasketService",
	function ($location, $scope, $http, localStorageService, $modal, sdBasketService) {

		//	Constants & view-model variables

		$scope.ROOT_URL = ("{0}://{1}:{2}/").format($location.protocol(),
			$location.host(), $location.port());
		$scope.SERVICE_URL = ("{0}article").format($scope.ROOT_URL);

		$scope.basket = new Basket();
		$scope.basketInfo = new BasketInfo(0, 0);

		$scope.showProgress = false;

		//	Pager

		$scope.ITEMS_PER_LINE = 5;
		$scope.pageConfig = [
			new PageConfig(("{0} per page").format($scope.ITEMS_PER_LINE), $scope.ITEMS_PER_LINE),
			new PageConfig(("{0} per page").format((2 * $scope.ITEMS_PER_LINE)), (2 * $scope.ITEMS_PER_LINE)),
			new PageConfig("Show all", 0)
		];
		$scope.pageConfigModel = $scope.pageConfig[0];
		$scope.currentPage = 1;

		$scope.pageConfigModelChanged = function () {
			$scope.currentPage = 1;
			$scope.getArticles($scope.currentPage, $scope.pageConfigModel.pageSize);
			console.log($scope.pageConfigModel.pageSize);
		};

		$scope.pageChanged = function () {
			$scope.getArticles($scope.currentPage, $scope.pageConfigModel.pageSize);
		};

		$scope.getArticlesCount = function() {
			$http.get(("{0}/t").format($scope.SERVICE_URL))
			.success(function(articlesTotal) {
				$scope.articlesTotal = articlesTotal;
				$scope.getArticles($scope.currentPage, $scope.pageConfigModel.pageSize);
			}).error(function() {
				alert("Get articles count error!");
			});
		}();

		//	Articles

		$scope.getArticles = function (pageNumber, pageSize) {
			$scope.showProgress = true;
			$http.get(("{0}/{1}/{2}").format($scope.SERVICE_URL, pageNumber, pageSize))
			.success(function (articles) {
				var articlesModel = [], lineModel = [];
				for (var i = 0, len = articles.length; i < len; i++) {
					if ((i % $scope.ITEMS_PER_LINE) == 0) {
						lineModel = [];
						articlesModel.push(lineModel);
					}

					lineModel.push(articles[i]);
				}
				$scope.articlesModel = articlesModel;
				$scope.restoreBacket();
			}).error(function () {
				$scope.showProgress = false;
				alert("Get articles error");
			});
		};

		//	Articles actions

		$scope.articleDetails = function (id) {
			$scope.showProgress = true;

			$http.get(("{0}/{1}").format($scope.SERVICE_URL, id))
			.success(function (articleDetails) {
				$scope.showProgress = false;
				$scope.details = articleDetails;

				var dialogDetails = $modal.open({
					templateUrl: "articleDialogContent.html",
					controller: "detailsDialogController",
					size: "sm",
					resolve: {
						articleDetails: function () {
							return articleDetails;
						}
					}
				});

				dialogDetails.result.then(function (quantity) {
					$scope.basket.addItem($scope.details.Id, $scope.details.Name,
						$scope.details.Price, quantity);

					$scope.basketInfo = $scope.basket.getBasketInfo();
					localStorageService.set("basket", $scope.basket.serialize());
				}, function () { });
			}).error(function () {
				$scope.showProgress = false;
				alert("Get article details error!");
			});
		};

		//	Basket

		$scope.restoreBacket = function () {
			$scope.bItems = sdBasketService.deserialize(localStorageService.get("basket"));
			if ($scope.bItems.idData.length == 0) {
				$scope.showProgress = false;
				return;
			} else {
				$http.post($scope.SERVICE_URL, $scope.bItems.idData).success(function (basketData) {

					for (var i = 0, iLen = basketData.length; i < iLen; i++) {
						for (var j = 0, jLen = $scope.bItems.idData.length; j < jLen; j++) {
							if (basketData[i].Id == $scope.bItems.idData[j]) {
								$scope.basket.addItem(basketData[i].Id, basketData[i].Name, basketData[i].Price,
									$scope.bItems.pairs[j].quantity);
							}
						}
					}

					$scope.basketInfo = $scope.basket.getBasketInfo();
					$scope.showProgress = false;
				}).error(function () {
					$scope.showProgress = false;
					alert("Restore basket error!");
				});
			}
		};

		$scope.showBasket = function () {
			var dialogBasketInfo = $modal.open({
				templateUrl: "basketDialogContent.html",
				controller: "basketDialogController",
				size: "lg",
				resolve: {
					basket: function () {
						return $scope.basket;
					}
				}
			});

			dialogBasketInfo.result.then(function () {
				window.location = window.checkoutLocation;
			}, function () { });
		};
	}
]);