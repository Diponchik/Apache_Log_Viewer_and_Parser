(function () {
	"use strict";

	angular
		.module("log.viewer.app")
		.service("logsService", logsService);

	logsService.$inject = ['httpRequestManager'];

	function logsService(httpRequestManager) {
		var url = '/api/Log/';
		var jsonContentType = "application/json";

		this.getAllLogsCount = function (logRequest) {
			return httpRequestManager.createHttpPostRequest(url + "GetAllLogsCount", logRequest, jsonContentType);
		};

		this.getAllLogs = function (logRequest) {
			return httpRequestManager.createHttpPostRequest(url + "GetAllLogs", logRequest, jsonContentType);
		};

		this.getTopRecords = function (logRequest) {
			return httpRequestManager.createHttpPostRequest(url + "GetTopRecords", logRequest, jsonContentType);
		};
	}
})();