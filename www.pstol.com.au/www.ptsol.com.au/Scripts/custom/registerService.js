var registerService = appModule.factory('registerService', ['$http', function ($http) {
    var registerService = {
        postRegistration: function (dataRegister) {
            return $http.post('/REST/REGISTRATION/SUBMIT', dataRegister);
        }
    };

    return registerService;
}]);