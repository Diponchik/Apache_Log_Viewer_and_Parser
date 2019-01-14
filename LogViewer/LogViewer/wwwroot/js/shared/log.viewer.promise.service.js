(function () {
	'use strict';

	angular
		.module('log.viewer.app')
		.service('promiseCommonService', promiseCommonService);

	promiseCommonService.$inject = ['alertService'];

	function promiseCommonService(alertService) {

		this.createPromise = function (serviceFunction, parameter, errorMessage, thenFunction) {
			return serviceFunction(parameter)
				.then(function (data) {
					thenFunction(data);
				}).catch(function () {
					alertService.showError(errorMessage);
				});
		};
	}
})();