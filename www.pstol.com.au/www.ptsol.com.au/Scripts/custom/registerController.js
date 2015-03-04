var registerController = appModule.controller('registerController', ['$scope', '$modal','$parse', 'registerService', function ($scope, $modal,$parse, registerService) {
    $scope.titleoptions = ['','Mr','Mrs','Miss','Sir','Lord'];
    $scope.title = '';
    $scope.username = '';
    $scope.username_e = '';
    $scope.firstname = '';
    $scope.firstname_e = '';
    $scope.lastname = '';
    $scope.lastname_e = '';
    $scope.email = '';
    $scope.email_e = '';
    $scope.password = '';
    $scope.password_e = '';
    $scope.conpassword = '';
    $scope.conpassword_e = '';
    $scope.answer = '';
    $scope.answer_e = '';
    $scope.agree = false;
    $scope.urlcaptcha = '/Captcha/Show-Image/' + $scope.guid;
    $scope.reset = function () {
        $scope.title = '';
        $scope.username = '';
        $scope.title = '';
        $scope.firstname = '';
        $scope.lastname = '';
        $scope.email = '';
        $scope.password = '';
        $scope.conpassword = '';
        $scope.answer = '';
        $scope.agree = false;
    };
    $scope.reset_e = function () {
        $scope.username_e = '';
        $scope.firstname_e = '';
        $scope.lastname_e = '';
        $scope.email_e = '';
        $scope.password_e = '';
        $scope.conpassword_e = '';
        $scope.answer_e = '';
    };

    $scope.click = function () {
        $scope.reset_e();
        var status = true;

        if ($scope.firstname == '') {
            status = false;
            $scope.firstname_e = 'Firstname must be provided.';
        }
        else if ($scope.lastname == '') {
            status = false;
            $scope.lastname_e = 'Lastname must be provided.';
        }
        else if ($scope.email == '') {
            status = false;
            $scope.email_e = 'Email must be provided.';
        }
        else if ($scope.username == '') {
            status = false;
            $scope.username_e = 'Username must be provided.';
        }
        else if ($scope.password == '') {
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
                title: $scope.title,
                firstname: $scope.firstname,
                lastname: $scope.lastname,
                email: $scope.email,
                password: $scope.password,
                conpassword: $scope.conpassword,
                answer: $scope.answer,
                agree: $scope.agree,
                guid: $scope.guid
            };
            registerService.postRegistration(obj)
            .then(function (response) {
                if (response.status == 200) {
                    $scope.reset();
                    var modalInstance = $modal.open({
                        templateUrl: '/Scripts/Custom/modalInfoController.html',
                        controller: 'modalInfoController',
                        resolve: {
                            content: function () {
                                return 'Your account has been created. Now please check your email and activate you account.';
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