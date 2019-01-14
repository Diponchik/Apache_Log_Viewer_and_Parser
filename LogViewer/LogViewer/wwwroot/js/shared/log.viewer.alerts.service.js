(function () {
	'use strict';

	angular
		.module('log.viewer.app')
		.service('alertService', alertService);

	alertService.$inject = ['toaster'];

	function alertService(toaster) {
		var alertSuccessTitle = "Success";
		var alertWarningTitle = "Warning";
		var alertErrorTitle = "Error";

		this.showWarning = function (message) {
			toaster.pop("warning", alertWarningTitle, message);
		};

		this.showSuccess = function (message) {
			toaster.pop("success", alertSuccessTitle, message);
		};

		this.showError = function (message) {
			toaster.pop("error", alertErrorTitle, message);
		};
	}
})();