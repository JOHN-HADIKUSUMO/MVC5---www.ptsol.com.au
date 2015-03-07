var profileController = appModule.controller('profileController', ['$scope', '$modal', '$parse', 'profileService', function ($scope, $modal, $parse, profileService) {
    $scope.titleoptions = ['','Mr','Mrs','Miss','Sir','Lord'];
    $scope.firstname_e = '';
    $scope.lastname_e = '';
    $scope.email_e = '';
    $scope.reset = function () {
        $scope.title = '';
        $scope.firstname = '';
        $scope.lastname = '';
        $scope.email = '';
    };
    $scope.reset_e = function () {
        $scope.firstname_e = '';
        $scope.lastname_e = '';
        $scope.email_e = '';
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

        if (status) {
            var obj = {
                title: $scope.title,
                firstname: $scope.firstname,
                lastname: $scope.lastname,
                email: $scope.email
            };
            profileService.postProfile(obj)
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