    var changePwdController = appModule.controller('changePwdController', ['$scope', '$modal', '$parse', 'changePwdService', function ($scope, $modal, $parse, changePwdService) {
    $scope.password = '';
    $scope.password_e = '';
    $scope.conpassword = '';
    $scope.conpassword_e = '';
    $scope.reset = function () {
        $scope.password = '';
        $scope.conpassword = '';
    };
    $scope.reset_e = function () {
        $scope.password_e = '';
        $scope.conpassword_e = '';
    };
    $scope.checkpassword = function () {
        $scope.password_e = '';
        if($scope.password.length < 5)
        {
            $scope.password_e = 'Password is too short.';
        }
    };
    $scope.checkconpassword = function () {
        $scope.conpassword_e = '';
        if ($scope.conpassword.length < 5) {
            $scope.conpassword_e = 'Confirming password is too short.';
        }
    };
    $scope.click = function () {
        $scope.reset_e();
        var status = true;

        if ($scope.password == '') {
            status = false;
            $scope.password_e = 'Password must be provided.';
        }
        else if ($scope.password.length < 5) {
            status = false;
            $scope.password_e = 'Password is too short.';
        }
        else if ($scope.conpassword == '') {
            status = false;
            $scope.conpassword_e = 'Confirm password must be provided.';
        }
        else if ($scope.conpassword.length < 5) {
            status = false;
            $scope.conpassword_e = 'Confirming password is too short.';
        }
        else if ($scope.conpassword != $scope.password) {
            status = false;
            $scope.conpassword_e = 'Password and confirm password must be the same.';
        }

        if (status) {
            var obj = {
                username: $scope.username,
                stamp: $scope.stamp,
                password: $scope.password
            };
            changePwdService.postPwd(obj)
            .then(function (response) {
                if (response.status == 200) {
                    $scope.reset();
                    var modalInstance = $modal.open({
                        templateUrl: '/Scripts/Custom/modalInfoController.html',
                        controller: 'modalInfoController',
                        resolve: {
                            content: function () {
                                return 'Your password has been updated.';
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