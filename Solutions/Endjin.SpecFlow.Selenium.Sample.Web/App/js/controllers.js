'use strict';

/* Controllers */

var spaControllers = angular.module('spaControllers', []);

spaControllers.controller('view1Controller', ['$scope',
  function ($scope) {
      $scope.value = 'View1';
  }]);

spaControllers.controller('view2Controller', ['$scope',
  function ($scope) {
      $scope.value = 'View1';
  }]);