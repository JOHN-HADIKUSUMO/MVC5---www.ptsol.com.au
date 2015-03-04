var resetService = appModule.factory('resetService', ['$http', function ($http) {
    var resetService = {
        postReset: function (dataReset) {
            return $http.post('/REST/REGISTRATION/RESET', dataReset);
        }
    };

    return resetService;
}]);