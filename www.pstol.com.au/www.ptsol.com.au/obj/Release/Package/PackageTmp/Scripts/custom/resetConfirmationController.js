var resetConfirmationController = appModule.controller('resetConfirmationController', ['$scope', '$modal', '$parse', 'resetConfirmationService', function ($scope, $modal, $parse, resetConfirmationService) {
    $scope.token_e = '';
    $scope.password = '';
    $scope.password_e = '';
    $scope.conpassword = '';
    $scope.conpassword_e = '';
    $scope.answer = '';
    $scope.answer_e = '';
    $scope.urlcaptcha = '/Captcha/Show-Image/' + $scope.guid;
    $scope.reset = function () {
        $scope.password = '';
        $scope.conpassword = '';
        $scope.answer = '';
    };
    $scope.reset_e = function () {
        $scope.token_e = '';
        $scope.password_e = '';
        $scope.conpassword_e = '';
        $scope.answer_e = '';
    };

    $scope.click = function () {
        $scope.reset_e();
        var status = true;

        if ($scope.password == '') {
            status = false;
            $scope.password_e = 'Password must be provided.';
        }
        else if ($scope.conpassword == '') {
            status = false;
            $scope.conpassword_e = 'Confirm password must be provided.';
        }
        else if ($scope.conpassword != $scope.password) {
            status = false;
            $scope.conpassword_e = 'Password and confirm password must be the same.';
        }
        else if ($scope.answer == '') {
            status = false;
            $scope.answer_e = 'Answer must be provided.';
        }

        if (status) {
            var obj = {
                username: $scope.username,
                token: $scope.token,
                password: $scope.password,
                conpassword: $scope.conpassword,
                answer: $scope.answer,
                guid: $scope.guid
            };
            resetConfirmationService.postConfirmation(obj)
            .then(function (response) {
                if (response.status == 200) {
                    $scope.reset();
                    var modalInstance = $modal.open({
                        templateUrl: '/Scripts/Custom/modalInfoController.html',
                        controller: 'modalInfoController',
                        resolve: {
                            content: function () {
                                return 'Your new password has been saved.';
                            }
                        }
                    });

                    modalInstance.result.then(function (result) {
                        window.location = '/Home';
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