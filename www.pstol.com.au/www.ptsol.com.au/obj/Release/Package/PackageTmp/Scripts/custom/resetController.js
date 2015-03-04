var resetController = appModule.controller('resetController', ['$scope', '$modal', '$parse', 'resetService', function ($scope, $modal, $parse, resetService) {
    $scope.username = '';
    $scope.username_e = '';
    $scope.answer = '';
    $scope.answer_e = '';
    $scope.urlcaptcha = '/Captcha/Show-Image/' + $scope.guid;
    $scope.reset = function () {
        $scope.username = '';
        $scope.answer = '';
    };
    $scope.reset_e = function () {
        $scope.username_e = '';
        $scope.answer_e = '';
    };

    $scope.click = function () {
        $scope.reset_e();
        var status = true;

        if ($scope.username == '') {
            status = false;
            $scope.username_e = 'Username must be provided.';
        }
        else if ($scope.answer == '') {
            status = false;
            $scope.answer_e = 'Answer must be provided.';
        }

        if (status) {
            var obj = {
                username: $scope.username,
                answer: $scope.answer,
                guid: $scope.guid
            };

            resetService.postReset(obj)
            .then(function (response) {
                if (response.status == 200) {
                    $scope.reset();
                    var modalInstance = $modal.open({
                        templateUrl: '/Scripts/Custom/modalInfoController.html',
                        controller: 'modalInfoController',
                        resolve: {
                            content: function () {
                                return 'Please check your email to complete the process.';
                            }
                        }
                    });

                }
            })
            .catch(function (response) {
                if (response.status == '503')
                {
                    var modalInstance = $modal.open({
                        templateUrl: '/Scripts/Custom/modalInfoController.html',
                        controller: 'modalInfoController',
                        resolve: {
                            content: function () {
                                return 'This service is temporarily not available. Please try again later.';
                            }
                        }
                    });
                }
                else
                {
                    var scopeid = response.data.ScopeID;
                    var temp = $parse(scopeid);
                    temp.assign($scope, response.data.Message);
                    $scope.apply();
                }
            })
        }

        return false;
    };
}]);