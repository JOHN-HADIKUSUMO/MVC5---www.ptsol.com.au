var changePwdService = appModule.factory('changePwdService', ['$http', function ($http) {
    var changePwdService = {
        postPwd: function (dataPwd) {
            return $http.post('/REST/REGISTRATION/CHANGE-PASSWORD', dataPwd);
        }
    };

    return changePwdService;
}]);