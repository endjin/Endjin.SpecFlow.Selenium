'use strict';

/* App Module */

var spaApp = angular.module('spaApp', [
  'ngRoute',
  'spaControllers'
]);

spaApp.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
          when('/view1', {
              templateUrl: '/Home/SpaView1',
              controller: 'view1Controller'
          }).
          when('/view2', {
              templateUrl: '/Home/SpaView2',
              controller: 'view2Controller'
          });
  }]);