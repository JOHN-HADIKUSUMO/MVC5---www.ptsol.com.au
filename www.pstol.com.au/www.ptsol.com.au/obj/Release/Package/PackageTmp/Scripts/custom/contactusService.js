var contactusService = appModule.factory('contactusService', ['$http', function ($http) {
    var contactusService = {
        postContactUs: function (dataContactUs) {
            return $http.post('/REST/CONTACT-US/SUBMIT', dataContactUs);
        }
    };

    return contactusService;
}]);