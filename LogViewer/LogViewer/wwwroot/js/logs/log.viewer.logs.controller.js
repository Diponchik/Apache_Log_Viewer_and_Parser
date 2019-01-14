(function () {
	'use strict';

	angular
		.module('log.viewer.logs')
		.controller('logsController', logsController);

	logsController.$inject = ['$scope', 'logsService', 'promiseCommonService', 'alertService'];

	function logsController($scope,
		logsService,
		promiseCommonService,
		alertService) {
		$scope.totalItems = 0;
		$scope.maxVisiblePages = 7;
		$scope.startDateOptions = null;
		$scope.endDateOptions = null;

		$scope.startDatePopup = {
			opened: false
		};

		$scope.endDatePopup = {
			opened: false
		};

		$scope.openStartDatePopup = function () {
			$scope.startDatePopup.opened = true;
		};

		$scope.openEndDatePopup = function () {
			$scope.endDatePopup.opened = true;
		};

		$scope.changeMinEndTime = function () {
			$scope.endDateOptions.minDate = $scope.logsFilter.startTime;
		};

		$scope.changeMaxStartTime = function () {
			$scope.startDateOptions.maxDate = $scope.logsFilter.endTime;
		};

		$scope.logsFilter = {
			"isHostRequest": true,
			"pageNumber": 1,
			"itemsPerPage": 25,
			"fromDate": null,
			"toDate": null,
			"top": null
		};

		$scope.setPage = function (pageNubmer) {
			$scope.logsFilter.pageNumber = pageNubmer;
			$scope.getAllLogs();
		};

		$scope.init = function () {
			$scope.getAllLogs();
		}

		$scope.getAllLogs = function () {
			$scope.fetchAllLogsCountPromise = promiseCommonService.createPromise(logsService.getAllLogsCount,
				$scope.logsFilter,
				"Error during geting all logs",
				function (response) {
					if (!response) {
						alertService.showError("Error during geting all logs");
						return;
					}

					$scope.totalItems = response.data;
					getAllLogs();
				});

		};

		$scope.getTopRecords = function () {
			$scope.getTopRecordsPromise = promiseCommonService.createPromise(logsService.getTopRecords,
				$scope.logsFilter,
				"Error during geting top records",
				function (response) {
					if (!response) {
						alertService.showError("Error during geting top records");
						return;
					}

					$scope.topRecords = response.data;
				});
		};

		$scope.reset = function () {
			$scope.logsFilter = {
				"topNumber": null,
				"pageNumber": 1,
				"itemsPerPage": 25,
				"fromDate": null,
				"toDate": null,
				"top": null
			};

			$scope.getAllLogs();
		};

		function getAllLogs() {
			$scope.fetchLogsPromise = promiseCommonService.createPromise(logsService.getAllLogs,
				$scope.logsFilter,
				"Error during geting all logs",
				function (response) {
					if (!response) {
						alertService.showError("Error during geting all logs");
						return;
					}

					$scope.logs = response.data;
				});
		}
	}
})();