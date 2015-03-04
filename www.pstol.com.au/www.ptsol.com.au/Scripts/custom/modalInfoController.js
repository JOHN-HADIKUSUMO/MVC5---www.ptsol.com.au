var modalInfoController = appModule.controller('modalInfoController', ['$scope', '$modalInstance', 'content', function ($scope, $modalInstance, content) {
    $scope.content = content;
    $scope.close = function () {
        $modalInstance.close();
    };
}]);