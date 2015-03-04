var contactusController = appModule.controller('contactusController', ['$scope', '$modal', '$parse', 'contactusService', function ($scope, $modal, $parse,contactusService) {
    $scope.name = '';
    $scope.name_e = '';
    $scope.email = '';
    $scope.email_e = '';
    $scope.mobile = '';
    $scope.mobile_e = '';
    $scope.subject = '';
    $scope.subject_e = '';
    $scope.message = '';
    $scope.message_e = '';
    $scope.answer = '';
    $scope.answer_e = '';
    $scope.sendemail = false;
    $scope.urlcaptcha = '/Captcha/Show-Image/' + $scope.guid;
    $scope.reset = function () {
        $scope.name = '';
        $scope.email = '';
        $scope.mobile = '';
        $scope.subject = '';
        $scope.message = '';
        $scope.answer = '';
        $scope.sendemail = false;
    };
    $scope.reset_e = function () {
        $scope.name_e = '';
        $scope.email_e = '';
        $scope.mobile_e = '';
        $scope.subject_e = '';
        $scope.message_e = '';
        $scope.answer_e = '';
    };

    $scope.click = function () {
        $scope.reset_e();
        var status = true;
        if ($scope.name == '') {
            status = false;
            $scope.name_e = 'Name must be provided.';
        }
        else if ($scope.email == '') {
            status = false;
            $scope.email_e = 'Email must be provided.';
        }
        else if ($scope.mobile == '') {
            status = false;
            $scope.mobile_e = 'Mobile must be provided.';
        }
        else if ($scope.subject == '') {
            status = false;
            $scope.subject_e = 'Subject must be provided.';
        }
        else if ($scope.message == '') {
            status = false;
            $scope.message_e = 'Message must be provided.';
        }
        else if ($scope.answer == '') {
            status = false;
            $scope.answer_e = 'Answer must be provided.';
        }

        if (status) {
            var obj = {
                name: $scope.name,
                email: $scope.email,
                mobile: $scope.mobile,
                subject: $scope.subject,
                message: $scope.message,
                answer: $scope.answer,
                sendemail: $scope.sendemail,
                guid: $scope.guid
            };
            contactusService.postContactUs(obj)
            .then(function (response) {
                if (response.status == 200) {
                    $scope.reset();

                    var modalInstance = $modal.open({
                        templateUrl: '/Scripts/Custom/modalInfoController.html',
                        controller: 'modalInfoController',
                        resolve: {
                            content: function () {
                                return 'Your message has been sent.';
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

                    //switch (response.data.ScopeID) {
                    //    case 'answer_e':
                    //        $scope.answer_e = response.data.Message;
                    //        break;
                    //    case 'email_e':
                    //        $scope.email_e = response.data.Message;
                    //        break;
                    //    case 'mobile_e':
                    //        $scope.mobile_e = response.data.Message;
                    //        break;
                    //}
                }
            })
        }

        return false;
    };
}]);