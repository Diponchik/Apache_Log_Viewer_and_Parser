(function () {
	'use strict';

	function createBusyDefaults() {
		return {
			backdrop: true,
			delay: 300,
			minDuration: 700,
			templateUrl: "templates/angularBusyTemplate.html"
		};
	}

	function setUnhandleRejection($qProvider) {
		$qProvider.errorOnUnhandledRejections(false);
	}

	angular
		.module('log.viewer.app', ['toaster', 'ngAnimate', 'ui.bootstrap', 'cgBusy', 'ngSanitize', 'ui.bootstrap.timepicker', 'angularMoment'])
		.config(['$qProvider', setUnhandleRejection])
		.value('cgBusyDefaults', createBusyDefaults())
		.run();

	angular
		.module('log.viewer.logs', [
			'log.viewer.app'
		]);
})();
