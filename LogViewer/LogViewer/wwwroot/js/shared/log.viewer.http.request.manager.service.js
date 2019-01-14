(function () {
	'use strict';

	angular
		.module('log.viewer.app')
		.service('httpRequestManager', httpRequestManager);

	httpRequestManager.$inject = ['$http'];

	function httpRequestManager($http) {

		this.createHttpPostRequest = function (url, data, contentType) {
			return $http({
				method: 'POST',
				url: url,
				data: data,
				headers: { 'Content-Type': contentType }
			});
		};

		this.createHttpGetRequest = function (url) {
			return $http({
				method: 'GET',
				url: url
			});
		};
	}
})();